using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Server.Opcodes;
using Shared.Opcodes;
using System.Net.Sockets;

namespace Server.Handlers
{
    class LogoutHandler
    {
        /// <summary>
        /// There is a client that disconnected.
        /// </summary>
        /// <param name="data"></param>
        [Opcode(ClientMessage.CMSG_LOGOUT)]
        public static void HandleLogout(ref TcpClient sender, byte[] data)
        {
            ListenerSocket.Clients.Remove(sender);
            Logger.ShowMessage("Sender: " + sender.Client.LocalEndPoint + " disconnected.");
        }
    }
}
