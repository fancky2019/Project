using DotNetty.Buffers;
using DotNetty.Codecs;
using DotNetty.Transport.Channels;
using MessagePack;
using MessagePack.Resolvers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZDFixService.SocketNetty.NettyCodec
{
    public class ZDDecoder : ByteToMessageDecoder
    {
        protected override void Decode(IChannelHandlerContext context, IByteBuffer byteBuffer, List<object> output)
        {
            int readableBytes = byteBuffer.ReadableBytes;

            if (readableBytes > 0)
            {
                try
                {
                    ArraySegment<byte> ioBuf = byteBuffer.GetIoBuffer(0, byteBuffer.ReadableBytes);
            
                    var bytes = new byte[readableBytes];
                    byteBuffer.GetBytes(0, bytes);
                    //byteBuffer.GetBytes(byteBuffer.ReaderIndex, bytes, 0, readableBytes);
                    var t = Encoding.UTF8.GetString(bytes);
                 
                    if (t != null)
                    {
                        //{(len=21)TEST0001@@@@@@@@@@@0&}
                        int index = t.IndexOf(")");
                        var msg = t.Substring(index + 1, t.Length - index - 1 - 1);
                        output.Add(msg);
                    }
                }
                catch (Exception innerException)
                {
                    throw new CodecException(innerException);
                }
                //必须要跳过
                byteBuffer.SkipBytes(byteBuffer.ReadableBytes);
            }
        }
    }
}
