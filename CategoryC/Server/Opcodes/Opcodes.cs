using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server.Opcodes
{
    // 8 bits long, range 0 - 255
    public enum ClientMessage : byte
    {
        SMSG_DISCONNECT= 0x00,
        SMSG_BROADCAST = 0x01,
        SMSG_LOGIN     = 0x02,
    }

    public struct Opcode
    {
        public ClientMessage opcode { get; set; }
        public byte[] data { get; set; }
    }
}
