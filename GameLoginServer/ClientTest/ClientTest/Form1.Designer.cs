namespace ClientTest
{
    partial class Form1
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
            this.ConnectServer = new System.Windows.Forms.Button();
            this.SendText = new System.Windows.Forms.Button();
            this.StopServer = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.SendInfo = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.Ip = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.Port = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // ConnectServer
            // 
            this.ConnectServer.Location = new System.Drawing.Point(44, 40);
            this.ConnectServer.Name = "ConnectServer";
            this.ConnectServer.Size = new System.Drawing.Size(118, 41);
            this.ConnectServer.TabIndex = 0;
            this.ConnectServer.Text = "连接服务器";
            this.ConnectServer.UseVisualStyleBackColor = true;
            this.ConnectServer.Click += new System.EventHandler(this.ConnectServer_Click);
            // 
            // SendText
            // 
            this.SendText.Location = new System.Drawing.Point(44, 117);
            this.SendText.Name = "SendText";
            this.SendText.Size = new System.Drawing.Size(118, 41);
            this.SendText.TabIndex = 1;
            this.SendText.Text = "发送信息";
            this.SendText.UseVisualStyleBackColor = true;
            this.SendText.Click += new System.EventHandler(this.SendText_Click);
            // 
            // StopServer
            // 
            this.StopServer.Location = new System.Drawing.Point(208, 40);
            this.StopServer.Name = "StopServer";
            this.StopServer.Size = new System.Drawing.Size(118, 41);
            this.StopServer.TabIndex = 2;
            this.StopServer.Text = "断开连接";
            this.StopServer.UseVisualStyleBackColor = true;
            this.StopServer.Click += new System.EventHandler(this.StopServer_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(45, 220);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(281, 218);
            this.richTextBox1.TabIndex = 3;
            this.richTextBox1.Text = "";
            // 
            // SendInfo
            // 
            this.SendInfo.Location = new System.Drawing.Point(44, 164);
            this.SendInfo.Name = "SendInfo";
            this.SendInfo.Size = new System.Drawing.Size(281, 21);
            this.SendInfo.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(45, 194);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(130, 23);
            this.label1.TabIndex = 5;
            this.label1.Text = "收到服务器的响应";
            this.label1.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(437, 87);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(27, 23);
            this.label2.TabIndex = 6;
            this.label2.Text = "ip";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Ip
            // 
            this.Ip.Location = new System.Drawing.Point(470, 89);
            this.Ip.Name = "Ip";
            this.Ip.Size = new System.Drawing.Size(161, 21);
            this.Ip.TabIndex = 7;
            this.Ip.Text = "127.0.0.1";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(421, 135);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 23);
            this.label3.TabIndex = 8;
            this.label3.Text = "port";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Port
            // 
            this.Port.Location = new System.Drawing.Point(470, 137);
            this.Port.Name = "Port";
            this.Port.Size = new System.Drawing.Size(161, 21);
            this.Port.TabIndex = 9;
            this.Port.Text = "8017";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.Port);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.Ip);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.SendInfo);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.StopServer);
            this.Controls.Add(this.SendText);
            this.Controls.Add(this.ConnectServer);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.TextBox Port;

        private System.Windows.Forms.Label label3;

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox Ip;

        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.TextBox SendInfo;
        private System.Windows.Forms.Label label1;

        private System.Windows.Forms.Button SendText;
        private System.Windows.Forms.Button StopServer;

        private System.Windows.Forms.Button ConnectServer;

        #endregion
    }
}