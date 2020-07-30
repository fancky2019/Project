using AuthCommon;
using CommonClassLib;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace ZDTradeClientTT
{
    public class SocketEngine : SocketInitor
    {
        static SocketEngine _socketEngine = null;
        static object _lockObj = new object();
        public bool IsConnected { get; private set; }
        public SocketEngine(ZDLogger errLogger, string serverIP, string serverPort)
        : base(errLogger, serverIP, serverPort, "10", "45")
        {
            base.netDataState.networkParser = new NetInfoParserV2(10 * 1024);
        }

        public static SocketEngine GetInstance()
        {
            if (_socketEngine == null)
            {
                lock (_lockObj)
                {
                    if (_socketEngine == null)
                    {
                        //CfgManager cfgManager = CfgManager.getInstance(null);
                        string ip = ZDTradeClientTTConfiurations.Instance.Gate_FUT_IP;
                        string port = ZDTradeClientTTConfiurations.Instance.Gate_FUT_Port;
                        ZDLogger logger = new SynWriteLogger("HistoryGate.log");
                        logger.setLogLevel(ZDLogger.LVL_DEBUG);
                        _socketEngine = new SocketEngine(logger, ip, port);
                        _socketEngine.start();
                    }
                }
            }

            return _socketEngine;
        }

        public static void Stop()
        {
            _socketEngine.stop();
        }

        public override void onConnectStateChange(int code, string state)
        {
            IsConnected = code == 1;
        }

        public override void onMsgReady(byte[] rawMsg)
        {
            var str = Encoding.ASCII.GetString(rawMsg, 8, rawMsg.Length - 8);
            var jsonStr = Regex.Split(str, "@@", RegexOptions.IgnoreCase).Last();
            TPlusOneHelper.TPlusOneList = JsonConvert.DeserializeObject<List<TPlusOne>>(jsonStr);
            Stop();
        }


        public override void onTimeSendHeartbeat()
        {
            //throw new NotImplementedException();
        }
    }



    class TPlusOneHelper
    {
        public static List<TPlusOne> TPlusOneList = new List<TPlusOne>();
     //   private static ZDLogger logger = null;
        //static TPlusOneHelper()
        //{
        //    logger = new SynWriteLogger("TPlusOneHelper.log");//ZDLoggerFactory.getSynWriteLogger("TPlusOneHelper.log");
        //    logger.setLogLevel(ZDLogger.LVL_TRACE);
        //}
        public static bool IsTPlusOne(string product)
        {
            var tPlusOne = TPlusOneList.Find(p => p.Product == product);
            //为空，数据库没有T+1数据
            if (tPlusOne == null)
            {
                return false;
            }
            TimeSpan nowTimeSpan = TimeSpan.Parse(DateTime.Now.ToLongTimeString().ToString());
            //当前时间在T+1时间内
            if (nowTimeSpan > tPlusOne.TPlusOneStartTime || nowTimeSpan < tPlusOne.TPlusOneEndTime)
            {
                return true;
            }
            return false;
        }

        public static void GetTPlusOneData()
        {
            Task.Factory.StartNew(() =>
            {
                SocketEngine socketEngine = SocketEngine.GetInstance();
                if (!socketEngine.IsConnected)
                {
                    int i = 0;
                    while (i < 10)
                    {
                        Thread.Sleep(500);
                        if (socketEngine.IsConnected)
                        {
                            break;
                        }
                        i++;
                    }

                    if (i == 10)
                    {
                        TT.Common.NLogUtility.Info( "GetTPlusOneData:服务器未连接！");
                        return;
                    }
                }

                string message = "SNDMSG01@@@@@@@@@@&M@XX@SW@REQ-C@@,,TPlusOne";
                byte[] data = CommonFunction.ObjectStringToBytes2(message);
                NetworkLogic.syncSend(socketEngine.receiveSocket, data, 0, data.Length);
            });

        }

    }

    class TPlusOne
    {
        public int ID { get; set; }
        public string Product { get; set; }
        public string Exchange { get; set; }
        public TimeSpan TPlusOneStartTime { get; set; }
        public TimeSpan TPlusOneEndTime { get; set; }

    }
}