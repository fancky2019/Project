using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Server
{
    public partial class FrmTradeServer : Form
    {
        public FrmTradeServer()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            RedisQueue();
 
        }

        #region 生产者消费者队列
        private static void RedisQueue()
        {
            try
            {
                var MasterRedis = "fancky123456@127.0.0.1:6379?db=0&amp;connectTimeout=2&amp;sendtimeout=3&amp;receiveTimeout=4&amp;idletimeoutsecs=5&amp;NamespacePrefix=prefix.";
                var SlaveRedis = "";
                var PooledRedisClientManager = new PooledRedisClientManager(new string[] { MasterRedis },
                                               new string[] { SlaveRedis },
                                               new RedisClientManagerConfig
                                               {
                                                   MaxWritePoolSize = 200,//“写”链接池链接数 ，设置大点不然很容易报池都在用造成超时的异常,下面英文的异常。
                                                   MaxReadPoolSize = 200,//“读”链接池链接数
                                                   AutoStart = true
                                               }, 0, null, 4
                                              );

                var listKey = "redisQueue";
                Task.Run(() =>
                {

                    var producerClient = PooledRedisClientManager.GetClient() as RedisClient;
                    Console.WriteLine("Producer Connected");
                    producerClient.Db = 13;
                    producerClient.FlushDb();
                    var message = "message";
                    //Stopwatch stopwatch = Stopwatch.StartNew();
                    for (int i = 0; i < 1; i++)
                    {
                        message = $"message - {i}";
                        producerClient.LPush(listKey, Encoding.UTF8.GetBytes(message));
                    }
                    //stopwatch.Stop();
                    //Console.WriteLine(stopwatch.ElapsedMilliseconds);
                });
                //Thread.Sleep(1000);

                Task.Run(() =>
                {
                    var consumerClient = PooledRedisClientManager.GetClient() as RedisClient;
                    Console.WriteLine("Consumer Connected");
                    consumerClient.Db = 13;

                    while (true)
                    {
                        //没有数据就阻塞
                        var re = consumerClient.BRPop(listKey, 0);
                        var key = Encoding.UTF8.GetString(re[0]);
                        var value = Encoding.UTF8.GetString(re[1]);
                        Console.WriteLine($"{value}");
                    }

                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        #endregion
    }
}
