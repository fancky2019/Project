﻿using CommonClassLib;
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

namespace ZDFixService.Service.SubService.TT
{
    class TTTradeService : TradeClientAppService
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
                // Tag11
                newOrderSingle.ClOrdID = new ClOrdID(clOrdID);
                //// Tag60
                //newOrderSingle.TransactTime = new TransactTime(DateTime.UtcNow);
                var securityExchange = orderInfo.exchangeCode;

                #region When specifying by alternate security ID
                //
                var newCode = orderInfo.code;

                //简单测试使用，没有集成TT的业务逻辑
                var symbol = orderInfo.code.Split(' ')[0];
                // Tag55
                newOrderSingle.Symbol = new Symbol(symbol);
                // Tag207
                newOrderSingle.SecurityExchange = new SecurityExchange(orderInfo.exchangeCode);

                //167
                newOrderSingle.SecurityType = new SecurityType("FUT");
                //454
                NoSecurityAltIDGroup noSecurityAltIDGroup = new NoSecurityAltIDGroup();
                //455
                noSecurityAltIDGroup.SecurityAltID = new SecurityAltID(orderInfo.code);

                //PRICE:9596
                //// Tag55
                //newOrderSingle.Symbol = new Symbol("FDAX");
                //// Tag207
                //newOrderSingle.SecurityExchange = new SecurityExchange("Eurex");

                ////167
                //newOrderSingle.SecurityType = new SecurityType("FUT");
                ////454
                //NoSecurityAltIDGroup noSecurityAltIDGroup = new NoSecurityAltIDGroup();
                ////455
                //noSecurityAltIDGroup.SecurityAltID = new SecurityAltID("FDAX Dec20");


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

                var stopPx = decimal.Parse(orderInfo.triggerPrice);
                // Tag99
                if (charOrdType == OrdType.STOP || charOrdType == OrdType.STOP_LIMIT)
                {
                    //decimal prx = CodeTransfer_TT.toGlobexPrx(orderInfo.triggerPrice, newOrderSingle.Symbol.getValue());
                    newOrderSingle.StopPx = new StopPx(stopPx);
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
                //永久有效
                if (timeInForce == "1")
                {
                    order.IsGTCOrder = true;
                }
                // Tag1  上手号
                newOrderSingle.Account = new Account(netInfo.accountNo);

                // SenderSubID(Tag50 ID)
                //newOrderSingle.SetField(new SenderSubID(obj.todayCanUse));

                ////Tag582
                //newOrderSingle.CustOrderCapacity = new CustOrderCapacity(4);




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
                netInfo.errorMsg = msg;
                //netInfo.NewOrderSingleException(msg, netInfo.code);
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
                order.TempCommandCode = netInfo.code;
                //order.CancelNetInfo = netInfo;
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
                netInfo.errorMsg = msg;
                //netInfo.OrderCancelRequestException(msg, cancelInfo.orderNo, netInfo.code);

                _nLog.Error($"SystemCode -  { netInfo.systemCode}");
                _nLog.Error(ex.ToString());
                throw ex;
            }

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="netInfo"></param>
        protected override void OrderCancelReplaceRequest(NetInfo netInfo)
        {
            ModifyInfo modifyInfo = new ModifyInfo();
            modifyInfo.MyReadString(netInfo.infoT);
            Order order = null;
            try
            {

                if (!MemoryData.Orders.TryGetValue(netInfo.systemCode, out order))
                {
                    throw new Exception($"Can not find SystemCode - {netInfo.systemCode}");
                }
                if (order.NewOrderSingleClientID != modifyInfo.orderNo)
                {
                    throw new Exception($"SystemCode don't match orderNo .SystemCode- {netInfo.systemCode},orderNo - {modifyInfo.orderNo}");
                }
                if (order.Pending)
                {
                    throw new Exception($"Order  is pending .SystemCode- {netInfo.systemCode}");
                }
                order.TempCommandCode = netInfo.code;
                NetInfo orderNetInfo = order.OrderNetInfo;
                OrderInfo orderInfo = new OrderInfo();
                orderInfo.MyReadString(orderNetInfo.infoT);
                QuickFix.FIX42.OrderCancelReplaceRequest orderCancelReplaceRequest = new QuickFix.FIX42.OrderCancelReplaceRequest();

                //NewOrderSingle newOrderSingle = new NewOrderSingle();
                //newOrderSingle.FromString(order.NewOrderSingle, false, null, null);


                //Tag 37
                orderCancelReplaceRequest.OrderID = new OrderID(order.OrderID);



                //Tag 41
                orderCancelReplaceRequest.OrigClOrdID = new OrigClOrdID(order.CurrentCliOrderID);

                var clOrdID = MemoryData.GetNextClOrderID().ToString();
                //Tag 11
                orderCancelReplaceRequest.ClOrdID = new ClOrdID(clOrdID);
                // Tag1
                orderCancelReplaceRequest.Account = new Account(netInfo.accountNo);
                // Tag38
                orderCancelReplaceRequest.OrderQty = new OrderQty(decimal.Parse(modifyInfo.modifyNumber));

                // Tag54
                var side = !string.IsNullOrEmpty(modifyInfo.buySale) ? modifyInfo.buySale : orderInfo.buySale;
                orderCancelReplaceRequest.Side = ZDUperTagValueConvert.QuerySide(side);


                var priceType = "";
                if (string.IsNullOrEmpty(modifyInfo.priceType))
                {
                    modifyInfo.priceType = orderInfo.priceType;
                }
                priceType = modifyInfo.priceType;

                string orderType = ZDUperTagValueConvert.ConvertToTTOrdType(priceType);
                char charOrdType = char.Parse(orderType);
                // Tag40 ,不能用QueryOrdType(info.priceType);方法，有的客户端LME交易所不传值
                orderCancelReplaceRequest.OrdType = new OrdType(charOrdType);




                //string symbol = newOrderSingle.Symbol.getValue();


                var price = decimal.Parse(modifyInfo.modifyPrice);
                // Tag44
                if (charOrdType == OrdType.LIMIT || charOrdType == OrdType.STOP_LIMIT)
                {
                    //decimal prx = CodeTransfer_TT.toGlobexPrx(orderInfo.orderPrice, newOrderSingle.Symbol.getValue());
                    orderCancelReplaceRequest.Price = new Price(price);
                }

                var stopPx = decimal.Parse(modifyInfo.modifyTriggerPrice);
                //Tag99
                if (charOrdType == OrdType.STOP || charOrdType == OrdType.STOP_LIMIT)
                {
                    //decimal prx = CodeTransfer_TT.toGlobexPrx(orderInfo.triggerPrice, newOrderSingle.Symbol.getValue());
                    orderCancelReplaceRequest.StopPx = new StopPx(stopPx);
                }

                ////tag 77
                //orderCancelReplaceRequest.OpenClose = new OpenClose('O');

                string timeInForce = ZDUperTagValueConvert.ConvertToTTTimeInForce(orderInfo.validDate);


                //tag 59
                orderCancelReplaceRequest.TimeInForce = new TimeInForce(char.Parse(timeInForce));


                var ret = TradeClient.Instance.SendMessage(orderCancelReplaceRequest);

                if (ret)
                {
                    order.Pending = true;
                    order.TempCliOrderID = clOrdID;
                    order.AmendNetInfo = netInfo;

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

                netInfo.errorMsg = msg;
                //netInfo.OrderCancelReplaceRequestException(msg, modifyInfo.orderNo, netInfo.code);
                //ExecutionReport?.Invoke(netInfo?.MyToString());
                _nLog.Error($"SystemCode -  { netInfo.systemCode}");
                _nLog.Error(ex.ToString());
                throw ex;
            }
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
                order.CommandCode = order.TempCommandCode;
                order.TempCommandCode = "";

                OrderInfo orderInfo = new OrderInfo();
                orderInfo.MyReadString(order.OrderNetInfo.infoT);
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

                orderResponseInfo.exchangeCode = orderInfo.exchangeCode;

                //if (execReport.Side.getValue() == Side.BUY)
                //    info.buySale = "1";
                //else
                //    info.buySale = "2";
                orderResponseInfo.buySale = ZDUperTagValueConvert.QuerySide(execReport.Side);
                orderResponseInfo.tradeType = "1";

                char ordType = execReport.OrdType.getValue();
                orderResponseInfo.priceType = ZDUperTagValueConvert.ConvertToZDOrdType(ordType.ToString());
                orderResponseInfo.orderPrice = orderInfo.orderPrice;

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
                orderResponseInfo.acceptType = orderInfo.userType;
                orderResponseInfo.code = orderInfo.code;


                netInfo = order.OrderNetInfo.CloneWithNewCode(ErrorCode.SUCCESS, CommandCode.ORDER);
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
            order.CommandCode = order.TempCommandCode;
            order.TempCommandCode = "";

            long origClOrdID = long.Parse(execReport.OrigClOrdID.getValue());
            MemoryData.UsingCliOrderIDSystemCode.TryRemove(origClOrdID, out _);



            OrderInfo orderInfo = new OrderInfo();
            orderInfo.MyReadString(order.OrderNetInfo.infoT);


            //改单信息给原单
            ModifyInfo modifyInfo = new ModifyInfo();
            modifyInfo.MyReadString(order.AmendNetInfo.infoT);
            orderInfo.orderNumber = modifyInfo.modifyNumber;
            orderInfo.orderPrice = modifyInfo.modifyPrice;
            orderInfo.triggerPrice = modifyInfo.modifyTriggerPrice;
            order.OrderNetInfo.infoT = orderInfo.MyToString();
            order.AmendNetInfo = null;

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
            orderResponseInfo.orderNo = execReport.ClOrdID.getValue(); 
            orderResponseInfo.origOrderNo = order.NewOrderSingleClientID;
            orderResponseInfo.code = orderInfo.code;

            //盘房和TT对单用，关联字段。
            if (execReport.IsSetField(Tags.SecondaryClOrdID))
            {
                orderResponseInfo.origOrderNo = execReport.GetString(Tags.SecondaryClOrdID);
            }


            NetInfo netInfo = order.OrderNetInfo.CloneWithNewCode(ErrorCode.SUCCESS, CommandCode.MODIFY);
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
            cancelResponseInfo.filledNumber = ((int)order.CumQty).ToString();
            cancelResponseInfo.cancelNo = cancelResponseInfo.orderNo;

            DateTime transTime = execReport.TransactTime.getValue();
            cancelResponseInfo.cancelTime = transTime.ToString("HH:mm:ss");
            cancelResponseInfo.cancelDate = transTime.ToString("yyyy-MM-dd");

            NetInfo netInfo = order.OrderNetInfo.CloneWithNewCode(ErrorCode.SUCCESS, CommandCode.CANCELCAST);
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
            filledResponseInfo.code = orderInfo.code;

            NetInfo netInfo = order.OrderNetInfo.CloneWithNewCode(ErrorCode.SUCCESS, CommandCode.FILLEDCAST);
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
