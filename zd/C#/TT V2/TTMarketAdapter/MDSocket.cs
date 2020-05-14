using AuthCommon;
using CommonClassLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using TT.Common;

namespace TTMarketAdapter
{
    class MDSocket : SocketInitor
    {
        static MDSocket _mDSocket = null;
        static object _lockObj = new object();
        public bool IsConnected { get; private set; }
        public MDSocket(ZDLogger errLogger, string serverIP, string serverPort)
        : base(errLogger, serverIP, serverPort, "10", "30")
        {
            base.netDataState.networkParser = new NetInfoParserV2(10 * 1024);
        }
        static ZDLogger _logger = null;
        public static MDSocket GetInstance()
        {
            if (_mDSocket == null)
            {
                lock (_lockObj)
                {
                    if (_mDSocket == null)
                    {
                        //CfgManager cfgManager = CfgManager.getInstance(null);
                        string ip = Configurations.MulticastTCPIP;
                        string port = Configurations.MulticastTCPPort;
                         _logger = new SynWriteLogger("MDSocket.log");
                        _logger.setLogLevel(ZDLogger.LVL_DEBUG);
                        _mDSocket = new MDSocket(_logger, ip, port);
                        //_mDSocket.heartbeatIntervalSec = 10;
                        _mDSocket.start();

                        ////测试返回的心跳
                        //Task.Run(() =>
                        //{
                        //    byte[] bytes = new byte[256];
                        //    while (true)
                        //    {
                        //        if (_mDSocket.IsConnected)
                        //        {
                        //            var i = _mDSocket.receiveSocket.Receive(bytes);
                        //            var reseiveMsg = Encoding.UTF8.GetString(bytes,0,i);
                        //        }
                        //    }
                        //});
                    }
                }
            }

            return _mDSocket;
        }

        public static void Stop()
        {
            _mDSocket.stop();
        }

        public override void onConnectStateChange(int code, string state)
        {
            IsConnected = code == 1;
            _logger.log(1, state);
        }

        public override void onMsgReady(byte[] rawMsg)
        {
            var data = System.Text.Encoding.ASCII.GetString(rawMsg, 0, rawMsg.Length);
            var str = Encoding.ASCII.GetString(rawMsg, 8, rawMsg.Length - 8);
            //var jsonStr = Regex.Split(str, "@@", RegexOptions.IgnoreCase).Last();
            //TPlusOneHelper.TPlusOneList = JsonConvert.DeserializeObject<List<TPlusOne>>(jsonStr);
            //Stop();
        }


        public override void onTimeSendHeartbeat()
        {
         
            ZDMDMsgProtocol zDMDMsgProtocol = ZDMDMsgProtocol.GetInstance(); ;

            zDMDMsgProtocol.MsgType = ZDMDMsgType.HeartBeat;
            zDMDMsgProtocol.FiveLevelLength = 0;

            zDMDMsgProtocol.OneLevelLength = 0;
            zDMDMsgProtocol.TimeStamp = DateTime.Now.GetTimeStamp();
            zDMDMsgProtocol.MsgBody = $"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}@{_mDSocket.receiveSocket.LocalEndPoint.ToString()}";

            var length = 12 + zDMDMsgProtocol.MsgBody.Length;
            TCPData tCPData = TCPData.GetInstance();

            tCPData.MsgLength = (ushort)length;
            tCPData.ZDMDMsgProtocol = zDMDMsgProtocol;



            //// _mDSocket.receiveSocket.RemoteEndPoint.AddressFamily
            //string heartBeatStr = $"1@{_mDSocket.receiveSocket.LocalEndPoint.ToString()}";
            ////throw new NotImplementedException();

            //byte[] data = Encoding.UTF8.GetBytes(heartBeatStr);

            byte[] data = tCPData.GetBytes();
            Send(data, length + 2);
      
        }

        public void Send(byte[] data,int length)
        {
            if (!IsConnected)
            {
                return;
            }
            _mDSocket.receiveSocket.Send(data,length,System.Net.Sockets.SocketFlags.None);
        }
    }

}
