using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server.Opcodes
{
    // 8 bits long, range 0 - 255
    enum Opcodes : byte
    {
        SMSG_BROADCAST = 0x01,
        SMSG_LOGIN     = 0x02,
    }
}
