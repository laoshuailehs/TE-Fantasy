
using System.Net.Sockets;
using System.Text;
using Newtonsoft.Json;

namespace RedisServer
{
    public class MessageManage:LSingleInstance<MessageManage>
    {
        
        public Response LoginSuccess(Socket  clientSocket, LoginInfo loginInfo)
        {
            Response response = new Response()
            {
                PackId = 1,
                Result = 1,
                Description = "登入成功",
                UserName = loginInfo.UserName,
                Password = loginInfo.Password
            };
            clientSocket.Send(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(response)));
            return response;
        }
        public Response LoginFail(Socket  clientSocket, LoginInfo loginInfo)
        {
            Response response = new Response()
            {
                PackId = 1,
                Result = -1,
                Description = "登入失败",
                UserName = loginInfo.UserName,
                Password = loginInfo.Password
            };
            clientSocket.Send(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(response)));
            return response;
        }
        
        public Response RegisterSuccess(Socket  clientSocket, LoginInfo loginInfo)
        {
            Response response = new Response()
            {
                PackId = 2,
                Result = 1,
                Description = "注册成功",
                UserName = loginInfo.UserName,
                Password = loginInfo.Password
            };
            clientSocket.Send(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(response)));
            return response;
        }

        public Response RegisterFail(Socket clientSocket, LoginInfo loginInfo)
        {
            Response response = new Response()
            {
                PackId = 2,
                Result = -1,
                Description = "注册失败,该用户已存在！",
                UserName = loginInfo.UserName,
                Password = loginInfo.Password
            };
            clientSocket.Send(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(response)));
            return response;
        }

        public Response ModifySuccess(Socket clientSocket, LoginInfo loginInfo)
        {
            Response response = new Response()
            {
                PackId = 3,
                Result = 1,
                Description = "修改成功",
                UserName = loginInfo.UserName,
                Password = loginInfo.Password
            };
            clientSocket.Send(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(response)));
            return response;
        }
        public Response ModifyFail(Socket clientSocket, LoginInfo loginInfo)
        {
            Response response = new Response()
            {
                PackId = 3,
                Result = -1,
                Description = "修改失败,该用户已存在！",
                UserName = loginInfo.UserName,
                Password = loginInfo.Password
            };
            clientSocket.Send(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(response)));
            return response;
        }
        
        
    }
}