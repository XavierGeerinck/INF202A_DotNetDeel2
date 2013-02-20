namespace Client
{
    partial class Client
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnConnect = new System.Windows.Forms.Button();
            this.lblIp = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblConnection = new System.Windows.Forms.Label();
            this.txtIp = new System.Windows.Forms.TextBox();
            this.txtSend = new System.Windows.Forms.TextBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.txtMess = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(276, 4);
            this.btnConnect.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(133, 29);
            this.btnConnect.TabIndex = 0;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // lblIp
            // 
            this.lblIp.AutoSize = true;
            this.lblIp.Location = new System.Drawing.Point(13, 16);
            this.lblIp.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblIp.Name = "lblIp";
            this.lblIp.Size = new System.Drawing.Size(23, 17);
            this.lblIp.TabIndex = 1;
            this.lblIp.Text = "Ip:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(247, 208);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(12, 17);
            this.label1.TabIndex = 7;
            this.label1.Text = ".";
            // 
            // lblConnection
            // 
            this.lblConnection.AutoSize = true;
            this.lblConnection.Location = new System.Drawing.Point(17, 16);
            this.lblConnection.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblConnection.Name = "lblConnection";
            this.lblConnection.Size = new System.Drawing.Size(0, 17);
            this.lblConnection.TabIndex = 10;
            // 
            // txtIp
            // 
            this.txtIp.Location = new System.Drawing.Point(67, 11);
            this.txtIp.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtIp.Name = "txtIp";
            this.txtIp.Size = new System.Drawing.Size(192, 22);
            this.txtIp.TabIndex = 11;
            // 
            // txtSend
            // 
            this.txtSend.Location = new System.Drawing.Point(13, 341);
            this.txtSend.Margin = new System.Windows.Forms.Padding(4);
            this.txtSend.Multiline = true;
            this.txtSend.Name = "txtSend";
            this.txtSend.Size = new System.Drawing.Size(396, 78);
            this.txtSend.TabIndex = 13;
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(417, 341);
            this.btnSend.Margin = new System.Windows.Forms.Padding(4);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(78, 78);
            this.btnSend.TabIndex = 14;
            this.btnSend.Text = "Send";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // txtMess
            // 
            this.txtMess.Location = new System.Drawing.Point(12, 40);
            this.txtMess.Name = "txtMess";
            this.txtMess.Size = new System.Drawing.Size(483, 294);
            this.txtMess.TabIndex = 15;
            this.txtMess.Text = "";
            // 
            // Client
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(508, 432);
            this.Controls.Add(this.txtMess);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.txtSend);
            this.Controls.Add(this.txtIp);
            this.Controls.Add(this.lblConnection);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblIp);
            this.Controls.Add(this.btnConnect);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Client";
            this.Text = "Client";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Label lblIp;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblConnection;
        private System.Windows.Forms.TextBox txtIp;
        private System.Windows.Forms.TextBox txtSend;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.RichTextBox txtMess;
    }
}

