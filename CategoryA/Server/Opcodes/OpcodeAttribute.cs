using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Shared.Opcodes;

namespace Server.Opcodes
{
    public class OpcodeAttribute : Attribute
    {
        public ClientMessage Opcode { get; set; }

        public OpcodeAttribute(ClientMessage opcode)
        {
            Opcode = opcode;
        }
    }
}
