using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Client.Opcodes;
using Shared.Opcodes;
using System.Net;

namespace Client.Handlers
{
    class ServerHandler
    {
        /// <summary>
        /// The broadcast command is going to send the message that we receive to all the connected clients.
        /// </summary>
        /// <param name="data"></param>
        [Opcode(ClientMessage.SMSG_SERVERIP)]
        public static void HandleBroadcast(ref Client mainForm, byte[] data)
        {
            ASCIIEncoding encoding = new ASCIIEncoding();

            //packet = SERVERIP|SERVERPORT
            String[] messageSplit = new String[2];
            messageSplit = encoding.GetString(data).Split('|');
            string serverIp = messageSplit[0];
            int serverPort = int.Parse(messageSplit[1]);

            mainForm.ConnectionHandler.Connect(serverIp, serverPort);
        }
    }
}
