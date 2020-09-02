using CommonClassLib;
using DotNetty.Buffers;
using DotNetty.Handlers.Timeout;
using DotNetty.Transport.Channels;
using MessagePack;
using MessagePack.Resolvers;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDFixService.Models;
using ZDFixService.Service.Base;

namespace ZDFixService.SocketNetty
{
    class ZDFixServiceServerHandler : ChannelHandlerAdapter
    {
        private static readonly NLog.Logger _nLog = NLog.LogManager.GetCurrentClassLogger();
        internal ConcurrentDictionary<string, IChannel> ConnectedChannel { get; set; }

        internal ZDFixServiceServerHandler()
        {
            ConnectedChannel = new ConcurrentDictionary<string, IChannel>();
        }

        public override void HandlerAdded(IChannelHandlerContext context)
        {
            base.HandlerAdded(context);
        }

        /// <summary>
        /// 客户端断开连接
        /// </summary>
        /// <param name="context"></param>
        public override void HandlerRemoved(IChannelHandlerContext context)
        {

            base.HandlerRemoved(context);
        }

        public override void ChannelRegistered(IChannelHandlerContext context)
        {
            base.ChannelRegistered(context);

        }
        public override void ChannelUnregistered(IChannelHandlerContext context) => base.ChannelUnregistered(context);


        public override void ChannelActive(IChannelHandlerContext context)
        {
            _nLog.Info($"Client - {context.Channel.RemoteAddress.ToString()} connected。");
            ConnectedChannel.TryAdd(context.Channel.RemoteAddress.ToString(), context.Channel);
            base.ChannelActive(context);
        }

        public override void ChannelInactive(IChannelHandlerContext context)
        {
            _nLog.Info($"Client - {context.Channel.RemoteAddress.ToString()} disconnected。");
            ConnectedChannel.TryRemove(context.Channel.RemoteAddress.ToString(), out _);
            base.ChannelInactive(context);
        }

        public override void ChannelRead(IChannelHandlerContext context, object message)
        {
            var buffer = message as IByteBuffer;
            if (buffer != null)
            {
                _nLog.Info("Received from client: " + buffer.ToString(Encoding.UTF8));
            }
            else
            {
                var socketMessage = (SocketMessage<NetInfo>)message;

                _nLog.Info($"Received from client:{socketMessage.ToString()}");
                if (socketMessage.MessageType == MessageType.BusinessData)
                {
                    TradeServiceFactory.ITradeService.Order(socketMessage.Data);
                }

            }
            //context.WriteAsync(message);
        }

        public override void ChannelReadComplete(IChannelHandlerContext context) => context.Flush();

        public override void UserEventTriggered(IChannelHandlerContext context, object evt)
        {
            if (evt is IdleStateEvent eventState)
            {

                if (eventState != null)
                {
                    switch (eventState.State)
                    {
                        case IdleState.ReaderIdle:
                            break;
                        case IdleState.WriterIdle:

                            //服务端不回心跳。
                            // 长时间未写入数据, 则发送心跳数据
                            // context.WriteAndFlushAsync();
                            break;
                        case IdleState.AllIdle:
                            _nLog.Info($"Server disconnect  client:{context.Channel.RemoteAddress.ToString()}");
                            //客户端超时就主动断开，客户端会收到ChannelInactive
                            //6秒既没有读，也没有写，即发生了3次没有读写，可认为网络断开。
                            context.DisconnectAsync();
                            break;
                    }
                }
            }
            context.FireUserEventTriggered(evt);
        }


        public override void ExceptionCaught(IChannelHandlerContext context, Exception exception)
        {
            Console.WriteLine("Exception: " + exception);
            context.CloseAsync();
        }


    }
}
