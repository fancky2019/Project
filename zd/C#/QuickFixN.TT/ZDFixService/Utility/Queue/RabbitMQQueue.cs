using CommonClassLib;
using QuickFix;
using System;
using System.Collections.Generic;
using System.Text;

namespace ZDFixService.Utility.Queue
{
    class RabbitMQQueue:IQueue
    {
        private static readonly NLog.Logger _nLog = NLog.LogManager.GetCurrentClassLogger();
        public static RabbitMQQueue Instance { get; private set; }
        public event Action<NetInfo> OrderDequeue;
        public event Action<Message> MessageDequeue;
        static RabbitMQQueue()
        {
            Instance = new RabbitMQQueue();
        }

        public void EnqueueOrder(NetInfo netInfo)
        {
            throw new NotImplementedException();
        }

        public void DequeueOrder()
        {
            throw new NotImplementedException();
        }

        public void EnqueueFixMessage(Message message)
        {
            throw new NotImplementedException();
        }

        public void DequeueFixMessage()
        {
            throw new NotImplementedException();
        }

        public void WaitForAdding()
        {
            throw new NotImplementedException();
        }
    }
}
