using CommonClassLib;
using QuickFix;
using System;
using System.Collections.Generic;
using System.Text;

namespace ZDFixService.Utility.Queue
{
    interface IQueue
    {
        event Action<NetInfo> OrderDequeue;
        event Action<Message> MessageDequeue;
        void EnqueueOrder(NetInfo netInfo);
        //void DequeueOrder();
        void EnqueueFixMessage(Message message);
        //void DequeueFixMessage();
        void WaitForAdding();
    }
}
