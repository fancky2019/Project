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
    /// 消费失败无法重试。
    /// </summary>
    class RedisQueue : IQueue
    {
        private static readonly NLog.Logger _nLog = NLog.LogManager.GetCurrentClassLogger();

        private const string List_Order_Queue = "ListKeyOrderQueue";
        private const string List_Message_Queue = "ListKeyMessageQueue";

        public static RedisQueue Instance { get; private set; }
        public event Action<NetInfo> OrderDequeue;
        //持久化信息类型丢失，只支持内存队列。
        public event Action<Message> MessageDequeue;


        static RedisQueue()
        {
            Instance = new RedisQueue();

        }
        private RedisQueue()
        {
            Task.Run(() =>
            {
                DequeueOrder();
            });

            Task.Run(() =>
            {
                DequeueFixMessage();
            });
        }

        public void EnqueueOrder(NetInfo netInfo)
        {

            var bytes = MessagePackUtility.Serialize<NetInfo>(netInfo);
            RedisHelper.ListEnqueue(List_Order_Queue, bytes);

        }

        public void DequeueOrder()
        {
            while (true)
            {
                var bytes = RedisHelper.ListDequeue(List_Order_Queue);
                var netInfo = MessagePackUtility.Deserialize<NetInfo>(bytes);
                this.OrderDequeue?.Invoke(netInfo);
            }
        }

        public void EnqueueFixMessage(Message message)
        {
            //没解决Message 序列化问题
            this.MessageDequeue?.Invoke(message);
        }

        public void DequeueFixMessage()
        {
            //while (true)
            //{
            //    var bytes = RedisHelper.ListDequeue(List_Message_Queue);
            //    Message message = new Message();
            //    var msgStr = MessagePackUtility.Deserialize<string>(bytes);
            //    msgStr = msgStr.Replace('|', (char)1);
            //    message.FromString(msgStr, false, null, null);
            //    this.MessageDequeue?.Invoke(message);
            //}
        }

        public void WaitForAdding()
        {
            while (RedisHelper.ListLen(List_Order_Queue) != 0 || RedisHelper.ListLen(List_Message_Queue) != 0)
            {
                //直到所有的单据处理完成。
                Thread.Sleep(1);
            }
        }

        public bool RemoveFixMessage(Message message)
        {
            throw new NotImplementedException();
        }

        public void RemoveOrder()
        {
            throw new NotImplementedException();
        }
    }
}
