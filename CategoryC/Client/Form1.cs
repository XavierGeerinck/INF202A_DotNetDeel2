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
using Server.Opcodes;

namespace Client
{
    public partial class Client : Form
    {
        private const int PortNumber = 8888;
        private TcpClient ClientSocket;
        private ASCIIEncoding Encoder;
        DateTime TimeNow;

        public Client()
        {
            InitializeComponent();
            ClientSocket = new TcpClient();
            Encoder = new ASCIIEncoding();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (!checkIpAddress())
            {
                lblConnection.Text = "Server Connection Failed";
            }
            else
            {
                ClientSocket.Connect(txtIp.Text, PortNumber);
                if (ClientSocket.Connected)
                {
                    NetworkStream clientStream = ClientSocket.GetStream();

                    byte[] s = Encoder.GetBytes("Hello Server!");

                    clientStream.Write(s, 0, s.Length);
                    clientStream.Flush();

                    lblConnection.Text = "Server Connected";
                }
            }
        }

        private bool checkIpAddress()
        {
            bool isOk = true;
            try
            {
                IPAddress.Parse(txtIp.Text);
            }
            catch
            {
                isOk = false;
            }
            return isOk;
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            if (!TimeNow.ToShortDateString().Equals(DateTime.Now.ToShortDateString()))
            {
                TimeNow = DateTime.Now;
                AppendText(TimeNow.ToShortDateString() + "\n", Color.Blue);
                AppendText(TimeNow.ToShortTimeString() + "\n", Color.Red);
            }
            if (!TimeNow.ToShortTimeString().Equals(DateTime.Now.ToShortTimeString()))
            {
                AppendText(TimeNow.ToShortTimeString() + "\n", Color.Red);
            }
            TimeNow = DateTime.Now;
            txtMess.AppendText(txtSend.Text + "\n");

            // CREATE
            byte[] message = Encoder.GetBytes(txtSend.Text);
            byte[] appendedOpcode = new byte[message.Length + 1];
            message.CopyTo(appendedOpcode, 1);
            appendedOpcode[0] = (byte)ClientMessage.SMSG_BROADCAST;
            message = appendedOpcode;

            // SEND
            NetworkStream clientStream = ClientSocket.GetStream();
            clientStream.Write(message, 0, message.Count());
            clientStream.Flush();
            txtSend.ResetText();
        }

        private void AppendText(string text, Color color)
        {
            txtMess.SelectionStart = txtMess.TextLength;
            txtMess.SelectionLength = 0;

            txtMess.SelectionColor = color;
            txtMess.AppendText(text);
            txtMess.SelectionColor = txtMess.ForeColor;
        }
    }
}