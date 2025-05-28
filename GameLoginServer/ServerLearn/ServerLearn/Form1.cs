using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;

namespace ServerLearn
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            MongoClient mongoclient = new MongoClient("mongodb://localhost:27017");
            _mongoDatabase = mongoclient.GetDatabase("hsgamedb");
            _loginCollection = _mongoDatabase.GetCollection<BsonDocument>("gameLogin");
        }

        private Socket  serverSocket;
        private IMongoDatabase _mongoDatabase;
        private IMongoCollection<BsonDocument> _loginCollection;
        byte[] bytes = new byte[1024];
        private void StartServer_Click(object sender, EventArgs e)
        {
            StartServerInfo();
        }

        private void StopServer_Click(object sender, EventArgs e)
        {
            if (serverSocket != null)
            {
                serverSocket.Close(); // Close 会自动中止所有 pending 的 BeginAccept
                serverSocket = null;
            }
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
            if (listener == null || !listener.IsBound)
            {
                AddText("Socket 已释放，跳过 Accept 操作");
                return;
            }

            try
            {
                Socket clientSocket = listener.EndAccept(ar);
                AddText("客户端已连接");

                clientSocket.BeginReceive(bytes, 0, bytes.Length, SocketFlags.None, ReceiveCallback, clientSocket);

                // 继续监听下一个客户端连接
                listener.BeginAccept(AcceptCallback, listener);
            }
            catch (ObjectDisposedException)
            {
                AddText("监听 Socket 已释放，停止接受新连接");
            }
            catch (Exception ex)
            {
                AddText($"Accept 异常: {ex.Message}");
            }
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
            AddText(data);
        
            // 检查请求是否为空或空白
            if (string.IsNullOrWhiteSpace(data))
            {
                return "0"; // 请求为空
            }

            LoginInfo loginInfo=JsonConvert.DeserializeObject<LoginInfo>(data);

            //采用id标识1 登入 还是 2 注册
            if (loginInfo.PackId == 1)
            {
                return LoginCheck(loginInfo);
            }
            else if (loginInfo.PackId == 2)
            {
                Register(loginInfo);
                return "0";
            }
            else
            {
                AddText("未知请求");
                return "0"; // 未知请求
            }
            return "0";
        }

        private void Register(LoginInfo loginInfo)
        {
            
        }

        private string LoginCheck(LoginInfo loginInfo)
        {
            // 查询用户
            var filter = Builders<BsonDocument>.Filter.Eq("username", loginInfo.UserName);
            var result = _loginCollection.Find(filter).ToList();
        
            // 如果查询结果为空，表示用户不存在
            if (result.Count == 0)
            {
                AddText($"用户不存在");
                return "0"; // 用户不存在
            }
        
            foreach (var document in result)
            {
                AddText($"{document.ToString()}");
            }
        
            // 验证密码
            if (result[0]["password"].ToString() == loginInfo.Password)
            {
                AddText($"登录成功");
                return "1"; // 登录成功
            }
            else
            {
                AddText($"密码错误");
                return "0"; // 密码错误
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