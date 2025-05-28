using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using TEngine;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoginUIClient : MonoBehaviour
{
    [SerializeField]
    private InputField _inputAccount;
    [SerializeField]
    private InputField _inputPassword;
    [SerializeField]
    private Button _btnLogin;
    [SerializeField]
    private Button _btnRegistration;
    [SerializeField]
    private Text _textLogin;
    private bool canLogin;
    string serverIp = "127.0.0.1"; // 服务器 IP 地址
    int port = 8017;             // 服务器监听端口
    private TcpClient _client = null;
    private NetworkStream _stream = null;
    bool isConnected = false;
    private CancellationTokenSource cts = null;
    
    void Start()
    {
        _btnLogin.onClick.AddListener(OnClickLoginBtn);
        _btnRegistration.onClick.AddListener(OnClickRegistration);
    }

    private void OnClickRegistration()
    {
        
    }

    private void OnClickLoginBtn()
    {
        try
        {
            if (_client != null && _client.Connected)
            {
                // 已连接则先断开旧连接
                CloseClient().Forget();
            }

            _client = new TcpClient();
            _client.Connect(serverIp, port);
            _stream = _client.GetStream();
            isConnected = true;
            cts = new CancellationTokenSource();

            UniTask.RunOnThreadPool(async () => await ReceiveMessages(cts.Token), cancellationToken: cts.Token);
        }
        catch (Exception e)
        {
            Log.Info(e);
        }

        SendLoginMessage();
    }


    void Update()
    {
        if (canLogin)
        {
            // cts?.Cancel();
            // this.Close();
            // CloseClient();
            // GameModule.Scene.LoadScene("Game");
            // GameModule.UI.ShowUIAsync<HsTestUI>();
            // SceneManager.LoadScene("Game");
        }
    }

    async void SendLoginMessage()
    {
        if (_client != null && _client.Connected)
        {
            NetworkStream stream = _client.GetStream();
            
            LoginInfo loginInfo = new LoginInfo
            {
                PackId = 1,
                UserName = _inputAccount.text,
                Password = _inputPassword.text
            };
            
            string message = JsonConvert.SerializeObject(loginInfo); // 获取用户输入的消息
            
            byte[] data = Encoding.UTF8.GetBytes(message); // 将字符串转换为字节数组

            stream.Write(data, 0, data.Length); // 发送数据到服务器

            Log.Info("消息已发送！");
                
        }
        else
        {
            Log.Info("当前未连接到服务器！");
        }
    }
    
    private async UniTask ReceiveMessages(CancellationToken token)
    {
        byte[] buffer = new byte[1024];
        int bytesRead;

        while (isConnected && _stream != null && _stream.CanRead && !token.IsCancellationRequested)
        {
            try
            {
                if (_stream.DataAvailable)
                {
                    bytesRead = _stream.Read(buffer, 0, buffer.Length);

                    if (bytesRead == 0)
                    {
                        Log.Info("服务器已断开连接");
                        break;
                    }

                    string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    Log.Info("收到消息：" + message);

                    if (message.Equals("1"))
                    {
                        // canLogin = true;
                        _textLogin.text = "1";
                        Log.Info("登入成功！");
                    }
                    else
                    {
                        Log.Info("账号或密码错误,登录失败");
                    }
                }
                else
                {
                    await UniTask.Delay(50, cancellationToken: token); // 避免 CPU 空转
                }
            }
            catch (IOException ex)
            {
                if (ex.InnerException is SocketException se &&
                    (se.ErrorCode == 10004 || se.ErrorCode == 10054)) // 被中断或连接重置
                {
                    Log.Info("检测到连接中断或关闭");
                }
                else
                {
                    Log.Error("接收消息出错：" + ex.Message);
                }
                break;
            }
            catch (OperationCanceledException)
            {
                Log.Info("接收任务已取消");
                break;
            }
            catch (Exception ex)
            {
                Log.Error("接收消息出错：" + ex.Message);
                break;
            }
        }
    }

    private void OnDisable()
    {
        CloseClient().Forget();
    }

    private async UniTask CloseClient()
    {
        isConnected = false;

        cts?.Cancel();
        cts?.Dispose();
        cts = null;

        if (_client != null && _client.Connected)
        {
            _stream?.Close();
            _client.Close(); // 关闭连接
            Log.Info("已成功断开与服务器的连接！");
        }
        else
        {
            Log.Info("当前未连接到服务器！");
        }
    }
    
    public class LoginInfo
    {
        public int PackId;
        public string  UserName;
        public string  Password;
    }
}
