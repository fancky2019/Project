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
    class RedisQueue<T>
    {
        // Debug, Info, Warn, Error and Fatal
        private static readonly NLog.Logger _nLog = NLog.LogManager.GetCurrentClassLogger();
        //PooledRedisClientManager _pooledRedisClientManager = null;
        private const string QUEUE_KEY = "RedisQueueKey";
        internal static readonly RedisQueue<T> Instance;

        RedisClient _producerClient => RedisHelper.GetQueueClient();

        internal event Action<T> DequeueRedis;
        static RedisQueue()
        {
            Instance = new RedisQueue<T>();
        }
        public RedisQueue()
        {
            //var redisConStr = ConfigurationManager.AppSettings["ServiceStackMasterRedis"].ToString();
            //_pooledRedisClientManager = new PooledRedisClientManager(new string[] { redisConStr },
            //                                            new string[] { },
            //                                            new RedisClientManagerConfig
            //                                            {
            //                                                MaxWritePoolSize = 200,//“写”链接池链接数 ，设置大点不然很容易报池都在用造成超时的异常,下面英文的异常。
            //                                                MaxReadPoolSize = 200,//“读”链接池链接数
            //                                                AutoStart = true
            //                                            }, 0, null, 4
            //                                           );
            //_producerClient = _pooledRedisClientManager.GetClient() as RedisClient;
            //_producerClient.Db = 13;

            Consumer();
        }

        public void EnqueueRedis(T data)
        {

            _producerClient.LPush(QUEUE_KEY, MessagePackUtility.Serialize<T>(data));


        }

        public void Consumer()
        {
            Task.Run(() =>
            {
                var consumerClient = RedisHelper.GetQueueClient() as RedisClient;
                consumerClient.Db = 13;
                //try
                //{



                while (true)
                {

                    //没有数据就阻塞
                    var re = consumerClient.BRPop(QUEUE_KEY, 0);
                    //var key = Encoding.UTF8.GetString(re[0]);
                    var order = MessagePackUtility.Deserialize<T>(re[1]);
                    DequeueRedis?.Invoke(order);

                }
                //}
                //catch(Exception ex)
                //{
                //    consumerClient = null;
                //}

            });
        }
    }
}
