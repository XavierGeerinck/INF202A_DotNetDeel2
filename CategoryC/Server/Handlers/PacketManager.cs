using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;
using Server.Opcodes;
using Shared.Opcodes;

namespace Server.Handlers
{
    public class PacketManager
    {
        static Dictionary<ClientMessage, HandlePacket> OpcodeHandlers = new Dictionary<ClientMessage, HandlePacket>();
        delegate void HandlePacket(byte[] data);

        public static void DefineOpcodeHandlers()
        {
            Assembly currentAsm = Assembly.GetExecutingAssembly();
            foreach (var type in currentAsm.GetTypes())
            {
                foreach (MethodInfo methodInfo in type.GetMethods())
                {
                    foreach (OpcodeAttribute attr in methodInfo.GetCustomAttributes(typeof(OpcodeAttribute), false))
                    {
                        if (attr != null)
                            OpcodeHandlers[attr.Opcode] = (HandlePacket)Delegate.CreateDelegate(typeof(HandlePacket), methodInfo);
                    }
                }
            }
        }

        public static bool InvokeHandler(ClientMessage opcode, byte[] data)
        {
            if (OpcodeHandlers.ContainsKey(opcode))
            {
                OpcodeHandlers[opcode].Invoke(data);
                return true;
            }
            else
                return false;
        }
    }
}
