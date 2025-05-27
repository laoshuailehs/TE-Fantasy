using System;
using System.Buffers;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Net.WebSockets;
using System.Threading;
using Cysharp.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Driver;
using JsonConvert = Newtonsoft.Json.JsonConvert;

namespace LoginServer
{
    public partial class Form1 : Form
    {
        // private TcpListener tcpListener;
        private bool isServerRunning = false;
        // 新增用于控制异步操作的 token source
        private CancellationTokenSource cts;
        
        int port = 8017; // 监听端口号，可以根据需要修改
        IPAddress  ip = IPAddress.Parse("127.0.0.1");
        
        //每次调用 ProcessLoginRequest 都会重新获取数据库和集合对象。
        //考虑将 IMongoDatabase 和 IMongoCollection 提升为类成员变量，避免重复解析。
        private IMongoDatabase _mongoDatabase;
        private IMongoCollection<BsonDocument> _loginCollection;
        
        //当前你使用的是 new byte[1024] 缓冲区，每次读写都创建新的数组。
        //推荐使用 ArrayPool<byte> 或者 MemoryOwner<byte>（如 System.Buffers）来复用缓冲区，减少 GC 压力。
        private const int BufferSize = 1024;
        private readonly ArrayPool<byte> _bufferPool = ArrayPool<byte>.Shared;

        private HttpListener listener;
        private HttpListenerContext context;
        private HttpListenerWebSocketContext webSocketContext;
        private WebSocket webSocket;
        public Form1()
        {
            InitializeComponent();
            MongoClient mongoclient = new MongoClient("mongodb://localhost:27017");
            _mongoDatabase = mongoclient.GetDatabase("hsgamedb");
            _loginCollection = _mongoDatabase.GetCollection<BsonDocument>("gameLogin");

        }

        private async void button1_Click(object sender, EventArgs e)
        {
            //Socket测试
            // try
            // {
            //     tcpListener = new TcpListener(ip, port);
            //     tcpListener.Start();
            //     isServerRunning = true;
            //
            //     // 初始化 Cancellation Token Source
            //     cts = new CancellationTokenSource();
            //
            //     AppendLog("服务器已启动，正在监听客户端连接...");
            //
            //     await ListenForClientsAsync(cts.Token); // 启动异步监听并传入 token
            // }
            // catch (Exception ex)
            // { 
            //     AppendLog(ex.ToString());
            // }
            
            //WebSocket服务器测试
            listener= new HttpListener();
            listener.Prefixes.Add("http://localhost:8017/");
            listener.Start();
            AppendLog("服务器已启动，正在监听客户端连接...");
            while (true)
            {
                context = await listener.GetContextAsync();
                if (context.Request.IsWebSocketRequest)
                {
                    webSocketContext = await context.AcceptWebSocketAsync(null);
                    webSocket = webSocketContext.WebSocket;
                    AppendLog("有客户端连接");
                    await HandleWebSocketConnectionAsync(webSocket);
                }
                else
                {
                    context.Response.StatusCode = 400;
                    context.Response.Close();
                }
                
            }
        }

        #region Socket服务器

        //  private async UniTask ListenForClientsAsync(CancellationToken token)
        // {
        //     while (isServerRunning && !token.IsCancellationRequested)
        //     {
        //         try
        //         {
        //             TcpClient client = await tcpListener.AcceptTcpClientAsync();
        //             _ = HandleClientAsync(client, token);
        //         }
        //         catch (ObjectDisposedException)
        //         {
        //             // TcpListener 已被释放，安全退出
        //             LogText.Text += $"服务器正在关闭，监听器已被释放。\r\n";
        //             break;
        //         }
        //         catch (SocketException ex) when (ex.SocketErrorCode == SocketError.Interrupted)
        //         {
        //             LogText.Text += $"服务器正在关闭，停止监听...\r\n";
        //             break;
        //         }
        //         catch (Exception ex)
        //         {
        //             LogText.Text += $"监听客户端时发生错误: {ex.Message}\r\n";
        //             break;
        //         }
        //     }
        // }
        //
        //
        // private async UniTask HandleClientAsync(TcpClient client, CancellationToken token)
        // {
        //     using (NetworkStream stream = client.GetStream())
        //     {
        //         AppendLog("有客户端连接");
        //         byte[] buffer = _bufferPool.Rent(BufferSize);
        //         int bytesRead;
        //
        //         try
        //         {
        //             while (!token.IsCancellationRequested &&
        //                    (bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length, token)) > 0)
        //             {
        //                 if (bytesRead == 0)
        //                 {
        //                     // 客户端主动断开连接
        //                     AppendLog("客户端已断开连接。");
        //                     break;
        //                 }
        //                 string request = Encoding.UTF8.GetString(buffer, 0, bytesRead);
        //                 string response = ProcessLoginRequest(request);
        //                 byte[] responseBytes = Encoding.UTF8.GetBytes(response);
        //                 await stream.WriteAsync(responseBytes, 0, responseBytes.Length, token);
        //             }
        //         }
        //         catch (OperationCanceledException)
        //         {
        //             AppendLog("客户端处理被取消。");
        //         }
        //         catch (Exception ex)
        //         {
        //             AppendLog(ex.ToString());
        //         }
        //         finally
        //         {
        //             _bufferPool.Return(buffer);
        //             client.Close();
        //         }
        //     }
        // }
        //
        //
        //
        // private void StopServerLogin()
        // {
        //     if (isServerRunning)
        //     {
        //         isServerRunning = false;
        //         // 触发取消信号
        //         cts?.Cancel();
        //         tcpListener.Stop();
        //         AppendLog($"服务器已停止");
        //     }
        // }

        #endregion

        #region WebSocket服务器

        private async UniTask HandleWebSocketConnectionAsync(WebSocket webSocket)
        {
            byte[] buffer = _bufferPool.Rent(BufferSize);
            try
            {
                while (webSocket.State == WebSocketState.Open)
                {
                    WebSocketReceiveResult result =
                        await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                    if (result.MessageType == WebSocketMessageType.Close)
                    {
                        await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, string.Empty,
                            CancellationToken.None);
                        Console.WriteLine("客户端断开连接。");
                        break;
                    }

                    string receivedMessage = Encoding.UTF8.GetString(buffer, 0, result.Count);
                    Console.WriteLine($"收到消息：{receivedMessage}");

                    // 回复消息
                    string responseMessage = ProcessLoginRequest(receivedMessage);
                    byte[] responseBuffer = Encoding.UTF8.GetBytes(responseMessage);
                    await webSocket.SendAsync(new ArraySegment<byte>(responseBuffer), WebSocketMessageType.Text, true,
                        CancellationToken.None);
                }
            }
            catch (Exception e)
            {
                AppendLog($"发生错误：{e.Message}, 内部异常: {e.InnerException?.Message}");
            }finally
            {
                if (webSocket.State != WebSocketState.Closed)
                {
                    await webSocket.CloseAsync(WebSocketCloseStatus.InternalServerError, "Error", CancellationToken.None);
                    AppendLog("服务器已经关闭");
                }
                webSocket.Dispose();
            }

        }

        private void StopWebSocket(WebSocket webSocket)
        {
            if (webSocket.State != WebSocketState.Closed)
            {
                webSocket.CloseAsync(WebSocketCloseStatus.InternalServerError, "Error", CancellationToken.None);
                AppendLog("服务器已经关闭");
            }
            webSocket.Dispose();
            
        }

        #endregion

        private string ProcessLoginRequest(string request)
        {
            AppendLog(request);
        
            // 检查请求是否为空或空白
            if (string.IsNullOrWhiteSpace(request))
            {
                return "0"; // 请求为空
            }

            LoginInfo loginInfo=JsonConvert.DeserializeObject<LoginInfo>(request);
        
            // 查询用户
            var filter = Builders<BsonDocument>.Filter.Eq("username", loginInfo.UserName);
            var result = _loginCollection.Find(filter).ToList();
        
            // 如果查询结果为空，表示用户不存在
            if (result.Count == 0)
            {
                AppendLog($"用户不存在");
                return "0"; // 用户不存在
            }
        
            foreach (var document in result)
            {
                AppendLog($"{document.ToString()}");
            }
        
            // 验证密码
            if (result[0]["password"].ToString() == loginInfo.Password)
            {
                AppendLog($"登录成功");
                return "1"; // 登录成功
            }
            else
            {
                AppendLog($"密码错误");
                return "0"; // 密码错误
            }
        }
        
        private void StopServer_Click(object sender, EventArgs e)
        {
            // StopServerLogin();
            // StopWebSocket(webSocket);
        }
        
        private void AppendLog(string message)
        {
            if (LogText.InvokeRequired)
            {
                LogText.Invoke(new MethodInvoker(() => AppendLog(message)));
            }
            else
            {
                LogText.AppendText(message + "\r\n");
                LogText.SelectionStart = LogText.TextLength;
                LogText.ScrollToCaret();
            }
        }

        
    }
}