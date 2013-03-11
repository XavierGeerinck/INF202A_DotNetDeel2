using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;

namespace LoadBalancer
{
    public class Program
    {
        public static List<TcpClient> ServerSockets { get; set; }

        static void Main(string[] args)
        {
            ListenerSocket listenerSocket = new ListenerSocket();
        }
    }
}
