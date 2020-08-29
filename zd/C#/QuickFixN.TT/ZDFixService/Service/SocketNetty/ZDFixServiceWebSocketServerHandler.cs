﻿using DotNetty.Buffers;
using DotNetty.Codecs.Http;
using DotNetty.Codecs.Http.WebSockets;
using DotNetty.Common.Utilities;
using DotNetty.Transport.Channels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//官方Demo使用了静态类
using static DotNetty.Codecs.Http.HttpVersion;
using static DotNetty.Codecs.Http.HttpResponseStatus;

namespace ZDFixService.Service.SocketNetty
{
    public class ZDFixServiceWebSocketServerHandler : SimpleChannelInboundHandler<object>
    {
        private static readonly NLog.Logger _nLog = NLog.LogManager.GetCurrentClassLogger();

        const string WebsocketPath = "/websocket";

        WebSocketServerHandshaker _handshaker;

        public override void ChannelActive(IChannelHandlerContext context)
        {
            _nLog.Info($"Client - {context.Channel.RemoteAddress.ToString()} connected。");
            base.ChannelActive(context);
        }

        public override void ChannelInactive(IChannelHandlerContext context)
        {
            _nLog.Info($"Client - {context.Channel.RemoteAddress.ToString()} disconnected。");
            base.ChannelInactive(context);
        }


        protected override void ChannelRead0(IChannelHandlerContext ctx, object msg)
        {

            if (msg is IFullHttpRequest request)
            {
                this.HandleHttpRequest(ctx, request);
            }
            else if (msg is WebSocketFrame frame)
            {
                this.HandleWebSocketFrame(ctx, frame);
            }
        }

        public override void ChannelReadComplete(IChannelHandlerContext context) => context.Flush();

        void HandleHttpRequest(IChannelHandlerContext ctx, IFullHttpRequest req)
        {
            // Handle a bad request.
            if (!req.Result.IsSuccess)
            {
                SendHttpResponse(ctx, req, new DefaultFullHttpResponse(Http11, BadRequest));
                return;
            }

            // Allow only GET methods.
            if (!Equals(req.Method, HttpMethod.Get))
            {
                SendHttpResponse(ctx, req, new DefaultFullHttpResponse(Http11, Forbidden));
                return;
            }

            // Send the demo page and favicon.ico
            //if ("/".Equals(req.Uri))
            //{
            //    IByteBuffer content = Unpooled.WrappedBuffer(
            //    Encoding.ASCII.GetBytes("已连接!")); ;// WebSocketServerBenchmarkPage.GetContent(GetWebSocketLocation(req));
            //    var res = new DefaultFullHttpResponse(Http11, OK, content);

            //    res.Headers.Set(HttpHeaderNames.ContentType, "text/html; charset=UTF-8");
            //    HttpUtil.SetContentLength(res, content.ReadableBytes);

            //    SendHttpResponse(ctx, req, res);
            //    return;
            //}
            if ("/favicon.ico".Equals(req.Uri))
            {
                var res = new DefaultFullHttpResponse(Http11, NotFound);
                SendHttpResponse(ctx, req, res);
                return;
            }

            //建立websocket 连接
            // Handshake
            var wsFactory = new WebSocketServerHandshakerFactory(
                GetWebSocketLocation(req), null, true, 5 * 1024 * 1024);
            this._handshaker = wsFactory.NewHandshaker(req);
            if (this._handshaker == null)
            {
                WebSocketServerHandshakerFactory.SendUnsupportedVersionResponse(ctx.Channel);
            }
            else
            {
                this._handshaker.HandshakeAsync(ctx.Channel, req);
            }
        }

        void HandleWebSocketFrame(IChannelHandlerContext ctx, WebSocketFrame frame)
        {
            // Check for closing frame
            if (frame is CloseWebSocketFrame)
            {
                this._handshaker.CloseAsync(ctx.Channel, (CloseWebSocketFrame)frame.Retain());
                return;
            }

            if (frame is PingWebSocketFrame)
            {
                ctx.WriteAsync(new PongWebSocketFrame((IByteBuffer)frame.Content.Retain()));
                return;
            }

            if (frame is TextWebSocketFrame textWebSocketFrame)
            {
                //接收到来自客户端的字符串消息
                var reveivedMsg = textWebSocketFrame.Text();

                // Echo the frame
                //ctx.WriteAsync(frame.Retain());
                //返回客户端信息，参考java 的netty 的websocket sample
                ctx.WriteAsync(new TextWebSocketFrame($"服务端已收到客户端消息:{reveivedMsg}"));
                return;
            }

            if (frame is BinaryWebSocketFrame)
            {
                // Echo the frame
                ctx.WriteAsync(frame.Retain());
            }
        }

        static void SendHttpResponse(IChannelHandlerContext ctx, IFullHttpRequest req, IFullHttpResponse res)
        {
            // Generate an error page if response getStatus code is not OK (200).
            if (res.Status.Code != 200)
            {
                IByteBuffer buf = Unpooled.CopiedBuffer(Encoding.UTF8.GetBytes(res.Status.ToString()));
                res.Content.WriteBytes(buf);
                buf.Release();
                HttpUtil.SetContentLength(res, res.Content.ReadableBytes);
            }

            // Send the response and close the connection if necessary.
            Task task = ctx.Channel.WriteAndFlushAsync(res);
            if (!HttpUtil.IsKeepAlive(req) || res.Status.Code != 200)
            {
                task.ContinueWith((t, c) => ((IChannelHandlerContext)c).CloseAsync(),
                    ctx, TaskContinuationOptions.ExecuteSynchronously);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="e"></param>
        public override void ExceptionCaught(IChannelHandlerContext ctx, Exception e)
        {
            _nLog.Info($"{nameof(ZDFixServiceWebSocketServerHandler)} {e.ToString()}");
            ctx.CloseAsync();
        }

        string GetWebSocketLocation(IFullHttpRequest req)
        {
            bool result = req.Headers.TryGet(HttpHeaderNames.Host, out ICharSequence value);
            string location = value.ToString() + WebsocketPath;
            return "ws://" + location;
        }
    }
}
