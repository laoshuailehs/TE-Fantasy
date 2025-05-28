using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // 创建客户端套接字
        TcpClient clientSocket = null;
        private NetworkStream networkStream = null;
        private bool isListening = false;
        private void ConnectServer_Click(object sender, EventArgs e)
        {
            try
            {
                // 连接到服务器
                clientSocket = new TcpClient();
                clientSocket.Connect(Ip.Text,  int.Parse(Port.Text));

                AddText("成功连接到服务器！");
                // 启动接收消息的线程
                networkStream = clientSocket.GetStream();
                isListening = true;
                Task.Run(() => ReceiveMessages());
            }
            catch (Exception ex)
            {
                AddText("连接服务器失败：" + ex.Message);
            }
        }

        private void StopServer_Click(object sender, EventArgs e)
        {
            isListening = false; // 停止监听循环
            if (clientSocket != null && clientSocket.Connected)
            {
                clientSocket.Close(); // 关闭连接
                AddText("已成功断开与服务器的连接！");
            }
            else
            {
                AddText("当前未连接到服务器！");
            }
        }

        private void SendText_Click(object sender, EventArgs e)
        {
            if (clientSocket != null && clientSocket.Connected)
            {
                NetworkStream stream = clientSocket.GetStream();

                string message = SendInfo.Text; // 获取用户输入的消息
                byte[] data = Encoding.UTF8.GetBytes(message); // 将字符串转换为字节数组

                stream.Write(data, 0, data.Length); // 发送数据到服务器

                AddText("消息已发送！");
                
            }
            else
            {
                AddText("当前未连接到服务器！");
            }
        }
        
        private void ReceiveMessages()
        {
            byte[] buffer = new byte[1024];
            int bytesRead;

            while (isListening && networkStream != null && networkStream.CanRead)
            {
                try
                {
                    bytesRead = networkStream.Read(buffer, 0, buffer.Length);

                    if (bytesRead == 0)
                    {
                        // 服务器关闭了连接
                        AddText("服务器已断开连接");
                        break;
                    }

                    string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    AddText("收到消息：" + message);
                }
                catch (IOException ex)
                {
                    // 特别处理连接关闭时的异常
                    if (ex.InnerException is SocketException se && 
                        se.ErrorCode == 10004) // WSAEINTR 错误码: 被中断的系统调用
                    {
                        AddText("检测到连接中断");
                    }
                    else
                    {
                        AddText("接收消息出错：" + ex.Message);
                    }
                    break;
                }
                catch (Exception ex)
                {
                    AddText("接收消息出错：" + ex.Message);
                    break;
                }
            }
        }

        private void AddText(string text)
        {
            if (richTextBox1.InvokeRequired)
            {
                richTextBox1.Invoke(new Action<string>(AddText), text);
            }
            else
            {
                richTextBox1.AppendText(text + "\r\n");
                richTextBox1.ScrollToCaret(); // 这里必须也在 UI 线程执行
            }
        }
    }
}