using QuickFix.Fields;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Service
{
    public static class ZDUperTagValueConvert
    {

        public static Side QuerySide(string zdSide)
        {
            //Side side = null;
            //if (zdSide == "1")
            //    side = new Side(Side.BUY);
            //else if (zdSide == "2")
            //    side = new Side(Side.SELL);

            //return side;

            return new Side(char.Parse(zdSide));
        }

        public static string QuerySide(Side side)
        {
            //string sideStr = "";
            //if (side.getValue() == Side.BUY)
            //{
            //    sideStr = "1";
            //}   
            //else
            //{
            //    sideStr = "2";
            //}
            //return sideStr;

            return side.getValue().ToString();
        }

        public static string ConvertToZDOrdType(string ttOrdType)
        {
            //客户端用的是FIX 7X和 新TT的FIX版本 不一样
            //两个版本的OrderType值1和2反了，所以
            //此处为了兼容客户端，避免忘记修改
            //TAG40:OrdType
            string zdOrdType = ttOrdType;
            switch (ttOrdType)
            {
                case "1":
                    zdOrdType = "2";
                    break;
                case "2":
                    zdOrdType = "1";
                    break;
                case "4":
                    zdOrdType = "3";
                    break;
                case "3":
                    zdOrdType = "4";
                    break;
                case "":
                    break;
            }
            return zdOrdType;


        }

        public static string ConvertToTTOrdType(string zdOrdType)
        {
            string ttOrderType = zdOrdType;
            switch (zdOrdType)
            {
                case "1":
                    ttOrderType = "2";
                    break;
                case "2":
                    ttOrderType = "1";
                    break;
                case "3":
                    ttOrderType = "4";
                    break;
                case "4":
                    ttOrderType = "3";
                    break;
                default:
                    throw new Exception($"Unsupported OrdType Value :{zdOrdType}");
            }
            return ttOrderType;
        }

        public static string ConvertToZDTimeInForce(string ttTimeInForce)
        {
            //TAG59:TimeInForce
            //新TT和老TT不一样
            string zdTimeInForce = ttTimeInForce;
            switch (ttTimeInForce)
            {
                case "0"://当日有效
                    zdTimeInForce = "1";
                    break;
                case "1"://永久有效
                    zdTimeInForce = "2";
                    break;
                case "3"://IOC
                    zdTimeInForce = "4";
                    break;
                case "4":// Fill Or Kill (FOK)
                    zdTimeInForce = "5";
                    break;
            }
            return zdTimeInForce;
        }


        public static string ConvertToTTTimeInForce(string zdTimeInForce)
        {
            string ttTimeInForce = zdTimeInForce;
            switch (zdTimeInForce)
            {
                case "1"://当日有效
                    ttTimeInForce = "0";
                    break;
                case "2"://永久有效
                    ttTimeInForce = "1";
                    break;
                case "4"://IOC
                    ttTimeInForce = "3";
                    break;
                case "5":// Fill Or Kill (FOK)
                    ttTimeInForce = "4";
                    break;
                case "W":
                    break;
                default:
                    throw new Exception($"Unsupported TimeInForce Value :{zdTimeInForce}");
            }
            return ttTimeInForce;
        }
    }
}
