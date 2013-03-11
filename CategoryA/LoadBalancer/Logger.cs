using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LoadBalancer
{
    public enum LogType : byte
    {
        ERROR   = 0x00,
        INFO    = 0x01
    }

    public class Logger
    {
        public static void ShowMessage(String message, LogType logType = LogType.INFO)
        {
            // Create the message
            StringBuilder sb = new StringBuilder();

            // Append the logtype name
            sb.Append("[" + Enum.GetName(typeof(LogType), logType) + "]");

            // Append the Date + time
            sb.Append("[" + DateTime.Now.Hour + ":" + DateTime.Now.Minute + "|" + DateTime.Today.Day + "/" + DateTime.Today.Month + "/" + DateTime.Today.Year + "]");

            // Append the message
            sb.Append(message);

            // Set color
            SetColor(logType);

            // Write to console.
            Console.WriteLine(sb.ToString());

            // Reset the color
            Console.ResetColor();
        }

        public static void SetColor(LogType logType = LogType.INFO)
        {
            switch (logType)
            {
                case LogType.INFO:
                    Console.ForegroundColor = ConsoleColor.Blue;
                    break;
                case LogType.ERROR:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
            }
        }
    }
}
