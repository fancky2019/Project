using CommonClassLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TradeTool.Common
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
