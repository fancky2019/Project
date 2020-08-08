using MessagePack;
using MessagePack.Resolvers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZDFixService.Utility
{
    class MessagePackUtility
    {
        static MessagePackUtility()
        {

            //var bin = MessagePackSerializer.Serialize( data, ContractlessStandardResolver.Options);

            //// {"MyProperty1":99,"MyProperty2":9999}
            //Console.WriteLine(MessagePackSerializer.SerializeToJson(bin));


            //指定默认ContractlessStandardResolver.Options
            // You can also set ContractlessStandardResolver as the default.
            // (Global state; Not recommended when writing library code)
            MessagePackSerializer.DefaultOptions = ContractlessStandardResolver.Options;
        }

        public static byte[] Serialize<T>(T data)
        {
            return MessagePackSerializer.Serialize(data);
        }


        public static T Serialize<T>(byte[] bytes)
        {
            return MessagePackSerializer.Deserialize<T>(bytes);
        }
 

        public static T Deserialize<T>(byte[] bytes)
        {
            return MessagePackSerializer.Deserialize<T>(bytes);
        }

        //public static string SerializeToJson<T>(T data)
        //{
        //    return MessagePackSerializer.SerializeToJson(data);
        //}

        //反序列化ConcurrentDictionary报错

        //public static T DeserializeFromJson<T>(string jsonStr)
        //{
        //    var bytesFromJson = MessagePackSerializer.ConvertFromJson(jsonStr);
        //    return MessagePackSerializer.Deserialize<T>(bytesFromJson);
        //}

    }
}
