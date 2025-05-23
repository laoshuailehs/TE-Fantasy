// using System.Collections;
// using System.Collections.Generic;
// using GameLogic;
// using MongoDB.Bson;
// using MongoDB.Driver;
// using TEngine;
// using UnityEngine;
//
// namespace MongoDBDemo
// {
//     public class MongoDBDemo : Singleton<MongoDBDemo>
//     {
//         private IMongoDatabase datebase;
//         private IMongoCollection<BsonDocument> collection;
//
//         private string connectionString = "mongodb://localhost:27017";
//         private string databaseName = "hsgamedb";
//         private string collectionName = "gameLogin";
//         
//         public void StartConnect()
//         {
//             MongoClient client = new MongoClient(connectionString);
//             datebase = client.GetDatabase(databaseName);
//             collection = datebase.GetCollection<BsonDocument>(collectionName);
//             
//         }
//
//         public void QueryData(string name ,int password)
//         {
//             var documents = collection.Find(new BsonDocument()).ToList();
//             Log.Debug("QueryData");
//             foreach (var document in documents)
//             {
//                 Debug.Log(document.ToString());
//             }
//         }
//
//         void Update()
//         {
//         
//         }
//     }
// }