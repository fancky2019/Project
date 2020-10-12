using DotNetty.Buffers;
using DotNetty.Codecs;
using DotNetty.Transport.Channels;
using MessagePack;
using MessagePack.Resolvers;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZDFixService.SocketNetty.NettyCodec
{
    public class ZDEncoder : MessageToByteEncoder<string>
    {
        protected override void Encode(IChannelHandlerContext context, string message, IByteBuffer output)
        {
            // Now serializable...
            var messageBytes = StringToBytes(message.ToString());
            output = Unpooled.WrappedBuffer(messageBytes);
            context.WriteAndFlushAsync(output);

        }

        byte[] StringToBytes(string inPut)
        {
            inPut = $"{{(len={inPut.Length}){inPut}}}";
            return Encoding.UTF8.GetBytes(inPut);
        }

    }
}
