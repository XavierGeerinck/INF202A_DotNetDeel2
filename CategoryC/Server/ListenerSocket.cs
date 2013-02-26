using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Net;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using System.Threading;
using Shared.Opcodes;
using Server.Handlers;

namespace Server
{
    class ListenerSocket
    {
        private int ListeningPort;
        private TcpListener listenerSocket;
        private Thread listenThread;
        private MessageHandler messageHandler;
        private const string opcodeDelimiter = "|";
        public static List<TcpClient> Clients { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public ListenerSocket()
        {
            ListeningPort = int.Parse(Server.Properties.Resources.PortNumber);
            IPAddress ipAddressToListenOn = IPAddress.Parse(Server.Properties.Resources.ListeningAddress);

            Clients = new List<TcpClient>();
            messageHandler = new MessageHandler();

            // Make the socket listener and thread
            listenerSocket = new TcpListener(ipAddressToListenOn, ListeningPort);
            listenThread = new Thread(new ThreadStart(ListenForClients));
            listenThread.Start();

            Logger.ShowMessage("Listener initialized.");
            Logger.ShowMessage("Listening on: " + ipAddressToListenOn + ":" + ListeningPort);

            // Define the handlers.
            PacketManager.DefineOpcodeHandlers();
        }

        /// <summary>
        /// Listen for incoming clients and add them to a new thread so we can accept data.
        /// </summary>
        private void ListenForClients()
        {
            // Start the socket.
            listenerSocket.Start();

            Logger.ShowMessage("Waiting for clients.");

            // Loop on connections
            while (true)
            {
                // Block everything until the client has connected
                TcpClient client = listenerSocket.AcceptTcpClient();

                // Add the client to our list.
                Clients.Add(client);

                Logger.ShowMessage(String.Format("Incoming connection on {0}", client.Client.RemoteEndPoint));

                // Create thread to handle connection, this won't be reached untill we got a connection.
                Thread clientThread = new Thread(new ParameterizedThreadStart(HandleClientComm));
                clientThread.Start(client);
            }
        }

        /// <summary>
        /// Handle the clients sending data.
        /// </summary>
        /// <param name="client"></param>
        private void HandleClientComm(object client)
        {
            TcpClient tcpClient = (TcpClient)client;
            NetworkStream clientStream = tcpClient.GetStream();

            Logger.ShowMessage(String.Format("Client connected on {0}, waiting for data.", tcpClient.Client.RemoteEndPoint));

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
                    Logger.ShowMessage("Could not read data from the client.", LogType.ERROR);
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

                // Let the packetmanager invoke the correct handler
                PacketManager.InvokeHandler(ref tcpClient, opcode.opcode, data);
            }
        }
    }
}
