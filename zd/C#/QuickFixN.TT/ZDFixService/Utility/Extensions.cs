using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ZDFixService.Utility
{
    public static class Extensions
    {

        /// <summary>
        /// 获取时间戳
        /// </summary>
        /// <returns></returns>
        public static UInt64 GetTimeStamp(this DateTime dateTime)
        {
            TimeSpan ts = dateTime - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToUInt64(ts.TotalMilliseconds);
        }

        //public static T Implement<T>(this Type type, string implementClassName)
        //{
        //    T t = default(T);
        //    Assembly assembly = Assembly.GetAssembly(type);
        //    //var assemblies = AppDomain.CurrentDomain.GetAssemblies();
        //    var types = assembly.GetTypes();
        //    //以下字符串中的`1表示泛型参数占位符个数，一个泛型参数则表示为：`1,多个泛型参数则表示为：`N;
        //    var classType = types.Where(p => p.Name == implementClassName).FirstOrDefault();
        //    if (classType != null)
        //    {
        //        var className = classType.Name;
        //        t = (T)Activator.CreateInstance(classType);
        //    }
        //    return t;
        //}


    }
}
