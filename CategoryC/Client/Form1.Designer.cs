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
      this.SuspendLayout();
      // 
      // btnConnect
      // 
      this.btnConnect.Location = new System.Drawing.Point(39, 299);
      this.btnConnect.Name = "btnConnect";
      this.btnConnect.Size = new System.Drawing.Size(100, 40);
      this.btnConnect.TabIndex = 0;
      this.btnConnect.Text = "Connect";
      this.btnConnect.UseVisualStyleBackColor = true;
      this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
      // 
      // lblIp
      // 
      this.lblIp.AutoSize = true;
      this.lblIp.Location = new System.Drawing.Point(13, 264);
      this.lblIp.Name = "lblIp";
      this.lblIp.Size = new System.Drawing.Size(19, 13);
      this.lblIp.TabIndex = 1;
      this.lblIp.Text = "Ip:";
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(185, 169);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(10, 13);
      this.label1.TabIndex = 7;
      this.label1.Text = ".";
      // 
      // lblConnection
      // 
      this.lblConnection.AutoSize = true;
      this.lblConnection.Location = new System.Drawing.Point(13, 13);
      this.lblConnection.Name = "lblConnection";
      this.lblConnection.Size = new System.Drawing.Size(0, 13);
      this.lblConnection.TabIndex = 10;
      // 
      // txtIp
      // 
      this.txtIp.Location = new System.Drawing.Point(39, 264);
      this.txtIp.Name = "txtIp";
      this.txtIp.Size = new System.Drawing.Size(145, 20);
      this.txtIp.TabIndex = 11;
      // 
      // Client
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(381, 351);
      this.Controls.Add(this.txtIp);
      this.Controls.Add(this.lblConnection);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.lblIp);
      this.Controls.Add(this.btnConnect);
      this.Margin = new System.Windows.Forms.Padding(2);
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
    }
}

