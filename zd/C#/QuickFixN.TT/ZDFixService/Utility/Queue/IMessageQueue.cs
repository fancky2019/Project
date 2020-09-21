using CommonClassLib;
using QuickFix;
using System;
using System.Collections.Generic;
using System.Text;

namespace ZDFixService.Utility.Queue
{
    interface IMessageQueue<T>
    {
        event Action<T> Dequeue;
        //event Action<Message> MessageDequeue;
        void Enqueue(T t);
        //void DequeueOrder();
        //void EnqueueFixMessage(Message message);
        //void DequeueFixMessage();
        void WaitForCompleting();
    }
}
