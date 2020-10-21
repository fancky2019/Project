using DotNetty.Buffers;
using DotNetty.Codecs;
using DotNetty.Transport.Channels;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestService.Netty
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
                        //int index = t.IndexOf(")");
                        //var msg = t.Substring(index + 1, t.Length - index - 1 - 1);
                        var logContent = t;
                        var endString = "}";
                        int m = 0;
                        int firstIndex = logContent.IndexOf(endString);
                        var logContentLength = logContent.Length;
                        List<string> listString = new List<string>();
                        if (firstIndex == logContentLength - 1)
                        {
                            //var content = RemoveLength(logContent);
                            listString.Add(RemoveLength(logContent));
                        }
                        else
                        {
                            string currentContent = "";
                            while (firstIndex != logContentLength - 1)
                            {
                                currentContent = logContent.Substring(0, firstIndex + 1);
                                var content = RemoveLength(currentContent);
                                listString.Add(content);

                                logContent = logContent.Substring(firstIndex + 1);
                                logContentLength = logContent.Length;
                                firstIndex = logContent.IndexOf(endString);
                            }
                            listString.Add(RemoveLength(logContent));
                        }


                        //output.Add(msg);
                        output.AddRange(listString);
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

        private string GetLength(string logContent)
        {
            var lenIndexStr = "len=";
            var lenIndexStrIndex = logContent.IndexOf(lenIndexStr);
            var lenEndStr = ")";
            var lenEndStrIndex = logContent.IndexOf(lenEndStr);

            var lenNumIndex = lenIndexStrIndex + lenIndexStr.Length;
            var lengthStr = logContent.Substring(lenNumIndex, lenEndStrIndex - lenNumIndex);

            return lengthStr;
        }
        private string RemoveLength(string logContent)
        {

            var lenEndStr = ")";
            var lenEndStrIndex = logContent.IndexOf(lenEndStr);
            var content = logContent.Substring(lenEndStrIndex + 1, logContent.Length - (lenEndStrIndex + 1) - 1);
            return content;
        }

    }
}
