using StackExchange.Redis;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace Mir.WorkServer.Extension.Redis
{
    /// <summary>
    /// ConnectionMultiplexer对象管理帮助类
    /// </summary>
    public class RedisConnectionHelp
    {
        /// <summary>
        /// 系统自定义Key前缀
        /// </summary>
        public readonly string SysCustomKey = "";
        private string _redisConnectionString = "127.0.0.1:6379,abortConnect=false";
        private readonly object Locker = new object();
        private ConnectionMultiplexer _instance;
        private readonly ConcurrentDictionary<string, ConnectionMultiplexer> ConnectionCache = new ConcurrentDictionary<string, ConnectionMultiplexer>();

        /// <summary>
        /// 单例获取
        /// </summary>
        public ConnectionMultiplexer Instance
        {
            get
            {
                if (_instance == null || !_instance.IsConnected)
                {
                    _instance = GetManager();
                }
                return _instance;
            }
        }

        /// <summary>
        /// Redis链接字符串
        /// </summary>
        public string RedisConnectionString { get => _redisConnectionString; private set => _redisConnectionString = value; }

        /// <summary>
        /// Redis 初始化
        /// </summary>
        /// <param name="redisConnectionString">Redis链接字符串</param>
        public void Init(string redisConnectionString)
        {
            RedisConnectionString = redisConnectionString;
        }

        /// <summary>
        /// 缓存获取
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public ConnectionMultiplexer GetConnectionMultiplexer(string connectionString)
        {
            if (!ConnectionCache.ContainsKey(connectionString))
            {
                ConnectionCache[connectionString] = GetManager(connectionString);
            }
            return ConnectionCache[connectionString];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        private ConnectionMultiplexer GetManager(string connectionString = null)
        {
            connectionString = connectionString ?? RedisConnectionString;
            var connect = ConnectionMultiplexer.Connect(connectionString);

            //注册如下事件
            connect.ConnectionFailed += MuxerConnectionFailed;
            //connect.ConnectionRestored += MuxerConnectionRestored;
            //connect.ErrorMessage += MuxerErrorMessage;
            //connect.ConfigurationChanged += MuxerConfigurationChanged;
            //connect.HashSlotMoved += MuxerHashSlotMoved;
            connect.InternalError += MuxerInternalError;

            return connect;
        }

        #region 事件

        ///// <summary>
        ///// 配置更改时
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void MuxerConfigurationChanged(object sender, EndPointEventArgs e)
        //{
        //    Console.WriteLine("Configuration changed: " + e.EndPoint);
        //}

        ///// <summary>
        ///// 发生错误时
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void MuxerErrorMessage(object sender, RedisErrorEventArgs e)
        //{
        //    LogHelper.WriteLog("ErrorMessage: " + e.Message, Common.Log.LogType.Error);
        //}

        ///// <summary>
        ///// 重新建立连接之前的错误
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void MuxerConnectionRestored(object sender, ConnectionFailedEventArgs e)
        //{
        //    LogHelper.WriteLog("ConnectionRestored: " + e.EndPoint, Common.Log.LogType.Error);
        //}

        /// <summary>
        /// 连接失败 ， 如果重新连接成功你将不会收到这个通知
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MuxerConnectionFailed(object sender, ConnectionFailedEventArgs e)
        {
            Console.WriteLine("Redis连接失败：Endpoint failed: " + e.EndPoint + ", " + e.FailureType + (e.Exception == null ? "" : (", " + e.Exception.Message)));
        }

        ///// <summary>
        ///// 更改集群
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void MuxerHashSlotMoved(object sender, HashSlotMovedEventArgs e)
        //{
        //    LogHelper.WriteLog("HashSlotMoved:NewEndPoint" + e.NewEndPoint + ", OldEndPoint" + e.OldEndPoint, Common.Log.LogType.Info);
        //}

        /// <summary>
        /// Redis类库错误
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MuxerInternalError(object sender, InternalErrorEventArgs e)
        {
            Console.WriteLine("Redis类库错误,InternalError:Message" + e.Exception.Message);
        }
        #endregion 事件
    }
}
