using CommonClassLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCommon
{
    class TradeBaseDataConfig
    {
        private static Dictionary<string, string> _tradeServiceCommandCode;
        static TradeBaseDataConfig()
        {
            _tradeServiceCommandCode = new Dictionary<string, string>();
            _tradeServiceCommandCode.Add(TradeServiceName.TTTradeService, TradeServiceType.Future);
            _tradeServiceCommandCode.Add(TradeServiceName.PSHKTradeService, TradeServiceType.Stock);
        }

        private static string GetCommandCode(string tradeService, CommandType commandType)
        {
            var tradeServiceType = _tradeServiceCommandCode[tradeService];
            switch (tradeServiceType)
            {
                case TradeServiceType.Future:
                    switch (commandType)
                    {
                        case CommandType.Order:
                            return CommandCode.ORDER;
                        case CommandType.Modify:
                            return CommandCode.MODIFY;
                        case CommandType.Cancel:
                            return CommandCode.CANCEL;
                        case CommandType.OrderResonse:
                            return CommandCode.ORDER;
                        case CommandType.ModifyResonse:
                            return CommandCode.MODIFY;
                        case CommandType.CancelResonse:
                            return CommandCode.CANCELCAST;
                        case CommandType.Fill:
                            return CommandCode.FILLEDCAST;
                        default:
                            throw new Exception("Can not find appropriate CommandCode.");
                    }
                //break;

                case TradeServiceType.Stock:
                    switch (commandType)
                    {
                        case CommandType.Order:
                            return CommandCode.OrderStockHK;
                        case CommandType.Modify:
                            return CommandCode.ModifyStockHK;
                        case CommandType.Cancel:
                            return CommandCode.CancelStockHK;
                        case CommandType.OrderResonse:
                            return CommandCode.OrderStockHK;
                        case CommandType.ModifyResonse:
                            return CommandCode.ModifyStockHK;
                        case CommandType.CancelResonse:
                            return CommandCode.CancelStockHK;
                        case CommandType.Fill:
                            return CommandCode.FilledStockHK;
                        default:
                            throw new Exception("Can not find appropriate CommandCode.");
                    }
                //break;
                default:
                    throw new Exception("Can not find appropriate CommandCode.");

            }
        }

        public static string GetResponseCommandCode(string commandCode)
        {
            switch (commandCode)
            {
                case "ORDER001":
                    return CommandCode.ORDER;
                case "MODIFY01":
                    return CommandCode.MODIFY;
                case "CANCEL01":
                    return CommandCode.CANCELCAST;
                case "FillFuture":
                    return CommandCode.FILLEDCAST;
                case "OrdeStHK":
                    return CommandCode.OrderStockHK;
                case "ModiStHK":
                    return CommandCode.ModifyStockHK;
                case "CancStHK":
                    return CommandCode.CancelStockHK;
                case "FillStock":
                    return CommandCode.FilledStockHK;
                default:
                    throw new Exception("Can not find appropriate CommandCode.");
            }

        }

        public static string GetCancelComandCode()
        {

            var tradeServiceName = Configurations.Configuration["ZDFixService:ITradeService"];


            //tradeServiceName switch
            //{
            //    "TTTradeService" => CommandCode.CANCEL,
            //    "PSHKTradeService" => CommandCode.CANCELCAST,
            //    _ => throw new Exception("GetCancelComand Exception");
            //};

            var commandCode = "";
            switch (tradeServiceName)
            {
                case "TTTradeService":
                    commandCode = CommandCode.CANCEL;
                    break;
                case "PSHKTradeService":
                    commandCode = CommandCode.CancelStockHK;
                    break;
                default:
                    throw new Exception("GetCancelComand Exception");
            }
            return commandCode;
        }
    }

    enum CommandType : byte
    {
        Order,
        Modify,
        Cancel,
        OrderResonse,
        ModifyResonse,
        CancelResonse,
        Fill
    }

    class TradeServiceName
    {
        public const string TTTradeService = "TTTradeService";
        public const string PSHKTradeService = "PSHKTradeService";
    }
    class TradeServiceType
    {
        public const string Future = "Future";
        public const string Stock = "Stock";
    }
}
