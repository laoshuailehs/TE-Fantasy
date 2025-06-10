namespace RedisServer
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
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.connect = new System.Windows.Forms.Button();
            this.StartServer = new System.Windows.Forms.Button();
            this.selectdb = new System.Windows.Forms.Button();
            this.searsh1 = new System.Windows.Forms.TextBox();
            this.stopserver = new System.Windows.Forms.Button();
            this.ip = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.port = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(52, 214);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(377, 224);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            // 
            // connect
            // 
            this.connect.Location = new System.Drawing.Point(52, 127);
            this.connect.Name = "connect";
            this.connect.Size = new System.Drawing.Size(144, 48);
            this.connect.TabIndex = 1;
            this.connect.Text = "连接Redis";
            this.connect.UseVisualStyleBackColor = true;
            this.connect.Click += new System.EventHandler(this.connect_Click);
            // 
            // StartServer
            // 
            this.StartServer.Location = new System.Drawing.Point(52, 29);
            this.StartServer.Name = "StartServer";
            this.StartServer.Size = new System.Drawing.Size(144, 48);
            this.StartServer.TabIndex = 2;
            this.StartServer.Text = "启动服务器";
            this.StartServer.UseVisualStyleBackColor = true;
            this.StartServer.Click += new System.EventHandler(this.StartServer_Click);
            // 
            // selectdb
            // 
            this.selectdb.Location = new System.Drawing.Point(228, 127);
            this.selectdb.Name = "selectdb";
            this.selectdb.Size = new System.Drawing.Size(144, 48);
            this.selectdb.TabIndex = 3;
            this.selectdb.Text = "查找数据";
            this.selectdb.UseVisualStyleBackColor = true;
            this.selectdb.Click += new System.EventHandler(this.selectdb_Click);
            // 
            // searsh1
            // 
            this.searsh1.Location = new System.Drawing.Point(228, 187);
            this.searsh1.Name = "searsh1";
            this.searsh1.Size = new System.Drawing.Size(144, 21);
            this.searsh1.TabIndex = 4;
            // 
            // stopserver
            // 
            this.stopserver.Location = new System.Drawing.Point(228, 29);
            this.stopserver.Name = "stopserver";
            this.stopserver.Size = new System.Drawing.Size(144, 48);
            this.stopserver.TabIndex = 5;
            this.stopserver.Text = "关闭服务器";
            this.stopserver.UseVisualStyleBackColor = true;
            this.stopserver.Click += new System.EventHandler(this.stopserver_Click);
            // 
            // ip
            // 
            this.ip.Location = new System.Drawing.Point(80, 90);
            this.ip.Name = "ip";
            this.ip.Size = new System.Drawing.Size(116, 21);
            this.ip.TabIndex = 6;
            this.ip.Text = "127.0.0.1";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(45, 88);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 23);
            this.label1.TabIndex = 7;
            this.label1.Text = "ip";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // port
            // 
            this.port.Location = new System.Drawing.Point(256, 90);
            this.port.Name = "port";
            this.port.Size = new System.Drawing.Size(116, 21);
            this.port.TabIndex = 8;
            this.port.Text = "8019";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(221, 88);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 23);
            this.label2.TabIndex = 9;
            this.label2.Text = "port";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.port);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ip);
            this.Controls.Add(this.stopserver);
            this.Controls.Add(this.searsh1);
            this.Controls.Add(this.selectdb);
            this.Controls.Add(this.StartServer);
            this.Controls.Add(this.connect);
            this.Controls.Add(this.richTextBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.TextBox port;
        private System.Windows.Forms.Label label2;

        private System.Windows.Forms.Label label1;

        private System.Windows.Forms.TextBox ip;

        private System.Windows.Forms.Button stopserver;

        private System.Windows.Forms.TextBox searsh1;

        private System.Windows.Forms.Button selectdb;

        private System.Windows.Forms.Button StartServer;

        private System.Windows.Forms.Button connect;

        private System.Windows.Forms.RichTextBox richTextBox1;

        #endregion
    }
}