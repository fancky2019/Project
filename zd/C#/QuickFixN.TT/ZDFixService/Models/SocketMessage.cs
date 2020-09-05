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

    public class MessageData
    {
        public string MessageCommand { get; set; }
        public string ProtocolData { get; set; }
    }

    public class MessageCommand
    {
        /// <summary>
        /// 下单
        /// </summary>
        public const string Order = "Order";
        /// <summary>
        /// 获取内存订单信息
        /// </summary>
        public const string GetOrders = "GetOrders";
        /// <summary>
        /// 添加丢失的内存数据
        /// </summary>
        public const string AddLosedOrder = "AddLosedOrder";
        /// <summary>
        /// 从日志中获取丢失的Order
        /// </summary>
        public const string GetLosedOrderFromLog = "GetLosedOrderFromLog";
    }

}
