using DotNetty.Codecs.Http;
using DotNetty.Transport.Bootstrapping;
using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Sockets;
using DotNetty.Transport.Libuv;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using ZDFixService;
using ZDFixService.Utility;

namespace ZDFixService.SocketNetty
{
    public class ZDFixServiceWebSocketServer
    {
        private static readonly NLog.Logger _nLog = NLog.LogManager.GetCurrentClassLogger();

        IEventLoopGroup _bossGroup;
        IEventLoopGroup _workerGroup;
        IChannel _channel;

        public static ZDFixServiceWebSocketServer Instance;

        ZDFixServiceWebSocketServerHandler _serverHandler;
        static ZDFixServiceWebSocketServer()
        {
            Instance = new ZDFixServiceWebSocketServer();
        }

        public async Task RunServerAsync()
        {
            bool useLibuv = true;

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
                    if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux)
                        || RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                    {
                        bootstrap
                            .Option(ChannelOption.SoReuseport, true)
                            .ChildOption(ChannelOption.SoReuseaddr, true);
                    }
                }
                else
                {
                    bootstrap.Channel<TcpServerSocketChannel>();
                }

                bootstrap
                    .Option(ChannelOption.SoBacklog, 8192)
                    .ChildHandler(new ActionChannelInitializer<IChannel>(channel =>
                    {
                        IChannelPipeline pipeline = channel.Pipeline;
                        pipeline.AddLast(new HttpServerCodec());
                        pipeline.AddLast(new HttpObjectAggregator(65536));
                        _serverHandler = new ZDFixServiceWebSocketServerHandler();
                        pipeline.AddLast(_serverHandler);
                    }));

                var port = int.Parse(Configurations.Configuration["ZDFixService:WebSocketPort"]);
                _channel = await bootstrap.BindAsync(port);
                _nLog.Info($"Listening on  ws://any:{port}/websocket");
            }
            catch (Exception ex)
            {
                _nLog.Error(ex.ToString());
            }
        }

        /// <summary>
        /// 如果用Protobuf其实T已经被指定了，编解码器。NetInfo
        /// 当前使用MessagePack
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public async void SendMsgAsync(string msg)
        {
            await Task.Run(() =>
            {
                //var bytes = Encoding.UTF8.GetBytes(msg);
                var clientChannel = _serverHandler?.ConnectedChannel.Values.ToList();
                clientChannel?.ForEach(p =>
                {
                    p.WriteAndFlushAsync(msg);
                });
            });
        }

        public async void Close()
        {
            await _channel.CloseAsync();
            _workerGroup.ShutdownGracefullyAsync().Wait();
            _bossGroup.ShutdownGracefullyAsync().Wait();
        }
    }
}
