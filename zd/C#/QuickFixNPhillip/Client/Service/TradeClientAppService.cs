using Client.FixUtility;
using Client.Models;
using Client.Utility;
using CommonClassLib;
using QuickFix;
using QuickFix.Fields;
using QuickFix.FIX42;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static QuickFix.FIX42.Advertisement;
using static QuickFix.FIX42.NewOrderSingle;

namespace Client.Service
{
    public class TradeClientAppService
    {
        #region 私有成员
        private static readonly NLog.Logger _nLog = NLog.LogManager.GetCurrentClassLogger();
        private BlockingCollection<QuickFix.Message> _receiveFromAppMsgs = new BlockingCollection<QuickFix.Message>();
        private BlockingCollection<object> _orders = new BlockingCollection<object>();
        #endregion

        #region 公有成员
        public static TradeClientAppService Instance { get; private set; }
        public event Action<string> ExecutionReport;
        public event Action<string> Logon;
        public event Action<string> LogonOut;
        #endregion

        static TradeClientAppService()
        {
            Instance = new TradeClientAppService();
        }

        private TradeClientAppService()
        {
            TradeClient.Instance.Logon += (msg =>
            {
                Logon?.Invoke(msg);

            });

            TradeClient.Instance.LogOut += (msg =>
            {
                LogonOut?.Invoke(msg);
            });

            TradeClient.Instance.ReceiveMsgFromApp += (message, sessionID) =>
            {
                if (!_receiveFromAppMsgs.TryAdd(message, 1000))
                {
                    //异常
                }

            };

            RedisQueue<Order>.Instance.DequeueRedis += (order) =>
              {
                  ConsumerOrders(order);
              };



            Task.Run(() =>
            {
                ConsumerFromAppMsgs();
            });
        }



        public void Connect()
        {
            TradeClient.Instance.SocketInitiator.Start();
        }

        public void Disnnect()
        {
            TradeClient.Instance.SocketInitiator.Stop();
            WaitForAdding();
        }

        private void WaitForAdding()
        {
            //为了避免异常--未扔到交易所的单在内存就丢失
            while (!_orders.IsAddingCompleted && !_receiveFromAppMsgs.IsAddingCompleted)
            {
                //所有的单据不送交易所处理。
            }
        }

        public void Order(NetInfo netInfo)
        {
            Order order = new Order();
            if (netInfo.code == CommandCode.ORDER)
            {
                order.OrderNetInfo = netInfo;
            }
            else if (netInfo.code == CommandCode.MODIFY)
            {
                order.AmendNetInfo = netInfo;
            }
            else if (netInfo.code == CommandCode.CANCEL)
            {
                order.CancelNetInfo = netInfo;
            }

            RedisQueue<Order>.Instance.EnqueueRedis(order);
        }

        private void ConsumerOrders(Order order)
        {

            if (order.OrderNetInfo.code == CommandCode.ORDER)
            {
                NewOrderSingle(order);
            }
            else if (order.OrderNetInfo.code == CommandCode.MODIFY)
            {

            }
            else if (order.OrderNetInfo.code == CommandCode.CANCEL)
            {

            }
        }

        private void NewOrderSingle(Order order)
        {
            try
            {

                NetInfo netInfo = order.OrderNetInfo;
                OrderInfo orderInfo = new OrderInfo();
                orderInfo.MyReadString(netInfo.infoT);

         

                long clOrdID = RedisHelper.GetNextClOrderID();

        


                NewOrderSingle newOrderSingle = new NewOrderSingle();
                // Tag11
                newOrderSingle.ClOrdID = new ClOrdID(clOrdID.ToString());
                //// Tag60
                //newOrderSingle.TransactTime = new TransactTime(DateTime.UtcNow);
                var securityExchange = orderInfo.exchangeCode;

                #region When specifying by alternate security ID
                //
                var newCode = orderInfo.code;
                //var securityType = TTMarketAdapterCommon.GetSecurityType(orderInfo.code);
                //if (securityType == SecurityTypeEnum.OPT)
                //{
                //    //newCode = CompatibleOpenInterestContract.ConvertToNewTTContract(info.code);
                //    CompatibleOptionCodeConverter.IsCompatibleOption(orderInfo.code, ref newCode);
                //}

                //TTMarketAdapter.Model.OrderModel orderModel = TTMarketAdapterCommon.GetOrderModel(newCode);
                //var validate = orderModel.Validate();
                //if (!validate.Success)
                //{
                //    OrderException(netInfo, validate.ErrorMessage);
                //    return;
                //}
                // Tag55
                //newOrderSingle.Symbol = sd.Symbol;
                newOrderSingle.Symbol = new Symbol("BRN");
                // Tag207
                newOrderSingle.SecurityExchange = new SecurityExchange("ICE");

                //167
                newOrderSingle.SecurityType = new SecurityType("FUT");
                //454
                NoSecurityAltIDGroup noSecurityAltIDGroup = new NoSecurityAltIDGroup();
                //455
                noSecurityAltIDGroup.SecurityAltID = new SecurityAltID("BRN Dec20");
                //456
                noSecurityAltIDGroup.SecurityAltIDSource = new SecurityAltIDSource("97");
                newOrderSingle.AddGroup(noSecurityAltIDGroup);
                #endregion


                //委托量
                var orderQty = decimal.Parse(orderInfo.orderNumber);
                // Tag38
                newOrderSingle.OrderQty = new OrderQty(orderQty);
                // Tag54
                newOrderSingle.Side = ZDUperTagValueConvert.QuerySide(orderInfo.buySale);



                //客户端用的是FIX 7X和 新TT的FIX版本 不一样
                //两个版本的OrderType值1和2反了
                string orderType = ZDUperTagValueConvert.ConvertToTTOrdType(orderInfo.priceType);

                char charOrdType = char.Parse(orderType);
                // Tag40
                newOrderSingle.OrdType = new OrdType(charOrdType);// QueryOrdType(info.priceType);

                var price = decimal.Parse(orderInfo.orderPrice);
                // Tag44
                if (charOrdType == OrdType.LIMIT || charOrdType == OrdType.STOP_LIMIT)
                {
                    //decimal prx = CodeTransfer_TT.toGlobexPrx(orderInfo.orderPrice, newOrderSingle.Symbol.getValue());
                    newOrderSingle.Price = new Price(price);
                }

                // Tag99
                if (charOrdType == OrdType.STOP || charOrdType == OrdType.STOP_LIMIT)
                {
                    //decimal prx = CodeTransfer_TT.toGlobexPrx(orderInfo.triggerPrice, newOrderSingle.Symbol.getValue());
                    newOrderSingle.StopPx = new StopPx(price);
                }
                //tag 77
                //newOrderSingle.OpenClose = new OpenClose('O');
                // Tag11028
                //newOrderSingle.ManualOrderIndicator = moi;

                string timeInForce = orderInfo.validDate;


                timeInForce = ZDUperTagValueConvert.ConvertToTTTimeInForce(timeInForce);
                /*
                 * CME官网：Fill and Kill (FAK) and Fill or Kill (FOK) - 
                 *          order is immediately executed against any available quantity and any remaining quantity is eliminated (FAK)
                 *          or order is filled completely or else eliminated (FOK).
                 * FOK:要么都成交，要么都撤销。(TT-FOK = CME-FAK)
                 * IOC：成交剩余的部分被撤销。(TT-IOC = CME-FOK)
                 */
                if (timeInForce == "3")//IOC
                {
                    //根据MinQty和订单数量大小判断是FOK还是IOC
                    //FOK
                    if (orderInfo.MinQty == orderInfo.orderNumber)
                    {
                        timeInForce = "4";//FOK
                    }
                    //IOC:info.MinQty < info.orderNumber
                    if (!string.IsNullOrEmpty(orderInfo.MinQty) && orderInfo.MinQty != "0")
                        newOrderSingle.SetField(new MinQty(decimal.Parse(orderInfo.MinQty)));
                }

                //tag 59
                newOrderSingle.TimeInForce = new TimeInForce(char.Parse(timeInForce));

                // Tag1  上手号
                newOrderSingle.Account = new Account(netInfo.accountNo);

                // SenderSubID(Tag50 ID)
                //newOrderSingle.SetField(new SenderSubID(obj.todayCanUse));

                ////Tag582
                //newOrderSingle.CustOrderCapacity = new CustOrderCapacity(4);




                bool ret = TradeClient.Instance.SendMessage(newOrderSingle);
                StopwatchHelper.Instance.Stop();
                _nLog.Info($"Send - {StopwatchHelper.Instance.Stopwatch.ElapsedMilliseconds}");

                if (ret)
                {
                    order.SystemCode = netInfo.systemCode;
                    order.ClientID = newOrderSingle.ClOrdID.getValue();
                    // newOrderSingle.FromString()
                    order.NewOrderSingle = newOrderSingle.ToString();



                    order.NewOrderSingleClientID = order.ClientID;
                    RedisHelper.SetCurrentClientOrderIDAndSysytemCode(order.SystemCode, order.ClientID, order.ClientID);
                    RedisHelper.SaveOrder(order);
                    //OrderException(netInfo, "can not connect to TT server!");
                }

            }
            catch (Exception ex)
            {
                //去掉汉字
                //string msg = Regex.IsMatch(ex.Message, @"[\u4e00-\u9fa5]") ? "server exception" : $"server exception:{ex.Message}";
                //OrderException(netInfo, msg);

            }
        }


        /// <summary>
        /// 撤单
        /// </summary>
        /// <param name="netInfo"></param>
        public void OrderCancelRequest(NetInfo netInfo)
        {

            try
            {

                CancelInfo cancelInfo = new CancelInfo();
                cancelInfo.MyReadString(netInfo.infoT);



                string currentCliOrderID = RedisHelper.GetCurrentClientOrderID(netInfo.systemCode, cancelInfo.orderNo);

                var order = RedisHelper.GetOrdder(currentCliOrderID);


                QuickFix.FIX42.OrderCancelRequest orderCancelRequest = new QuickFix.FIX42.OrderCancelRequest();

                NewOrderSingle newOrderSingle = new NewOrderSingle();
                newOrderSingle.FromString(order.NewOrderSingle, false, null, null);
                //Tag 37
                orderCancelRequest.OrderID = new OrderID(newOrderSingle.GetString(Tags.OrderID));

                //Tag 11
                //long clOrdID = ClOrderIDGen.getNextClOrderID();
                orderCancelRequest.ClOrdID = new ClOrdID(RedisHelper.GetNextClOrderID().ToString());


                //Tag 41
                orderCancelRequest.OrigClOrdID = new OrigClOrdID(currentCliOrderID);

                var ret = TradeClient.Instance.SendMessage(orderCancelRequest);

                if (!ret)
                {

                }


            }
            catch (Exception ex)
            {
                //去掉汉字
                string msg = Regex.IsMatch(ex.Message, @"[\u4e00-\u9fa5]") ? "server exception" : $"server exception:{ex.Message}";

            }

        }


        /// <summary>
        /// 改单
        /// 
        /// rainer 壳调用改单入口
        /// </summary>
        /// <param name="netInfo"></param>
        public void OrderCancelReplaceRequest(NetInfo netInfo)
        {

            try
            {

                ModifyInfo modifyInfo = new ModifyInfo();
                modifyInfo.MyReadString(netInfo.infoT);

                string currentCliOrderID = RedisHelper.GetCurrentClientOrderID(netInfo.systemCode, modifyInfo.orderNo);

                var order = RedisHelper.GetOrdder(currentCliOrderID);

                QuickFix.FIX42.OrderCancelReplaceRequest ocrr = new QuickFix.FIX42.OrderCancelReplaceRequest();

                NewOrderSingle newOrderSingle = new NewOrderSingle();
                newOrderSingle.FromString(order.NewOrderSingle, false, null, null);
                //Tag 37
                ocrr.OrderID = new OrderID(newOrderSingle.GetString(Tags.OrderID));



                //Tag 41
                ocrr.OrigClOrdID = new OrigClOrdID(currentCliOrderID);
                //Tag 11

                ocrr.ClOrdID = new ClOrdID(RedisHelper.GetNextClOrderID().ToString());
                // Tag1
                ocrr.Account = new Account(netInfo.accountNo);
                // Tag38
                ocrr.OrderQty = new OrderQty(decimal.Parse(modifyInfo.modifyNumber));
                // Tag54
                ocrr.Side = newOrderSingle.Side;




                // Tag40 ,不能用QueryOrdType(info.priceType);方法，有的客户端LME交易所不传值
                ocrr.OrdType = newOrderSingle.OrdType;
                modifyInfo.priceType = newOrderSingle.OrdType.ToString();

                char ordType = Char.Parse(modifyInfo.priceType);
                string symbol = newOrderSingle.Symbol.getValue();


                var price = decimal.Parse(modifyInfo.orderPrice);
                // Tag44
                if (ordType == OrdType.LIMIT || ordType == OrdType.STOP_LIMIT)
                {
                    //decimal prx = CodeTransfer_TT.toGlobexPrx(orderInfo.orderPrice, newOrderSingle.Symbol.getValue());
                    ocrr.Price = new Price(price);
                }

                // Tag99
                if (ordType == OrdType.STOP || ordType == OrdType.STOP_LIMIT)
                {
                    //decimal prx = CodeTransfer_TT.toGlobexPrx(orderInfo.triggerPrice, newOrderSingle.Symbol.getValue());
                    ocrr.StopPx = new StopPx(price);
                }

                //tag 77
                ocrr.OpenClose = new OpenClose('O');
                //tag 59
                ocrr.TimeInForce = newOrderSingle.TimeInForce;






                var ret = TradeClient.Instance.SendMessage(ocrr);

                if (ret)
                {

                    order.SystemCode = netInfo.systemCode;
                    order.ClientID = ocrr.ClOrdID.getValue();
                    order.OrderCancelReplaceRequest = ocrr.ToString();


                    RedisHelper.DeleteCurrentClientOrderIDAndSysytemCode(netInfo.systemCode, modifyInfo.orderNo);
                    RedisHelper.DeleteOrder(currentCliOrderID);

                    RedisHelper.SetCurrentClientOrderIDAndSysytemCode(order.SystemCode, order.ClientID, order.ClientID);
                    RedisHelper.SaveOrder(order);
                }




            }
            catch (Exception ex)
            {
                //去掉汉字
                string msg = Regex.IsMatch(ex.Message, @"[\u4e00-\u9fa5]") ? "server exception" : $"server exception:{ex.Message}";
                //obj.errorMsg = msg;

            }
        }


        #region FromAppMsg
        private void ConsumerFromAppMsgs()
        {
            try
            {
                foreach (var message in _receiveFromAppMsgs.GetConsumingEnumerable())
                {

                    ConsumerFromAppMsg(message);
                }
            }
            catch (Exception ex)
            {
                _nLog.Info(ex.ToString());
            }
        }

        private void ConsumerFromAppMsg(QuickFix.Message message)
        {
            NetInfo netInfo = null;
            try
            {
                switch (message)
                {
                    case ExecutionReport executionReport:
                        //ExecutionReport?.Invoke(executionReport.ToString());
                        char execType = executionReport.ExecType.getValue();


                        switch (execType)
                        {
                            case ExecType.NEW:
                                netInfo = ExecType_New(executionReport);
                                break;

                            case ExecType.FILL:
                            case ExecType.PARTIAL_FILL:
                                netInfo = PartiallyFilledOrFilled(executionReport);
                                break;

                            case ExecType.CANCELED:
                                netInfo = Cancelled(executionReport);
                                break;

                            case ExecType.REJECTED:
                                netInfo = ExecType_Rejected(executionReport);
                                break;

                            case ExecType.PENDING_CANCEL:
                                //doPendingCancel(execReport);
                                break;

                            case ExecType.REPLACED:
                                netInfo = Replaced(executionReport);
                                break;

                            case ExecType.EXPIRED:
                                //netInfo = doExpired(execReport);
                                //netInfo = replyCancelled(execReport);
                                break;
                            default:
                                break;
                                //case GlobexExt.ORD_STATUS_TRADE_CANCELLATION:
                                //    break;
                        }
                        break;
                    case OrderCancelReject orderCancelReject:
                        break;
                    case News news:
                        break;
                    case BusinessMessageReject businessMessageReject:
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {

            }
            var responseMsg = netInfo != null ? netInfo?.MyToString() : "";
 
            ExecutionReport?.Invoke(responseMsg);
        }

        #region ExecutionReport
        NetInfo ExecType_New(ExecutionReport execReport)
        {
            NetInfo netInfo = new NetInfo();
            try
            {
                //系统号
                var order = RedisHelper.GetOrdder(execReport.ClOrdID.getValue());
                OrderResponseInfo info = new OrderResponseInfo();

                info.orderNo = execReport.ClOrdID.getValue();

                string OrderID = execReport.OrderID.getValue();
                //info.origOrderNo = info.orderNo;
                //OrderID 是GUID,长度过长，改赋值ClOrdID
                info.origOrderNo = info.orderNo;
                //盘房和TT对单用，关联字段。
                if (execReport.IsSetField(Tags.SecondaryClOrdID))
                {
                    info.origOrderNo = execReport.GetString(Tags.SecondaryClOrdID);
                }
                info.orderMethod = "1";
                info.htsType = "";

                string strSymbol = execReport.Symbol.getValue();
                //CodeBean codeBean = CodeTransfer_TT.getZDCodeInfoByUpperCode(execReport.SecurityID.getValue());

                //info.exchangeCode = codeBean.zdExchg;

                info.exchangeCode = execReport.GetString(Tags.SecurityExchange);

                //if (execReport.Side.getValue() == Side.BUY)
                //    info.buySale = "1";
                //else
                //    info.buySale = "2";
                info.buySale = ZDUperTagValueConvert.QuerySide(execReport.Side);
                info.tradeType = "1";

                char ordType = execReport.OrdType.getValue();
                info.priceType = ZDUperTagValueConvert.ConvertToZDOrdType(ordType.ToString());


                //if (ordType == OrdType.LIMIT || ordType == OrdType.STOP_LIMIT)
                //{
                //    info.orderPrice = CodeTransfer_TT.toClientPrx(execReport.Price.getValue(), strSymbol);
                //}

                //if (ordType == OrdType.STOP || ordType == OrdType.STOP_LIMIT)
                //{
                //    info.triggerPrice = CodeTransfer_TT.toClientPrx(execReport.StopPx.getValue(), strSymbol);
                //}


                info.orderNumber = execReport.OrderQty.getValue().ToString();
                info.filledNumber = "0";

                DateTime transTime = execReport.TransactTime.getValue();
                info.orderTime = transTime.ToString("HH:mm:ss");
                info.orderDate = transTime.ToString("yyyy-MM-dd");



                //info.validDate = ConvertToZDTimeInForce(execReport.TimeInForce.ToString());

                info.validDate = ZDUperTagValueConvert.ConvertToZDTimeInForce(execReport.TimeInForce.ToString());

                //refObj.orderID = OrderID;
                //refObj.addGlobexRes(execReport);



                info.code = order.OrderNetInfo.code;
                info.accountNo = order.OrderNetInfo.accountNo;
                info.systemNo = order.OrderNetInfo.systemCode;

                OrderInfo orderInfo = new OrderInfo();
                orderInfo.MyReadString(order.OrderNetInfo.infoT);
                info.acceptType = orderInfo.userType;



                netInfo.infoT = info.MyToString();
                netInfo.exchangeCode = info.exchangeCode;
                netInfo.errorCode = ErrorCode.SUCCESS;
                netInfo.code = CommandCode.ORDER;

                netInfo.accountNo = info.accountNo;
                netInfo.systemCode = info.systemNo;
                //obj.todayCanUse = execReport.Header.GetField(Tags.TargetSubID);
                netInfo.todayCanUse = order.OrderNetInfo.todayCanUse;
                netInfo.clientNo = order.OrderNetInfo.clientNo;
                netInfo.localSystemCode = order.OrderNetInfo.localSystemCode;


            }
            catch (Exception ex)
            {

            }
            return netInfo;
        }

        public NetInfo Replaced(QuickFix.FIX42.ExecutionReport execReport)
        {
            OrderResponseInfo info = new OrderResponseInfo();

            var order = RedisHelper.GetOrdder(execReport.ClOrdID.getValue());



            ModifyInfo modifyInfo = new ModifyInfo();
            modifyInfo.MyReadString(order.AmendNetInfo.infoT);

            info.exchangeCode = modifyInfo.exchangeCode;

            info.buySale = execReport.Side.getValue().ToString();
            info.tradeType = "1";

            char ordType = execReport.OrdType.getValue();


            info.priceType = ZDUperTagValueConvert.ConvertToZDOrdType(ordType.ToString());

            if (ordType == OrdType.LIMIT || ordType == OrdType.STOP_LIMIT)
            {
                info.orderPrice = execReport.Price.getValue().ToString();
            }
            // changed by Rainer on 20150304 -begin
            //else if (ordType == OrdType.STOP || ordType == OrdType.STOP_LIMIT)
            if (ordType == OrdType.STOP || ordType == OrdType.STOP_LIMIT)
            {
                info.triggerPrice = execReport.StopPx.getValue().ToString();
            }
            // changed by Rainer on 20150304 -end

            //  char tif = execReport.TimeInForce.getValue();
            //info.validDate = QueryValidDate(tif);

            //info.modifyNumber = execReport.OrderQty.getValue().ToString();
            info.orderNumber = execReport.OrderQty.getValue().ToString();
            info.filledNumber = "0";

            DateTime transTime = execReport.TransactTime.getValue();
            info.orderTime = transTime.ToString("HH:mm:ss");
            info.orderDate = transTime.ToString("yyyy-MM-dd");


            //info.validDate = ConvertToZDTimeInForce(execReport.TimeInForce.ToString());
            info.validDate = ZDUperTagValueConvert.ConvertToZDTimeInForce(execReport.TimeInForce.ToString());
            info.orderNo = modifyInfo.orderNo;
            info.origOrderNo = execReport.ClOrdID.getValue();
            //盘房和TT对单用，关联字段。
            if (execReport.IsSetField(Tags.SecondaryClOrdID))
            {
                info.origOrderNo = execReport.GetString(Tags.SecondaryClOrdID);
            }





            NetInfo netInfo = new NetInfo();

            netInfo.infoT = info.MyToString();
            netInfo.exchangeCode = info.exchangeCode;
            netInfo.errorCode = ErrorCode.SUCCESS;
            netInfo.code = CommandCode.MODIFY;
            netInfo.accountNo = info.accountNo;
            netInfo.systemCode = info.systemNo;
            //obj.todayCanUse = execReport.Header.GetField(Tags.TargetSubID);
            netInfo.todayCanUse = order.AmendNetInfo.todayCanUse;
            netInfo.clientNo = order.AmendNetInfo.clientNo;



            return netInfo;
        }

        public NetInfo Cancelled(QuickFix.FIX42.ExecutionReport execReport)
        {
            CancelResponseInfo info = new CancelResponseInfo();

            var order = RedisHelper.GetOrdder(execReport.OrigClOrdID.getValue());

            CancelInfo cancelInfo = new CancelInfo();
            cancelInfo.MyReadString(order.CancelNetInfo.infoT);

            info.exchangeCode = order.CancelNetInfo.exchangeCode;
            //if (execReport.Side.getValue() == Side.BUY)
            //    info.buySale = "1";
            //else
            //    info.buySale = "2";

            info.buySale = cancelInfo.buySale;


            info.orderNo = cancelInfo.orderNo;
            //系统号
            info.accountNo = order.CancelNetInfo.accountNo;
            info.systemNo = cancelInfo.systemNo;
            info.code = order.CancelNetInfo.code;

            char ordType = execReport.OrdType.getValue();
            info.priceType = cancelInfo.priceType;


            info.orderPrice = cancelInfo.orderPrice;

            info.cancelNumber = execReport.LeavesQty.ToString();
            info.orderNumber = execReport.OrderQty.ToString();
            //待兼容
            info.filledNumber = order.CumQty.ToString();
            info.cancelNo = info.orderNo;

            DateTime transTime = execReport.TransactTime.getValue();
            info.cancelTime = transTime.ToString("HH:mm:ss");
            info.cancelDate = transTime.ToString("yyyy-MM-dd");

            NetInfo obj = new NetInfo();
            obj.infoT = info.MyToString();
            obj.exchangeCode = info.exchangeCode;
            obj.errorCode = ErrorCode.SUCCESS;
            obj.code = CommandCode.CANCELCAST;
            obj.accountNo = info.accountNo;
            obj.systemCode = info.systemNo;
            //obj.todayCanUse = execReport.Header.GetField(Tags.TargetSubID);
            obj.todayCanUse = order.CancelNetInfo.todayCanUse;
            obj.clientNo = order.CancelNetInfo.clientNo;


            RedisHelper.DeleteCurrentClientOrderIDAndSysytemCode(order.OrderNetInfo.systemCode, execReport.OrigClOrdID.getValue());
            RedisHelper.DeleteOrder(execReport.OrigClOrdID.getValue());
            return obj;
        }


        public NetInfo PartiallyFilledOrFilled(QuickFix.FIX42.ExecutionReport execReport)
        {
            FilledResponseInfo info = new FilledResponseInfo();

            if (execReport.IsSetExecTransType())
            {
                char execTransType = execReport.ExecTransType.getValue();
                if (execTransType == ExecTransType.CORRECT)
                {

                    return null;
                }
            }
            var order = RedisHelper.GetOrdder(execReport.ClOrdID.getValue());
            info.exchangeCode = order.OrderNetInfo.exchangeCode;

            //if (execReport.Side.getValue() == Side.BUY)
            //    info.buySale = "1";
            //else
            //    info.buySale = "2";
            info.buySale = execReport.Side.getValue().ToString();
            info.filledNo = execReport.ExecID.getValue();
            info.filledNumber = execReport.LastShares.ToString();
            info.filledPrice = execReport.LastPx.getValue().ToString();



            DateTime transTime = execReport.TransactTime.getValue();
            info.filledTime = transTime.ToString("HH:mm:ss");
            info.filledDate = transTime.ToString("yyyy-MM-dd");

            int mReportType = 1;

            // 1 = Outright;2 = Leg of spread;3 = Spread
            if (execReport.IsSetMultiLegReportingType())
            {
                mReportType = execReport.GetInt(Tags.MultiLegReportingType);
            }

            //系统号
            //RefObj refObj;
            //bool ret = _downReference.TryGetValue(execReport.OrderID.getValue(), out refObj);
            //if (!ret)
            //{
            //    TT.Common.NLogUtility.Error($"订单成交返回，ClOrdID:{execReport.ClOrdID.getValue()}内存数据丢失");
            //    return null;
            //}



            if (mReportType == 1 || mReportType == 3)
            {
                decimal cumQty = execReport.CumQty.getValue();
                if (order.CumQty + execReport.LastShares.getValue() != cumQty)
                {

                    return null;
                }

                order.CumQty = cumQty;
            }
            OrderInfo orderInfo = new OrderInfo();
          
            orderInfo.MyReadString(order.OrderNetInfo.infoT);
            info.orderNo = order.NewOrderSingleClientID;
            info.accountNo = order.OrderNetInfo.accountNo;
            info.systemNo = order.OrderNetInfo.systemCode;
            info.code = order.OrderNetInfo.code;

            NetInfo obj = new NetInfo();
            obj.infoT = info.MyToString();
            obj.exchangeCode = info.exchangeCode;
            obj.errorCode = ErrorCode.SUCCESS;
            obj.code = CommandCode.FILLEDCAST;
            obj.accountNo = info.accountNo;
            //obj.todayCanUse = execReport.Header.GetField(Tags.SenderSubID);
            obj.todayCanUse = order.OrderNetInfo.todayCanUse;


            if (mReportType == 1)//FUT
            {
               
                if (execReport.LeavesQty.getValue() == 0)
                {
                    RedisHelper.DeleteCurrentClientOrderIDAndSysytemCode(order.OrderNetInfo.systemCode, execReport.OrigClOrdID.getValue());
                    RedisHelper.DeleteOrder(execReport.OrigClOrdID.getValue());
                }
            }
            else if (mReportType == 2)// multi-leg 
            {
                QuickFix.Group g2 = execReport.GetGroup(2, Tags.NoSecurityAltID);
                //BRN Jul19
                var securityAltID = g2.GetString(Tags.SecurityAltID);
                var securityExchange = execReport.SecurityExchange.getValue();
   
                obj.infoT = info.MyToString();
          
            }
            else if (mReportType == 3)
            {
                // Can not clear because each leg execution will follow
                //if (execReport.LeavesQty.getValue() == 0)
                //    xReference.TryRemove(clOrdID, out refObj);
            }

            return obj;
        }

        NetInfo ExecType_Rejected(ExecutionReport executionReport)
        {
            NetInfo netInfo = new NetInfo();
            var msgType = executionReport.GetString(Tags.MsgType);
            var order = RedisHelper.GetOrdder(executionReport.ClOrdID.getValue());
            if (msgType == "D")
            {
                netInfo = order.OrderNetInfo;
                netInfo.errorCode = ErrorCode.ERR_ORDER_0000;
            }
            else if (msgType == "G")
            {
                netInfo = order.AmendNetInfo;
                netInfo.errorCode = ErrorCode.ERR_ORDER_0016;
            }
            else if (msgType == "F")
            {
                netInfo = order.CancelNetInfo;
                netInfo.errorCode = ErrorCode.ERR_ORDER_0014;
            }

            netInfo.errorMsg = executionReport.GetString(Tags.Text); ;


            return netInfo;
        }
        #endregion

        #endregion

    }
}
