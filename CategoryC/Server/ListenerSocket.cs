using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Net;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using System.Threading;

namespace Server
{
    class ListenerSocket
    {
        private int ListeningPort;
        private IPAddress ipAddress;
        private TcpListener listenerSocket;
        private Thread listenThread;
        private Logger logger;
        private MessageHandler messageHandler;
        private const string opcodeDelimiter = "|";

        /// <summary>
        /// Constructor
        /// </summary>
        public ListenerSocket()
        {
            ListeningPort = int.Parse(Server.Properties.Resources.PortNumber);
            IPAddress ipAddressToListenOn = IPAddress.Parse(Server.Properties.Resources.ListeningAddress);

            logger = new Logger();
            ipAddress = GetExternalIp();
            messageHandler = new MessageHandler();

            // Make the socket listener and thread
            listenerSocket = new TcpListener(ipAddressToListenOn, ListeningPort);
            listenThread = new Thread(new ThreadStart(ListenForClients));
            listenThread.Start();
        }

        /// <summary>
        /// Listen for incoming clients and add them to a new thread so we can accept data.
        /// </summary>
        private void ListenForClients()
        {
            logger.ShowMessage("Listener initialized.");

            // Start the socket.
            listenerSocket.Start();

            logger.ShowMessage("Listening on: " + ipAddress + ":" + ListeningPort);
            logger.ShowMessage("Waiting for clients.");

            // Loop on connections
            while (true)
            {
                // Block everything until the client has connected
                TcpClient client = listenerSocket.AcceptTcpClient();

                logger.ShowMessage(String.Format("Incoming connection on {0}", client.Client.RemoteEndPoint));

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

            logger.ShowMessage(String.Format("Client connected on {0}, waiting for data.", tcpClient.Client.RemoteEndPoint));

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
                    //logger.ShowMessage(e.ToString(), LogType.ERROR);
                    logger.ShowMessage("Could not read data from the client.", LogType.ERROR);
                    break;
                }

                if (bytesRead == 0)
                {
                    // Client Disconnected
                    logger.ShowMessage("Client disconnected.");
                    break;
                }

                // Message received successfully
                ASCIIEncoding encoder = new ASCIIEncoding();
                String receivedMessage = encoder.GetString(message, 0, bytesRead);

                MessageHandler.HandleMessage(receivedMessage, opcodeDelimiter);
                //logger.ShowMessage(receivedMessage);
            }
        }

        /// <summary>
        /// Method to get our external IP.
        /// </summary>
        /// <returns></returns>
        public IPAddress GetExternalIp()
        {
            // Variables, like the website and the regex to get the IP
            string whatIsMyIp = "http://monip.org/";
            string getIpRegex = @"<BR>IP : (\d*\.\d*\.\d*\.\d*)<br>";

            // Make a webclient, that is going to the site and gets the HTML
            WebClient wc = new WebClient();
            UTF8Encoding utf8 = new UTF8Encoding();
            string requestHtml = "";
            try
            {
                requestHtml = utf8.GetString(wc.DownloadData(whatIsMyIp));
            }
            catch (WebException we)
            {
                // do something with exception
                logger.ShowMessage(we.ToString(), LogType.ERROR);
            }
            
            // Create the regex and get the result
            Match m = Regex.Match(requestHtml, getIpRegex);

            // Create the IPAdress object, and parse our string in it.
            if (m.Success)
                return IPAddress.Parse(m.Groups[1].Value);

            // Default value if error occurred.
            return null;
        }
    }
}
