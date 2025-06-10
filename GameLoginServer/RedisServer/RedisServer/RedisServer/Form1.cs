using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using StackExchange.Redis;

namespace RedisServer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        
        private Socket serverSocket;
        private ConnectionMultiplexer redisConnection;
        private IDatabase redisDatabase;

        private void ConnectRedis()
        {
            string redisConnectionString = "127.0.0.1:6379";
            try
            {
                redisConnection = ConnectionMultiplexer.Connect(redisConnectionString);
                redisDatabase = redisConnection.GetDatabase();
                AddText("Redis 连接成功");
            }
            catch (Exception e)
            {
                AddText("Redis 连接失败: " + e.Message);
            }
        }

        private void SearchRedisData(string key)
        {
            if (redisDatabase == null)
            {
                AddText("请先连接 Redis");
                return;
            }
            try
            {
                RedisType type = redisDatabase.KeyType(key);
                switch (type)
                {
                    case RedisType.String:
                        string value = redisDatabase.StringGet(key);
                        AddText($"[String] {key} = {value}");
                        break;
                    case RedisType.Hash:
                        var hash=redisDatabase.HashGetAll(key);
                        foreach (var item in hash)
                        {
                            AddText($"[Hash]{key} {item.Name} = {item.Value}");
                        }
                        break;
                    case RedisType.List:
                        var list = redisDatabase.ListRange(key);
                        foreach (var item in list)
                        {
                            AddText($"[List]{key} {item}");
                        }
                        break;
                    case RedisType.Set:
                        var set = redisDatabase.SetMembers(key);
                        foreach (var item in set)
                        {
                            AddText($"[Set]{key} {item}");
                        }
                        break;
                    case RedisType.SortedSet:
                        var sortedSet = redisDatabase.SortedSetRangeByRank(key);
                        for (int i = 0; i < sortedSet.Length; i++)
                        {
                            AddText($"[SortedSet] {key}[{i}] = {sortedSet[i]} | Score: {sortedSet[i]}");
                        }
                        break;
                }
            }
            catch (Exception e)
            {
                AddText("Redis 查询失败: " + e.Message);
            }
            
        }
        
        private void SearchRedisHylogData(string key)
        {
            if (redisDatabase == null)
            {
                AddText("请先连接 Redis");
                return;
            }
            try
            {
                var value = redisDatabase.HyperLogLogLength(key);
                AddText($"[HyperLogLog] {key} 的唯一元素数量 = {value}");
            }
            catch  (Exception e)
            {
                AddText("Redis 搜索失败: " + e.Message);
            }
        }
        private void SearchRedisBitData(string key, long offset)
        {
            if (redisDatabase == null)
            {
                AddText("请先连接 Redis");
                return;
            }

            try
            {
                var value = redisDatabase.StringGetBit(key,offset);
                AddText($"[Bitmap] {key} 偏移量 [{offset}] 的值为: {(value ? 1 : 0)}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        private void SearchRedisEgoData(string key)
        {
            if (redisDatabase == null)
            {
                AddText("请先连接 Redis");
                return;
            }
            try
            {
                var value = redisDatabase.SortedSetRangeByRank(key);
                if (value.Length == 0)
                {
                    AddText($"[Geospatial] {key}  中无地理数据");
                    return;
                }
                foreach (var item in value)
                {
                    var position=redisDatabase.GeoPosition(key,item);
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
        
        private void StopServerConnect()
        {
            if (serverSocket != null)
            {
                serverSocket.Close();
                serverSocket = null;
                AddText("服务器已关闭");
            }
        }
        
        private void StartServerConnect()
        {
            serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress ip = IPAddress.Parse(this.ip.Text);
            int port=int.Parse(this.port.Text);
            IPEndPoint endPoint = new IPEndPoint(ip, port);
            serverSocket.Bind(endPoint);
            serverSocket.Listen(10);
            AddText("服务器启动成功,等待连接。。。");
            serverSocket.BeginAccept(AcceptClient, serverSocket);
        }

        private void AcceptClient(IAsyncResult result)
        {
            
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
            SearchRedisData(searsh1.Text);
            
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
        
    }
}