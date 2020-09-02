using CommonClassLib;
using System;
using System.Collections.Generic;
using System.Text;

namespace ZDFixService.Models
{
    public class SocketMessage<T>
    {
        public MessageType MessageType { get; set; }
        public T Data { get; set; }

        public override string ToString()
        {
            if (this.MessageType == MessageType.HeartBeat)
            {
                return "HeartBeat";
            }
            else
            {
                if (Data is NetInfo netInfo)
                {
                    return netInfo.MyToString();
                }
                else
                {
                    return Data.ToString();
                }

            }
        }

        public static  implicit operator SocketMessage<T>(T t)
        {
            return new SocketMessage<T>() { MessageType = MessageType.BusinessData, Data = t };
        }

    }

    public enum MessageType
    {
        HeartBeat,
        BusinessData
    }

}
