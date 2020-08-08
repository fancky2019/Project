using ZDFixService.Models;
using ZDFixService.Service.ZDCommon;
using CommonClassLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZDFixService.Service.ZDCommon
{
    /*
     * 150=0:ErrorCode.ERR_ORDER_0000;CommandCode.ORDER
     * 150=5:ErrorCode.ERR_ORDER_0016;CommandCode.MODIFY
     * 150=4:ErrorCode.ERR_ORDER_0014;CommandCode.CANCELCAST
     * 150=2:ErrorCode.SUCCESS;CommandCode.FILLEDCAST;
     * 
     * 
     * 下单、撤单可以没有消息体，改单必须有消息体
     */
    public  static class NetInfoExtensions
    {
        private static readonly NLog.Logger _nLog = NLog.LogManager.GetCurrentClassLogger();
        public static NetInfo NewOrderSingleException(this NetInfo netInfo, string errorMsg,string commandCode)
        {
            try
            {
                netInfo.errorMsg = errorMsg;
                netInfo.errorCode = ErrorCode.ERR_ORDER_0000;
                netInfo.code = TradeBaseDataConfig.GetResponseCommandCode(commandCode);

                OrderResponseInfo orderResponseInfo = new OrderResponseInfo();
                netInfo.infoT = orderResponseInfo.MyToString();
            }
            catch (Exception ex)
            {
                _nLog.Error(ex.ToString());
            }
            return netInfo;
        }

        public static NetInfo OrderCancelRequestException(this NetInfo netInfo, string errorMsg, string newOrderSingleClientID, string commandCode)
        {
            try
            {
                netInfo.errorMsg = errorMsg;
                netInfo.errorCode = ErrorCode.ERR_ORDER_0014;
                netInfo.code = TradeBaseDataConfig.GetResponseCommandCode(commandCode);

                CancelResponseInfo cancelResponseInfo = new CancelResponseInfo();
                cancelResponseInfo.orderNo = newOrderSingleClientID;
                netInfo.infoT = cancelResponseInfo.MyToString();
            }
            catch (Exception ex)
            {

                _nLog.Error(ex.ToString());
            }
            return netInfo;
        }

        public static NetInfo OrderCancelReplaceRequestException(this NetInfo netInfo, string errorMsg, string newOrderSingleClientID, string commandCode)
        {
            try
            {
                netInfo.errorMsg = errorMsg;
                netInfo.errorCode = ErrorCode.ERR_ORDER_0016;
                netInfo.code = TradeBaseDataConfig.GetResponseCommandCode(commandCode);

                OrderResponseInfo orderResponseInfo = new OrderResponseInfo();
                orderResponseInfo.orderNo = newOrderSingleClientID;
                netInfo.infoT = orderResponseInfo.MyToString();
            }
            catch (Exception ex)
            {

                _nLog.Error(ex.ToString());
            }
            return netInfo;

        }

        public  static NetInfo Clone(this NetInfo netInfo)
        {
            NetInfo netInfo1 = new NetInfo();
            try
            {
                netInfo1.accountNo = netInfo.accountNo;
                netInfo1.clientNo = netInfo.clientNo;
                netInfo1.code = netInfo.code;
                netInfo1.errorCode = netInfo.errorCode;
                netInfo1.exchangeCode = netInfo.exchangeCode;
                netInfo1.infoT = netInfo.MyToString();
                netInfo1.localSystemCode = netInfo.localSystemCode;
                netInfo1.systemCode = netInfo.systemCode;
                netInfo1.todayCanUse = netInfo.todayCanUse;
            }
            catch (Exception ex)
            {
                _nLog.Error(ex.ToString());
            }
            return netInfo1;
        }

        public static NetInfo CloneWithNewCode(this NetInfo netInfo, string errorCode, string commandCode)
        {
            NetInfo netInfo1 = netInfo.Clone();
            netInfo1.code = commandCode;
            netInfo1.errorCode = errorCode;
            return netInfo1;
        }


    }
}
