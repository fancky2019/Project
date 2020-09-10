using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ZDFixService.Service.MemoryDataManager;
using ZDFixService.Utility;
using System.Linq;
using CommonClassLib;
using ZDFixService.Models;
using ZDFixService.Service.ZDCommon;

namespace ZDFixService.Service.RepairOrders
{
    /// <summary>
    /// 无法根据log恢复内存数据
    /// </summary>
    public class RepairOrder
    {
        private static readonly NLog.Logger _nLog = NLog.LogManager.GetCurrentClassLogger();
        ClintInToClintLog _clintInToClintLog;
        public RepairOrder()
        {
            _clintInToClintLog = new ClintInToClintLog();
        }

        /// <summary>
        /// tempClientID 丢失，OrderID(tag37)丢失，无法找到原单信息。
        /// </summary>
        /// <param name="clienInPath"></param>
        public void Repair(string clienInPath = "")
        {
            var serializeTimeLogPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"logs/{ DateTime.Now.ToString("yyyy-MM")}/{DateTime.Now.ToString("yyyy-MM-dd")}/SerializeTime.log");
            var serializeTimes = TxtFile.ReadTxtFile(serializeTimeLogPath);
            var lastestLogTime = serializeTimes.LastOrDefault();
            if (string.IsNullOrEmpty(lastestLogTime))
            {
                _nLog.Info("SerializeTime.log没有持久化订单记录。");
                return;
            }
            //2020-09-09 17:45:30.985 20200909 17:45:30.985|20200909 17:45:30.985
            var serializeDuration = lastestLogTime.Substring(24);
            var duration = serializeDuration.Split('|');
            var startTime = DateTime.Parse(duration[0]);
            var endTime = DateTime.Parse(duration[1]);

            MemoryData.Init();
            if (string.IsNullOrEmpty(clienInPath))
            {
                var dateStr = DateTime.Now.ToString("yyyyMMdd");
                clienInPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"log/{dateStr}/ClientIn_{dateStr}.log");
            }

            var clientInDatas = _clintInToClintLog.ReadClientInData(clienInPath);
            var toClientPath = clienInPath.Replace("ClientIn", "ToClient");
            var toClientDatas = _clintInToClintLog.ReadToClientData(toClientPath);
            if (MemoryData.Orders == null)
            {
                MemoryData.Orders = new System.Collections.Concurrent.ConcurrentDictionary<string, Models.Order>();
            }

            clientInDatas.ForEach(p =>
            {
                if (p.LogTime >= startTime)
                {
                    var commandCode = p.NetInfo.code;
                    //改单撤单直接从toClient中替换。
                    if (commandCode == CommandCode.ORDER || commandCode == CommandCode.OrderStockHK)
                    {

                        if (!MemoryData.Orders.ContainsKey(p.NetInfo.systemCode))
                        {
                            OrderInfo orderInfo = new OrderInfo();
                            orderInfo.MyReadString(p.NetInfo.infoT);
                            string timeInForce = orderInfo.validDate;

                            timeInForce = ZDUperTagValueConvert.ConvertToTTTimeInForce(timeInForce);

                            Order order = new Order();
                            order.OrderNetInfo = p.NetInfo;
                            order.TempCommandCode = p.NetInfo.code;
                            //如果未收到下单返回就宕机，此条下单信息无法正常解析，TempCliOrderID丢失。
                            //order.TempCliOrderID = clOrdID;
                            order.Pending = true;
                            order.SystemCode = p.NetInfo.systemCode;
                            // newOrderSingle.FromString()
                            order.CreateNewOrderSingleTime = DateTime.Now;
                            //永久有效
                            if (timeInForce == "1")
                            {
                                order.IsGTCOrder = true;
                            }
                            MemoryData.Orders.TryAdd(order.SystemCode, order);
                        }
                    }



                    //修复时候打出，宕机前未收到订单生成的订单。人工修复
                    //内存中移除无效的订单
                    //else if (tempCommandCode == CommandCode.MODIFY || tempCommandCode == CommandCode.ModifyStockHK)
                    //{
                    //    //netInfo = order.AmendNetInfo;
                    //    //netInfo = order.OrderNetInfo.Clone(); 
                    //    netInfo.OrderCancelReplaceRequestException(errorMessage, order.NewOrderSingleClientID, tempCommandCode);
                    //}
                    //else if (tempCommandCode == CommandCode.CANCEL || tempCommandCode == CommandCode.CancelStockHK)
                    //{
                    //    //netInfo = order.OrderNetInfo.Clone(); 
                    //    netInfo.OrderCancelRequestException(errorMessage, order.NewOrderSingleClientID, tempCommandCode);
                    //}
                }
            });

            toClientDatas.ForEach(p =>
            {
                NetInfo netInfo = new NetInfo();
                netInfo.MyReadString(p.NetInfo.infoT);
                Order order = null;
                switch (netInfo.code)
                {
                    case "ORDER001":
                    case "OrdeStHK":

                        //@@@@@ICE@BRN2012@1@1@42.59@@1@@@42.59@1@@@@0
                        if (MemoryData.Orders.TryGetValue(netInfo.systemCode, out order))
                        {
                            OrderResponseInfo orderResponseInfo = new OrderResponseInfo();
                            orderResponseInfo.MyReadString(netInfo.infoT);
                            order.Pending = false;
                            //order.OrderID = orderInfo.cl;//tag 37丢失
                            //log  tag 37丢失
                            order.NewOrderSingleClientID = orderResponseInfo.orderNo;
                            order.CurrentCliOrderID = orderResponseInfo.orderNo;
                            order.TempCliOrderID = "";
                            order.CommandCode = netInfo.code;
                            order.TempCommandCode = "";
                        }
                        else
                        {
                            _nLog.Info($"systemCode - {netInfo.systemCode},订单信息丢失");
                        }

                        break;
                    case "CANCST01":
                    case "CancStHK":
                        MemoryData.Orders.TryRemove(netInfo.systemCode, out _);
                        break;
                    case "MODIFY01":
                    case "ModiStHK":
              
                        if (MemoryData.Orders.TryGetValue(netInfo.systemCode, out order))
                        {
                            OrderResponseInfo orderResponseInfo = new OrderResponseInfo();
                            orderResponseInfo.MyReadString(netInfo.infoT);
                    
                            order.Pending = false;
                            //order.OrderID = execReport.OrderID.getValue();
                            order.CurrentCliOrderID = orderResponseInfo.orderNo;
                            order.TempCliOrderID = "";
                            //order.CommandCode = order.TempCommandCode;
                            //order.TempCommandCode = "";

                            //long origClOrdID = long.Parse(execReport.OrigClOrdID.getValue());
                            //MemoryData.UsingCliOrderIDSystemCode.TryRemove(origClOrdID, out _);



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
                        }
                        else
                        {
                            _nLog.Info($"systemCode - {netInfo.systemCode},订单信息丢失");
                        }
                        break;
                    case "FILCST01":
                    case "FillStHK":
                        FilledResponseInfo filledResponseInfo = new FilledResponseInfo();
                        filledResponseInfo.MyReadString(netInfo.infoT);

                        break;
                    default:
                        //MessageBox.Show("订单指令有误！");
                        return;
                }
            });
        }
    }
}
