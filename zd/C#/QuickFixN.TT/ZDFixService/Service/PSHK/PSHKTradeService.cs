using CommonClassLib;
using QuickFix.Fields;
using QuickFix.FIX42;
using System;
using System.Text.RegularExpressions;
using ZDFixService.FixUtility;
using ZDFixService.Models;
using ZDFixService.Service.Base;
using ZDFixService.Service.MemoryDataManager;
using ZDFixService.Service.ZDCommon;
using static QuickFix.FIX42.NewOrderSingle;

namespace ZDFixService.Service.PSHK
{
    class PSHKTradeService : TradeClientAppService
    {
        private static readonly NLog.Logger _nLog = NLog.LogManager.GetCurrentClassLogger();

        protected override void NewOrderSingle(Order order)
        {
            NetInfo netInfo = order.OrderNetInfo;
            try
            {
                OrderInfo orderInfo = new OrderInfo();
                orderInfo.MyReadString(netInfo.infoT);

                string clOrdID = MemoryData.GetNextClOrderID().ToString();

                NewOrderSingle newOrderSingle = new NewOrderSingle();
                // Tag1  上手号
                newOrderSingle.Account = new Account(netInfo.accountNo);
                // Tag11
                newOrderSingle.ClOrdID = new ClOrdID(clOrdID);
                //tag109
                //newOrderSingle.ClientID = new ClientID(netInfo.clientNo);
                //tag21
                newOrderSingle.HandlInst = new HandlInst('1');
                //tag60
                newOrderSingle.TransactTime = new TransactTime(DateTime.UtcNow, true);
                // Tag55
                newOrderSingle.Symbol = new Symbol(ZDUperTagValueConvert.ConvertToPSHKCode(orderInfo.code));
                //var exchangeCode = orderInfo.exchangeCode;
                var exchangeCode = "PSHK";
                // Tag207
                newOrderSingle.SecurityExchange = new SecurityExchange(exchangeCode);
                //167
                //newOrderSingle.SecurityType = new SecurityType(SecurityType.COMMON_STOCK);


                //委托量
                var orderQty = decimal.Parse(orderInfo.orderNumber);
                // Tag38
                newOrderSingle.OrderQty = new OrderQty(orderQty);
                // Tag54
                newOrderSingle.Side = ZDUperTagValueConvert.QuerySide(orderInfo.buySale);

                //客户端用的是FIX 7X和 新TT的FIX版本 不一样
                char charOrdType = ZDUperTagValueConvert.ConverttoHKEXOrdType(orderInfo.priceType);
                // Tag40
                newOrderSingle.OrdType = new OrdType(charOrdType);// QueryOrdType(info.priceType);

                var price = decimal.Parse(orderInfo.orderPrice);
                // Tag44
                newOrderSingle.Price = new Price(price);

                //tag 59
                newOrderSingle.TimeInForce = ZDUperTagValueConvert.ConverttoHKEXTimeInForce(orderInfo.validDate);
                //永久有效
                if (orderInfo.validDate == "1")
                {
                    order.IsGTCOrder = true;
                }


                bool ret = TradeClient.Instance.SendMessage(newOrderSingle);
                if (ret)
                {
                    order.Pending = true;
                    order.SystemCode = netInfo.systemCode;
                    order.TempCliOrderID = clOrdID;
                    // newOrderSingle.FromString()
                    order.CreateNewOrderSingleTime = DateTime.Now;
                    //order.NewOrderSingle = newOrderSingle.ToString();
                    MemoryData.Orders.TryAdd(order.SystemCode, order);

                    var clOrdIDLong = long.Parse(clOrdID);
                    MemoryData.UsingCliOrderIDSystemCode.TryAdd(clOrdIDLong, order.SystemCode);

                }
                else
                {
                    throw new Exception("Server socket throw exception! Can not sent to uper!");
                }

            }
            catch (Exception ex)
            {
                //去掉汉字
                string msg = Regex.IsMatch(ex.Message, @"[\u4e00-\u9fa5]") ? "server exception" : $"server exception:{ex.Message}";
                netInfo.NewOrderSingleException(msg, netInfo.code);
                //ExecutionReport?.Invoke(netInfo?.MyToString());
                throw ex;
            }
        }


        /// <summary>
        /// 撤单
        /// </summary>
        /// <param name="netInfo"></param>
        protected override void OrderCancelRequest(NetInfo netInfo)
        {
            CancelInfo cancelInfo = new CancelInfo();
            cancelInfo.MyReadString(netInfo.infoT);
            Order order = null;
            try
            {
                if (!MemoryData.Orders.TryGetValue(netInfo.systemCode, out order))
                {
                    throw new Exception($"Can not find SystemCode - {netInfo.systemCode}");
                }
                if (order.NewOrderSingleClientID != cancelInfo.orderNo)
                {
                    throw new Exception($"SystemCode don't match orderNo .SystemCode- {netInfo.systemCode},orderNo - {cancelInfo.orderNo}");
                }
                if (order.Pending)
                {
                    throw new Exception($"Order  is pending .SystemCode- {netInfo.systemCode}");
                }
                order.CommandCode = netInfo.code;

                OrderInfo orderInfo = new OrderInfo();
                orderInfo.MyReadString(order.OrderNetInfo.infoT);

                QuickFix.FIX42.OrderCancelRequest orderCancelRequest = new QuickFix.FIX42.OrderCancelRequest();

                //NewOrderSingle newOrderSingle = new NewOrderSingle();
                //newOrderSingle.FromString(order.NewOrderSingle, false, null, null);



                //Tag 37
                orderCancelRequest.OrderID = new OrderID(order.OrderID);

                //Tag 11
                var clOrdID = MemoryData.GetNextClOrderID().ToString();
                orderCancelRequest.ClOrdID = new ClOrdID(clOrdID);
                //Tag 41
                orderCancelRequest.OrigClOrdID = new OrigClOrdID(order.CurrentCliOrderID.ToString());


                //傻x辉立还要发送这些tag。
                //tag55
                orderCancelRequest.Symbol = new Symbol(ZDUperTagValueConvert.ConvertToPSHKCode(orderInfo.code));
                // Tag54
                orderCancelRequest.Side = ZDUperTagValueConvert.QuerySide(orderInfo.buySale);
                //tag60
                orderCancelRequest.TransactTime = new TransactTime(DateTime.UtcNow, true);
                // Tag207
                //orderCancelRequest.SecurityExchange = new SecurityExchange(orderInfo.exchangeCode);
                orderCancelRequest.SecurityExchange = new SecurityExchange("PSHK");


                var ret = TradeClient.Instance.SendMessage(orderCancelRequest);

                if (ret)
                {
                    order.Pending = true;
                    order.TempCliOrderID = clOrdID;


                    var clOrdIDLong = long.Parse(clOrdID);
                    MemoryData.UsingCliOrderIDSystemCode.TryAdd(clOrdIDLong, order.SystemCode);

                }
                else
                {
                    throw new Exception("Server socket throw exception! Can not sent to uper!");
                }

            }
            catch (Exception ex)
            {
                //去掉汉字
                string msg = Regex.IsMatch(ex.Message, @"[\u4e00-\u9fa5]") ? "server exception" : $"server exception:{ex.Message}.";
                netInfo.OrderCancelRequestException(msg, cancelInfo.orderNo, netInfo.code);

                _nLog.Error($"SystemCode -  { netInfo.systemCode}");
                _nLog.Error(ex.ToString());
                throw ex;
            }

        }

        // PSHK的FIX没有改单
        protected override void OrderCancelReplaceRequest(NetInfo netInfo)
        {
            throw new NotImplementedException();
        }


        #region ExecutionReport

        #region  New
        protected override NetInfo ExecType_New(ExecutionReport execReport)
        {
            NetInfo netInfo = new NetInfo();
            try
            {
                var currentCliOrderID = execReport.ClOrdID.getValue();
                //var order = MemoryDataManager.Orders.Values.Where(p => p.TempCliOrderID == currentCliOrderID).FirstOrDefault();
                var order = MemoryData.GetOrderByCliOrderID(currentCliOrderID);
                order.Pending = false;
                order.OrderID = execReport.OrderID.getValue();
                order.NewOrderSingleClientID = currentCliOrderID;
                order.CurrentCliOrderID = currentCliOrderID;
                order.TempCliOrderID = "";

                OrderResponseInfo orderResponseInfo = new OrderResponseInfo();

                orderResponseInfo.orderNo = execReport.ClOrdID.getValue();

                string OrderID = execReport.OrderID.getValue();
                //info.origOrderNo = info.orderNo;
                //OrderID 是GUID,长度过长，改赋值ClOrdID
                orderResponseInfo.origOrderNo = orderResponseInfo.orderNo;
                //盘房和TT对单用，关联字段。
                if (execReport.IsSetField(Tags.SecondaryClOrdID))
                {
                    orderResponseInfo.origOrderNo = execReport.GetString(Tags.SecondaryClOrdID);
                }
                orderResponseInfo.orderMethod = "1";
                orderResponseInfo.htsType = "";

                string strSymbol = execReport.Symbol.getValue();
                //CodeBean codeBean = CodeTransfer_TT.getZDCodeInfoByUpperCode(execReport.SecurityID.getValue());

                //info.exchangeCode = codeBean.zdExchg;

                orderResponseInfo.exchangeCode = execReport.GetString(Tags.SecurityExchange);

                //if (execReport.Side.getValue() == Side.BUY)
                //    info.buySale = "1";
                //else
                //    info.buySale = "2";
                orderResponseInfo.buySale = ZDUperTagValueConvert.QuerySide(execReport.Side);
                orderResponseInfo.tradeType = "1";

                char ordType = execReport.OrdType.getValue();
                orderResponseInfo.priceType = ZDUperTagValueConvert.ConvertToZDOrdType(ordType.ToString());


                //if (ordType == OrdType.LIMIT || ordType == OrdType.STOP_LIMIT)
                //{
                //    info.orderPrice = CodeTransfer_TT.toClientPrx(execReport.Price.getValue(), strSymbol);
                //}

                //if (ordType == OrdType.STOP || ordType == OrdType.STOP_LIMIT)
                //{
                //    info.triggerPrice = CodeTransfer_TT.toClientPrx(execReport.StopPx.getValue(), strSymbol);
                //}


                orderResponseInfo.orderNumber = execReport.OrderQty.getValue().ToString();
                orderResponseInfo.filledNumber = "0";

                DateTime transTime = execReport.TransactTime.getValue();
                orderResponseInfo.orderTime = transTime.ToString("HH:mm:ss");
                orderResponseInfo.orderDate = transTime.ToString("yyyy-MM-dd");



                //info.validDate = ConvertToZDTimeInForce(execReport.TimeInForce.ToString());

                orderResponseInfo.validDate = ZDUperTagValueConvert.ConvertToZDTimeInForce(execReport.TimeInForce.ToString());


                orderResponseInfo.accountNo = order.OrderNetInfo.accountNo;
                orderResponseInfo.systemNo = order.OrderNetInfo.systemCode;

                OrderInfo orderInfo = new OrderInfo();
                orderInfo.MyReadString(order.OrderNetInfo.infoT);
                orderResponseInfo.acceptType = orderInfo.userType;
                orderResponseInfo.code = orderInfo.code;


                netInfo = order.OrderNetInfo.CloneWithNewCode(ErrorCode.SUCCESS, CommandCode.OrderStockHK);
                netInfo.infoT = orderResponseInfo.MyToString();

            }
            catch (Exception ex)
            {
                _nLog.Error(ex.ToString());
            }
            return netInfo;
        }
        #endregion

        #region  Replaced
        protected override NetInfo Replaced(QuickFix.FIX42.ExecutionReport execReport)
        {
            OrderResponseInfo orderResponseInfo = new OrderResponseInfo();


            var currentCliOrderID = execReport.ClOrdID.getValue();
            //var order = MemoryDataManager.Orders.Values.Where(p => p.TempCliOrderID == currentCliOrderID).FirstOrDefault();
            var order = MemoryData.GetOrderByCliOrderID(currentCliOrderID);
            order.Pending = false;
            order.OrderID = execReport.OrderID.getValue();
            order.CurrentCliOrderID = execReport.ClOrdID.getValue();
            order.TempCliOrderID = "";

            long origClOrdID = long.Parse(execReport.OrigClOrdID.getValue());
            MemoryData.UsingCliOrderIDSystemCode.TryRemove(origClOrdID, out _);



            OrderInfo orderInfo = new OrderInfo();
            orderInfo.MyReadString(order.OrderNetInfo.infoT);

            orderResponseInfo.exchangeCode = orderInfo.exchangeCode;

            orderResponseInfo.buySale = execReport.Side.getValue().ToString();
            orderResponseInfo.tradeType = "1";

            char ordType = execReport.OrdType.getValue();


            orderResponseInfo.priceType = ZDUperTagValueConvert.ConvertToZDOrdType(ordType.ToString());

            if (ordType == OrdType.LIMIT || ordType == OrdType.STOP_LIMIT)
            {
                orderResponseInfo.orderPrice = execReport.Price.getValue().ToString();
            }
            // changed by Rainer on 20150304 -begin
            //else if (ordType == OrdType.STOP || ordType == OrdType.STOP_LIMIT)
            if (ordType == OrdType.STOP || ordType == OrdType.STOP_LIMIT)
            {
                orderResponseInfo.triggerPrice = execReport.StopPx.getValue().ToString();
            }
            // changed by Rainer on 20150304 -end

            //  char tif = execReport.TimeInForce.getValue();
            //info.validDate = QueryValidDate(tif);

            //info.modifyNumber = execReport.OrderQty.getValue().ToString();
            orderResponseInfo.orderNumber = execReport.OrderQty.getValue().ToString();
            orderResponseInfo.filledNumber = "0";

            DateTime transTime = execReport.TransactTime.getValue();
            orderResponseInfo.orderTime = transTime.ToString("HH:mm:ss");
            orderResponseInfo.orderDate = transTime.ToString("yyyy-MM-dd");


            //info.validDate = ConvertToZDTimeInForce(execReport.TimeInForce.ToString());
            orderResponseInfo.validDate = ZDUperTagValueConvert.ConvertToZDTimeInForce(execReport.TimeInForce.ToString());
            orderResponseInfo.orderNo = order.NewOrderSingleClientID;
            orderResponseInfo.origOrderNo = execReport.ClOrdID.getValue();
            orderResponseInfo.code = orderInfo.code;

            //盘房和TT对单用，关联字段。
            if (execReport.IsSetField(Tags.SecondaryClOrdID))
            {
                orderResponseInfo.origOrderNo = execReport.GetString(Tags.SecondaryClOrdID);
            }

            NetInfo netInfo = order.OrderNetInfo.CloneWithNewCode(ErrorCode.SUCCESS, CommandCode.ModifyStockHK);
            netInfo.infoT = orderResponseInfo.MyToString();
            return netInfo;
        }
        #endregion

        #region Cancelled
        protected override NetInfo Cancelled(QuickFix.FIX42.ExecutionReport execReport)
        {
            CancelResponseInfo cancelResponseInfo = new CancelResponseInfo();

            var currentCliOrderID = execReport.ClOrdID.getValue();
            //var order = MemoryDataManager.Orders.Values.Where(p => p.TempCliOrderID == currentCliOrderID).FirstOrDefault();
            var order = MemoryData.GetOrderByCliOrderID(currentCliOrderID);
            order.Pending = false;
            MemoryData.Orders.TryRemove(order.SystemCode, out _);

            long clOrdID = long.Parse(currentCliOrderID);
            MemoryData.UsingCliOrderIDSystemCode.TryRemove(clOrdID, out _);


            OrderInfo orderInfo = new OrderInfo();
            orderInfo.MyReadString(order.OrderNetInfo.infoT);


            cancelResponseInfo.exchangeCode = orderInfo.exchangeCode;
            //if (execReport.Side.getValue() == Side.BUY)
            //    info.buySale = "1";
            //else
            //    info.buySale = "2";

            cancelResponseInfo.buySale = orderInfo.buySale;


            cancelResponseInfo.orderNo = order.NewOrderSingleClientID;
            //系统号
            cancelResponseInfo.accountNo = orderInfo.accountNo;
            cancelResponseInfo.systemNo = order.OrderNetInfo.systemCode;
            cancelResponseInfo.code = orderInfo.code;

            char ordType = execReport.OrdType.getValue();
            cancelResponseInfo.priceType = orderInfo.priceType;


            //info.orderPrice = orderInfo.orderPrice;

            cancelResponseInfo.cancelNumber = execReport.LeavesQty.ToString();
            cancelResponseInfo.orderNumber = execReport.OrderQty.ToString();
            //待兼容
            cancelResponseInfo.filledNumber = order.CumQty.ToString();
            cancelResponseInfo.cancelNo = cancelResponseInfo.orderNo;

            DateTime transTime = execReport.TransactTime.getValue();
            cancelResponseInfo.cancelTime = transTime.ToString("HH:mm:ss");
            cancelResponseInfo.cancelDate = transTime.ToString("yyyy-MM-dd");

            NetInfo netInfo = order.OrderNetInfo.CloneWithNewCode(ErrorCode.SUCCESS, CommandCode.CancelStockHK);
            netInfo.infoT = cancelResponseInfo.MyToString();

            return netInfo;
        }
        #endregion

        #region PartiallyFilledOrFilled
        protected override NetInfo PartiallyFilledOrFilled(QuickFix.FIX42.ExecutionReport execReport)
        {
            FilledResponseInfo filledResponseInfo = new FilledResponseInfo();

            if (execReport.IsSetExecTransType())
            {
                char execTransType = execReport.ExecTransType.getValue();
                if (execTransType == ExecTransType.CORRECT)
                {

                    return null;
                }
            }
            var currentCliOrderID = execReport.ClOrdID.getValue();
            var order = MemoryData.GetOrderByCliOrderID(currentCliOrderID);
            filledResponseInfo.exchangeCode = order.OrderNetInfo.exchangeCode;

            //if (execReport.Side.getValue() == Side.BUY)
            //    info.buySale = "1";
            //else
            //    info.buySale = "2";
            filledResponseInfo.buySale = execReport.Side.getValue().ToString();
            filledResponseInfo.filledNo = execReport.ExecID.getValue();
            filledResponseInfo.filledNumber = execReport.LastShares.ToString();
            filledResponseInfo.filledPrice = execReport.LastPx.getValue().ToString();



            DateTime transTime = execReport.TransactTime.getValue();
            filledResponseInfo.filledTime = transTime.ToString("HH:mm:ss");
            filledResponseInfo.filledDate = transTime.ToString("yyyy-MM-dd");

            int multiLegReportingType = 1;

            // 1 = Outright;2 = Leg of spread;3 = Spread
            if (execReport.IsSetMultiLegReportingType())
            {
                multiLegReportingType = execReport.GetInt(Tags.MultiLegReportingType);
            }

            //系统号
            //RefObj refObj;
            //bool ret = _downReference.TryGetValue(execReport.OrderID.getValue(), out refObj);
            //if (!ret)
            //{
            //    TT.Common.NLogUtility.Error($"订单成交返回，ClOrdID:{execReport.ClOrdID.getValue()}内存数据丢失");
            //    return null;
            //}



            if (multiLegReportingType == 1 || multiLegReportingType == 3)
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
            filledResponseInfo.orderNo = order.NewOrderSingleClientID;
            filledResponseInfo.accountNo = order.OrderNetInfo.accountNo;
            filledResponseInfo.systemNo = order.OrderNetInfo.systemCode;
            filledResponseInfo.code = order.OrderNetInfo.code;

            //NetInfo netInfo = new NetInfo();
            //netInfo.infoT = filledResponseInfo.MyToString();
            //netInfo.systemCode = order.OrderNetInfo.systemCode;
            //netInfo.exchangeCode = order.OrderNetInfo.exchangeCode;
            //netInfo.errorCode = ErrorCode.SUCCESS;
            //netInfo.code = CommandCode.FILLEDCAST;
            //netInfo.accountNo = filledResponseInfo.accountNo;
            ////obj.todayCanUse = execReport.Header.GetField(Tags.SenderSubID);
            //netInfo.todayCanUse = order.OrderNetInfo.todayCanUse;


            NetInfo netInfo = order.OrderNetInfo.CloneWithNewCode(ErrorCode.SUCCESS, CommandCode.FilledStockHK);
            netInfo.infoT = filledResponseInfo.MyToString();

            if (multiLegReportingType == 1)//FUT
            {

                if (execReport.LeavesQty.getValue() == 0)
                {
                    MemoryData.Orders.TryRemove(order.SystemCode, out _);

                    long clOrdID = long.Parse(currentCliOrderID);
                    MemoryData.UsingCliOrderIDSystemCode.TryRemove(clOrdID, out _);

                }
            }
            else if (multiLegReportingType == 2)// multi-leg 
            {
                QuickFix.Group g2 = execReport.GetGroup(2, Tags.NoSecurityAltID);
                //BRN Jul19
                var securityAltID = g2.GetString(Tags.SecurityAltID);
                var securityExchange = execReport.SecurityExchange.getValue();

                netInfo.infoT = filledResponseInfo.MyToString();

            }
            else if (multiLegReportingType == 3)
            {
                // Can not clear because each leg execution will follow
                //if (execReport.LeavesQty.getValue() == 0)
                //    xReference.TryRemove(clOrdID, out refObj);
            }

            return netInfo;
        }

        #endregion



        #endregion
    }
}
