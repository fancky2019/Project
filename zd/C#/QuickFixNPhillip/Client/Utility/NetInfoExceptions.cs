using CommonClassLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Utility
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
    internal static class NetInfoExceptions
    {
        private static readonly NLog.Logger _nLog = NLog.LogManager.GetCurrentClassLogger();
        internal static NetInfo NewOrderSingleException(this NetInfo netInfo, string errorMsg)
        {
            try
            {
                OrderInfo orderInfo = new OrderInfo();
                orderInfo.MyReadString(netInfo.infoT);
                OrderResponseInfo orderResponseInfo = new OrderResponseInfo();
                netInfo.infoT = orderResponseInfo.MyToString();
                netInfo.errorMsg = errorMsg;
                netInfo.exchangeCode = orderInfo.exchangeCode;
                netInfo.accountNo = orderInfo.accountNo;
                netInfo.errorCode = ErrorCode.ERR_ORDER_0000;
                netInfo.code = CommandCode.ORDER;
            }
            catch (Exception ex)
            {
                _nLog.Error(ex.ToString());
            }
            return netInfo;
        }

        internal static NetInfo OrderCancelRequestException(this NetInfo netInfo, string errorMsg, string newOrderSingleClientID)
        {
            try
            {
                OrderInfo orderInfo = new OrderInfo();
                orderInfo.MyReadString(netInfo.infoT);
                CancelResponseInfo cancelResponseInfo = new CancelResponseInfo();
                cancelResponseInfo.orderNo = newOrderSingleClientID;
                netInfo.errorMsg = errorMsg;
                netInfo.infoT = cancelResponseInfo.MyToString();
                netInfo.exchangeCode = orderInfo.exchangeCode;
                netInfo.accountNo = orderInfo.accountNo;
                netInfo.systemCode = netInfo.systemCode;
                netInfo.errorCode = ErrorCode.ERR_ORDER_0014;
                netInfo.code = CommandCode.CANCELCAST;
            }
            catch (Exception ex)
            {

                _nLog.Error(ex.ToString());
            }
            return netInfo;
        }

        internal static NetInfo OrderCancelReplaceRequestException(this NetInfo netInfo, string errorMsg, string newOrderSingleClientID)
        {
            try
            {
                OrderInfo orderInfo = new OrderInfo();
                orderInfo.MyReadString(netInfo.infoT);
                OrderResponseInfo orderResponseInfo = new OrderResponseInfo();
                orderResponseInfo.orderNo = newOrderSingleClientID;
                netInfo.errorMsg = errorMsg;
                netInfo.systemCode = netInfo.systemCode;
                netInfo.infoT = orderResponseInfo.MyToString();
                netInfo.exchangeCode = orderInfo.exchangeCode;
                netInfo.accountNo = orderInfo.accountNo;
                netInfo.errorCode = ErrorCode.ERR_ORDER_0016;
                netInfo.code = CommandCode.MODIFY;
            }
            catch (Exception ex)
            {

                _nLog.Error(ex.ToString());
            }
            return netInfo;

        }

        internal static NetInfo Clone(this NetInfo netInfo)
        {
            NetInfo netInfo1 = new NetInfo();
            try
            {

                netInfo1.infoT = netInfo.MyToString();
                netInfo1.exchangeCode = netInfo.exchangeCode;
                netInfo1.accountNo = netInfo.accountNo;
                netInfo1.systemCode = netInfo.systemCode;
                netInfo1.clientNo = netInfo.clientNo;
                netInfo1.todayCanUse = netInfo.todayCanUse;

            }
            catch (Exception ex)
            {

                _nLog.Error(ex.ToString());
            }
            return netInfo1;
        }

    }
}
