using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockAdapterHKEX
{
    class Utils
    {
        private static TimeZoneInfo cstZone = TimeZoneInfo.FindSystemTimeZoneById("China Standard Time");
        public static DateTime toChinaLocalTime(DateTime utcTime)
        {
            DateTime localTime;
            try
            {
                localTime = TimeZoneInfo.ConvertTimeFromUtc(utcTime, cstZone);
            }
            catch (Exception)
            {
                return utcTime.ToLocalTime();
            }

            return localTime;
        }

    }
}
