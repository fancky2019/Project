using CommonClassLib;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDFixService.Models;
using ZDFixService.Service.Base;
using ZDFixService.Service.MemoryDataManager;
using ZDFixService.Service.PSHK;
using ZDFixService.Service.TT;
using ZDFixService.Service.ZDCommon;

namespace ZDFixService.Utility.Jobs
{
    class PSHKTradeServiceJob : IJob
    {
        private static readonly NLog.Logger _nLog = NLog.LogManager.GetCurrentClassLogger();
        /// <summary>
        /// Called by the <see cref="IScheduler" /> when a
        /// <see cref="ITrigger" /> fires that is associated with the <see cref="IJob" />.
        /// </summary>
        public virtual Task Execute(IJobExecutionContext context)
        {
            _nLog.Info("PSHKTradeServiceJob is excuting!");
            ServerAutoCancelOrder();
            return Task.CompletedTask;
        }

        protected void ServerAutoCancelOrder()
        {
            foreach (var key in MemoryData.Orders.Keys)
            {
                var order = MemoryData.Orders[key];
                try
                {
                    CancelResponseInfo cancelResponseInfo = new CancelResponseInfo();
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

                    cancelResponseInfo.priceType = orderInfo.priceType;


                    //info.orderPrice = orderInfo.orderPrice;

                    cancelResponseInfo.cancelNumber = (int.Parse(orderInfo.orderNumber) - ((int)order.CumQty)).ToString();
                    cancelResponseInfo.orderNumber = orderInfo.orderNumber;
                    //待兼容
                    cancelResponseInfo.filledNumber = ((int)order.CumQty).ToString();
                    cancelResponseInfo.cancelNo = cancelResponseInfo.orderNo;

                    DateTime transTime = DateTime.Now;
                    cancelResponseInfo.cancelTime = transTime.ToString("HH:mm:ss");
                    cancelResponseInfo.cancelDate = transTime.ToString("yyyy-MM-dd");

                    NetInfo netInfo = order.OrderNetInfo.CloneWithNewCode(ErrorCode.SUCCESS, CommandCode.CancelStockHK);
                    netInfo.infoT = cancelResponseInfo.MyToString();

                    var tradeServiceName = Configurations.Configuration["ZDFixService:ITradeService"];
                    if(tradeServiceName!= "PSHKTradeService")
                    {
                        _nLog.Info("ServerAutoCancelOrder failed!");
                        _nLog.Info("ITradeService is not PSHKTradeService!Please amend ITradeService configuration!");
                        return;
                    }

                    ((PSHKTradeService)TradeServiceFactory.ITradeService).ServerAutoCancelOrder(netInfo);

                    MemoryData.Orders.TryRemove(key, out _);
                    long cliOrderID = long.Parse(order.CurrentCliOrderID);
                    MemoryData.UsingCliOrderIDSystemCode.TryRemove(cliOrderID, out _);
                    _nLog.Info($"ServerAutoCancelOrder: Cancel order SystemCode - {order.SystemCode} CliOrderID - {order.CurrentCliOrderID} successfull");
                }
                catch (Exception ex)
                {
                    _nLog.Info($"ServerAutoCancelOrder: Cancel order SystemCode - {order.SystemCode} CliOrderID - {order.CurrentCliOrderID}  failed");
                    _nLog.Error(ex.ToString());
                }
            };



        }
    }
}
