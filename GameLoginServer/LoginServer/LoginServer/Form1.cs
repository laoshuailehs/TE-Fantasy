using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;

namespace LoginServer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        
        private TcpListener tcpListener;
        private bool isServerRunning = false;
        
        private void button1_Click(object sender, EventArgs e)
        {
            int port = 8017; // 监听端口号，可以根据需要修改
            IPAddress  ip = IPAddress.Parse("127.0.0.1");
            try
            {
                // 初始化 TcpListener
                tcpListener = new TcpListener(ip, port);
                tcpListener.Start();
                isServerRunning = true;

                // 开启一个新线程来监听客户端连接
                Task.Run(() => ListenForClients());
                LogText.Text  += $"服务器已启动，正在监听客户端连接...\r\n";
                // MessageBox.Show("服务器已启动，正在监听客户端连接...");
            }
            catch (Exception ex)
            {
                LogText.Text  += $"启动服务器失败: {ex.Message}\r\n";
                // MessageBox.Show($"启动服务器失败: {ex.Message}");
            }
        }
        
        private void ListenForClients()
        {
            while (isServerRunning)
            {
                try
                {
                    // 接收客户端连接
                    TcpClient client = tcpListener.AcceptTcpClient();
            
                    // 处理客户端连接（可以新开线程或使用异步方式）
                    Task.Run(() => HandleClient(client));
                }
                catch (Exception ex)
                {
                    // 处理异常，例如服务器关闭时的异常
                    // ServerMassage.Items.Add($"监听客户端时发生错误: {ex.Message}");
                    LogText.Text  += $"监听客户端时发生错误: {ex.Message}\r\n";
                    break;
                }
            }
        }

        private void HandleClient(TcpClient client)
        {
            using (NetworkStream stream = client.GetStream())
            {
                LogText.Text  += $"有客户端连接\r\n";
                byte[] buffer = new byte[1024];
                int bytesRead;

                try
                {
                    while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        // 接收客户端发送的数据
                        string request = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                
                        // 处理登录请求（根据实际需求解析和响应）
                        string response = ProcessLoginRequest(request);
                
                        // 返回响应给客户端
                        byte[] responseBytes = Encoding.UTF8.GetBytes(response);
                        stream.Write(responseBytes, 0, responseBytes.Length);
                    }
                }
                catch (Exception ex)
                {
                    LogText.Text  += $"处理客户端时发生错误: {ex.Message}\r\n";
                }
                finally
                {
                    client.Close();
                }
            }
        }

        private string ProcessLoginRequest(string request)
        {
            // 根据实际需求解析登录请求并返回响应
            Console.WriteLine($"收到登录请求: {request}");
            LogText.Text  += $"收到登录请求: {request}\r\n";
            //去mongoDB数据库查找是否有此用户
            
    
            return "1"; // 示例响应
        }
        
        private void StopServerLogin()
        {
            if (isServerRunning)
            {
                isServerRunning = false;
                tcpListener.Stop();
                // MessageBox.Show("服务器已停止");
                LogText.Text  += $"服务器已停止\r\n";
            }
        }

        private void StopServer_Click(object sender, EventArgs e)
        {
            StopServerLogin();
        }
        
    }
}