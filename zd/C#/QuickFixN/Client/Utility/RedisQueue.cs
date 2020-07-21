using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Client.Utility
{
    class RedisQueue
    {
        // Debug, Info, Warn, Error and Fatal
        private static readonly NLog.Logger _nLog = NLog.LogManager.GetCurrentClassLogger();
        PooledRedisClientManager _pooledRedisClientManager = null;
        private const string QUEUE_KEY = "RedisQueueKey";
        static readonly RedisQueue Instance;
        static RedisQueue()
        {
            Instance = new RedisQueue();
        }
        public RedisQueue()
        {
            var redisConStr = ConfigurationManager.AppSettings["ServiceStackMasterRedis"].ToString();

            //var redisConStr = "fancky123456@127.0.0.1:6379?db=0&amp;connectTimeout=2&amp;sendtimeout=3&amp;receiveTimeout=4&amp;idletimeoutsecs=5&amp;NamespacePrefix=prefix.";


            //DBIndex:0,
            //poolSizeMultiplier :默认20
            //PoolTimeout 4S
            _pooledRedisClientManager = new PooledRedisClientManager(new string[] { redisConStr },
                                                        new string[] { },
                                                        new RedisClientManagerConfig
                                                        {
                                                            MaxWritePoolSize = 200,//“写”链接池链接数 ，设置大点不然很容易报池都在用造成超时的异常,下面英文的异常。
                                                            MaxReadPoolSize = 200,//“读”链接池链接数
                                                            AutoStart = true
                                                        }, 0, null, 4
                                                       );
            //Redis Timeout expired. The timeout period elapsed prior to obtaining a connection from the pool. 
            //This may have occurred because all pooled connections were in use.

            //PooledRedisClientManager.PoolTimeout  默认两秒 上面改成4秒
        }

        public void SaveToRedis<T>(T data)
        {
            try
            {

                Task.Run(() =>
                {
                    var producerClient = _pooledRedisClientManager.GetClient() as RedisClient;
                    producerClient.Db = 13;
                    producerClient.LPush(QUEUE_KEY, MessagePackUtility.Serialize<T>(data));

                });
                Thread.Sleep(1000);

                Task.Run(() =>
                {
                    var consumerClient = _pooledRedisClientManager.GetClient() as RedisClient;
                    consumerClient.Db = 13;
                    while (true)
                    {
                        //没有数据就阻塞
                        var re = consumerClient.BRPop(QUEUE_KEY, 0);
                        var key = Encoding.UTF8.GetString(re[0]);
                        var value = MessagePackUtility.Deserialize<T>(re[1]);
                    }

                });
            }
            catch (Exception ex)
            {
                _nLog.Error(ex.ToString());
            }
        }
    }
}
