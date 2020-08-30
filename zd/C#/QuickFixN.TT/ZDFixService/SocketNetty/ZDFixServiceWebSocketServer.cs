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

namespace ZDFixService.SocketNetty
{
    public class ZDFixServiceWebSocketServer
    {
        private static readonly NLog.Logger _nLog = NLog.LogManager.GetCurrentClassLogger();
        /*
        * 客户端测试网页：在DW项目的websocketdemo.html。
        * 启动WebSocket服务程序：监听8031端口
        */

        #region 客户端测试相关代码
        /*
         * 浏览器控制台测试连接
         * 在浏览器控制台执行：
         * var ws = new WebSocket("ws://127.0.0.1:8031/");
            ws.onopen = function() { 
                ws.send('websocekt测试'); 
            };
            ws.onmessage = function(e) {
                alert("收到服务端的消息：" + e.data);
            };




          // 客户端网页js代码
            $(function () {

                var inc = document.getElementById('incomming');
                var input = document.getElementById('sendText');
                inc.innerHTML += "connecting to server ..<br/>";

                // create a new websocket and connect
                // window.ws = new wsImpl('ws://127.0.0.1:8031/');
                let ws=null;
                $('#btnConnect').on('click', function () {
                   // create a new websocket and connect
                     ws=new WebSocket('ws://127.0.0.1:8031/');
                    // when data is comming from the server, this metod is called
                    ws.onmessage = function (evt) {
                        inc.innerHTML += evt.data + '<br/>';
                    };

                    // when the connection is established, this method is called
                    ws.onopen = function () {
                        inc.innerHTML += '.. connection open<br/>';
                    };

                    // when the connection is closed, this method is called
                    ws.onclose = function () {
                        inc.innerHTML += '.. connection closed<br/>';
                    }
                });


                $('#btnSend').on('click', function () {
                 var val = input.value;
                if(!window.WebSocket||ws==null){return;}
                if(ws.readyState == WebSocket.OPEN){
                    ws.send(message);
                }else{
                    alert("WebSocket 连接没有建立成功！");
                }
                });

                $('#btnDisconnect').on('click', function () {
                    ws.close();
                });
            });
         */
        #endregion

        IEventLoopGroup _bossGroup;
        IEventLoopGroup _workerGroup;
        IChannel _channel;

        public static ZDFixServiceWebSocketServer Instance;
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
                        pipeline.AddLast(new ZDFixServiceWebSocketServerHandler());
                    }));

                var port = int.Parse(Configurations.Configuration["ZDFixService:WebSocketPort"]);
                _channel = await bootstrap.BindAsync(IPAddress.Loopback, port);
                _nLog.Info($"Listening on  ws://127.0.0.1:{port}/websocket");
            }
            catch (Exception ex)
            {
                _nLog.Error(ex.ToString());
            }
        }

        public async void Close()
        {
            await _channel.CloseAsync();
            _workerGroup.ShutdownGracefullyAsync().Wait();
            _bossGroup.ShutdownGracefullyAsync().Wait();
        }
    }
}
