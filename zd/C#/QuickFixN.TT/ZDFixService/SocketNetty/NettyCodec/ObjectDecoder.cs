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
    public class ObjectDecoder<T> : ByteToMessageDecoder
    {
        protected override void Decode(IChannelHandlerContext context, IByteBuffer byteBuffer, List<object> output)
        {
            int readableBytes = byteBuffer.ReadableBytes;
        
            if (readableBytes > 0)
            {
                try
                {
                    ArraySegment<byte> ioBuf = byteBuffer.GetIoBuffer(0, byteBuffer.ReadableBytes);
                    MessagePackSerializer.DefaultOptions = ContractlessStandardResolver.Options;
                    var bytes = new byte[readableBytes];
                    byteBuffer.GetBytes(0, bytes);
                    //byteBuffer.GetBytes(byteBuffer.ReaderIndex, bytes, 0, readableBytes);
                    var t = MessagePackSerializer.Deserialize<T>(bytes);

                    if (t != null)
                    {
                        output.Add(t);
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
