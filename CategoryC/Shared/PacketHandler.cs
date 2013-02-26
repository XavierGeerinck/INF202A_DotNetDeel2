using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using Shared.Opcodes;

namespace Shared
{
    public class PacketHandler
    {
        public static void SendPacket(TcpClient client, ClientMessage opcode, byte[] message)
        {
            // create the packet that will be send
            byte[] appendedOpcode = new byte[message.Length + 1];
            message.CopyTo(appendedOpcode, 1);
            appendedOpcode[0] = (byte)opcode;
            message = appendedOpcode;

            // SEND
            NetworkStream clientStream = client.GetStream();
            clientStream.Write(message, 0, message.Count());
            clientStream.Flush();
        }
    }
}
