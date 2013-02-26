using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Client.Opcodes;
using Shared.Opcodes;
using System.IO;
using System.Windows.Forms;

namespace Client.Handlers
{
    public class MessageHandler
    {
        /// <summary>
        /// The broadcast command is going to send the message that we receive to all the connected clients.
        /// </summary>
        /// <param name="data"></param>
        [Opcode(ClientMessage.SMSG_BROADCAST)]
        public static void HandleBroadcast(ref Client mainForm, byte[] data)
        {
            ASCIIEncoding encoding = new ASCIIEncoding();
            
            //packet = SENDER|MESSAGE
            String[] messageSplit = new String[2];
            messageSplit = encoding.GetString(data).Split('|');
            mainForm.AppendText(messageSplit[0] + ":" + messageSplit[1]);
        }
    }
}
