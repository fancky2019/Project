﻿using DotNetty.Buffers;
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
            //发送二进制数据
            MessagePackSerializer.DefaultOptions = ContractlessStandardResolver.Options;

            // Now serializable...
            //15byte
            var messageBytes = MessagePackSerializer.Serialize(message);
            //175 byte
            //var daBytes = Serialization.Serialize<Person>(data);
            //var obj= Serialization.Deserialize<Person>(daBytes);


            IByteBuffer byteBuffer = Unpooled.DirectBuffer(messageBytes.Length);
            byteBuffer.WriteBytes(messageBytes);
            context.WriteAndFlushAsync(byteBuffer);

        }
    }
}
