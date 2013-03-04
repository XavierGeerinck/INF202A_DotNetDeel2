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
using Shared;
using System.Threading;
using Shared.Opcodes;

namespace Client
{
    public partial class Client : Form
    {
        private const int PortNumber = 8890;
        private ASCIIEncoding Encoder;
        public ConnectionHandler ConnectionHandler { get; set; }

        public Client()
        {
            InitializeComponent();
            ConnectionHandler = new ConnectionHandler(this);
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
              if (txtName.Text.Equals("")) {
                lblConnection.Text = "No name entered!";
              }
              else {
                ConnectionHandler.Connect(txtIp.Text, PortNumber);
                lblConnection.Text = "Server Connection!";
                Name = txtName.Text;
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
            // CREATE
            AppendText(txtName.Text + ": ", Color.Blue);
            AppendText(txtSend.Text, Color.Black);
            txtMess.AppendText("\n");

            byte[] message = Encoder.GetBytes(txtName.Text + "|" + txtSend.Text);
            PacketHandler.sendPacket(ConnectionHandler.ServerSocket, ClientMessage.CMSG_BROADCAST, message);
            txtSend.ResetText();
        }

        public void AppendText(string text, Color color)
        {
            txtMess.SelectionStart = txtMess.TextLength;
            txtMess.SelectionLength = 0;

            txtMess.SelectionColor = color;
            txtMess.AppendText(text);
            txtMess.SelectionColor = txtMess.ForeColor;
        }

        private void Client_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (ConnectionHandler.ServerSocket.Connected)
            {
                byte[] message = new byte[0];
                PacketHandler.sendPacket(ConnectionHandler.ServerSocket, ClientMessage.CMSG_LOGOUT, message);
            }
        }
    }
}