using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Server.Opcodes;

namespace Server
{
    class MessageHandler
    {
        [Opcode(ClientMessage.SMSG_BROADCAST)]
        public static void handleBroadcast(byte[] data)
        {
            // Data = String(Data)
            ASCIIEncoding encoding = new ASCIIEncoding();
            Logger.ShowMessage(encoding.GetString(data));
            Logger.ShowMessage("SMSG_BROADCAST");
        }
    }
}
