using System.Windows.Forms;

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
            this.sub = new System.Windows.Forms.Button();
            this.subtext = new System.Windows.Forms.TextBox();
            this.stopredis = new System.Windows.Forms.Button();
            this.redisip = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.publish = new System.Windows.Forms.Button();
            this.channel = new System.Windows.Forms.TextBox();
            this.message = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.redisui = new System.Windows.Forms.TabPage();
            this.pingdao2 = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.rabbitMQ = new System.Windows.Forms.TabPage();
            this.xqueuename = new System.Windows.Forms.TextBox();
            this.label23 = new System.Windows.Forms.Label();
            this.rabbitmqsub = new System.Windows.Forms.Button();
            this.stoprabbitmq = new System.Windows.Forms.Button();
            this.rabbitmessage = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.queuename = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.button12 = new System.Windows.Forms.Button();
            this.rabbitmqport = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.PassWord = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.UserName = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.HostName = new System.Windows.Forms.TextBox();
            this.rabbitmqtext = new System.Windows.Forms.RichTextBox();
            this.connectrabbitmq = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.button7 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.label16 = new System.Windows.Forms.Label();
            this.button9 = new System.Windows.Forms.Button();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.button10 = new System.Windows.Forms.Button();
            this.button11 = new System.Windows.Forms.Button();
            this.richTextBox2 = new System.Windows.Forms.RichTextBox();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.textBox8 = new System.Windows.Forms.TextBox();
            this.tabControl1.SuspendLayout();
            this.redisui.SuspendLayout();
            this.rabbitMQ.SuspendLayout();
            this.SuspendLayout();
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(10, 202);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(330, 206);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            // 
            // connect
            // 
            this.connect.Location = new System.Drawing.Point(20, 105);
            this.connect.Name = "connect";
            this.connect.Size = new System.Drawing.Size(144, 48);
            this.connect.TabIndex = 1;
            this.connect.Text = "连接Redis";
            this.connect.UseVisualStyleBackColor = true;
            this.connect.Click += new System.EventHandler(this.connect_Click);
            // 
            // StartServer
            // 
            this.StartServer.Location = new System.Drawing.Point(20, 7);
            this.StartServer.Name = "StartServer";
            this.StartServer.Size = new System.Drawing.Size(144, 48);
            this.StartServer.TabIndex = 2;
            this.StartServer.Text = "启动服务器";
            this.StartServer.UseVisualStyleBackColor = true;
            this.StartServer.Click += new System.EventHandler(this.StartServer_Click);
            // 
            // selectdb
            // 
            this.selectdb.Location = new System.Drawing.Point(408, 268);
            this.selectdb.Name = "selectdb";
            this.selectdb.Size = new System.Drawing.Size(144, 48);
            this.selectdb.TabIndex = 3;
            this.selectdb.Text = "查找数据";
            this.selectdb.UseVisualStyleBackColor = true;
            this.selectdb.Click += new System.EventHandler(this.selectdb_Click);
            // 
            // searsh1
            // 
            this.searsh1.Location = new System.Drawing.Point(408, 336);
            this.searsh1.Name = "searsh1";
            this.searsh1.Size = new System.Drawing.Size(144, 21);
            this.searsh1.TabIndex = 4;
            // 
            // stopserver
            // 
            this.stopserver.Location = new System.Drawing.Point(196, 7);
            this.stopserver.Name = "stopserver";
            this.stopserver.Size = new System.Drawing.Size(144, 48);
            this.stopserver.TabIndex = 5;
            this.stopserver.Text = "关闭服务器";
            this.stopserver.UseVisualStyleBackColor = true;
            this.stopserver.Click += new System.EventHandler(this.stopserver_Click);
            // 
            // ip
            // 
            this.ip.Location = new System.Drawing.Point(48, 68);
            this.ip.Name = "ip";
            this.ip.Size = new System.Drawing.Size(116, 21);
            this.ip.TabIndex = 6;
            this.ip.Text = "127.0.0.1";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(13, 66);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 23);
            this.label1.TabIndex = 7;
            this.label1.Text = "ip";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // port
            // 
            this.port.Location = new System.Drawing.Point(224, 68);
            this.port.Name = "port";
            this.port.Size = new System.Drawing.Size(116, 21);
            this.port.TabIndex = 8;
            this.port.Text = "8019";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(189, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 23);
            this.label2.TabIndex = 9;
            this.label2.Text = "port";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // sub
            // 
            this.sub.Location = new System.Drawing.Point(408, 7);
            this.sub.Name = "sub";
            this.sub.Size = new System.Drawing.Size(144, 48);
            this.sub.TabIndex = 10;
            this.sub.Text = "订阅频道";
            this.sub.UseVisualStyleBackColor = true;
            this.sub.Click += new System.EventHandler(this.sub_Click);
            // 
            // subtext
            // 
            this.subtext.Location = new System.Drawing.Point(408, 68);
            this.subtext.Name = "subtext";
            this.subtext.Size = new System.Drawing.Size(144, 21);
            this.subtext.TabIndex = 11;
            // 
            // stopredis
            // 
            this.stopredis.Location = new System.Drawing.Point(196, 105);
            this.stopredis.Name = "stopredis";
            this.stopredis.Size = new System.Drawing.Size(144, 48);
            this.stopredis.TabIndex = 12;
            this.stopredis.Text = "断开Redis连接";
            this.stopredis.UseVisualStyleBackColor = true;
            this.stopredis.Click += new System.EventHandler(this.stopredis_Click);
            // 
            // redisip
            // 
            this.redisip.Location = new System.Drawing.Point(83, 165);
            this.redisip.Name = "redisip";
            this.redisip.Size = new System.Drawing.Size(257, 21);
            this.redisip.TabIndex = 13;
            this.redisip.Text = "127.0.0.1:6379";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(595, 105);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(144, 48);
            this.button1.TabIndex = 14;
            this.button1.Text = "取消所有订阅";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // publish
            // 
            this.publish.Location = new System.Drawing.Point(408, 105);
            this.publish.Name = "publish";
            this.publish.Size = new System.Drawing.Size(144, 48);
            this.publish.TabIndex = 15;
            this.publish.Text = "发布消息到指定频道";
            this.publish.UseVisualStyleBackColor = true;
            this.publish.Click += new System.EventHandler(this.publish_Click);
            // 
            // channel
            // 
            this.channel.Location = new System.Drawing.Point(408, 165);
            this.channel.Name = "channel";
            this.channel.Size = new System.Drawing.Size(144, 21);
            this.channel.TabIndex = 16;
            this.channel.Text = "pingdao1";
            // 
            // message
            // 
            this.message.Location = new System.Drawing.Point(408, 202);
            this.message.Name = "message";
            this.message.Size = new System.Drawing.Size(144, 21);
            this.message.TabIndex = 17;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(9, 163);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 23);
            this.label3.TabIndex = 18;
            this.label3.Text = "redis地址";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(337, 68);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 23);
            this.label4.TabIndex = 19;
            this.label4.Text = "频道";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(337, 165);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 23);
            this.label5.TabIndex = 20;
            this.label5.Text = "频道";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(370, 200);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(32, 23);
            this.label6.TabIndex = 21;
            this.label6.Text = "消息";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(378, 334);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(24, 23);
            this.label7.TabIndex = 22;
            this.label7.Text = "key";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.redisui);
            this.tabControl1.Controls.Add(this.rabbitMQ);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(776, 437);
            this.tabControl1.TabIndex = 0;
            // 
            // redisui
            // 
            this.redisui.Controls.Add(this.pingdao2);
            this.redisui.Controls.Add(this.label8);
            this.redisui.Controls.Add(this.button2);
            this.redisui.Controls.Add(this.publish);
            this.redisui.Controls.Add(this.label3);
            this.redisui.Controls.Add(this.label7);
            this.redisui.Controls.Add(this.redisip);
            this.redisui.Controls.Add(this.selectdb);
            this.redisui.Controls.Add(this.stopredis);
            this.redisui.Controls.Add(this.label2);
            this.redisui.Controls.Add(this.label6);
            this.redisui.Controls.Add(this.port);
            this.redisui.Controls.Add(this.searsh1);
            this.redisui.Controls.Add(this.label1);
            this.redisui.Controls.Add(this.label5);
            this.redisui.Controls.Add(this.ip);
            this.redisui.Controls.Add(this.sub);
            this.redisui.Controls.Add(this.stopserver);
            this.redisui.Controls.Add(this.label4);
            this.redisui.Controls.Add(this.StartServer);
            this.redisui.Controls.Add(this.subtext);
            this.redisui.Controls.Add(this.connect);
            this.redisui.Controls.Add(this.button1);
            this.redisui.Controls.Add(this.richTextBox1);
            this.redisui.Controls.Add(this.message);
            this.redisui.Controls.Add(this.channel);
            this.redisui.Location = new System.Drawing.Point(4, 22);
            this.redisui.Name = "redisui";
            this.redisui.Size = new System.Drawing.Size(768, 411);
            this.redisui.TabIndex = 0;
            this.redisui.Text = "RedisUI";
            // 
            // pingdao2
            // 
            this.pingdao2.Location = new System.Drawing.Point(595, 66);
            this.pingdao2.Name = "pingdao2";
            this.pingdao2.Size = new System.Drawing.Size(144, 21);
            this.pingdao2.TabIndex = 25;
            this.pingdao2.Text = "pingdao1";
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(558, 66);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(37, 23);
            this.label8.TabIndex = 24;
            this.label8.Text = "频道";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(595, 7);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(144, 48);
            this.button2.TabIndex = 23;
            this.button2.Text = "取消所指定阅";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // rabbitMQ
            // 
            this.rabbitMQ.Controls.Add(this.xqueuename);
            this.rabbitMQ.Controls.Add(this.label23);
            this.rabbitMQ.Controls.Add(this.rabbitmqsub);
            this.rabbitMQ.Controls.Add(this.stoprabbitmq);
            this.rabbitMQ.Controls.Add(this.rabbitmessage);
            this.rabbitMQ.Controls.Add(this.label22);
            this.rabbitMQ.Controls.Add(this.queuename);
            this.rabbitMQ.Controls.Add(this.label21);
            this.rabbitMQ.Controls.Add(this.button12);
            this.rabbitMQ.Controls.Add(this.rabbitmqport);
            this.rabbitMQ.Controls.Add(this.label20);
            this.rabbitMQ.Controls.Add(this.PassWord);
            this.rabbitMQ.Controls.Add(this.label19);
            this.rabbitMQ.Controls.Add(this.label18);
            this.rabbitMQ.Controls.Add(this.UserName);
            this.rabbitMQ.Controls.Add(this.label17);
            this.rabbitMQ.Controls.Add(this.HostName);
            this.rabbitMQ.Controls.Add(this.rabbitmqtext);
            this.rabbitMQ.Controls.Add(this.connectrabbitmq);
            this.rabbitMQ.Location = new System.Drawing.Point(4, 22);
            this.rabbitMQ.Name = "rabbitMQ";
            this.rabbitMQ.Size = new System.Drawing.Size(768, 411);
            this.rabbitMQ.TabIndex = 0;
            this.rabbitMQ.Text = "RabbitMQ";
            // 
            // xqueuename
            // 
            this.xqueuename.Location = new System.Drawing.Point(617, 74);
            this.xqueuename.Name = "xqueuename";
            this.xqueuename.Size = new System.Drawing.Size(112, 21);
            this.xqueuename.TabIndex = 18;
            this.xqueuename.Text = "shushu";
            // 
            // label23
            // 
            this.label23.Location = new System.Drawing.Point(558, 74);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(53, 23);
            this.label23.TabIndex = 17;
            this.label23.Text = "队列Name";
            this.label23.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // rabbitmqsub
            // 
            this.rabbitmqsub.Location = new System.Drawing.Point(602, 20);
            this.rabbitmqsub.Name = "rabbitmqsub";
            this.rabbitmqsub.Size = new System.Drawing.Size(127, 46);
            this.rabbitmqsub.TabIndex = 16;
            this.rabbitmqsub.Text = "rabbitmq消费指定队列中的数据";
            this.rabbitmqsub.UseVisualStyleBackColor = true;
            this.rabbitmqsub.Click += new System.EventHandler(this.rabbitmqsub_Click);
            // 
            // stoprabbitmq
            // 
            this.stoprabbitmq.Location = new System.Drawing.Point(218, 20);
            this.stoprabbitmq.Name = "stoprabbitmq";
            this.stoprabbitmq.Size = new System.Drawing.Size(127, 46);
            this.stoprabbitmq.TabIndex = 15;
            this.stoprabbitmq.Text = "断开连接rabbitmq";
            this.stoprabbitmq.UseVisualStyleBackColor = true;
            this.stoprabbitmq.Click += new System.EventHandler(this.stoprabbitmq_Click);
            // 
            // rabbitmessage
            // 
            this.rabbitmessage.Location = new System.Drawing.Point(433, 111);
            this.rabbitmessage.Name = "rabbitmessage";
            this.rabbitmessage.Size = new System.Drawing.Size(112, 21);
            this.rabbitmessage.TabIndex = 14;
            this.rabbitmessage.Text = "localhost";
            // 
            // label22
            // 
            this.label22.Location = new System.Drawing.Point(374, 109);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(53, 23);
            this.label22.TabIndex = 13;
            this.label22.Text = "消息";
            this.label22.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // queuename
            // 
            this.queuename.Location = new System.Drawing.Point(433, 72);
            this.queuename.Name = "queuename";
            this.queuename.Size = new System.Drawing.Size(112, 21);
            this.queuename.TabIndex = 12;
            this.queuename.Text = "shushu";
            // 
            // label21
            // 
            this.label21.Location = new System.Drawing.Point(374, 72);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(53, 23);
            this.label21.TabIndex = 11;
            this.label21.Text = "队列Name";
            this.label21.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // button12
            // 
            this.button12.Location = new System.Drawing.Point(418, 20);
            this.button12.Name = "button12";
            this.button12.Size = new System.Drawing.Size(127, 46);
            this.button12.TabIndex = 10;
            this.button12.Text = "rabbitmq发布消息到指定队列";
            this.button12.UseVisualStyleBackColor = true;
            this.button12.Click += new System.EventHandler(this.button12_Click);
            // 
            // rabbitmqport
            // 
            this.rabbitmqport.Location = new System.Drawing.Point(77, 182);
            this.rabbitmqport.Name = "rabbitmqport";
            this.rabbitmqport.Size = new System.Drawing.Size(112, 21);
            this.rabbitmqport.TabIndex = 9;
            this.rabbitmqport.Text = "5672";
            // 
            // label20
            // 
            this.label20.Location = new System.Drawing.Point(18, 182);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(53, 23);
            this.label20.TabIndex = 8;
            this.label20.Text = "Port";
            this.label20.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // PassWord
            // 
            this.PassWord.Location = new System.Drawing.Point(77, 148);
            this.PassWord.Name = "PassWord";
            this.PassWord.Size = new System.Drawing.Size(112, 21);
            this.PassWord.TabIndex = 7;
            this.PassWord.Text = "guest";
            // 
            // label19
            // 
            this.label19.Location = new System.Drawing.Point(18, 146);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(53, 23);
            this.label19.TabIndex = 6;
            this.label19.Text = "PassWord";
            this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label18
            // 
            this.label18.Location = new System.Drawing.Point(18, 109);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(53, 23);
            this.label18.TabIndex = 5;
            this.label18.Text = "UserName";
            this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // UserName
            // 
            this.UserName.Location = new System.Drawing.Point(77, 109);
            this.UserName.Name = "UserName";
            this.UserName.Size = new System.Drawing.Size(112, 21);
            this.UserName.TabIndex = 4;
            this.UserName.Text = "guest";
            // 
            // label17
            // 
            this.label17.Location = new System.Drawing.Point(18, 72);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(53, 23);
            this.label17.TabIndex = 3;
            this.label17.Text = "HostName";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // HostName
            // 
            this.HostName.Location = new System.Drawing.Point(77, 72);
            this.HostName.Name = "HostName";
            this.HostName.Size = new System.Drawing.Size(112, 21);
            this.HostName.TabIndex = 2;
            this.HostName.Text = "localhost";
            // 
            // rabbitmqtext
            // 
            this.rabbitmqtext.Location = new System.Drawing.Point(18, 208);
            this.rabbitmqtext.Name = "rabbitmqtext";
            this.rabbitmqtext.Size = new System.Drawing.Size(409, 200);
            this.rabbitmqtext.TabIndex = 1;
            this.rabbitmqtext.Text = "";
            // 
            // connectrabbitmq
            // 
            this.connectrabbitmq.Location = new System.Drawing.Point(62, 20);
            this.connectrabbitmq.Name = "connectrabbitmq";
            this.connectrabbitmq.Size = new System.Drawing.Size(127, 46);
            this.connectrabbitmq.TabIndex = 0;
            this.connectrabbitmq.Text = "连接rabbitmq";
            this.connectrabbitmq.UseVisualStyleBackColor = true;
            this.connectrabbitmq.Click += new System.EventHandler(this.connectrabbitmq_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(595, 66);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(144, 21);
            this.textBox1.TabIndex = 25;
            this.textBox1.Text = "pingdao1";
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(558, 66);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(37, 23);
            this.label9.TabIndex = 24;
            this.label9.Text = "频道";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(595, 7);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(144, 48);
            this.button3.TabIndex = 23;
            this.button3.Text = "取消所指定阅";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button2_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(408, 105);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(144, 48);
            this.button4.TabIndex = 15;
            this.button4.Text = "发布消息到指定频道";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.publish_Click);
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(9, 163);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(68, 23);
            this.label10.TabIndex = 18;
            this.label10.Text = "redis地址";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(378, 334);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(24, 23);
            this.label11.TabIndex = 22;
            this.label11.Text = "key";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(83, 165);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(257, 21);
            this.textBox2.TabIndex = 13;
            this.textBox2.Text = "127.0.0.1:6379";
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(408, 268);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(144, 48);
            this.button5.TabIndex = 3;
            this.button5.Text = "查找数据";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.selectdb_Click);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(196, 105);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(144, 48);
            this.button6.TabIndex = 12;
            this.button6.Text = "断开Redis连接";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.stopredis_Click);
            // 
            // label12
            // 
            this.label12.Location = new System.Drawing.Point(189, 66);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(29, 23);
            this.label12.TabIndex = 9;
            this.label12.Text = "port";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label13
            // 
            this.label13.Location = new System.Drawing.Point(370, 200);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(32, 23);
            this.label13.TabIndex = 21;
            this.label13.Text = "消息";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(224, 68);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(116, 21);
            this.textBox3.TabIndex = 8;
            this.textBox3.Text = "8019";
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(408, 336);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(144, 21);
            this.textBox4.TabIndex = 4;
            // 
            // label14
            // 
            this.label14.Location = new System.Drawing.Point(13, 66);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(29, 23);
            this.label14.TabIndex = 7;
            this.label14.Text = "ip";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label15
            // 
            this.label15.Location = new System.Drawing.Point(337, 165);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(65, 23);
            this.label15.TabIndex = 20;
            this.label15.Text = "频道";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBox5
            // 
            this.textBox5.Location = new System.Drawing.Point(48, 68);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(116, 21);
            this.textBox5.TabIndex = 6;
            this.textBox5.Text = "127.0.0.1";
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(408, 7);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(144, 48);
            this.button7.TabIndex = 10;
            this.button7.Text = "订阅频道";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.sub_Click);
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(196, 7);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(144, 48);
            this.button8.TabIndex = 5;
            this.button8.Text = "关闭服务器";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.stopserver_Click);
            // 
            // label16
            // 
            this.label16.Location = new System.Drawing.Point(337, 68);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(65, 23);
            this.label16.TabIndex = 19;
            this.label16.Text = "频道";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // button9
            // 
            this.button9.Location = new System.Drawing.Point(20, 7);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(144, 48);
            this.button9.TabIndex = 2;
            this.button9.Text = "启动服务器";
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Click += new System.EventHandler(this.StartServer_Click);
            // 
            // textBox6
            // 
            this.textBox6.Location = new System.Drawing.Point(408, 68);
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(144, 21);
            this.textBox6.TabIndex = 11;
            // 
            // button10
            // 
            this.button10.Location = new System.Drawing.Point(20, 105);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(144, 48);
            this.button10.TabIndex = 1;
            this.button10.Text = "连接Redis";
            this.button10.UseVisualStyleBackColor = true;
            this.button10.Click += new System.EventHandler(this.connect_Click);
            // 
            // button11
            // 
            this.button11.Location = new System.Drawing.Point(595, 105);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(144, 48);
            this.button11.TabIndex = 14;
            this.button11.Text = "取消所有订阅";
            this.button11.UseVisualStyleBackColor = true;
            this.button11.Click += new System.EventHandler(this.button1_Click);
            // 
            // richTextBox2
            // 
            this.richTextBox2.Location = new System.Drawing.Point(10, 202);
            this.richTextBox2.Name = "richTextBox2";
            this.richTextBox2.Size = new System.Drawing.Size(330, 206);
            this.richTextBox2.TabIndex = 0;
            this.richTextBox2.Text = "";
            // 
            // textBox7
            // 
            this.textBox7.Location = new System.Drawing.Point(408, 202);
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new System.Drawing.Size(144, 21);
            this.textBox7.TabIndex = 17;
            // 
            // textBox8
            // 
            this.textBox8.Location = new System.Drawing.Point(408, 165);
            this.textBox8.Name = "textBox8";
            this.textBox8.Size = new System.Drawing.Size(144, 21);
            this.textBox8.TabIndex = 16;
            this.textBox8.Text = "pingdao1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tabControl1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.tabControl1.ResumeLayout(false);
            this.redisui.ResumeLayout(false);
            this.redisui.PerformLayout();
            this.rabbitMQ.ResumeLayout(false);
            this.rabbitMQ.PerformLayout();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.TextBox xqueuename;

        private System.Windows.Forms.Button rabbitmqsub;

        private System.Windows.Forms.Button stoprabbitmq;

        private System.Windows.Forms.Button button12;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TextBox queuename;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.TextBox rabbitmessage;

        private System.Windows.Forms.TextBox PassWord;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox rabbitmqport;

        private System.Windows.Forms.Label label19;

        private System.Windows.Forms.TextBox UserName;
        private System.Windows.Forms.Label label18;

        private System.Windows.Forms.Label label17;

        private System.Windows.Forms.TextBox HostName;

        private System.Windows.Forms.RichTextBox rabbitmqtext;

        private System.Windows.Forms.Button connectrabbitmq;

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.Button button10;
        private System.Windows.Forms.Button button11;
        private System.Windows.Forms.RichTextBox richTextBox2;
        private System.Windows.Forms.TextBox textBox7;
        private System.Windows.Forms.TextBox textBox8;

        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox pingdao2;

        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;

        private System.Windows.Forms.Label label4;

        private System.Windows.Forms.Label label3;

        private System.Windows.Forms.TextBox message;

        private System.Windows.Forms.Button publish;
        private System.Windows.Forms.TextBox channel;

        private System.Windows.Forms.TextBox redisip;
        private System.Windows.Forms.Button button1;

        private System.Windows.Forms.Button stopredis;

        private System.Windows.Forms.TextBox subtext;

        private System.Windows.Forms.Button sub;

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
        
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage redisui;
        private System.Windows.Forms.TabPage rabbitMQ;

        #endregion
    }
}