using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using StackExchange.Redis;

namespace RedisServer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        
        private Socket _serverSocket;

        #region 服务器

        /// <summary>
        /// 关闭服务器
        /// </summary>
        private void StopServerConnect()
        {
            if (_serverSocket != null)
            {
                _serverSocket.Close();
                _serverSocket = null;
                AddText("服务器已关闭");
            }
            else
            {
                AddText("服务器未启动");
            }
        }
        
        /// <summary>
        /// 启动服务器
        /// </summary>
        private void StartServerConnect()
        {
            _serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress ip = IPAddress.Parse(this.ip.Text);
            int port=int.Parse(this.port.Text);
            IPEndPoint endPoint = new IPEndPoint(ip, port);
            _serverSocket.Bind(endPoint);
            _serverSocket.Listen(10);
            AddText("服务器启动成功,等待连接。。。");
            _serverSocket.BeginAccept(AcceptClient, _serverSocket);
        }

        /// <summary>
        /// 接受客户端连接
        /// </summary>
        /// <param name="result"></param>
        private void AcceptClient(IAsyncResult result)
        {
            try
            {
                Socket clientSocket = _serverSocket.EndAccept(result);
                AddText("已连接: " + clientSocket.RemoteEndPoint);
                
                _serverSocket.BeginAccept(AcceptClient, _serverSocket);
                while (true)
                {
                    byte[] buffer = new byte[1024];
                    int bytesRead =clientSocket.Receive(buffer);
                    if (bytesRead == 0)
                    {
                        AddText("客户端已断开连接");
                        clientSocket.Close();
                        return;
                    }
                    string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    AddText("客户端消息: " + message);
                    LoginInfo loginInfo = JsonConvert.DeserializeObject<LoginInfo>(message);
                    CheckLoginType(loginInfo, clientSocket);
                }
            }
            catch (Exception e)
            {
                AddText("服务器异常: " + e.Message);
            }
        }

        private void CheckLoginType(LoginInfo loginInfo , Socket clientSocket)
        {
            switch (loginInfo.PackId)
            {
                case 1:AddText("客户端登入请求！");
                    if (SearchRedisData(loginInfo))
                    {
                        var response =  MessageManage.Instance.LoginSuccess(clientSocket, loginInfo);
                        AddText("发送消息: " + JsonConvert.SerializeObject(response));
                    }
                    else
                    {
                        var response = MessageManage.Instance.LoginFail(clientSocket, loginInfo);
                        AddText("发送消息: " + JsonConvert.SerializeObject(response));
                    }
                    break;
                case 2: AddText("客户端注册请求！");
                    if (InsertRedisData(loginInfo))
                    {
                        var response = MessageManage.Instance.RegisterSuccess(clientSocket, loginInfo);
                        AddText("发送消息: " + JsonConvert.SerializeObject(response));
                    }
                    else
                    {
                        var response = MessageManage.Instance.RegisterFail(clientSocket, loginInfo);
                        AddText("发送消息: " + JsonConvert.SerializeObject(response));
                    }
                    break;
                case 3: AddText("客户端修改密码请求！");
                    if (ModifyRedisData(loginInfo))
                    {
                        var response = MessageManage.Instance.ModifySuccess(clientSocket, loginInfo);
                        AddText("发送消息: " + JsonConvert.SerializeObject(response));
                    }
                    else
                    {
                        var response = MessageManage.Instance.ModifyFail(clientSocket, loginInfo);
                        AddText("发送消息: " + JsonConvert.SerializeObject(response));
                    }
                    break;
            }
        }
        
        #endregion
        
        
        private ConnectionMultiplexer _redisConnection;
        private IDatabase _redisDatabase;
        private ISubscriber _subscriber;
        #region Redis测试
        
        /// <summary>
        /// 连接 Redis
        /// </summary>
        private void ConnectRedis()
        {
            string redisConnectionString = redisip.Text;
            try
            {
                _redisConnection = ConnectionMultiplexer.Connect(redisConnectionString);
                _redisDatabase = _redisConnection.GetDatabase();
                _subscriber = _redisConnection.GetSubscriber();
                AddText("Redis 连接成功");
            }
            catch (Exception e)
            {
                AddText("Redis 连接失败: " + e.Message);
            }
        }

        /// <summary>
        /// 断开连接 Redis
        /// </summary>
        private void StopConnectRedis()
        {
            if (_redisConnection != null)
            {
                _redisConnection.Close();
                _redisConnection = null;
                _redisDatabase = null;
                _subscriber = null;
                AddText("Redis 已断开");
            }
        }
        
        /// <summary>
        /// 搜索 Redis 数据
        /// </summary>
        /// <param name="key"></param>
        private bool SearchRedisData(LoginInfo loginInfo)
        {
            if (_redisDatabase == null)
            {
                AddText("请先连接 Redis");
                return false;
            }
            try
            {
                if (_redisDatabase.KeyExists(loginInfo.UserName))
                {
                    RedisType type = _redisDatabase.KeyType(loginInfo.UserName);
                    switch (type)
                    {
                        case RedisType.String:
                            string value = _redisDatabase.StringGet(loginInfo.UserName);
                            AddText($"[String] {loginInfo.UserName} = {value}");
                            return loginInfo.Password == value;
                            break;
                        case RedisType.Hash:
                            var hash=_redisDatabase.HashGetAll(loginInfo.UserName);
                            foreach (var item in hash)
                            {
                                AddText($"[Hash]{loginInfo.UserName} {item.Name} = {item.Value}");
                            }
                            break;
                        case RedisType.List:
                            var list = _redisDatabase.ListRange(loginInfo.UserName);
                            foreach (var item in list)
                            {
                                AddText($"[List]{loginInfo.UserName} {item}");
                            }
                            break;
                        case RedisType.Set:
                            var set = _redisDatabase.SetMembers(loginInfo.UserName);
                            foreach (var item in set)
                            {
                                AddText($"[Set]{loginInfo.UserName} {item}");
                            }
                            break;
                        case RedisType.SortedSet:
                            var sortedSet = _redisDatabase.SortedSetRangeByRank(loginInfo.UserName);
                            for (int i = 0; i < sortedSet.Length; i++)
                            {
                                AddText($"[SortedSet] {loginInfo.UserName}[{i}] = {sortedSet[i]} | Score: {sortedSet[i]}");
                            }
                            break;
                    }
                    return false;
                }
                else
                {
                    AddText($"[Redis] {loginInfo.UserName} 不存在");
                    return false;
                }
               
            }
            catch (Exception e)
            {
                AddText("Redis 查询失败: " + e.Message);
            }
            return false;
        }

        /// <summary>
        /// 添加Redis数据
        /// </summary>
        /// <param name="loginInfo"></param>
        private bool InsertRedisData(LoginInfo loginInfo)
        {
            
            if (_redisDatabase.KeyExists(loginInfo.UserName))
            {
                AddText("Redis 数据已存在");
                return false;
            }
            else
            {
                _redisDatabase.StringSet(loginInfo.UserName, loginInfo.Password);
                AddText("Redis 数据添加成功");
                return true;
            }
        }

        /// <summary>
        /// 修改Redis数据
        /// </summary>
        private bool ModifyRedisData(LoginInfo loginInfo)
        {
            if (_redisDatabase.KeyExists(loginInfo.UserName))
            {
                _redisDatabase.StringSet(loginInfo.UserName, loginInfo.Password);
                AddText("Redis 数据修改成功");
                return true;
            }
            else
            {
                AddText("Redis 数据不存在");
                return false;
            }
        }

        /// <summary>
        /// 取消订阅所有频道
        /// </summary>
        private void Unsubscribe()
        {
            if (_subscriber != null)
            {
                _subscriber.UnsubscribeAll();
                AddText("[PubSub] 已取消订阅所有频道");
            }
        }
        
        /// <summary>
        /// 取消订阅
        /// </summary>
        /// <param name="channelName"></param>
        private void Unsubscribe(string channelName)
        {
            if (_subscriber != null)
            {
                _subscriber.Unsubscribe(channelName);
                AddText($"[PubSub] 频道 {channelName} 已取消订阅");
            }
        }
        
        /// <summary>
        /// 搜索 HyperLogLog 数据
        /// </summary>
        /// <param name="key"></param>
        private void SearchRedisHylogData(string key)
        {
            if (_redisDatabase == null)
            {
                AddText("请先连接 Redis");
                return;
            }
            try
            {
                var value = _redisDatabase.HyperLogLogLength(key);
                AddText($"[HyperLogLog] {key} 的唯一元素数量 = {value}");
            }
            catch  (Exception e)
            {
                AddText("Redis 搜索失败: " + e.Message);
            }
        }
        
        /// <summary>
        /// 搜索 Bitmap 数据
        /// </summary>
        /// <param name="key"></param>
        /// <param name="offset"></param>
        private void SearchRedisBitData(string key, long offset)
        {
            if (_redisDatabase == null)
            {
                AddText("请先连接 Redis");
                return;
            }

            try
            {
                var value = _redisDatabase.StringGetBit(key,offset);
                AddText($"[Bitmap] {key} 偏移量 [{offset}] 的值为: {(value ? 1 : 0)}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        
        /// <summary>
        /// 搜索 Geospatial 数据
        /// </summary>
        /// <param name="key"></param>
        private void SearchRedisEgoData(string key)
        {
            if (_redisDatabase == null)
            {
                AddText("请先连接 Redis");
                return;
            }
            try
            {
                var value = _redisDatabase.SortedSetRangeByRank(key);
                if (value.Length == 0)
                {
                    AddText($"[Geospatial] {key}  中无地理数据");
                    return;
                }
                foreach (var item in value)
                {
                    var position=_redisDatabase.GeoPosition(key,item);
                    if (position.HasValue)
                    {
                        AddText($"[Geospatial] 地点: {item} | 经纬度: {position.Value.Longitude},{position.Value.Latitude}");
                    }
                }
            }
            catch (Exception e)
            {
                AddText("Redis 搜索失败: " + e.Message);
            }
        }

        /// <summary>
        /// 订阅频道
        /// </summary>
        /// <param name="channelName"></param>
        private void SubscribeToChannel(string channelName)
        {
            if (_subscriber==null)
            {
                AddText("请先连接 Redis");
                return;
            }

            try
            {
                _subscriber.Subscribe(channelName, (channel, message) =>
                {
                    AddText($"[PubSub] 收到消息 | 频道: {channel} | {message}");
                });
                AddText($"[PubSub] 订阅成功 | 频道: {channelName}");
            }
            catch (Exception e)
            {
                AddText("Redis 订阅失败: " + e.Message);
            }
        }

        /// <summary>
        /// 发布消息redis
        /// </summary>
        /// <param name="channelName"></param>
        /// <param name="message"></param>
        private void PublishMessageToChannel(string channelName, string message)
        {
            if (_redisDatabase == null)
            {
                AddText("请先连接 Redis");
                return;
            }
            try
            {
                _redisDatabase.Publish(channelName, message);
                AddText($"[PubSub] 发布消息成功 | 频道: {channelName} | {message}");
            }
            catch (Exception e)
            {
                AddText("Redis 发布消息失败: " + e.Message);
            }
        }
        
        #endregion

        private IConnection _rabbitMQConnection;
        private IModel _rabbitMQChannel;
        private EventingBasicConsumer _currentConsumer;

        #region RabbitMQ测试

        private void ConnectRabbitMQ()
        {
            try
            {
                var factory = new ConnectionFactory()
                {
                    HostName = HostName.Text,
                    UserName = UserName.Text,
                    Password = PassWord.Text,
                    Port =int.Parse(rabbitmqport.Text),
                };
                _rabbitMQConnection =  factory.CreateConnection();
                _rabbitMQChannel =  _rabbitMQConnection.CreateModel();

                AddTextR("[RabbitMQ] 连接成功");
            }
            catch (Exception ex)
            {
                AddTextR("[RabbitMQ] 连接失败: " + ex.Message);
            }
        }

        private void PublishRabbitMQMessage(string queueName, string message)
        {
            if (_rabbitMQChannel == null || !_rabbitMQChannel.IsOpen)
            {
                AddTextR("请先连接 RabbitMQ");
                return;
            }
            if (string.IsNullOrWhiteSpace(queueName))
            {
                AddTextR("[RabbitMQ] 队列名称不能为空");
                return;
            }
            try
            {
                // _rabbitMQChannel.QueueDeclare(queueName, false, false,false,null);
                _rabbitMQChannel.ExchangeDeclare("logs", ExchangeType.Fanout, false);
                var body=Encoding.UTF8.GetBytes(message);
                 _rabbitMQChannel.BasicPublish("logs",  "", null, body);
                AddTextR($"[RabbitMQ] 发送消息成功 | 队列: {queueName} | {message}");
            }
            catch (Exception e)
            {
                AddTextR("RabbitMQ 发送消息失败: " + e.Message);
            }
        }
        
        private void SubscribeToRabbitMQQueue(string queueName)
        {
            if (_rabbitMQChannel == null || !_rabbitMQChannel.IsOpen)
            {
                AddTextR("[RabbitMQ] 请先连接 RabbitMQ");
                return;
            }

            try
            {
                // _rabbitMQChannel.QueueDeclare(queue: queueName,
                //     durable: false,
                //     exclusive: false,
                //     autoDelete: false,
                //     arguments: null);
                // 如果已有消费者，先取消订阅
                if (_currentConsumer != null)
                {
                    _rabbitMQChannel.BasicCancel(_currentConsumer.ConsumerTag);
                    // _currentConsumer.Received -= ConsumerReceived; // 避免重复绑定
                }
                _rabbitMQChannel.ExchangeDeclare("logs", ExchangeType.Fanout, false);
                _rabbitMQChannel.QueueBind(queueName, "logs", "", null);
                _currentConsumer = new EventingBasicConsumer(_rabbitMQChannel);
                _currentConsumer.Received += ConsumerReceived;

                _rabbitMQChannel.BasicConsume(queue: queueName,noAck:  true,
                    consumer: _currentConsumer);
                AddTextR($"[RabbitMQ] 订阅成功 | 队列: {queueName}");
            }
            catch (Exception ex)
            {
                AddTextR("[RabbitMQ] 订阅失败: " + ex.Message);
            }
        }

        private void ConsumerReceived(object model, BasicDeliverEventArgs ea)
        {
            var body = ea.Body.ToArray();
            var message = System.Text.Encoding.UTF8.GetString(body);
            AddTextR($"[RabbitMQ] 收到消息 | 队列: {ea.RoutingKey} | 内容: {message}");
        }


        private void StopRabbitMQ()
        {
            try
            {
                if (_rabbitMQConnection != null && _rabbitMQConnection.IsOpen)
                {
                    _rabbitMQConnection.Close();
                    _rabbitMQChannel.Close();
                    _rabbitMQConnection = null;
                    _rabbitMQChannel = null;
                    _currentConsumer = null;
                    AddTextR("[RabbitMQ] 已断开连接");
                }
            }
            catch (Exception ex)
            {
                AddTextR("[RabbitMQ] 断开连接失败: " + ex.Message);
            }
        }
        
        #endregion
        
        /// <summary>
        /// 添加文本
        /// </summary>
        /// <param name="text"></param>
        private void AddTextR(string text)
        {
            if (rabbitmqtext.InvokeRequired)
            {
                rabbitmqtext.Invoke(new Action<string>(AddTextR), text);
                // rabbitmqtext.Invoke(new MethodInvoker(() =>
                // {
                //     rabbitmqtext.AppendText(text +"非主线程调用！" + "\r\n");
                // }));
            }
            else
            {
                rabbitmqtext.AppendText(text + "\r\n");
                rabbitmqtext.ScrollToCaret(); // 这里必须也在 UI 线程执行
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

        private void StartServer_Click(object sender, EventArgs e)
        {
            StartServerConnect();
        }
        
        private void connect_Click(object sender, EventArgs e)
        {
            ConnectRedis();
        }

        private void selectdb_Click(object sender, EventArgs e)
        {
            //RedisType已有类型 string hash set sortedset(zset) list
            // SearchRedisData(searsh1.Text);
            
            //HyperLogLog类型
            // SearchRedisHylogData(searsh1.Text);
            
            //Bitmap类型
            // var array=searsh1.Text.Split(',');
            // long.TryParse(array[1],out long offset);
            // SearchRedisBitData(array[0],offset);
            
            //Geospatial类型
            // SearchRedisEgoData(searsh1.Text);
        }

        private void stopserver_Click(object sender, EventArgs e)
        {
            StopServerConnect();
        }

        private void sub_Click(object sender, EventArgs e)
        {
            SubscribeToChannel(subtext.Text);
        }

        private void stopredis_Click(object sender, EventArgs e)
        {
            StopConnectRedis();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Unsubscribe();
        }

        private void publish_Click(object sender, EventArgs e)
        {
            PublishMessageToChannel(channel.Text,  message.Text);
        }

        /// <summary>
        /// 取消指定订阅按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            Unsubscribe(pingdao2.Text);
        }

        private void connectrabbitmq_Click(object sender, EventArgs e)
        {
            ConnectRabbitMQ();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(queuename.Text) || String.IsNullOrEmpty(rabbitmessage.Text))
            {
                AddText("请输入队列名称和消息内容,不可为空！");
                return;
            }
            PublishRabbitMQMessage(queuename.Text, rabbitmessage.Text);
        }

        private void stoprabbitmq_Click(object sender, EventArgs e)
        {
            StopRabbitMQ();
        }

        private void rabbitmqsub_Click(object sender, EventArgs e)
        {
            SubscribeToRabbitMQQueue(xqueuename.Text);
        }
        
    }
}