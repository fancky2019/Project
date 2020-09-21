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
    public class MemoryQueue<T> : IMessageQueue<T>
    {
        private static readonly NLog.Logger _nLog = NLog.LogManager.GetCurrentClassLogger();
        private BlockingCollection<T> _queue = new BlockingCollection<T>();

        public event Action<T> Dequeue;
        //public static MemoryQueue<T> Instance { get; private set; }

        //static MemoryQueue()
        //{
        //    Instance = new MemoryQueue<T>();

        //}
        public MemoryQueue()
        {
            Task.Run(() =>
            {
                DequeueMessage();
            });
        }

        public void Enqueue(T t)
        {
            if (!_queue.IsAddingCompleted)
            {
                if (!_queue.TryAdd(t, 1000))
                {
                    //异常
                }
            }
        }

        public void DequeueMessage()
        {
            foreach (T t in _queue.GetConsumingEnumerable())
            {
                try
                {
                    Dequeue?.Invoke(t);
                }
                catch (Exception ex)
                {
                    _nLog.Info(ex.ToString());
                }
            }
        }


        public void Remove(T t)
        {
            throw new NotImplementedException();
        }

        public void WaitForCompleting()
        {
            _queue.CompleteAdding();
            ////为了避免异常--未扔到交易所的单在内存就丢失
            while (_queue.Count != 0)
            {
                //直到所有的单据处理完成。
                Thread.Sleep(1);

            }

        }

    }
}
