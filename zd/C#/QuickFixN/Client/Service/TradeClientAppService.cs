using Client.FixUtility;
using Client.Models;
using Client.Utility;
using CommonClassLib;
using QuickFix;
using QuickFix.Fields;
using QuickFix.FIX44;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static QuickFix.FIX44.Advertisement;

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
                newOrderSingle.SecurityType = new SecurityType("fut");
                //454
                NoSecurityAltIDGroup noSecurityAltIDGroup = new NoSecurityAltIDGroup();
                //455
                noSecurityAltIDGroup.SecurityAltID = new SecurityAltID("BRN2009");
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
                ////Tag167
                //newOrderSingle.SecurityType = sd.SecurityType;
                ////Tag207
                //newOrderSingle.SecurityExchange = sd.SecurityExchange;


                //客户端用的是FIX 7X和 新TT的FIX版本 不一样
                //两个版本的OrderType值1和2反了
                string orderType = ZDUperTagValueConvert.ConvertToTTOrdType(orderInfo.priceType);

                char charOrdType = Char.Parse(orderType);
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

                if (!ret)
                {
                    order.SystemCode = netInfo.systemCode;
                    order.ClientID = newOrderSingle.ClOrdID.getValue();

                    order.NewOrderSingle = newOrderSingle;






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
            switch (message)
            {
                case QuickFix.FIX44.ExecutionReport executionReport:
                    //ExecutionReport?.Invoke(executionReport.ToString());
                    char execType = executionReport.ExecType.getValue();


                    switch (execType)
                    {
                        case ExecType.NEW:
                            netInfo = ExecType_New(executionReport);
                            break;

                        case ExecType.FILL:
                        case ExecType.PARTIAL_FILL:
                            //netInfo = replyFill(execReport);
                            break;

                        case ExecType.CANCELED:
                            //netInfo = replyCancelled(execReport);
                            break;

                        case ExecType.REJECTED:
                            //netInfo = replyRejected(execReport);
                            break;

                        case ExecType.PENDING_CANCEL:
                            //doPendingCancel(execReport);
                            break;

                        case ExecType.REPLACED:
                            //netInfo = replyReplaced(execReport);
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
                case QuickFix.FIX44.OrderCancelReject orderCancelReject:
                    break;
                case QuickFix.FIX44.News news:
                    break;
                case QuickFix.FIX44.BusinessMessageReject businessMessageReject:
                    break;
                default:
                    break;
            }
            var responseMsg = netInfo.MyToString();
            ExecutionReport?.Invoke(responseMsg);
        }

        #region ExecutionReport
        NetInfo ExecType_New(ExecutionReport execReport)
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

            NetInfo netInfo = new NetInfo();

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



            return netInfo;
        }
        #endregion

        #endregion

    }
}
