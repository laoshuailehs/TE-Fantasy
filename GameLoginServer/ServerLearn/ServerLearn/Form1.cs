using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace ServerLearn
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private Socket  serverSocket;
        
        byte[] bytes = new byte[1024];
        private void StartServer_Click(object sender, EventArgs e)
        {
            StartServerInfo();
        }

        private void StopServer_Click(object sender, EventArgs e)
        {
            serverSocket?.Close();
            AddText("服务器已关闭");
        }

        private void StartServerInfo()
        {
            serverSocket= new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress ipAddress= IPAddress.Parse(Ip.Text);
            int port= int.Parse(Port.Text);
            IPEndPoint ipEndPoint= new IPEndPoint(ipAddress, port);
            serverSocket.Bind(ipEndPoint);
            serverSocket.Listen(10);
            AddText("服务器启动成功,等待客户端连接。。。");
            serverSocket.BeginAccept(AcceptCallback, serverSocket);
        }

        private void AcceptCallback(IAsyncResult ar)
        {
            Socket listener = (Socket)ar.AsyncState;
            Socket clientSocket = listener.EndAccept(ar); // 完成客户端连接
            AddText("客户端已连接");

            // 可以继续接收客户端数据或处理通信
            clientSocket.BeginReceive(bytes, 0, bytes.Length, SocketFlags.None, ReceiveCallback, clientSocket);
            // 关键点：继续监听下一个客户端连接
            listener.BeginAccept(AcceptCallback, listener);
        }

        private void ReceiveCallback(IAsyncResult ar)
        {
            Socket clientSocket = (Socket)ar.AsyncState;
            try
            {
                int bytesRead = clientSocket.EndReceive(ar);

                if (bytesRead > 0)
                {
                    string data = Encoding.UTF8.GetString(bytes, 0, bytesRead);
                    AddText($"收到消息：{data}");

                    byte[] responseBytes = Encoding.UTF8.GetBytes(ResponseData(data));
                    clientSocket.BeginSend(responseBytes, 0, responseBytes.Length, SocketFlags.None, SendCallback, clientSocket);

                    // 继续监听下一次接收
                    clientSocket.BeginReceive(bytes, 0, bytes.Length, SocketFlags.None, ReceiveCallback, clientSocket);
                }
                else
                {
                    // 客户端主动断开连接
                    AddText("客户端已断开连接");
                    clientSocket.Shutdown(SocketShutdown.Both); // 更安全地关闭
                    clientSocket.Close(); // 关闭该客户端 socket
                }
            }
            catch (Exception ex)
            {
                AddText($"客户端通信异常：{ex.Message}");
                clientSocket.Close(); // 出现异常也关闭 socket
            }
        }

        private void SendCallback(IAsyncResult ar)
        {
            Socket clientSocket = (Socket)ar.AsyncState;
            int bytesSent = clientSocket.EndSend(ar);
            AddText($"成功发送 {bytesSent} 字节");

            // 继续监听下一次接收
            // clientSocket.BeginReceive(bytes, 0, bytes.Length, SocketFlags.None, ReceiveCallback, clientSocket);

        }

        private string ResponseData(string data)
        {
            if (data == "1")
            {
                return "成功";
            }
            return "失败";
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