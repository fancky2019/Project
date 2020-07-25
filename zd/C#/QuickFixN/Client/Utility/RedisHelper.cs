using Client.Models;
using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack;
namespace Client.Utility
{
    class RedisHelper
    {
        static PooledRedisClientManager _pooledRedisClientManager = null;
        const string NEXT_CLIENT_ORDER_ID = "NextClOrderID";
        static RedisHelper()
        {
            //var redisConStr = "fancky123456@127.0.0.1:6379?db=0&amp;connectTimeout=2&amp;sendtimeout=3&amp;receiveTimeout=4&amp;idletimeoutsecs=5&amp;NamespacePrefix=prefix.";
            var redisConStr = "fancky123456@127.0.0.1:6379?db=0;connectTimeout=2;sendtimeout=3;receiveTimeout=4;idletimeoutsecs=5;NamespacePrefix=prefix.";

            var slaveRedis = "";
            _pooledRedisClientManager = new PooledRedisClientManager(new string[] { redisConStr },
                                                        new string[] { slaveRedis },
                                                        new RedisClientManagerConfig
                                                        {
                                                            MaxWritePoolSize = 200,//“写”链接池链接数 ，设置大点不然很容易报池都在用造成超时的异常,下面英文的异常。
                                                            MaxReadPoolSize = 200,//“读”链接池链接数
                                                            AutoStart = true
                                                        }, 0, null, 4
                                                       );

        }

        static internal RedisClient GetQueueClient()
        {
            var client = _pooledRedisClientManager.GetClient() as RedisClient;
            client.Db = 13;
            return client;
        }



        static internal long GetNextClOrderID()
        {
            //  WriteReadRedisClient.SetValue("StringExpiryKey1", "StringExpiryValue1", TimeSpan.FromSeconds(20));
            var redisClient = GetQueueClient();
            var NextClOrderID = redisClient.Incr(NEXT_CLIENT_ORDER_ID);
            //if (redisClient.Exists(NEXT_CLIENT_ORDER_ID)==1)
            //{
            //    NextClOrderID = GetNextClOrderID();
            //}

            return NextClOrderID;

        }

        /// <summary>
        /// tag 11
        /// </summary>
        /// <param name="clOrdId"></param>
        /// <returns></returns>
        static internal Order GetOrdder(string clOrdId)
        {
            var redisClient = GetQueueClient();
            var hVals = redisClient.HVals(clOrdId);
            Order order = MessagePackUtility.Deserialize<Order>(hVals[0]);
            return order;
        }

        static internal void SaveOrder(Order order)
        {
            var redisClient = GetQueueClient();
            redisClient.Del(order.ClientID.ToUtf8Bytes());
            //redisClient.SetEntryInHash("RedisHashKey1", "HashKey1", "HashValue1");
            redisClient.HSet(order.ClientID.ToString().ToUtf8Bytes(), order.SystemCode.ToUtf8Bytes(), MessagePackUtility.Serialize<Order>(order));
        }

    }
}
