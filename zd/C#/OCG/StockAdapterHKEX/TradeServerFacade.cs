using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AuthCommon;
using CommonClassLib;
using StockAdapterHKEX.Common;

namespace StockAdapterHKEX
{
    //public class TradeServerFacade
    //{

    //    private static OrderForm orderForm = null;

    //    public static void setMsgReceiver(object receiver)
    //    {
    //        orderForm = (OrderForm)receiver;
    //    }

    //    public static void writeLog(int logLevel, string logContent)
    //    {
    //    }

    //    public static void SendString(NetInfo obj)
    //    {
    //        orderForm.lbMsgs_addItem(obj.MyToString());
    //    }
    //}



    public class TradeServerFacade
    {
        private static CommunicationServer server = null;
        private static Log _sendMsgsLog = null;
        static TradeServerFacade()
        {
            _sendMsgsLog = LogManager.GetLogger("SendMsgs");
  
        }

        public static void setCommuServer(CommunicationServer server)
        {
            TradeServerFacade.server = server;
        }
        public static CommunicationServer getCommuServer()
        {
            return TradeServerFacade.server;
        }


        public static void writeLog(int logLevel, string logContent)
        {
            server.writeLog(logLevel, logContent);
        }

        public static void SendString(NetInfo obj)
        {
            _sendMsgsLog.WriteLog( obj.MyToString());
            server.SendString(obj);

        }
    }
}
