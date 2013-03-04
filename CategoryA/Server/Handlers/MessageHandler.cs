using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using Server.Opcodes;
using System.IO;
using Shared.Opcodes;
using Shared;

namespace Server
{
    class MessageHandler
    {
        /// <summary>
        /// The broadcast command is going to send the message that we receive to all the connected clients.
        /// </summary>
        /// <param name="data"></param>
        [Opcode(ClientMessage.CMSG_BROADCAST)]
        public static void HandleBroadcast(ref TcpClient sender, byte[] data)
        {
            foreach (TcpClient client in ListenerSocket.Clients)
            {
                if (!client.Equals(sender))
                {
                    PacketHandler.sendPacket(client, ClientMessage.SMSG_BROADCAST, data);
                }
            }
            
            //ASCIIEncoding encoding = new ASCIIEncoding();
            //Logger.ShowMessage(encoding.GetString(data));
            Logger.ShowMessage("CMSG_BROADCAST");
        }
    }
}
