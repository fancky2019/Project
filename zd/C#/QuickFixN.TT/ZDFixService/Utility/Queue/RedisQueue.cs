using CommonClassLib;
using QuickFix;
using QuickFix.Fields;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ZDFixService.Utility.Queue
{
    /// <summary>
    /// 不支持消费失败重试。不支持Fix Message队列。
    /// 
    ///序列化造成Message子类类型丢失，只支持内存队列。
    ///如果强制实现，需要修改Fix的MessageFactory相关代码，性能不太好。
    /// </summary
    public class RedisQueue<T> : IMessageQueue<T>
    {
        private static readonly NLog.Logger _nLog = NLog.LogManager.GetCurrentClassLogger();

        //private const string List_Order_Queue = "ListKeyOrderQueue";
        private string _listOrderQueue = "ListKeyOrderQueue";

        public event Action<T> Dequeue;
        //序列化造成Message子类类型丢失，只支持内存队列。
        //如果强制实现，需要修改Fix的MessageFactory相关代码，性能不太好。
        //public event Action<Message> MessageDequeue;




        public RedisQueue()
        {
            var tradeServiceName = Configurations.Configuration["ZDFixService:ITradeService"];
            _listOrderQueue = $"{tradeServiceName}_{_listOrderQueue}";
            Task.Run(() =>
            {
                DequeueMessage();
            });
        }

        public void Enqueue(T t)
        {
            var bytes = MessagePackUtility.Serialize<T>(t);
            RedisHelper.ListEnqueue(_listOrderQueue, bytes);
        }

        public void DequeueMessage()
        {
            while (true)
            {
                var bytes = RedisHelper.ListDequeue(_listOrderQueue);
                var netInfo = MessagePackUtility.Deserialize<T>(bytes);
                this.Dequeue?.Invoke(netInfo);
            }
        }

        public void WaitForCompleting()
        {
            while (RedisHelper.ListLen(_listOrderQueue) != 0)
            {
                //直到所有的单据处理完成。
                Thread.Sleep(1);
            }

            RedisHelper.Close();
        }

        public void Remove(T t)
        {
            throw new NotImplementedException();
        }
    }
}
