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

namespace ZDFixService.SocketNetty
{
    public class ZDFixServiceServer
    {
        private static readonly NLog.Logger _nLog = NLog.LogManager.GetCurrentClassLogger();

        public static ZDFixServiceServer Instance;

        IChannel _channel;
        IEventLoopGroup _bossGroup;
        IEventLoopGroup _workerGroup;

        static ZDFixServiceServer()
        {
            Instance = new ZDFixServiceServer();
        }

        public async Task RunServerAsync()
        {

            //libuv是一个高性能的，事件驱动的I/O库，并且提供了跨平台（如windows, linux）的API。
            //将Dll-->Netty下的libuv.dll复制到运行目录
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
                        //if (tlsCertificate != null)
                        //{
                        //    pipeline.AddLast("tls", TlsHandler.Server(tlsCertificate));
                        //}
                        //pipeline.AddLast(new LoggingHandler("SRV-CONN"));

                        pipeline.AddLast("framing-enc", new LengthFieldPrepender(2));
                        pipeline.AddLast("framing-dec", new LengthFieldBasedFrameDecoder(ushort.MaxValue, 0, 2, 0, 2));

                        //pipeline.AddLast("StringDecoder", new StringDecoder());
                        //pipeline.AddLast("StringEncoder", new StringEncoder());

                        //pipeline.AddLast("ProtobufDecoder", new ProtobufDecoder(PersonProto.Parser));
                        //pipeline.AddLast("ProtobufEncoder", new ProtobufEncoder());


                        pipeline.AddLast("ObjectDecoder", new ObjectDecoder<NetInfo>());
                        pipeline.AddLast("ObjectEncoder", new ObjectEncoder());

                        pipeline.AddLast("ZDFixServiceServerHandler", new ZDFixServiceServerHandler());
                    }));

                var port = int.Parse(Configurations.Configuration["ZDFixService:ServerPort"]);
                _channel = await bootstrap.BindAsync(port);
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
    }
}
