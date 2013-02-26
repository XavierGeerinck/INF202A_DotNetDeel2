using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using Shared.Opcodes;

namespace Shared
{
    public static class PacketHandler
    {
        public static void sendPacket(TcpClient client, ClientMessage clientMessage, byte[] data)
        {
            // create the packet that will be send
            byte[] appendedOpcode = new byte[data.Length + 1];
            data.CopyTo(appendedOpcode, 1);
            appendedOpcode[0] = (byte)clientMessage;
            data = appendedOpcode;

            // SEND
            NetworkStream clientStream = client.GetStream();
            clientStream.Write(data, 0, data.Count());
            clientStream.Flush();
        }
    }
}
