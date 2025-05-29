namespace ServerLearn
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
            this.StartServer = new System.Windows.Forms.Button();
            this.StopServer = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.Ip = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.Port = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.mongodb = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.Database = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.Collection = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // StartServer
            // 
            this.StartServer.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.StartServer.Location = new System.Drawing.Point(100, 84);
            this.StartServer.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.StartServer.Name = "StartServer";
            this.StartServer.Size = new System.Drawing.Size(211, 78);
            this.StartServer.TabIndex = 0;
            this.StartServer.Text = "启动服务器";
            this.StartServer.UseVisualStyleBackColor = true;
            this.StartServer.Click += new System.EventHandler(this.StartServer_Click);
            // 
            // StopServer
            // 
            this.StopServer.Location = new System.Drawing.Point(388, 84);
            this.StopServer.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.StopServer.Name = "StopServer";
            this.StopServer.Size = new System.Drawing.Size(222, 78);
            this.StopServer.TabIndex = 1;
            this.StopServer.Text = "关闭服务器";
            this.StopServer.UseVisualStyleBackColor = true;
            this.StopServer.Click += new System.EventHandler(this.StopServer_Click);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(15, 9);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(155, 51);
            this.label1.TabIndex = 2;
            this.label1.Text = "HSGameServer";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(100, 309);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(632, 233);
            this.richTextBox1.TabIndex = 3;
            this.richTextBox1.Text = "";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(41, 189);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 23);
            this.label2.TabIndex = 4;
            this.label2.Text = "ip";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Ip
            // 
            this.Ip.Location = new System.Drawing.Point(87, 189);
            this.Ip.Name = "Ip";
            this.Ip.Size = new System.Drawing.Size(245, 31);
            this.Ip.TabIndex = 5;
            this.Ip.Text = "127.0.0.1";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(22, 251);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 23);
            this.label4.TabIndex = 7;
            this.label4.Text = "端口";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Port
            // 
            this.Port.Location = new System.Drawing.Point(87, 249);
            this.Port.Name = "Port";
            this.Port.Size = new System.Drawing.Size(206, 31);
            this.Port.TabIndex = 8;
            this.Port.Text = "8017";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(356, 167);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 23);
            this.label3.TabIndex = 9;
            this.label3.Text = "mongo";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // mongodb
            // 
            this.mongodb.Location = new System.Drawing.Point(454, 165);
            this.mongodb.Name = "mongodb";
            this.mongodb.Size = new System.Drawing.Size(318, 31);
            this.mongodb.TabIndex = 10;
            this.mongodb.Text = "mongodb://localhost:27017";
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(346, 217);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(102, 23);
            this.label5.TabIndex = 11;
            this.label5.Text = "Database";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Database
            // 
            this.Database.Location = new System.Drawing.Point(454, 215);
            this.Database.Name = "Database";
            this.Database.Size = new System.Drawing.Size(245, 31);
            this.Database.TabIndex = 12;
            this.Database.Text = "hsgamedb";
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(299, 268);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(140, 23);
            this.label6.TabIndex = 13;
            this.label6.Text = "Collection";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Collection
            // 
            this.Collection.Location = new System.Drawing.Point(454, 266);
            this.Collection.Name = "Collection";
            this.Collection.Size = new System.Drawing.Size(245, 31);
            this.Collection.TabIndex = 14;
            this.Collection.Text = "gameLogin";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(773, 576);
            this.Controls.Add(this.Collection);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.Database);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.mongodb);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.Port);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.Ip);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.StopServer);
            this.Controls.Add(this.StartServer);
            this.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox mongodb;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox Database;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox Collection;

        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox Port;

        private System.Windows.Forms.TextBox Ip;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Label label2;

        private System.Windows.Forms.Label label1;

        private System.Windows.Forms.Button StopServer;

        private System.Windows.Forms.Button StartServer;

        #endregion
    }
}