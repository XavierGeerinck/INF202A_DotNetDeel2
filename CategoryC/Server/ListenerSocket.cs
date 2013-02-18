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

        public ListenerSocket()
        {
            ListeningPort = int.Parse(Server.Properties.Resources.PortNumber);
            IPAddress temp = IPAddress.Parse(Server.Properties.Resources.ListeningAdress);

            logger = new Logger();
            ipAddress = GetExternalIp();
            
            listenerSocket = new TcpListener(temp, ListeningPort);
            logger.ShowMessage("Listener initialized.");

            startListening();
        }

        public void startListening()
        {
            listenThread = new Thread(new ThreadStart(ListenForClients));
            logger.ShowMessage("Listening on: " + ipAddress + ":" + ListeningPort);
            ListenForClients();
        }

        private void ListenForClients()
        {
            listenerSocket.Start();
            logger.ShowMessage("Waiting for clients.");

            while (true)
            {
                // Block everything until the client has connected
                TcpClient client = listenerSocket.AcceptTcpClient();

                // Create thread to handle connection
                Thread clientThread = new Thread(new ParameterizedThreadStart(HandleClientComm));
                clientThread.Start();
            }
        }

        private void HandleClientComm(object client)
        {
            TcpClient tcpClient = (TcpClient)client;
            NetworkStream clientStream = tcpClient.GetStream();

            logger.ShowMessage("Client connected, waiting for data.");

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
                catch (Exception e)
                {
                    // Socket error occurred
                    logger.ShowMessage(e.ToString(), LogType.ERROR);
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

                logger.ShowMessage(receivedMessage);
            }
        }

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
