using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Net;
using System.Net.Sockets;

namespace Client
{
    public partial class Client : Form
    {
        private const int PortNumber = 8888;
        private TcpClient clientSocket;

        public Client()
        {
            InitializeComponent();
            clientSocket = new TcpClient();
        }

        private void btnConnect_Click(object sender, EventArgs e) {
          if (!checkIpAddress()) {
            lblConnection.Text = "Server Connection Failed";  
          }
          clientSocket.Connect(txtIp.Text, PortNumber);
          NetworkStream clientStream = clientSocket.GetStream();
          
          ASCIIEncoding encoder = new ASCIIEncoding();
          byte[] s = encoder.GetBytes("Hello Server!");
          
          clientStream.Write(s, 0, s.Length);
          clientStream.Flush();
          
          lblConnection.Text = "Server Connected";

        }

        private bool checkIpAddress() {
          bool isOk = true;
         
          

          return isOk;
        }
    }
}
