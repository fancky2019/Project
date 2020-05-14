using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TT.Common
{
    public class NLogUtility
    {
        // Debug, Info, Warn, Error and Fatal
        private static readonly NLog.Logger nLog = NLog.LogManager.GetCurrentClassLogger();

        public static void Debug(string message)
        {
            nLog.Debug(message);
        }

        public static void Info(string message)
        {
            nLog.Info(message);
        }

        public static void Warn(string message)
        {
            nLog.Warn(message);
        }

        public static void Error( string message)
        {
            nLog.Error(message);
        }

        public static void Error(Exception ex,string message)
        {
            nLog.Error(ex,message);
        }

        public static void Fatal( string message)
        {
            nLog.Fatal(message);
        }

        public static void Fatal(Exception ex, string message)
        {
            nLog.Fatal(ex,message);
        }

        public static void Shutdown()
        {
            NLog.LogManager.Shutdown();
        }
        
      
    }
}
