using ZDFixService.Models;
using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack;
using ZDFixService;
using System.Threading;

namespace ZDFixService.Utility
{
    /*
     * https://github.com/ServiceStack/ServiceStack.Redis
     */
    class RedisHelper
    {
        private static readonly NLog.Logger _nLog = NLog.LogManager.GetCurrentClassLogger();
        static PooledRedisClientManager _pooledRedisClientManager = null;
        const string NEXT_CLIENT_ORDER_ID = "NextClOrderID";
        static RedisHelper()
        {
            /*
             *  localhost
                127.0.0.1:6379
                redis://localhost:6379
                password@localhost:6379
                clientid:password@localhost:6379
                redis://clientid:password@localhost:6380?ssl=true&db=1
             */
            //不能将下面语句放入配置文件，读取的配置无法连接redis
            //var redisConStr = "fancky123456@127.0.0.1:6379?db=13&amp;connectTimeout=2&amp;sendtimeout=3&amp;receiveTimeout=4&amp;idletimeoutsecs=5&amp;NamespacePrefix=prefix.";
            //fancky123456@127.0.0.1:6379 ? db = 0
            var redisConStr = Configurations.Configuration["ZDFixService:Persist:Redis:ServiceStackMasterRedis"].ToString();
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

        static internal RedisClient GetRedisClient()
        {
            var client = _pooledRedisClientManager.GetClient() as RedisClient;
            client.Db = 13;
            return client;
        }

        static internal long GetNextClOrderID()
        {
            var nextClOrderID = 0L;
            //  WriteReadRedisClient.SetValue("StringExpiryKey1", "StringExpiryValue1", TimeSpan.FromSeconds(20));
            using (var redisClient = GetRedisClient())
            {

                nextClOrderID = redisClient.Incr(NEXT_CLIENT_ORDER_ID);


                while (true)
                {

                    if (redisClient.Exists(nextClOrderID.ToString()) == 1)
                    {
                        nextClOrderID = redisClient.IncrBy(NEXT_CLIENT_ORDER_ID, 1);
                    }
                    else
                    {
                        return nextClOrderID;
                    }

                }

            }

        }


        static internal void SaveData(string key, byte[] bytes)
        {
            using (var redisClient = GetRedisClient())
            {
                //更新：覆盖原有值
                //redisClient.Del(key);
                redisClient.Set(key, bytes);
            }

        }

        static internal byte[] GetData(string key)
        {
            byte[] bytes = null;
            using (var redisClient = GetRedisClient())
            {
                bytes = redisClient.Get(key);
            }
            return bytes;
        }


        static internal void ListEnqueue(string listKey, byte[] data)
        {
            using (var redisClient = GetRedisClient())
            {
                redisClient.LPush(listKey, data);
            }
        }

        static internal byte[] ListDequeue(string listKey)
        {
            using (var redisClient = GetRedisClient())
            {
                //没有数据就阻塞
                var re = redisClient.BRPop(listKey, 0);
                var value = re[1];
                return value;
            }
        }

        static internal long ListLen(string listKey)
        {
            var len = 0L;
            using (var redisClient = GetRedisClient())
            {
                len = redisClient.LLen(listKey);
            }
            return len;
        }

        static internal void Close()
        {
            _pooledRedisClientManager?.Dispose();
        }

    }
}
