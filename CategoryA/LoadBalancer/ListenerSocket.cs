using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Net;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using System.Threading;
using Shared.Opcodes;
using Shared;

namespace LoadBalancer
{
    public class ListenerSocket
    {
        public List<serverData> Servers { get; set; }
        public List<TcpClient> Clients { get; set; }

        public TcpListener ServerListener { get; set; }
        public TcpListener ClientListener { get; set; }

        public int ListenerPortServer { get; set; }
        public int ListenerPortClient { get; set; }

        public String ListenerIpServer { get; set; }
        public String ListenerIpClient { get; set; }

        private Thread listenerThreadClients;
        private Thread listenerThreadServers;

        public ListenerSocket()
        {
            ListenerIpServer = "0.0.0.0";
            ListenerIpClient = "0.0.0.0";

            ListenerPortServer = 8889;
            ListenerPortClient = 8890;

            Servers = new List<serverData>();
            Clients = new List<TcpClient>();

            ClientListener = new TcpListener(IPAddress.Parse(ListenerIpClient), ListenerPortClient);
            ServerListener = new TcpListener(IPAddress.Parse(ListenerIpServer), ListenerPortServer);
            
            listenerThreadClients = new Thread(new ThreadStart(ListenForClients));
            listenerThreadServers = new Thread(new ThreadStart(ListenForServers));

            listenerThreadClients.Start();
            listenerThreadServers.Start();
        }

        private void ListenForClients()
        {
            ClientListener.Start();
            Logger.ShowMessage("Listening on incoming clients");

            while (true)
            {
                // Block everything until the client has connected
                TcpClient client = ClientListener.AcceptTcpClient();

                // Add the client to our list, just for security purposes.
                Clients.Add(client);

                Logger.ShowMessage(String.Format("Added client to tempList: {0}", client.Client.RemoteEndPoint));

                // Make it so it actually finds the server based on the load
                int min = Servers.Min(s => s.ClientsConnected);
                serverData server = Servers.Where(s => s.ClientsConnected == min).First();
                server.ClientsConnected += 1;

                sendServerIp(client, server);

                Logger.ShowMessage("Clients Connected: " + server.ClientsConnected);
                Logger.ShowMessage(String.Format("Removed client: {0} from the tempList", client.Client.RemoteEndPoint));

                Clients.Remove(client);
            }
        }


        private void ListenForServers()
        {
            ServerListener.Start();
            Logger.ShowMessage("Listening on incoming servers");

            while (true)
            {
                // Block everything until the client has connected
                TcpClient server = ServerListener.AcceptTcpClient();

                Logger.ShowMessage(
                    String.Format("Incoming connection: {0}:{1} and {2}:{3}",
                        ((IPEndPoint)server.Client.LocalEndPoint).Address,
                        ((IPEndPoint)server.Client.LocalEndPoint).Port,
                        ((IPEndPoint)server.Client.RemoteEndPoint).Address,
                        ((IPEndPoint)server.Client.RemoteEndPoint).Port
                    )
                );
                // Create thread to handle connection, this won't be reached untill we got a connection.
                Thread serverThread = new Thread(new ParameterizedThreadStart(HandleServerComm));
                serverThread.Start(server);
            }
        }

        /// <summary>
        /// Get the server port.
        /// </summary>
        /// <param name="server"></param>
        private void HandleServerComm(object server)
        {
            TcpClient tcpClient = (TcpClient)server;
            NetworkStream clientStream = tcpClient.GetStream();

            Logger.ShowMessage(String.Format("Waiting for data."));

            byte[] message = new byte[4096];
            int bytesRead;


            while (true)
            {
                bytesRead = 0;

                try
                {
                    // Blocks until client sends a message
                    bytesRead = clientStream.Read(message, 0, 4096);
                }
                catch
                {
                    // Socket error occurred
                    Logger.ShowMessage("Could not read data from the server.", LogType.ERROR);
                    break;
                }

                Opcode opcode = new Opcode();

                // Set the opcode + the data
                opcode.opcode = (ClientMessage)message[0];
                opcode.data = message.Where(b => b != message[0]).ToArray();

                // Pack the byte array
                byte[] data = new byte[bytesRead];
                Buffer.BlockCopy(opcode.data, 0, data, 0, bytesRead);

                // DEBUG
                string showBitStream = "";
                foreach (byte receivedByte in data)
                {
                    showBitStream += Convert.ToString(receivedByte, 2).PadLeft(8, '0');
                }
                Logger.ShowMessage(showBitStream);
                // DEBUG.END

                ASCIIEncoding encoding = new ASCIIEncoding();

                //packet = SENDER|MESSAGE
                int port = int.Parse(encoding.GetString(opcode.data));

                // Add the client to our list.
                serverData serverStruct = new serverData
                {
                    Server = (TcpClient)server,
                    ServerPort = port,
                    ClientsConnected = 0
                };

                Servers.Add(serverStruct);

                Logger.ShowMessage(String.Format("Server connected on {0}:{1}", ((IPEndPoint)tcpClient.Client.RemoteEndPoint).Address, port));
            }
        }

        private void sendServerIp(TcpClient client, serverData server)
        {
            String serverIp = ((IPEndPoint)server.Server.Client.RemoteEndPoint).Address.ToString();
            int serverPort = server.ServerPort;

            // Send Packet back with the server ip.
            // create the packet that will be send
            ASCIIEncoding encoder = new ASCIIEncoding();
            byte[] data = encoder.GetBytes(String.Format("{0}|{1}", serverIp, serverPort));

            PacketHandler.sendPacket(client, ClientMessage.SMSG_SERVERIP, data);
            Logger.ShowMessage(String.Format("Sended server ip: {0}:{1} to the client with opcode SMSG_SERVERIP", serverIp, serverPort));
        }

        public class serverData
        {
            public TcpClient Server { get; set; }
            public int ServerPort { get; set; }
            public int ClientsConnected { get; set; }
        }
    }
}
