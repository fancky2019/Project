using CommonClassLib;
using DotNetty.Codecs;
using DotNetty.Transport.Bootstrapping;
using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Sockets;
using DotNetty.Transport.Libuv;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ZDFixService.SocketNetty.NettyCodec;
using ZDFixService.Utility;
using System.Linq;
using MessagePack;

namespace ZDFixService.SocketNetty
{
    public class ZDFixServiceServer
    {
        private static readonly NLog.Logger _nLog = NLog.LogManager.GetCurrentClassLogger();

        public static ZDFixServiceServer Instance;

        IChannel _channel;
        IEventLoopGroup _bossGroup;
        IEventLoopGroup _workerGroup;
        ZDFixServiceServerHandler _serverHandler;
        static ZDFixServiceServer()
        {
            Instance = new ZDFixServiceServer();
        }

        public async Task RunServerAsync()
        {
            var useLibuv = true;
            if (useLibuv)
            {
                var dispatcher = new DispatcherEventLoopGroup();
                _bossGroup = dispatcher;
                _workerGroup = new WorkerEventLoopGroup(dispatcher);
            }
            else
            {
                _bossGroup = new MultithreadEventLoopGroup(1);
                _workerGroup = new MultithreadEventLoopGroup();
            }
            try
            {
                var bootstrap = new ServerBootstrap();
                bootstrap.Group(_bossGroup, _workerGroup);

                if (useLibuv)
                {
                    bootstrap.Channel<TcpServerChannel>();
                }
                else
                {
                    bootstrap.Channel<TcpServerSocketChannel>();
                }

                bootstrap
                    .Option(ChannelOption.SoBacklog, 100)
                    .ChildHandler(new ActionChannelInitializer<IChannel>(channel =>
                    {
                        IChannelPipeline pipeline = channel.Pipeline;

                        pipeline.AddLast("framing-enc", new LengthFieldPrepender(2));
                        pipeline.AddLast("framing-dec", new LengthFieldBasedFrameDecoder(ushort.MaxValue, 0, 2, 0, 2));
                        pipeline.AddLast("ObjectDecoder", new ObjectDecoder<NetInfo>());
                        pipeline.AddLast("ObjectEncoder", new ObjectEncoder());
                        _serverHandler = new ZDFixServiceServerHandler();
                        pipeline.AddLast("ZDFixServiceServerHandler", _serverHandler);
                    }));

                var port = int.Parse(Configurations.Configuration["ZDFixService:ServerPort"]);
                _channel = await bootstrap.BindAsync(port);
                _nLog.Info($"Listening on  port:{port}");
            }
            catch (Exception ex)
            {
                _nLog.Error(ex.ToString());
            }
        }

        public async void Close()
        {
            await _channel.CloseAsync();
            await Task.WhenAll(
                                _bossGroup.ShutdownGracefullyAsync(TimeSpan.FromMilliseconds(100), TimeSpan.FromSeconds(1)),
                                _workerGroup.ShutdownGracefullyAsync(TimeSpan.FromMilliseconds(100), TimeSpan.FromSeconds(1)));
        }

        /// <summary>
        /// 如果用Protobuf其实T已经被指定了，编解码器。NetInfo
        /// 当前使用MessagePack
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public async void SendMsgAsync<T>(T t)
        {
            await Task.Run(() =>
                {
                    var clientChannel = _serverHandler?.ConnectedChannel.Values.ToList();
                    clientChannel?.ForEach(p =>
                    {
                        p.WriteAndFlushAsync(t);
                    });
                });
        }
    }
}
