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
        }

        private Socket  serverSocket;
        private IMongoDatabase _mongoDatabase;
        private IMongoCollection<BsonDocument> _loginCollection;
        byte[] bytes = new byte[1024];
        bool  isMongoConnected = false;
        private void StartServer_Click(object sender, EventArgs e)
        {
            StartServerInfo();
            ConnectMongo();
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

        private void ConnectMongo()
        {
            try
            {
                MongoClient mongoclient = new MongoClient(mongodb.Text);
                _mongoDatabase = mongoclient.GetDatabase(Database.Text);
                _loginCollection = _mongoDatabase.GetCollection<BsonDocument>(Collection.Text);
                var count = _loginCollection.CountDocuments(new BsonDocument());
                isMongoConnected = true;
            }
            catch (Exception e)
            {
                AddText("MongoDB 连接失败");
                isMongoConnected = false;
            }
            
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
                    AddText($"{isMongoConnected} 收到消息：{data}");
                    //  处理数据发送响应
                    byte[] responseBytes = new byte[bytesRead];
                    if (!isMongoConnected)
                    {
                        responseBytes = Encoding.UTF8.GetBytes("MongoDB 连接失败");
                    }
                    else
                    {
                        responseBytes = Encoding.UTF8.GetBytes(ResponseData(data));
                    }
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
                return Register(loginInfo);
            }
            else if (loginInfo.PackId == 3)
            {
                return UpdateUser(loginInfo);
            }   
            else
            {
                AddText("未知请求");
                return "0"; // 未知请求
            }
            // return "0";
        }

        private string Register(LoginInfo loginInfo)
        {
            try
            {
                // 查询用户
                var filter = Builders<BsonDocument>.Filter.Eq("username", loginInfo.UserName);
                var result = _loginCollection.Find(filter).ToList();
        
                // 如果查询结果为空，表示用户不存在
                if (result.Count > 0)
                {
                    AddText($"用户已存在");
                    Response response = new Response()
                    {
                        PackId = loginInfo.PackId,
                        Result = 0,
                        Description = "用户已存在",
                        UserName = loginInfo.UserName,
                        Password = loginInfo.Password
                    };
                    return JsonConvert.SerializeObject(response);  // 用户已存在
                }
                
                var document = new BsonDocument()
                {
                    { "username", loginInfo.UserName },
                    { "password", loginInfo.Password }
                };
                _loginCollection.InsertOne(document);
                AddText("注册成功");
                Response response1 = new Response()
                {
                    PackId = loginInfo.PackId,
                    Result = 1,
                    Description = "注册成功",
                    UserName = loginInfo.UserName,
                    Password = loginInfo.Password
                };
                return JsonConvert.SerializeObject(response1);  // 用户已存在
            }
            catch (Exception e)
            {
                AddText(e.Message);
                return "0";
            }
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
                Response response = new Response()
                {
                    PackId = loginInfo.PackId,
                    Result = 0,
                    Description = "用户不存在",
                    UserName = loginInfo.UserName,
                    Password = loginInfo.Password
                };
                return JsonConvert.SerializeObject(response); // 用户不存在
            }
        
            foreach (var document in result)
            {
                AddText($"{document.ToString()}");
            }
        
            // 验证密码
            if (result[0]["password"].ToString() == loginInfo.Password)
            {
                AddText($"登录成功");
                Response response = new Response()
                {
                    PackId = loginInfo.PackId,
                    Result = 1,
                    Description = "登录成功",
                    UserName = loginInfo.UserName,
                    Password = loginInfo.Password
                };
                return JsonConvert.SerializeObject(response); // 登录成功
            }
            else
            {
                AddText($"密码错误");
                Response response = new Response()
                {
                    PackId = loginInfo.PackId,
                    Result = 0,
                    Description = "密码错误",
                    UserName = loginInfo.UserName,
                    Password = loginInfo.Password
                };
                return JsonConvert.SerializeObject(response); // 密码错误
            }
        }
        
        private string UpdateUser(LoginInfo loginInfo)
        {
            try
            {
                // 构造查询条件：根据用户名查找用户
                var filter = Builders<BsonDocument>.Filter.Eq("username", loginInfo.UserName);

                // 构造更新内容：更新密码字段
                var update = Builders<BsonDocument>.Update.Set("password", loginInfo.Password);

                // 执行更新操作
                var result = _loginCollection.UpdateOne(filter, update);

                if (result.ModifiedCount > 0)
                {
                    AddText($"用户 {loginInfo.UserName} 更新成功");
                    return JsonConvert.SerializeObject(new Response
                    {
                        PackId = loginInfo.PackId,
                        Result = 1,
                        Description = "更新成功",
                        Password = loginInfo.Password,
                        UserName = loginInfo.UserName
                    });
                }
                else
                {
                    AddText($"用户 {loginInfo.UserName} 不存在或未修改");
                    return JsonConvert.SerializeObject(new Response
                    {
                        PackId = loginInfo.PackId,
                        Result = 0,
                        Description = "用户不存在",
                        Password = loginInfo.Password,
                        UserName = loginInfo.UserName
                    });
                }
            }
            catch (Exception ex)
            {
                AddText($"更新失败: {ex.Message}");
                return JsonConvert.SerializeObject(new Response
                {
                    PackId = loginInfo.PackId,
                    Result = 0,
                    Description = "更新失败",
                    UserName = loginInfo.UserName
                });
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