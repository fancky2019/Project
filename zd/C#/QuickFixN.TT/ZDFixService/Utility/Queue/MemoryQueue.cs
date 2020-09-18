using CommonClassLib;
using QuickFix;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ZDFixService.Utility.Queue
{
    class MemoryQueue : IQueue
    {
        private static readonly NLog.Logger _nLog = NLog.LogManager.GetCurrentClassLogger();
        private BlockingCollection<QuickFix.Message> _messageQueue = new BlockingCollection<QuickFix.Message>();
        private BlockingCollection<NetInfo> _orderQueue = new BlockingCollection<NetInfo>();

        public event Action<NetInfo> OrderDequeue;
        public event Action<Message> MessageDequeue;
        public static MemoryQueue Instance { get; private set; }

        static MemoryQueue()
        {
            Instance = new MemoryQueue();

        }
        private MemoryQueue()
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
            if (!_orderQueue.IsAddingCompleted)
            {
                if (!_orderQueue.TryAdd(netInfo, 1000))
                {
                    //异常
                }
            }
        }

        public void DequeueOrder()
        {
            foreach (NetInfo netInfo in _orderQueue.GetConsumingEnumerable())
            {
                try
                {
                    OrderDequeue?.Invoke(netInfo);
                }
                catch (Exception ex)
                {
                    _nLog.Info(ex.ToString());
                }
            }
        }




        public void EnqueueFixMessage(Message message)
        {
            if (!_messageQueue.IsAddingCompleted)
            {
                if (!_messageQueue.TryAdd(message, 1000))
                {
                    //异常

                }
            }
        }

        public void DequeueFixMessage()
        {
            foreach (var message in _messageQueue.GetConsumingEnumerable())
            {
                try
                {
                    MessageDequeue?.Invoke(message);
                }
                catch (Exception ex)
                {
                    _nLog.Info(ex.ToString());
                }
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

        public void WaitForAdding()
        {
            _orderQueue.CompleteAdding();
            _messageQueue.CompleteAdding();
            //为了避免异常--未扔到交易所的单在内存就丢失
            while (_orderQueue.Count != 0 || _messageQueue.Count != 0)
            {
                //直到所有的单据处理完成。
                Thread.Sleep(1);

            }

        }

    }
}
