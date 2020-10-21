
using CommonClassLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TestCommon
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
    public static class NetInfoExtensions
    {
    

        public static NetInfo Exception(this NetInfo netInfo)
        {
            try
            {

                if (netInfo.code == CommandCode.ORDER || netInfo.code == CommandCode.OrderStockHK)
                {
                    netInfo.NewOrderSingleException(netInfo.errorMsg,netInfo.code);
                }
                else if (netInfo.code == CommandCode.MODIFY || netInfo.code == CommandCode.ModifyStockHK)
                {
                    CancelInfo cancelInfo = new CancelInfo();
                    cancelInfo.MyReadString(netInfo.infoT);
                    netInfo.OrderCancelRequestException(netInfo.errorMsg, cancelInfo.orderNo, netInfo.code);
                }
                else if (netInfo.code == CommandCode.CANCEL || netInfo.code == CommandCode.CancelStockHK)
                {
                    ModifyInfo modifyInfo = new ModifyInfo();
                    modifyInfo.MyReadString(netInfo.infoT);
                    netInfo.OrderCancelReplaceRequestException(netInfo.errorMsg, modifyInfo.orderNo, netInfo.code);
                }
                else
                {
                    throw new Exception("Can not find appropriate CommandCode");
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return netInfo;
        }

        public static NetInfo NewOrderSingleException(this NetInfo netInfo, string errorMsg, string commandCode)
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
                throw ex;
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

                throw ex;
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

                throw ex;
            }
            return netInfo;

        }

        public static NetInfo Clone(this NetInfo netInfo)
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
                throw ex;
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

        public static string ToRequestJson(this NetInfo netInfo, string netInfoString)
        {
            if (string.IsNullOrEmpty(netInfoString))
            {
                throw new Exception($"netInfoString is empty.");
            }
            netInfo.MyReadString(netInfoString);
            return netInfo.ToRequestJson();
        }

        public static string ToRequestJson(this NetInfo netInfo)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(NewtonsoftHelper.JsonSerializeObjectFormat(netInfo));
            sb.Append("\r\n");
            switch (netInfo.code)
            {
                case "ORDER001":
                case "OrdeStHK":
                    OrderInfo orderInfo = new OrderInfo();
                    orderInfo.MyReadString(netInfo.infoT);
                    sb.Append(NewtonsoftHelper.JsonSerializeObjectFormat(orderInfo));
                    break;
                case "CANCST01":
                case "CancStHK":
                    CancelInfo cancelInfo = new CancelInfo();
                    cancelInfo.MyReadString(netInfo.infoT);
                    sb.Append(NewtonsoftHelper.JsonSerializeObjectFormat(cancelInfo));
                    break;
                case "MODIFY01":
                case "ModiStHK":
                    ModifyInfo modifyInfo = new ModifyInfo();
                    modifyInfo.MyReadString(netInfo.infoT);
                    sb.Append(NewtonsoftHelper.JsonSerializeObjectFormat(modifyInfo));
                    break;
                default:
                    throw new Exception($"订单指令有误 - {netInfo.MyToString()}");
            }
            return sb.ToString();
        }

        public static string ToResponseJson(this NetInfo netInfo, string netInfoString)
        {
            if (string.IsNullOrEmpty(netInfoString))
            {
                throw new Exception($"netInfoString is empty.");
            }
            netInfo.MyReadString(netInfoString);
            return netInfo.ToResponseJson();
        }

        public static string ToResponseJson(this NetInfo netInfo)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(NewtonsoftHelper.JsonSerializeObjectFormat(netInfo));
            sb.Append("\r\n");
            switch (netInfo.code)
            {
                case "ORDER001":
                case "OrdeStHK":
                    OrderResponseInfo orderInfo = new OrderResponseInfo();
                    orderInfo.MyReadString(netInfo.infoT);
                    sb.Append(NewtonsoftHelper.JsonSerializeObjectFormat(orderInfo));
                    break;
                case "CANCST01":
                case "CancStHK":
                    CancelResponseInfo cancelInfo = new CancelResponseInfo();
                    cancelInfo.MyReadString(netInfo.infoT);
                    sb.Append(NewtonsoftHelper.JsonSerializeObjectFormat(cancelInfo));
                    break;
                case "MODIFY01":
                case "ModiStHK":
                    OrderResponseInfo modifyInfo = new OrderResponseInfo();
                    modifyInfo.MyReadString(netInfo.infoT);
                    sb.Append(NewtonsoftHelper.JsonSerializeObjectFormat(modifyInfo));
                    break;
                case "FILCST01":
                case "FillStHK":
                    FilledResponseInfo filledResponseInfo = new FilledResponseInfo();
                    filledResponseInfo.MyReadString(netInfo.infoT);
                    sb.Append(NewtonsoftHelper.JsonSerializeObjectFormat(filledResponseInfo));
                    break;
                default:
                    throw new Exception($"订单指令有误 - {netInfo.MyToString()}");

            }
            return sb.ToString();
        }


    }
}
