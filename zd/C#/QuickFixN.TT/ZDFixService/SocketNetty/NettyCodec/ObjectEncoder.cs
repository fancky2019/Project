using DotNetty.Buffers;
using DotNetty.Codecs;
using DotNetty.Transport.Channels;
using MessagePack;
using MessagePack.Resolvers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZDFixService.SocketNetty.NettyCodec
{
    public class ObjectEncoder : MessageToByteEncoder<object>
    {
        protected override void Encode(IChannelHandlerContext context, object message, IByteBuffer output)
        {
            MessagePackSerializer.DefaultOptions = ContractlessStandardResolver.Options;
            // Now serializable...
            var messageBytes = MessagePackSerializer.Serialize(message);
            IByteBuffer byteBuffer = Unpooled.DirectBuffer(messageBytes.Length);
            byteBuffer.WriteBytes(messageBytes);
            context.WriteAndFlushAsync(byteBuffer);

        }
    }
}
