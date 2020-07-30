using CommonClassLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using TTMarketAdapter;
using TTMarketAdapter.Model;

namespace TTMarketAdapter.Utilities
{
    public class TTMarketAdapterCommon
    {
        /// <summary>
        /// 格式转换：
        /// BRN1908--->BRN Aug19
        /// DX_S1806-1809--->BRN Jul20-Oct25 Calendar
        /// </summary>
        /// <returns></returns>
        public static string GetSecurityAltID(OrderModel orderModel)
        {
            var contract = orderModel.ZDContract;
            SecurityTypeEnum securityType = SecurityTypeEnum.None;
            if (!String.IsNullOrEmpty(orderModel.SecurityType))
            {
                securityType = (SecurityTypeEnum)Enum.Parse(typeof(SecurityTypeEnum), orderModel.SecurityType);
            }

            string securityAltID = "";
            if (securityType == SecurityTypeEnum.None)
            {
                securityType = GetSecurityType(contract);
            }
            switch (securityType)
            {
                case SecurityTypeEnum.FUT:
                    if (contract.EndsWith("3M"))
                    {
                        //455=NI 3M
                        securityAltID = $"{orderModel.Symbol} 3M";
                    }
                    else
                    {
                        //BRN
                        var productFut = orderModel.Symbol;
                        //1908
                        var yearMonth = contract.Substring(contract.Length - 4, 4);
                        ////19
                        //var yearNumber = yearMonth.Substring(0, 2);
                        ////08
                        //var monthNumber = yearMonth.Substring(2, 2);
                        ////Aug
                        //var monthWord = GlobalData.ICEMonthDict[monthNumber];
                        //BRN Aug19
                        securityAltID = $"{productFut} {ConvertToTTYeatMonth(yearMonth)}";
                    }
                    break;
                case SecurityTypeEnum.MLEG:
                    // DX_S1912-1909--->455=DX Dec19-Sep19 Calendar
                    var _sIndex = contract.IndexOf("_S");//2
                    var productMulti_Leg = orderModel.Symbol;//DX
                    var lefLeg = contract.Substring(_sIndex + 2, 4);//1806
                    var rightLeg = contract.Substring(_sIndex + 2 + 4 + 1, 4);//1809
                    //securityAltID = $"{productMulti_Leg} {ConvertToTTYeatMonth(lefLeg)}-{ConvertToTTYeatMonth(rightLeg)}";
                    securityAltID = $"{productMulti_Leg} {ConvertToTTYeatMonth(lefLeg)}-{ConvertToTTYeatMonth(rightLeg)} Calendar";
                    break;
                case SecurityTypeEnum.OPT:
                    //throw new Exception("Not support Option yet!");
                    //SB_C1810 9.25---->455=SB Oct20 P14
                    var arrays = contract.Split(' ');
                    var yearMonthOpt = arrays[0].Substring(arrays[0].Length - 4, 4);
                    var pOrC= arrays[0].Substring(arrays[0].Length - 5, 1);
         
                    securityAltID = $"{orderModel.Symbol} {ConvertToTTYeatMonth(yearMonthOpt)} {pOrC}{arrays[1]}";
                    break;

            }
            return securityAltID;

        }


        ///// <summary>
        ///// 有bug未修改，没转成TT的
        ///// </summary>
        ///// <param name="contract"></param>
        ///// <param name="securityType"></param>
        ///// <returns></returns>
        //public static string GetSecurityAltID(string contract, SecurityTypeEnum securityType = SecurityTypeEnum.None)
        //{
        //    string securityAltID = "";
        //    if (securityType == SecurityTypeEnum.None)
        //    {
        //        securityType = GetSecurityType(contract);
        //    }
        //    switch (securityType)
        //    {
        //        case SecurityTypeEnum.FUT:
        //            if (contract.EndsWith("3M"))
        //            {
        //                //contract.Substring 要转成TT的，此处没转
        //                //455=NI 3M
        //                securityAltID = $"{contract.Substring(0, contract.Length - 2)} 3M";
        //            }
        //            else
        //            {
        //                //BRN
        //                var productFut = contract.Substring(0, contract.Length - 4);
        //                //1908
        //                var yearMonth = contract.Substring(contract.Length - 4, 4);
        //                ////19
        //                //var yearNumber = yearMonth.Substring(0, 2);
        //                ////08
        //                //var monthNumber = yearMonth.Substring(2, 2);
        //                ////Aug
        //                //var monthWord = GlobalData.ICEMonthDict[monthNumber];
        //                //BRN Aug19
        //                securityAltID = $"{productFut} {ConvertToTTYeatMonth(yearMonth)}";
        //            }
        //            break;
        //        case SecurityTypeEnum.MLEG:
        //            // DX_S1806-1809
        //            var _sIndex = contract.IndexOf("_S");//2
        //            var productMulti_Leg = contract.Substring(0, _sIndex);//DX
        //            var lefLeg = contract.Substring(_sIndex + 2, 4);//1806
        //            var rightLeg = contract.Substring(_sIndex + 2 + 4 + 1, 4);//1809
        //            //securityAltID = $"{productMulti_Leg} {ConvertToTTYeatMonth(lefLeg)}-{ConvertToTTYeatMonth(rightLeg)}";
        //            securityAltID = $"{productMulti_Leg} {ConvertToTTYeatMonth(lefLeg)}-{ConvertToTTYeatMonth(rightLeg)} Calendar";
        //            break;
        //        case SecurityTypeEnum.OPT:
        //            throw new Exception("Not support Option yet!");
        //    }
        //    return securityAltID;

        //}

        /// <summary>
        /// BRN Jul19--->BRN1907
        /// </summary>
        /// <param name="securityAltID"></param>
        /// <param name="securityExchange">TT交易所</param>
        /// <param name="securityType"></param>
        /// <returns></returns>
        public static string GetContract(string securityAltID, string securityExchange, SecurityTypeEnum securityType = SecurityTypeEnum.None)
        {
            string contract = "";
            try
            {
                var array = securityAltID.Split(' ');
                var zdProduct = Configurations.Instance.GetZDExchangeProduct(array[0], securityExchange, securityType).ZDProduct;
                if (securityExchange == "LME")
                {
                    contract = $"{zdProduct}3M";
                }
                else
                {
                    var monthYearWord = array[1];
                    var yearNumber = array[1].Substring(3, 2);//19
                    var monthWord = array[1].Substring(0, 3);//Jul
                    var monthNumber = GlobalData.ICEMonthDict[monthWord];
                    contract = $"{zdProduct}{yearNumber}{monthNumber}";
                }
            }
            catch (Exception ex)
            {

                TT.Common.NLogUtility.Error($"securityAltID:{securityAltID}{ex.ToString()}");
            }
            return contract;
        }

        public static string GetZDProduct(string contract, SecurityTypeEnum securityType)
        {
            var zdProduct = "";
            switch (securityType)
            {
                case SecurityTypeEnum.FUT:
                    // CA3M,BRN1912
                    zdProduct = contract.EndsWith("3M") ? contract.Substring(0, contract.Length - 2) : contract.Substring(0, contract.Length - 4);
                    break;
                case SecurityTypeEnum.MLEG:
                    // DX_S1806-1809
                    var _sIndex = contract.IndexOf("_S");//2
                    zdProduct = contract.Substring(0, _sIndex);//DX
                    break;
                case SecurityTypeEnum.OPT:
                    //SB_C1810 9.25
                    // throw new Exception("Not support Option yet!");

                    var arrays = contract.Split(' ');
                    zdProduct = arrays[0].Substring(0, arrays[0].Length - 6);
                    break;
            }
            return zdProduct;
        }

        /// <summary>
        /// 1908--->Aug19
        /// </summary>
        /// <param name="zdYearMonth"></param>
        /// <returns></returns>
        public static string ConvertToTTYeatMonth(string zdYearMonth)
        {
            //19
            var yearNumber = zdYearMonth.Substring(0, 2);
            //08
            var monthNumber = zdYearMonth.Substring(2, 2);
            //Aug
            var monthWord = GlobalData.ICEMonthDict.First(p => p.Value == monthNumber).Key;
            //Aug19
            return $"{monthWord}{yearNumber}";
        }

        public static string GetSecurityType(OrderInfo orderInfo)
        {
            return "";
        }

        public static OrderModel GetOrderModel(string contract)
        {
            /// BRN Aug25
            /// 455=BRN Jul20-Oct25
            /// 
            //BRN1908
            //DX_S1806-1809 
            OrderModel orderModel = GlobalData.OrderModelList.Find(p => p.ZDContract == contract);
            if (orderModel == null)
            {
                orderModel = new OrderModel();
                orderModel.ZDContract = contract;
                //Regex.IsMatch
                SecurityTypeEnum securityType = GetSecurityType(contract);
                orderModel.SecurityType = Enum.GetName(typeof(SecurityTypeEnum), securityType);
                var tt = Configurations.Instance.GetTTProductExchange(GetZDProduct(contract, securityType), securityType);
                orderModel.SecurityExchange = tt.TTExchange;
                orderModel.Symbol = tt.TTProduct;
                orderModel.SecurityAltID = GetSecurityAltID(orderModel);
                var validate = orderModel.Validate();
                if (validate.Success)
                {
                    GlobalData.OrderModelList.Add(orderModel);
                }
                else
                {
                    throw new Exception(validate.ErrorMessage);
                }
            }

            return orderModel;


        }

        public static SecurityTypeEnum GetSecurityType(string contract)
        {
            //CA3M  ,LME 是3M结尾
            if (contract.EndsWith("3M"))
            {
                return SecurityTypeEnum.FUT;
            }
            SecurityTypeEnum securityType;
            //匹配MLEG
            var pattern = @".+(_S)\d{4}-\d{4}";
            if (Regex.IsMatch(contract, pattern))//MLEG
            {
                securityType = SecurityTypeEnum.MLEG;
            }
            else if (!contract.Contains(" ") && Regex.IsMatch(contract, @".+\d{4}"))//FUT
            {
                securityType = SecurityTypeEnum.FUT;
            }
            else//OPT
            {
                //SB_C1810 9.25
                securityType = SecurityTypeEnum.OPT;
            }
            return securityType;
        }
    }
}
