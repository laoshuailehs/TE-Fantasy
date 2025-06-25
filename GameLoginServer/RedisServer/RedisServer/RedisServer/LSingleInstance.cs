using System;

namespace RedisServer
{
    public interface ISingleInstance
    {
    }

    
    public abstract class SingleInstance : ISingleInstance
    {
        public abstract void OnNew();
    }
    
    
    /// <summary>
    /// 懒汉式单例
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class LSingleInstance<T> : SingleInstance where T : SingleInstance, new()
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    try
                    {
                        _instance = new T();
                        _instance.OnNew();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("create Instance error:" + e);
                    }
                }

                return _instance;
            }
        }

        public override void OnNew()
        {
        }
    }

    /// <summary>
    /// 饿汉式单例
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class ESingleInstance<T> : ISingleInstance where T : ISingleInstance, new()
    {
        private static T _instance = new T();

        public static T Instance => _instance;
    }
}