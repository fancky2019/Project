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
    internal static class OrderExceptions
    {
        internal static NetInfo NewOrderSingleException(this NetInfo netInfo, string errorMsg)
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
            return netInfo;
        }

        internal static NetInfo OrderCancelRequestException(this NetInfo netInfo, string errorMsg)
        {
            CancelInfo cancelInfo = new CancelInfo();
            cancelInfo.MyReadString(netInfo.infoT);
            CancelResponseInfo cancelResponseInfo = new CancelResponseInfo();
            cancelResponseInfo.orderNo = cancelInfo.orderNo;
            netInfo.errorMsg = errorMsg;
            netInfo.infoT = cancelResponseInfo.MyToString();
            netInfo.exchangeCode = cancelInfo.exchangeCode;
            netInfo.accountNo = cancelInfo.accountNo;
            netInfo.systemCode = cancelInfo.systemNo;
            netInfo.errorCode = ErrorCode.ERR_ORDER_0014;
            netInfo.code = CommandCode.CANCELCAST;

            return netInfo;
        }

        internal static NetInfo OrderCancelReplaceRequestException(this NetInfo netInfo, string errorMsg)
        {
            ModifyInfo modifyInfo = new ModifyInfo();
            modifyInfo.MyReadString(netInfo.infoT);
            OrderResponseInfo orderResponseInfo = new OrderResponseInfo();
            netInfo.errorMsg = errorMsg;
            orderResponseInfo.orderNo = modifyInfo.orderNo;
            netInfo.infoT = orderResponseInfo.MyToString();
            netInfo.exchangeCode = modifyInfo.exchangeCode;
            netInfo.accountNo = modifyInfo.accountNo;
            netInfo.errorCode = ErrorCode.ERR_ORDER_0016;
            netInfo.code = CommandCode.MODIFY;
            return netInfo;

        }
    }
}
