using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonClassLib;

namespace ZDTradeClientTT
{

    /// <summary>
    /// 
    ///壳 ServerImpl : ClientWaiter, CommunicationServer，
    ///壳一键启动按钮事件	btnStartServer_Click(sender, e);
    ///                     btnConnectUpper_Click(sender, e)
    ///                           	((TtTrade)tradeInterface).init(serverImpl, this);
    ///                           	
    /// TtTrade 的init 方法调用
    ///        ZDTradeClientTT.TradeServerFacade.setCommuServer(communicationServer);  
    ///                           	
    ///                     
    ///通过TTcommunication 类的init将
    /// 
    /// 壳实现接口CommunicationServer：发送到XX的服务器
    /// FrmTradeClient实现接口CommunicationServer；打印到listView
    /// </summary>
    public class TradeServerFacade
    {
        private static CommunicationServer server = null;

        public static void setCommuServer(CommunicationServer server)
        {
            TradeServerFacade.server = server;
        }


        /// <summary>
        /// 壳注册：发送到XX的服务器
        /// FrmTradeClient；发送到listView
        /// </summary>
        /// <param name="obj"></param>
        public static void SendString(NetInfo obj)
        {
            #region 壳内toClientLogger 打印日志逻辑
            /*
             * ServerImpl : ClientWaiter, CommunicationServer
             * 
             * 
             * ClientWaiter： Socket的封装类接收到下单请求消息调用 handleClientMsg抽象方法，
             * ServerImpl 继承ClientWaiter并重写handleClientMsg抽象方法。handleClientMsg内部调用
             * SendDataState(发送消息的封装类）的fireSendProc(),fireSendProc内部启动一个线程轮询消息
             * 缓存集合 （BlockingCollection<string>）将消息扔给连接的客户端。同事toClientLogger写日志。
             * 
             * 
             * ServerImpl 类的SendString方法将要发送的消息添加到SendDataState类的消息缓存集合中
             * 
             * 壳一键启动按钮调用 btnConnectUpper_Click 事件，btnConnectUpper_Click内实例化全局对象serverImpl，
             * 并将serverImpl传参到TtTrade类的init方法，init方法内调用     
             * ZDTradeClientTT.TradeServerFacade.setCommuServer(communicationServer);方法，将壳的ServerImpl
             * 传给TT通信。
             * 
             * 壳一键启动按钮调用 btnStartServer_Click事件，其内通过serverImpl调用父类ClientWaiter类内的start(),
             * 启动监听socket线程。
             * 
             * 通信通过壳传参过来的CommunicationServer调用SendString(string str)
             * 
             * 
             * 壳内部分代码：
             * ServerImpl：
             * public void SendString(string str)
	            {
		            foreach (SendDataState value in clientDict.Values)
		            {
			            try
			            {
				            value.addData(str);
			            }
			            catch (Exception ex)
			            {
				            errorLogger.log(1, ex.ToString());
			            }
		            }
	            }

            	public override void handleClientMsg(Socket socket, string msg, RecvStateObject orso)
	            {
		            try
		            {
			            NetInfo netInfo = new NetInfo();
			            netInfo.MyReadString(msg);
			            errorLogger.log(4, msg);
			            if (!(netInfo.code == CommandCode.HEARTBIT))
			            {
				            if (netInfo.code == CommandCode.LOGIN)
				            {
					            Thread.Sleep(1500);
					            string text = (string)(orso.implProcessState = netInfo.clientNo);
					            if (uiBridge != null)
					            {
						            uiBridge.setStateInfo(orso.remoteIPPort + DateTime.Now.ToString("[yyyyMMdd-HH:mm:ss] ") + " logon: " + text);
					            }
					            SendDataState value = null;
					            if (clientDict.TryGetValue(text, out value))
					            {
						            value.rso = orso;
						            value.isCurrentSocketBroken = false;
					            }
					            else
					            {
						            value = new SendDataState(orso, uiBridge);
						            value.setLogger(toClientLogger);
						            value.isCurrentSocketBroken = false;
						            clientDict.TryAdd(text, value);
						            value.fireSendProc();
					            }

            *
            * 
            * 
            * 
            * 
            * 
            * 
            * 
            * SendDataState 类
                         * public bool addData(string data)
                            {
	                            if (dataBCQ.IsAddingCompleted)
	                            {
		                            return false;
	                            }
	                            dataBCQ.Add(data);
	                            return true;
                            }

            	public void fireSendProc()
	                {
		                thread = new Thread(sendData);
		                thread.Start();
	                }

            	public void sendData()
	                {
		                try
		                {
			                while (!dataBCQ.IsCompleted)
			                {
				                string item;
				                if (isCurrentSocketBroken)
				                {
					                Thread.Sleep(500);
				                }
				                else if (dataBCQ.TryTake(out item, 500) && !isCurrentSocketBroken)
				                {
					                byte[] array = CommonFunction.ObjectStringToBytes(item);
					                NetworkLogic.syncSend(rso.workSocket, array, 0, array.Length);
					                toClientLogger.log(2, rso.remoteIPPort + ": " + item);
				                }
			                }
		                }
		                catch (SocketException ex)
		                {
			                isCurrentSocketBroken = true;
			                doSocketBroken();
			                toClientLogger.log(1, ex.ToString());
		                }
		                catch (InvalidOperationException)
		                {
		                }
		                catch (Exception ex3)
		                {
			                toClientLogger.log(1, ex3.ToString());
			                if (!dataBCQ.IsCompleted)
			                {
				                sendData();
			                }
			                else
			                {
				                rso = null;
			                }
		                }
	                }

             *
             * 		            value.fireSendProc();
             * 
             */

            #endregion


            TT.Common.NLogUtility.Info($"Send to Clinet - {obj.MyToString()}");
            server.SendString(obj);
        }
    }
}
