using DotNetty.Codecs;
using DotNetty.Codecs.Protobuf;
using DotNetty.Handlers.Logging;
using DotNetty.Handlers.Timeout;
using DotNetty.Transport.Bootstrapping;
using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Sockets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ZDFixService.SocketNetty.NettyCodec;
using CommonClassLib;
using NLog;
using System.Configuration;
using System.Runtime.InteropServices;
using System.Threading;
using ZDFixService.Models;
using ZDFixService;

namespace ZDFixClient.SocketNettyClient
{

    public class CommunicationClient
    {
        private static readonly Logger _nLog = NLog.LogManager.GetCurrentClassLogger();
        Bootstrap _bootstrap = new Bootstrap();
        IChannel _clientChannel = null;

        IPEndPoint _iPEndPoint = null;
        MultithreadEventLoopGroup group;
        volatile bool _closed = false;
        public event Action<string> ReceiveMsg;

        public static CommunicationClient Instance { get; private set; }
        static CommunicationClient()
        {
            Instance = new CommunicationClient();
        }

        public CommunicationClient()
        {
            RunClientAsync();
        }

        public async void RunClientAsync()
        {
            var ipPort = ConfigurationManager.AppSettings["CommunicationIPPort"].ToString().Split(':');
            string ip = ipPort[0];
            //string _ip = "192.168.1.114";
            string port = ipPort[1];
            _iPEndPoint = new IPEndPoint(IPAddress.Parse(ip), int.Parse(port));
            group = new MultithreadEventLoopGroup();
            try
            {
                _bootstrap.Group(group)
                    .Channel<TcpSocketChannel>()
                    .Option(ChannelOption.TcpNodelay, true)
                    .Handler(new ActionChannelInitializer<ISocketChannel>(channel =>
                    {
                        IChannelPipeline pipeline = channel.Pipeline;
                        //6s未读写就断开了连接。和java的一样设计
                        IdleStateHandler idleStateHandler = new IdleStateHandler(2, 2, 6);

                        pipeline.AddLast("timeout", idleStateHandler);
                        //pipeline.AddLast("framing-enc", new LengthFieldPrepender(2));
                        //pipeline.AddLast("framing-dec", new LengthFieldBasedFrameDecoder(ushort.MaxValue, 0, 2, 0, 2));

                        //无参数，默认系统默认编码: Encoding.Default
                        //pipeline.AddLast("StringDecoder", new StringDecoder(Encoding.UTF8));
                        //pipeline.AddLast("StringDecoder", new StringDecoder());
                        //pipeline.AddLast("StringEncoder", new StringEncoder());

                        //pipeline.AddLast("ProtobufDecoder", new ProtobufDecoder(PersonProto.Parser));
                        //pipeline.AddLast("ProtobufEncoder", new ProtobufEncoder());

                        //pipeline.AddLast("ObjectDecoder", new ObjectDecoder<SocketMessage<NetInfo>>());
                        //pipeline.AddLast("ObjectEncoder", new ObjectEncoder());

                        pipeline.AddLast("ZDDecoder", new ZDDecoder());
                        pipeline.AddLast("ZDEncoder", new ZDEncoder());

                        CommunicationClientHandler echoClientHandler = new CommunicationClientHandler(ReceiveMsg);
                        echoClientHandler.DisConnected += () =>
                          {
                              Connect(_iPEndPoint);
                          };
                        pipeline.AddLast("echo", echoClientHandler);
                    }));

            }
            catch (Exception ex)
            {
                _nLog.Error(ex.ToString());
            }
        }

        public void SendMsg<T>(T t)
        {
            try
            {
                if (_clientChannel == null)
                {
                    _nLog.Info("连接异常，确保网络连接正常。");
                    return;
                }
                _clientChannel.WriteAndFlushAsync(t);
                //_nLog.Info($"Sent to Communication:{t.ToString()}");
            }
            catch (Exception ex)
            {
                _nLog.Error(ex.ToString());
            }
        }

        public void Connect()
        {
            Connect(_iPEndPoint);
        }

        public void Connect(IPEndPoint iPEndPoint)
        {
            if (_closed || (_clientChannel != null && _clientChannel.Active))
            {
                return;
            }
            Task.Run(() =>
            {
                try
                {
                    _nLog.Info("Reconnect......");
                    var task = _bootstrap.ConnectAsync(iPEndPoint);
                    task.Wait(2000);
                    _clientChannel = task.Result;
                }
                catch (Exception ex)
                {
                    _nLog.Error($"{ex.Message} - {ex.InnerException.Message}");
                    if (_clientChannel != null)
                    {
                        Thread.Sleep(2000);
                        Connect(iPEndPoint);
                    }

                }
            });


        }

        public async void Close()
        {
            _closed = true;
            if (group != null)
            {
                if (_clientChannel != null)
                {
                    await _clientChannel.CloseAsync();
                }
                await group.ShutdownGracefullyAsync(TimeSpan.FromMilliseconds(100), TimeSpan.FromSeconds(1));
            }
        }


    }
}
