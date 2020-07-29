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
     */
    internal static class OrderExceptions
    {
        private static NetInfo OrderException(this NetInfo netInfo, string errorMsg)
        {
            OrderInfo info = new OrderInfo();
            info.MyReadString(netInfo.infoT);
            netInfo.errorMsg = errorMsg;
            netInfo.exchangeCode = info.exchangeCode;
            netInfo.accountNo = info.accountNo;
            netInfo.errorCode = ErrorCode.ERR_ORDER_0000;
            netInfo.code = CommandCode.ORDER;
            return netInfo;
        }

        private static NetInfo CancelOrderException(this NetInfo netInfo, string errorMsg)
        {
            CancelInfo info = new CancelInfo();
            info.MyReadString(netInfo.infoT);
            CancelResponseInfo cinfo = new CancelResponseInfo();
            cinfo.orderNo = info.orderNo;
            netInfo.errorMsg = errorMsg;
            netInfo.infoT = cinfo.MyToString();
            netInfo.exchangeCode = info.exchangeCode;
            netInfo.accountNo = info.accountNo;
            netInfo.systemCode = info.systemNo;
            netInfo.errorCode = ErrorCode.ERR_ORDER_0014;
            netInfo.code = CommandCode.CANCELCAST;

            return netInfo;
        }

        private static NetInfo OrderCancelReplaceException(this NetInfo netInfo, string errorMsg)
        {
            ModifyInfo info = new ModifyInfo();
            info.MyReadString(netInfo.infoT);
            OrderResponseInfo minfo = new OrderResponseInfo();
            netInfo.errorMsg = errorMsg;
            minfo.orderNo = info.orderNo;
            netInfo.infoT = minfo.MyToString();
            netInfo.exchangeCode = info.exchangeCode;
            netInfo.accountNo = info.accountNo;
            netInfo.errorCode = ErrorCode.ERR_ORDER_0016;
            netInfo.code = CommandCode.MODIFY;
            return netInfo;

        }
    }
}
