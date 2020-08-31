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
        protected override void Decode(IChannelHandlerContext context, IByteBuffer message, List<object> output)
        {
            int readableBytes = message.ReadableBytes;
        
            if (readableBytes > 0)
            {
                try
                {
                    ArraySegment<byte> ioBuf = message.GetIoBuffer(0, message.ReadableBytes);
                    MessagePackSerializer.DefaultOptions = ContractlessStandardResolver.Options;
                    var bytes = new byte[readableBytes];
                    message.GetBytes(0, bytes);
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
                message.SkipBytes(message.ReadableBytes);
            }
        }
    }
}
