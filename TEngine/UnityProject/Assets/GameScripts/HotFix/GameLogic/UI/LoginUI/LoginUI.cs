// using System;
// using System.Net.Sockets;
// using System.Net.WebSockets;
// using System.Text;
// using System.Threading;
// using Cysharp.Threading.Tasks;
// using Newtonsoft.Json;
// using MongoDB.Bson;
// using MongoDB.Driver;
// using UnityEngine;
using UnityEngine.UI;
using TEngine;

namespace GameLogic
{
    [Window(UILayer.UI)]
    class LoginUI : UIWindow
    {
        #region 脚本工具生成的代码
        private InputField _inputAccount;
        private InputField _inputPassword;
        private Button _btnLogin;
        private Button _btnRegistration;
        private Text _textLogin;
        private bool canLogin;
        
        // string serverIp = "127.0.0.1"; // 服务器 IP 地址
        // int port = 8017;             // 服务器监听端口
        protected override void ScriptGenerator()
        {
            _inputAccount = FindChildComponent<InputField>("m_inputAccount");
            _inputPassword = FindChildComponent<InputField>("m_inputPassword");
            _btnLogin = FindChildComponent<Button>("m_btnLogin");
            _btnRegistration = FindChildComponent<Button>("m_btnRegistration");
            _textLogin = FindChildComponent<Text>("m_textLogin");
            _btnLogin.onClick.AddListener(OnClickLoginBtn);
            _btnRegistration.onClick.AddListener(OnClickRegistration);
        }
        #endregion

        protected override void OnUpdate()
        {
            base.OnUpdate();
            _hasOverrideUpdate = true;
            if (_textLogin.gameObject.activeSelf)
            {
                // cts?.Cancel();
                this.Close();
                // GameModule.Scene.LoadSceneAsync("Effect");
                GameModule.UI.ShowUIAsync<OptionUI>();
            }
        }

        protected override void OnCreate()
        {
            base.OnCreate();
        }

        #region 事件
        private void OnClickLoginBtn()
        {
            // 读取luban表数据
             // foreach (var item in ConfigSystem.Instance.Tables.TbInfo.DataMap)
             // {
             //     if (item.Value.Username == _inputAccount.text )
             //     {
             //         int.TryParse(_inputPassword.text, out int password);
             //         if (item.Value.Password == password)
             //         {
             //             // GameModule.Scene.LoadScene("hs");
             //             // GameModule.UI.ShowUIAsync<HsTestUI>();
             //             // this.Close();
             //             canLogin = true;
             //         }
             //     }
             // }
            
            //读取mysql数据库 未知报错 没解决(可能的解决方法，下载低版本mysql,懒得试了out！) KeyNotFoundException: The given key '29544' was not present in the dictionary.
            
            //读取mongoDB数据库 
            // StartConnect();
            // QueryData(_inputAccount.text, _inputPassword.text);
            
            //发送服务器请求，服务器验证登入信息
            // ConnectToServer(serverIp, port);
            // SendLoginRequest(_inputAccount.text, _inputPassword.text);

            // MainTest().Forget();
            
            // canLogin = true;


            // GameModule.Scene.LoadScene("Game");
            // GameModule.UI.ShowUIAsync<HsTestUI>();
            // this.Close();

        }
        private void OnClickRegistration()
        {
            //读取mongoDB数据库 
            // StartConnect();
            // InsertData(_inputAccount.text, _inputPassword.text);
            // tcpClient.Close();
        }
        #endregion

        #region MongoDB测试

        // private IMongoDatabase datebase;//数据库
        // private IMongoCollection<BsonDocument> collection;//集合 表
        //
        // private string connectionString = "mongodb://localhost:27017";
        // private string databaseName = "hsgamedb";
        // private string collectionName = "gameLogin";
        //
        // private void StartConnect()
        // {
        //     MongoClient client = new MongoClient(connectionString);
        //     datebase = client.GetDatabase(databaseName);
        //     collection = datebase.GetCollection<BsonDocument>(collectionName);
        //     Log.Debug("连接mongodb成功！");
        //     // QueryData(_inputAccount.text, _inputPassword.text);
        //
        // }
        //
        // private void QueryData(string name ,string password)
        // {
        //     var documents = collection.Find(new BsonDocument()).ToList();
        //     Log.Debug("QueryData");
        //     foreach (var document in documents)
        //     {
        //         Debug.Log(document.ToString());
        //         Log.Info(document["username"]+" "+document["password"]);
        //     }
        //     if(name=="")return;
        //     var filter = Builders<BsonDocument>.Filter.Eq("username", name);
        //     var result = collection.Find(filter).ToList();
        //     
        //     // var password1 = int.TryParse(password,  out int password2);
        //     if (result.Count == 0)
        //     {
        //         Log.Info("用户不存在");
        //         return;
        //     }
        //     if (result[0]["username"] + "" == name && result[0]["password"] + "" == password)
        //     {
        //         GameModule.Scene.LoadScene("hs");
        //         GameModule.UI.ShowUIAsync<HsTestUI>();
        //         this.Close();
        //     }
        //     else
        //     {
        //         Log.Info("密码错误");
        //     }
        //
        // }
        //
        // private void InsertData(string name, string password)
        // {
        //     if (name == "" || password == "")
        //     {
        //         Log.Info("用户名或密码不能为空");
        //         return;
        //     }
        //     var document=new BsonDocument
        //     {
        //         {"username",name},
        //         {"password",password}
        //     };
        //     collection.InsertOne(document);
        //     Log.Info("注册插入成功");
        // }
        //
        // private void UpdateData(string name, string password)
        // {
        //     if (name == "" || password == "")
        //     {
        //         Log.Info("用户名或密码不能为空");
        //         return;
        //     }
        //     var filter = Builders<BsonDocument>.Filter.Eq("username", name);
        //     var update = Builders<BsonDocument>.Update.Set("password", password);
        //     var result= collection.UpdateOne(filter, update);
        //     Log.Info(result.ModifiedCount > 0 ? "更新成功" : "更新失败");
        // }
        //
        // private void DeleteData(string name, string password)
        // {
        //     if (name == "" || password == "")
        //     {
        //         Log.Info("用户名或密码不能为空");
        //     }
        //     var filter = Builders<BsonDocument>.Filter.And(
        //         Builders<BsonDocument>.Filter.Eq("username", name),
        //         Builders<BsonDocument>.Filter.Eq("password", password));
        //     var result = collection.DeleteOne(filter);
        //     Log.Info(result.DeletedCount > 0 ? "删除成功" : "用户名或密码错误删除失败");
        // }
        
        #endregion

        // private TcpClient tcpClient;
        #region 发送服务器请求测试 Socket
        // private CancellationTokenSource cts;
        // // 连接服务器按钮点击事件
        // private void ConnectToServer(string serverIp, int port)
        // {
        //     try
        //     {
        //         tcpClient = new TcpClient();
        //         tcpClient.Connect(serverIp, port);
        //
        //         Log.Info("成功连接到服务器！");
        //         // 可选：启动接收服务器消息的任务
        //         cts = new CancellationTokenSource();
        //         UniTask.RunOnThreadPool(async () =>
        //         {
        //             await ReceiveMessagesAsync(cts.Token);
        //             
        //         }); // 在线程池中运行
        //         
        //     }
        //     catch (Exception ex)
        //     {
        //         Log.Error($"连接服务器失败: {ex.Message}");
        //     }
        // }
        //
        //
        // // 发送登录请求示例方法
        // private void SendLoginRequest(string username, string password)
        // {
        //     if (tcpClient != null && tcpClient.Connected)
        //     {
        //         // string loginData = $"{username},{password}"; // 构造登录数据包
        //         LoginInfo loginInfo = new LoginInfo
        //         {
        //             UserName = username,
        //             Password = password
        //         };
        //         string loginData = JsonConvert.SerializeObject(loginInfo);
        //         byte[] data = Encoding.UTF8.GetBytes(loginData);
        //         
        //         NetworkStream stream = tcpClient.GetStream();
        //         stream.Write(data, 0, data.Length);
        //     }
        //     else
        //     {
        //         Log.Error("未连接到服务器");
        //     }
        // }
        //
        // // 接收服务器消息
        // private async UniTask ReceiveMessagesAsync(CancellationToken token)
        // {
        //     try
        //     {
        //         NetworkStream stream = tcpClient.GetStream();
        //         byte[] buffer = new byte[1024];
        //
        //         while (!token.IsCancellationRequested)
        //         {
        //             if (!tcpClient.Connected) break;
        //
        //             var result = await stream.ReadAsync(buffer, 0, buffer.Length, token).ConfigureAwait(false);
        //             if (result == 0) break; // 连接关闭
        //
        //             string response = Encoding.UTF8.GetString(buffer, 0, result);
        //             Log.Info($"收到服务器响应: {response}");
        //
        //             if (response.Equals("1"))
        //             {
        //                 canLogin = true;
        //             }else if (response.Equals("0"))
        //             {
        //                 Log.Info("账号或密码错误,登录失败");
        //             }
        //             else
        //             {
        //                 Log.Info("账号或密码错误,登录失败");
        //             }
        //         }
        //     }
        //     catch (OperationCanceledException)
        //     {
        //         Log.Info("接收消息已取消");
        //     }
        //     catch (Exception ex)
        //     {
        //         Log.Warning($"接收服务器消息时出错: {ex.Message}");
        //     }
        //     finally
        //     {
        //         tcpClient?.Close();
        //     }
        // }
        
        #endregion

        // private ClientWebSocket webSocket;
        #region 发送服务器请求测试 WebSocket

        // private async UniTask ConnectToWebSocketServer(string url)
        // {
        //     webSocket = new ClientWebSocket();
        //     try
        //     {
        //         await webSocket.ConnectAsync(new Uri(url), CancellationToken.None);
        //         Log.Info("成功连接到WebSocket服务器！");
        //         // 可选：启动接收服务器消息的任务
        //         // await ReceiveMessagesAsync();
        //     }
        //     catch (Exception ex)
        //     {
        //         Log.Info($"连接 WebSocket 服务器失败: {ex.Message}, 内部异常: {ex.InnerException?.Message}");
        //     }
        // }
        //
        // private async UniTask SendWebSocketMessage()
        // {
        //     if (webSocket != null && webSocket.State == WebSocketState.Open)
        //     {
        //         LoginInfo loginInfo = new LoginInfo
        //         {
        //             UserName=_inputAccount.text,
        //             Password = _inputPassword.text
        //         };
        //         string message = JsonConvert.SerializeObject(loginInfo);
        //         byte[] buffer = Encoding.UTF8.GetBytes(message);
        //         await webSocket.SendAsync(new ArraySegment<byte>(buffer), WebSocketMessageType.Text, true, CancellationToken.None);
        //         Log.Info($"已发送消息: {message}");
        //     }
        //     else
        //     {
        //         Log.Info("WebSocket 未连接");
        //     }
        // }
        //
        // private async UniTask ReceiveMessagesAsync()
        // {
        //     if (webSocket.State != WebSocketState.Open)
        //     {
        //         Log.Warning("WebSocket 不在 Open 状态，无法接收消息");
        //         return;
        //     }
        //     byte[] receiveBuffer = new byte[1024];
        //
        //     while (webSocket.State == WebSocketState.Open)
        //     {
        //         var result = await webSocket.ReceiveAsync(new ArraySegment<byte>(receiveBuffer), CancellationToken.None);
        //
        //         if (result.MessageType == WebSocketMessageType.Close)
        //         {
        //             if (webSocket.State == WebSocketState.Open ||
        //                 webSocket.State == WebSocketState.CloseReceived ||
        //                 webSocket.State == WebSocketState.CloseSent)
        //             {
        //                 await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Client closing", CancellationToken.None);
        //             }
        //             break;
        //         }
        //
        //
        //         string receivedMessage = Encoding.UTF8.GetString(receiveBuffer, 0, result.Count);
        //         Log.Info($"收到服务器响应: {receivedMessage}");
        //         if (receivedMessage.Equals("1"))
        //         {
        //             canLogin = true;
        //         }
        //         else
        //         {
        //             Log.Debug("账号或密码错误，登入失败！");
        //         }
        //     }
        // }
        //
        // public void DisconnectWebSocket()
        // {
        //     if (webSocket != null && webSocket.State == WebSocketState.Open)
        //     {
        //         webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closing", CancellationToken.None);
        //         Log.Info("WebSocket 连接已关闭");
        //     }
        // }
        //
        // private async UniTask MainTest()
        // {
        //     if (webSocket != null && webSocket.State != WebSocketState.Closed)
        //     {
        //         DisconnectWebSocket();
        //     }
        //     
        //     await ConnectToWebSocketServer("ws://localhost:8017/"); // 公共测试 WebSocket 服务器
        //
        //     await SendWebSocketMessage();
        //     await UniTask.Delay(100); // 等待接收响应
        //     await ReceiveMessagesAsync();
        //
        // }
        //
        #endregion
        
        protected override void Close()
        {
            base.Close();
            // cts?.Cancel();
            // tcpClient.Close();
            // if (webSocket != null && webSocket.State == WebSocketState.Open)
            // {
            //    webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closing", CancellationToken.None);
            // }
        }
    }
    public class LoginInfo
    {
        public string  UserName;
        public string  Password;
    }
}

