using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Threading;
using Shared;
using System.Windows.Forms;
using Shared.Opcodes;
using Client.Handlers;

namespace Client
{
    public class ConnectionHandler
    {
        public TcpClient ServerSocket { get; set; }
        private MessageHandler messageHandler;
        private Client mainForm;

        public ConnectionHandler(Client mainForm)
        {
            this.mainForm = mainForm;
            ServerSocket = new TcpClient();
            messageHandler = new MessageHandler();
            PacketManager.DefineOpcodeHandlers();
        }

        public void Connect(string ip, int portNumber)
        {
            if (ServerSocket.Connected)
            {
                // Disconnect
                ServerSocket.Close();
                ServerSocket = new TcpClient();
            }

            // Connect
            ServerSocket.Connect(ip, portNumber);

            // Start thread that will listen for data
            Thread serverThread = new Thread(new ParameterizedThreadStart(HandleServerComm));
            serverThread.Start(ServerSocket);
        }

        private void HandleServerComm(object server)
        {
            TcpClient tcpClient = (TcpClient)server;
            NetworkStream clientStream = tcpClient.GetStream();

            //Logger.ShowMessage(String.Format("Client connected on {0}, waiting for data.", tcpClient.Client.RemoteEndPoint));

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
                    //Logger.ShowMessage("Could not read data from the client.", LogType.ERROR);
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
                /*string showBitStream = "";
                foreach (byte receivedByte in data)
                {
                    showBitStream += Convert.ToString(receivedByte, 2).PadLeft(8, '0');
                }
                Logger.ShowMessage(showBitStream);*/
                // DEBUG.END

                // Let the packetmanager invoke the correct handler
                PacketManager.InvokeHandler(ref mainForm, opcode.opcode, data);
            }
        }
    }
}
