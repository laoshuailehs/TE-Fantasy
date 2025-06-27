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
    private InputField _inputIpConfig;
    [SerializeField]
    private InputField _inputPassword;
    [SerializeField]
    private Button _btnLogin;
    [SerializeField]
    private Button _btnRegistration;
    [SerializeField]
    private Button _btnUpdate;
    [SerializeField]
    private Text _textLogin;
    private bool canLogin;
    public string serverIp = "192.168.0.127"; // 服务器 IP 地址
    public int port = 8019;             // 服务器监听端口
    private TcpClient _client = null;
    private NetworkStream _stream = null;
    bool isConnected = false;
    private CancellationTokenSource cts = null;
    
    void Start()
    {
        _btnLogin.onClick.AddListener(OnClickLoginBtn);
        _btnRegistration.onClick.AddListener(OnClickRegistration);
        _btnUpdate.onClick.AddListener(OnClickUpdateBtn);
        _inputIpConfig.text = serverIp;
    }

    private void OnClickRegistration()
    {
        ConnectServer();
        SendRegistrationMessage();
    }
    
    private void OnClickLoginBtn()
    {
        _textLogin.gameObject.SetActive(true);
        // ConnectServer();
        // SendLoginMessage();
    }

    private void OnClickUpdateBtn()
    {
        ConnectServer();
        SendUpdateMessage();
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

    private void ConnectServer()
    {
        try
        {
            if (_client != null && _client.Connected)
            {
                // 已连接则先断开旧连接
                CloseClient().Forget();
            }

            _client = new TcpClient();
            _client.Connect(_inputIpConfig.text, port);
            _stream = _client.GetStream();
            isConnected = true;
            cts = new CancellationTokenSource();

            UniTask.RunOnThreadPool(async () => await ReceiveMessages(cts.Token), cancellationToken: cts.Token);
        }
        catch (Exception e)
        {
            Log.Info(e);
        }
    }
    
    private void SendLoginMessage()
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
    
    private void SendRegistrationMessage()
    {
        if (_client != null && _client.Connected)
        {
            NetworkStream stream = _client.GetStream();
            
            LoginInfo loginInfo = new LoginInfo
            {
                PackId = 2,
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
    
    private void SendUpdateMessage()
    {
        if (_client != null && _client.Connected)
        {
            NetworkStream stream = _client.GetStream();
            
            LoginInfo loginInfo = new LoginInfo
            {
                PackId = 3,
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
                    bytesRead = await _stream.ReadAsync(buffer, 0, buffer.Length, token);

                    if (bytesRead == 0)
                    {
                        Log.Info("服务器已断开连接");
                        break;
                    }

                    string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    Log.Info("收到消息：" + message);
                    ProcessingRes(message);
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

    /// <summary>
    /// 处理服务器返回的消息
    /// </summary>
    /// <param name="message"></param>
    private void ProcessingRes(string message)
    {
        try
        {
            Response response = JsonConvert.DeserializeObject<Response>(message);
            switch (response.PackId)
            {
                case 1:
                {
                    if (response.Result == 1)
                    {
                        _textLogin.gameObject.SetActive(true);
                        Log.Info("登入成功！");
                    }
                    else
                    {
                        Log.Info($"{response.Description},登录失败");
                    }
                }break;
                case 2:
                {
                    if (response.Result == 1)
                    {
                        Log.Info("注册成功！");
                    }
                    else
                    {
                        Log.Info($"{response.Description},注册失败");
                    }
                }break;
                case 3:
                {
                    if (response.Result == 1)
                    {
                        Log.Info("更新成功！");
                    }
                    else
                    {
                        Log.Info($"{response.Description},更新失败");
                    }
                }break;
            }
                        
        }
        catch (Exception e)
        {
            Log.Info(message);
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
    
    public class Response
    {
        public int PackId;
        public int  Result;
        public string Description;
        public string  UserName;
        public string  Password;
    }
}
