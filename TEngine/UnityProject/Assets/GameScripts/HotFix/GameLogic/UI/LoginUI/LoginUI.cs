using System;
// using MongoDB.Bson;
// using MongoDB.Driver;
using UnityEngine;
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
        protected override void ScriptGenerator()
        {
            _inputAccount = FindChildComponent<InputField>("m_inputAccount");
            _inputPassword = FindChildComponent<InputField>("m_inputPassword");
            _btnLogin = FindChildComponent<Button>("m_btnLogin");
            _btnRegistration = FindChildComponent<Button>("m_btnRegistration");
            _btnLogin.onClick.AddListener(OnClickLoginBtn);
            _btnRegistration.onClick.AddListener(OnClickRegistration);
        }
        #endregion
        
        
        
        protected override void OnCreate()
        {
            base.OnCreate();
        }

        #region 事件
        private void OnClickLoginBtn()
        {
            //读取luban表数据
            // foreach (var item in ConfigSystem.Instance.Tables.Tb.DataMap)
            // {
            //     if (item.Value.Id.ToString() == _inputAccount.text && item.Value.Name == _inputPassword.text)
            //     {
            //         GameModule.Scene.LoadScene("hs");
            //         GameModule.UI.ShowUIAsync<HsTestUI>();
            //         this.Close();
            //     }
            // }
            
            //读取mysql数据库 未知报错 没解决(可能的解决方法，下载低版本mysql,懒得试了out！) KeyNotFoundException: The given key '29544' was not present in the dictionary.
            
            //读取mongoDB数据库 
            // StartConnect();
            // QueryData(_inputAccount.text, _inputPassword.text);
            
            //发送服务器请求，服务器验证登入信息
            
            GameModule.Scene.LoadScene("Game");
            GameModule.UI.ShowUIAsync<HsTestUI>();
            this.Close();
            
        }

        private void OnClickRegistration()
        {
            //读取mongoDB数据库 
            // StartConnect();
            // InsertData(_inputAccount.text, _inputPassword.text);
            
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
        
        protected override void Close()
        {
            base.Close();
        }
    }
}