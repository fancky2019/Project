using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Service
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


    }
}
