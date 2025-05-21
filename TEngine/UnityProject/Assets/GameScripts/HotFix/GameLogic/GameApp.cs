using System.Collections.Generic;
using System.IO;
using System.Reflection;
using GameConfig;
using GameConfig.ybtest;
using GameLogic;
using TEngine;
using TEngine.Localization.SimpleJSON;
using UnityEngine;

#pragma warning disable CS0436


/// <summary>
/// 游戏App。
/// </summary>
public partial class GameApp
{
    private static List<Assembly> _hotfixAssembly;

    /// <summary>
    /// 热更域App主入口。
    /// </summary>
    /// <param name="objects"></param>
    public static void Entrance(object[] objects)
    {
        GameEventHelper.Init();
        _hotfixAssembly = (List<Assembly>)objects[0];
        Log.Warning("======= 看到此条日志代表你成功运行了热更新代码 =======");
        Log.Warning("======= Entrance GameApp =======");
        Log.Warning("======= Entrance GameApp2 =======");
        Utility.Unity.AddDestroyListener(Release);
        StartGameLogic();
    }
   
    private static void StartGameLogic()
    {
        GameEvent.Get<ILoginUI>().ShowLoginUI();
        ConfigSystem.Instance.Load();
        foreach (var item in ConfigSystem.Instance.Tables.Tb.DataMap)
        {
            Log.Info(item.Value.Id + " |" + item.Value.Desc);
        }

        foreach (var item in ConfigSystem.Instance.Tables.TbItem.DataMap)
        {
            Log.Debug(item.Value.Id + " |" + item.Value.Name+ " |" +item.Value.Desc);
        }
        GameModule.UI.ShowUIAsync<LoginUI>();
    }
    
    private static void Release()
    {
        SingletonSystem.Release();
        Log.Warning("======= Release GameApp =======");
    }
}