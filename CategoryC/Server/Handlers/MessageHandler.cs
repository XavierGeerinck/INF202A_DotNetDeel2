using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server
{
    class MessageHandler
    {
        public static void HandleMessage(string receivedMessage, string delimiter)
        {
            // Messages comes in like : INFO|data
            string[] message = receivedMessage.Split(delimiter.ToCharArray());
            
            // Opcode
            byte opcode = byte.Parse(message[0]);

            // Message
            string data = message[1];

            // Find the correct annotation and Invoke it.
        }
    }
}
