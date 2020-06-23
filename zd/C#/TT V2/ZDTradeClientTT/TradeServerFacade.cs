using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonClassLib;

namespace ZDTradeClientTT
{

    /// <summary>
    /// 壳实现接口CommunicationServer：发送到XX的服务器
    /// FrmTradeClient实现接口CommunicationServer；发送到listView
    /// </summary>
    public class TradeServerFacade
    {
        private static CommunicationServer server = null;

        public static void setCommuServer(CommunicationServer server)
        {
            TradeServerFacade.server = server;
        }

        //public static void writeLog(int logLevel, string logContent)
        //{
        //    server.writeLog(logLevel, logContent);
        //}


        /// <summary>
        /// 壳注册：发送到XX的服务器
        /// FrmTradeClient；发送到listView
        /// </summary>
        /// <param name="obj"></param>
        public static void SendString(NetInfo obj)
        {
            TT.Common.NLogUtility.Info($"Send to Clinet - {obj.MyToString()}");
            server.SendString(obj);
        }
    }
}
