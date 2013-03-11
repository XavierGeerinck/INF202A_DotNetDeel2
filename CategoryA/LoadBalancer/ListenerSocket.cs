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

                sendServerIp(client);

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

                // Add the client to our list.
                serverData serverStruct = new serverData
                {
                    server = server,
                    serverPort = 0
                };

                Servers.Add(serverStruct);

                Logger.ShowMessage(String.Format("Incoming connection on {0}", server.Client.RemoteEndPoint));

                // Create thread to handle connection, this won't be reached untill we got a connection.
                Thread serverThread = new Thread(new ParameterizedThreadStart(HandleServerComm));
                serverThread.Start(server);
            }
        }

        private void HandleServerComm(object server)
        {

        }

        private void sendServerIp(TcpClient client)
        {
            IPEndPoint endPoint = Servers[0].server.Client.RemoteEndPoint as IPEndPoint;
            String serverIp = endPoint.Address.ToString();
            int serverPort = endPoint.Port;

            // Send Packet back with the server ip.
            // create the packet that will be send
            ASCIIEncoding encoder = new ASCIIEncoding();
            byte[] data = encoder.GetBytes(String.Format("{0}|{1}", serverIp, serverPort));

            PacketHandler.sendPacket(client, ClientMessage.SMSG_SERVERIP, data);
            Logger.ShowMessage(String.Format("Sended server ip: {0} to the client with opcode SMSG_SERVERIP", serverIp));
        }

        //private void HandleServerComm(object server)
        //{
        //}

        public struct serverData
        {
            public TcpClient server;
            public int serverPort;
        }
    }
}
