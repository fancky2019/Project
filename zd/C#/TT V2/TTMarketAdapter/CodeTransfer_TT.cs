using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QuickFix.FIX42;
using CommonClassLib.ContractTransfer;
using AuthCommon;
using System.IO;
using QuickFix;
using QuickFix.Fields;
using System.Configuration;
using TTMarketAdapter.Utilities;

namespace TTMarketAdapter
{

    public class FractionalPrxBean
    {
        /// <summary>
        /// 分子
        /// </summary>
        public decimal factor { get; set; }

        /// <summary>
        /// 分母
        /// </summary>
        public decimal denominator { get; set; }
    }

    /// <summary>
    /// TT和公司数据之间映射
    /// </summary>
    public class CodeTransfer_TT
    {
        #region public
        /// <summary>
        /// Key: ZD code, ex GC1201
        /// </summary>
        public static Dictionary<string, SecurityDefinition> zd2TTMapping { get; set; }
        /// <summary>
        /// Key: Security ID
        /// </summary>
        public static Dictionary<string, CodeBean> tt2ZdMapping { get; set; }
        /// <summary>
        ///Key: ZD product code; Val: CME standard product code  
        ///TT和公司的Product名字不一样的
        ///ProductCdMap  配置
        /// </summary>
        public static Dictionary<string, string> mismatchPrdCd { get; set; }
        /// <summary>
        /// Key: ZD product code; Val: ZD exchange code
        ///  <!--公司和TT交易所名称不一样的--><!--品种名称一样，交易所不一样-->
        /// Products  配置
        /// </summary>
        public static Dictionary<string, string> prdExchgDict { get; set; }
        /// <summary>
        /// Key: zdProductCode, value: list of securityId
        /// </summary>
        public static Dictionary<string, List<string>> productToContractsDict { get; set; }
        //  public static ZDLogger errLogger = null;
        #endregion

        #region private
        /// <summary>
        /// Key: TT product code; Val: price factor
        /// 转换成
        /// ZD价格：乘以倍率
        /// TT价格：除以倍率
        /// </summary>
        internal static Dictionary<string, decimal> PrdPrxFactorDict { get; private set; }
        /// <summary>
        /// Key: TT product code; Val: price factor
        /// </summary>
        private static Dictionary<string, FractionalPrxBean> fractionalPrxFactorDict = null;
        /// <summary>
        /// F- 01
        /// G- 02
        /// </summary>
        public static Dictionary<string, string> CMEMonthDict { get; private set; }
        /// <summary>
        /// Jan - 01
        /// </summary>
        public static Dictionary<string, string> ICEIpeMonthDict { get; private set; }

        #endregion


        static CodeTransfer_TT()
        {
            zd2TTMapping = new Dictionary<string, SecurityDefinition>();
            tt2ZdMapping = new Dictionary<string, CodeBean>();
            productToContractsDict = new Dictionary<string, List<string>>();

            CMEMonthDict = new Dictionary<string, string>();
            if (CMEMonthDict.Count == 0)
            {
                CMEMonthDict.Add("F", "01");
                CMEMonthDict.Add("G", "02");
                CMEMonthDict.Add("H", "03");
                CMEMonthDict.Add("J", "04");
                CMEMonthDict.Add("K", "05");
                CMEMonthDict.Add("M", "06");
                CMEMonthDict.Add("N", "07");
                CMEMonthDict.Add("Q", "08");
                CMEMonthDict.Add("U", "09");
                CMEMonthDict.Add("V", "10");
                CMEMonthDict.Add("X", "11");
                CMEMonthDict.Add("Z", "12");
            }

            ICEIpeMonthDict = new Dictionary<string, string>();
            if (ICEIpeMonthDict.Count == 0)
            {
                ICEIpeMonthDict.Add("Jan", "01");
                ICEIpeMonthDict.Add("Feb", "02");
                ICEIpeMonthDict.Add("Mar", "03");
                ICEIpeMonthDict.Add("Apr", "04");
                ICEIpeMonthDict.Add("May", "05");
                ICEIpeMonthDict.Add("Jun", "06");
                ICEIpeMonthDict.Add("Jul", "07");
                ICEIpeMonthDict.Add("Aug", "08");
                ICEIpeMonthDict.Add("Sep", "09");
                ICEIpeMonthDict.Add("Oct", "10");
                ICEIpeMonthDict.Add("Nov", "11");
                ICEIpeMonthDict.Add("Dec", "12");
            }


            mismatchPrdCd = new Dictionary<string, string>();
            if (!string.IsNullOrEmpty(Configurations.MappingZDProducts))
            {
                string[] codeGrp = Configurations.MappingZDProducts.Split(';');
                for (int i = 0; i < codeGrp.Length; i++)
                {

                    string[] oneItem = codeGrp[i].Split(':');

                    if (mismatchPrdCd.ContainsKey(oneItem[0]))
                        TT.Common.NLogUtility.Info("cfgManager.ProductCdMap repeat item: " + oneItem[0]);
                    else
                        mismatchPrdCd.Add(oneItem[0], oneItem[1]);
                }
            }



            prdExchgDict = new Dictionary<string, string>();
            if (!string.IsNullOrEmpty(Configurations.MappingZDExchanges))
            {
                // ZD exchange:product map
                string[] exchgPrdMap = Configurations.MappingZDExchanges.Split(';');
                for (int i = 0; i < exchgPrdMap.Length; i++)
                {
                    string[] exchgPrdPair = exchgPrdMap[i].Split(',');
                    if (prdExchgDict.ContainsKey(exchgPrdPair[1]))
                        TT.Common.NLogUtility.Info("cfgManager.Products repeat item: " + exchgPrdPair[1]);
                    else
                        prdExchgDict.Add(exchgPrdPair[1], exchgPrdPair[0]);
                }
            }

        }


        ///// <summary>
        ///// 上手的（TT）的品种、交易所映射到直达的品种、交易所
        ///// </summary>
        //public static void initProductMap()
        //{

        //}

        public static void addSecurity(string securityType, List<SecurityDefinition> secuDefList)
        {
            List<SecurityDefinition> list = null;

            //int secReqId = int.Parse(securityReqId);
            //if (secReqId < 300)
            //    list = makeFutureMapping(secuDefList);
            //else if (secReqId < 500)
            //    list = makeMappingSP(secuDefList);

            //else
            //    list = makeMappingOption(secuDefList);

            switch (securityType)
            {
                case "FUT":
                    list = makeFutureMapping(secuDefList);
                    break;
                case "MLEG":
                    list = makeMappingSP(secuDefList);
                    break;
                case "OPT":
                    list = makeMappingOption(secuDefList);
                    break;
            }

            // work section
            GlobalData.allSecuriryList.AddRange(list);
        }

        /// <summary>
        /// 期货映射
        /// </summary>
        /// <param name="secuDefList"></param>
        /// <returns></returns>
        public static List<SecurityDefinition> makeFutureMapping(List<SecurityDefinition> secuDefList)
        {
            try
            {
                for (int i = 0; i < secuDefList.Count; i++)
                {
                    SecurityDefinition one = secuDefList[i];

                    string securityID = one.SecurityID.getValue();

                    if (tt2ZdMapping.ContainsKey(securityID))
                    {
                        TT.Common.NLogUtility.Info($"Duplicate securityID:{securityID}");
                        secuDefList.RemoveAt(i);
                        i--;
                        continue;
                    }

                    CodeBean codeBean = new CodeBean();
                    codeBean.contractType = secuDefList[i].SecurityType.getValue();
                    string upperExchg = one.SecurityExchange.getValue();
                    codeBean.upperExchg = upperExchg;
                    string upperPrdCd = one.Symbol.getValue();
                    codeBean.upperProduct = upperPrdCd;
                    string uppperKey = upperExchg + "," + upperPrdCd;
                    if (mismatchPrdCd.ContainsKey(uppperKey))
                        codeBean.zdProduct = mismatchPrdCd[uppperKey];
                    else
                        codeBean.zdProduct = upperPrdCd;
                    //if (upperPrdCd == "MPI")
                    //{

                    //}

                    transferFutureToZDCode(codeBean, one);

                    // Mapping from CME to ZD 通过产品关联公司的交易所的名称
                    if (prdExchgDict.ContainsKey(codeBean.zdProduct))
                        codeBean.zdExchg = prdExchgDict[codeBean.zdProduct];
                    else
                        codeBean.zdExchg = upperExchg;



                    tt2ZdMapping.Add(one.SecurityID.getValue(), codeBean);
                    // 
                    List<string> securityIdList = null;
                    if (!productToContractsDict.TryGetValue(codeBean.zdProduct, out securityIdList))
                    {
                        securityIdList = new List<string>();
                        productToContractsDict.Add(codeBean.zdProduct, securityIdList);
                    }
                    securityIdList.Add(one.SecurityID.getValue());

                    if (codeBean.zdCode == null)
                    {
                        //int x = 0;
                        continue;
                    }

                    // Mapping from ZD to CME
                    //Console.WriteLine(i + ": " + zdCode);
                    if (!zd2TTMapping.ContainsKey(codeBean.zdCode))
                    {
                        zd2TTMapping.Add(codeBean.zdCode, one);
                    }

                }

            }
            catch (Exception ex)
            {
                TT.Common.NLogUtility.Error(ex.ToString());
            }
            //var RE = zd2TTMapping["DX1812"];
            return secuDefList;
        }

        /// <summary>
        /// MulLeg 映射
        /// </summary>
        /// <param name="spreadSecuList"></param>
        /// <returns></returns>
        public static List<SecurityDefinition> makeMappingSP(List<SecurityDefinition> spreadSecuList)
        {

            List<SecurityDefinition> outList = new List<SecurityDefinition>();
            try
            {
                for (int i = 0; i < spreadSecuList.Count; i++)
                {
                    SecurityDefinition one = spreadSecuList[i];

                    string securityID = one.SecurityID.getValue();
                    //if (tt2ZdMapping.ContainsKey(securityID))
                    //{
                    //    spreadSecuList.RemoveAt(i);
                    //    continue;
                    //}

                    CodeBean codeBean = new CodeBean();
                    codeBean.contractType = spreadSecuList[i].SecurityType.getValue();
                    string upperExchg = one.SecurityExchange.getValue();
                    codeBean.upperExchg = upperExchg;
                    string upperPrdCd = one.Symbol.getValue();
                    codeBean.upperProduct = upperPrdCd;
                    string uppperKey = upperExchg + "," + upperPrdCd;

                    string zdCode = null;
                    string zdPrdCd = null;
                    if (mismatchPrdCd.ContainsKey(uppperKey))
                    {
                        zdPrdCd = mismatchPrdCd[uppperKey];
                        zdCode = zdPrdCd + "_S";
                        codeBean.zdProduct = zdCode;
                    }
                    else
                    {
                        zdPrdCd = upperPrdCd;
                        zdCode = zdPrdCd + "_S";
                        codeBean.zdProduct = zdCode;
                    }

                    bool isTargetSpread = transferSpreadToZDCode(zdCode, codeBean, one);
                    if (!isTargetSpread)
                    {
                        //spreadSecuList.RemoveAt(i);
                        continue;
                    }

                    outList.Add(one);

                    //
                    List<string> securityIdList = null;
                    if (!productToContractsDict.TryGetValue(zdPrdCd, out securityIdList))
                    {
                        securityIdList = new List<string>();
                        productToContractsDict.Add(zdPrdCd, securityIdList);
                    }
                    securityIdList.Add(one.SecurityID.getValue());


                    // Mapping from CME to ZD
                    if (prdExchgDict.ContainsKey(codeBean.zdProduct))
                        codeBean.zdExchg = prdExchgDict[codeBean.zdProduct];
                    else
                        codeBean.zdExchg = upperExchg;

                    tt2ZdMapping.Add(one.SecurityID.getValue(), codeBean);


                    // Mapping from ZD to CME
                    //Console.WriteLine(i + ": " + zdCode);
                    if (!zd2TTMapping.ContainsKey(codeBean.zdCode))
                    {
                        zd2TTMapping.Add(codeBean.zdCode, one);
                    }

                    //errLogger.log(ZDLogger.LVL_TRACE, one.SecurityID.getValue());
                }
            }
            catch (Exception ex)
            {
                TT.Common.NLogUtility.Error(ex.ToString());
            }
            return outList;

        }

        /// <summary>
        /// 期权映射 
        /// </summary>
        /// <param name="secuDefList"></param>
        /// <returns></returns>
        public static List<SecurityDefinition> makeMappingOption(List<SecurityDefinition> secuDefList)
        {

            List<SecurityDefinition> outList = new List<SecurityDefinition>();
            outList = secuDefList.Where(p => p.Header.GetField(Tags.MsgType) == "d").ToList();
            try
            {
                for (int i = 0; i < secuDefList.Count; i++)
                {
                    SecurityDefinition one = secuDefList[i];

                    string securityID = one.SecurityID.getValue();
                    //if (tt2ZdMapping.ContainsKey(securityID))
                    //{
                    //    secuDefList.RemoveAt(i);
                    //    continue;
                    //}




                    //CodeBean codeBean = new CodeBean();
                    //codeBean.contractType = secuDefList[i].SecurityType.getValue();
                    //string upperExchg = one.SecurityExchange.getValue();
                    //string upperPrdCd = one.Symbol.getValue();
                    //string uppperKey = upperExchg + "," + upperPrdCd;
                    //if (mismatchPrdCd.ContainsKey(uppperKey))
                    //    codeBean.zdProduct = mismatchPrdCd[uppperKey];
                    //else
                    //    codeBean.zdProduct = upperPrdCd;
                    //if (codeBean.zdProduct == "CN")
                    //{

                    //}
                    //try
                    //{
                    //    transferFutureToZDCode(codeBean, one);
                    //}







                    CodeBean codeBean = new CodeBean();
                    string upperExchg = one.SecurityExchange.getValue();
                    string upperProduct = one.Symbol.getValue();
                    codeBean.upperProduct = upperProduct;
                    string uppperKey = upperExchg + "," + upperProduct;


                    int putOrCall = one.PutOrCall.getValue();


                    if (mismatchPrdCd.ContainsKey(uppperKey))
                        codeBean.zdProduct = mismatchPrdCd[uppperKey];
                    else
                        codeBean.zdProduct = upperProduct;
                    //if (codeBean.zdProduct.Contains("CT"))
                    //{

                    //}
                    string zdProduct = codeBean.zdProduct + "_" + (putOrCall == 0 ? "P" : "C");
                    //string securityAltID = one.SecurityAltID.getValue();
                    //string[] numbers = securityAltID.Split(' ');
                    //var number = numbers[numbers.Length - 1].Substring(1, numbers[numbers.Length - 1].Length - 1);

                    codeBean.upperExchg = upperExchg;
                    codeBean.upperProduct = upperProduct;
                    codeBean.zdProduct = zdProduct;
                    codeBean.contractType = secuDefList[i].SecurityType.getValue();

                    try
                    {
                        //codeBean.zdContractDate = zdContractDate;
                        //codeBean.zdCode = zdCode;
                        transferOptionToZDCode(codeBean, one);
                    }
                    catch (Exception ex)
                    {
                        TT.Common.NLogUtility.Error(ex.ToString());
                    }

                    // Mapping from CME to ZD 通过产品关联公司的交易所的名称
                    if (prdExchgDict.ContainsKey(codeBean.zdProduct))
                        codeBean.zdExchg = prdExchgDict[codeBean.zdProduct];
                    else
                        codeBean.zdExchg = upperExchg;

                    tt2ZdMapping.Add(securityID, codeBean);

                    //// 
                    //List<string> securityIdList = null;
                    //if (!productToContractsDict.TryGetValue(codeBean.zdProduct, out securityIdList))
                    //{
                    //    securityIdList = new List<string>();
                    //    productToContractsDict.Add(codeBean.zdProduct, securityIdList);
                    //}
                    //securityIdList.Add(one.SecurityID.getValue());


                    if (!productToContractsDict.Keys.Contains(codeBean.zdProduct))
                    {
                        productToContractsDict.Add(codeBean.zdProduct, new List<string>() { securityID });
                    }

                    if (codeBean.zdCode == null)
                    {
                        //int x = 0;
                        continue;
                    }

                    // Mapping from ZD to CME
                    //Console.WriteLine(i + ": " + zdCode);
                    if (!zd2TTMapping.ContainsKey(codeBean.zdCode))
                    {
                        zd2TTMapping.Add(codeBean.zdCode, one);
                    }


                }
                //if (!zd2TTMapping.ContainsKey("SB_C1810 9.25"))
                //{

                //}

            }
            catch (Exception ex)
            {

                TT.Common.NLogUtility.Error(ex.ToString());

            }
            //var re1 = zd2TTMapping["SB_C1810 10"].ToString();

            return outList;
        }


        public static void transferFutureToZDCode(CodeBean codeBean, SecurityDefinition secuDef)
        {
            string exchange = secuDef.SecurityExchange.getValue();


            //if (exchange == "LME")
            //{
            //    codeBean.zdContractDate = "3M";
            //    codeBean.zdCode = codeBean.zdProduct + codeBean.zdContractDate;
            //}
            //else if (exchange == "ICE")
            //{
            //    //test  待注释
            //    var zdContractDate = secuDef.MaturityMonthYear.getValue().Substring(2);

            //    ////10455=Feb15
            //    //string upperContract = secuDef.SecurityAltID.getValue();

            //    Group g2 = secuDef.GetGroup(2, Tags.NoSecurityAltID);
            //    //g2.GetString(Tags.SecurityAltID);
            //    //OVS2 OPT Jan19 C1
            //    //var securityAltID = secuDef.SecurityAltID.getValue();//old  tt


            //    //ULA S Jun18
            //    var securityAltID = g2.GetString(Tags.SecurityAltID);
            //    // var upperContract = securityAltID.Split(' ')[1];
            //    var array = securityAltID.Split(' ');
            //    var upperContract = array.Last();
            //    string upperMonth = upperContract.Substring(0, 3);
            //    codeBean.zdContractDate = upperContract.Substring(3) + iceIpeMonthDict[upperMonth];
            //    codeBean.zdCode = codeBean.zdProduct + codeBean.zdContractDate;
            //}
            //else if (exchange == "CFE")
            //{
            //    codeBean.zdContractDate = secuDef.MaturityMonthYear.getValue().Substring(2);
            //    codeBean.zdCode = codeBean.zdProduct + codeBean.zdContractDate;
            //}
            //// Changed by Rainer on 20150914 -begin
            ////else if ("SGX,CFE,jFCE,Eurex".IndexOf(exchange) > -1)
            //else
            //{
            //    //// old   200=201507  
            //    //codeBean.zdContractDate = secuDef.MaturityMonthYear.getValue().Substring(2);
            //    //交易所：Euronext
            //    if (secuDef.IsSetContractYearMonth())
            //    {
            //        codeBean.zdContractDate = secuDef.ContractYearMonth.getValue().Substring(2);
            //        codeBean.zdCode = codeBean.zdProduct + codeBean.zdContractDate;
            //    }
            //    else
            //    {
            //        Logger.Info("not set tag 18223");
            //        Logger.Info(secuDef.ToString());
            //    }
            //}

            try
            {
                switch (exchange)
                {
                    case "LME":
                        codeBean.zdContractDate = "3M";
                        codeBean.zdCode = codeBean.zdProduct + codeBean.zdContractDate;
                        break;
                    case "CFE":
                    case "CME":
                    case "SGX":
                    case "Eurex":
                    case "ICE":
                        //////10455=Feb15
                        ////string upperContract = secuDef.SecurityAltID.getValue();
                        //int noSecurityAltID = 2;

                        //Group g2 = secuDef.GetGroup(noSecurityAltID, Tags.NoSecurityAltID);
                        ////g2.GetString(Tags.SecurityAltID);
                        ////OVS2 OPT Jan19 C1
                        ////var securityAltID = secuDef.SecurityAltID.getValue();//old  tt
                        //if (exchange == "ICE")
                        //{

                        //}

                        ////ULA S Jun18   // ZW Sep19
                        //var securityAltID = g2.GetString(Tags.SecurityAltID);
                        //// var upperContract = securityAltID.Split(' ')[1];
                        //var array = securityAltID.Split(' ');
                        ////Sep19
                        //var upperContract = array.Last();
                        //string upperMonth = upperContract.Substring(0, 3);

                        var TTYearMonth = GetContractMonthYear(secuDef, 2);


                        if (!ICEIpeMonthDict.Keys.Contains(TTYearMonth.TTMonth))
                        {
                            TTYearMonth = GetContractMonthYear(secuDef, 3);
                        }

                        codeBean.zdContractDate = TTYearMonth.TTYear + ICEIpeMonthDict[TTYearMonth.TTMonth];
                        codeBean.zdCode = codeBean.zdProduct + codeBean.zdContractDate;
                        break;
                    default:
                        if (secuDef.IsSetContractYearMonth())
                        {
                            codeBean.zdContractDate = secuDef.ContractYearMonth.getValue().Substring(2);
                            codeBean.zdCode = codeBean.zdProduct + codeBean.zdContractDate;
                        }
                        else
                        {
                            throw new Exception("not set tag 18223");
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                TT.Common.NLogUtility.Error(secuDef.ToString());
                TT.Common.NLogUtility.Error(ex.ToString());

                //从Tag200 取
                if (secuDef.IsSetMaturityMonthYear())
                {
                    codeBean.zdContractDate = secuDef.MaturityMonthYear.getValue().Substring(2);
                    codeBean.zdCode = codeBean.zdProduct + codeBean.zdContractDate;
                }
            }


        }

        private static (string TTYear, string TTMonth) GetContractMonthYear(SecurityDefinition securityDefinition, int noSecurityAltID = 2)
        {
            Group g2 = securityDefinition.GetGroup(noSecurityAltID, Tags.NoSecurityAltID);
            //g2.GetString(Tags.SecurityAltID);
            //OVS2 OPT Jan19 C1
            //var securityAltID = secuDef.SecurityAltID.getValue();//old  tt


            //ULA S Jun18   ZW Sep19
            var securityAltID = g2.GetString(Tags.SecurityAltID);
            // var upperContract = securityAltID.Split(' ')[1];
            // ZW Sep19
            var array = securityAltID.Split(' ');
            //Sep19
            var upperContract = array.Last();
            //   Sep
            string upperMonth = upperContract.Substring(0, 3);
            string upperYear = upperContract.Substring(3);

            return (upperYear, upperMonth);
        }

        // return value -  true: is target spread;
        //                 false: not target spread(spread cover different product)
        public static bool transferSpreadToZDCode(string partialZDCode, CodeBean codeBean, SecurityDefinition secuDef)
        {

            try
            {
                string exchange = secuDef.SecurityExchange.getValue();

                //if (exchange == "ICE" || exchange == "HKEX" || exchange == "SGX" || exchange == "Eurex"|| exchange=="CME")
                //{
                Group g2 = secuDef.GetGroup(2, Tags.NoSecurityAltID);
                //  455=DX Dec18-Sep18 
                var securityDesc = g2.GetString(Tags.SecurityAltID);
                char split = securityDesc.Contains("/") ? '/' : '-';
                //Tailor
                if (securityDesc.StartsWith("Tailor"))
                {
                    return false;
                }

                //Sep18/Jun18
                string monthStr = securityDesc.Split(' ')[1];
                string[] legArr = monthStr.Split(split);

                string temp = null;
                string leg1 = legArr[0];
                string leg2 = legArr[1];
                int leg1Len = leg1.Length;
                int leg2Len = leg2.Length;


                temp = leg1.Substring(leg1Len - 2) + ICEIpeMonthDict[leg1.Substring(0, 3)] + "-"
                            + leg2.Substring(leg2Len - 2) + ICEIpeMonthDict[leg2.Substring(0, 3)];
                //DX_S1806-1809   //DX_S
                codeBean.zdCode = partialZDCode + temp;
                //}
                //else
                //{
                //    Group g2 = secuDef.GetGroup(2, Tags.NoSecurityAltID);
                //    //  455=DX Dec18-Sep18 
                //    var securityDesc = g2.GetString(Tags.SecurityAltID);
                //    //  iceIpeMonthDict[upperMonth];
                //    string monthStr = securityDesc.Split(' ')[1];
                //    string[] legArr = monthStr.Split('-');

                //    string temp = null;
                //    string leg1 = legArr[0];
                //    string leg2 = legArr[1];
                //    int leg1Len = leg1.Length;
                //    int leg2Len = leg2.Length;
                //    temp = leg1.Substring(leg1Len - 2) + cmeMonthDict[leg1.Substring(leg1Len - 3, 1)] + "-"
                //                + leg2.Substring(leg2Len - 2) + cmeMonthDict[leg2.Substring(leg2Len - 3, 1)];

                //    codeBean.zdCode = partialZDCode + temp;
                //}

                return true;
            }
            catch (Exception ex)
            {
                TT.Common.NLogUtility.Error(ex.ToString());
                return false;
            }
        }


        public static void transferOptionToZDCode(CodeBean codeBean, SecurityDefinition secuDef)
        {
            #region old
            ////var c=   secuDef.GroupCount(Tags.NoSecurityAltID);
            //Group g2 = secuDef.GetGroup(2, Tags.NoSecurityAltID);
            ////g2.GetString(Tags.SecurityAltID);
            ////OVS2 OPT Jan19 C1
            ////var securityAltID = secuDef.SecurityAltID.getValue();//old  tt
            //var securityAltID = g2.GetString(Tags.SecurityAltID);
            //string exchange = secuDef.SecurityExchange.getValue();
            //////执行价
            ////var strikePrice1 = securityAltID.Split(' ').Last();
            ////var strikePrice = strikePrice1.Substring(1, strikePrice1.Length - 1);
            //////最小变动单位  16552////"0.05";
            ////// var changeUnit = secuDef.ExchTickSize.getValue().ToString();
            ////////生成合约编号
            ////////获取小数点位数
            //////int decimalCout = changeUnit.Split('.')[1].Length;
            ////////合约编号,1804 1
            //////strikePrice = Decimal.Parse(strikePrice.ToString($"F{decimalCout}"));

            ////执行价
            //var strikePrice = secuDef.StrikePrice.getValue();
            //if (exchange == "LME")
            //{
            //    codeBean.zdContractDate = "3M";
            //    codeBean.zdCode = codeBean.zdProduct + codeBean.zdContractDate;
            //}
            //else if (exchange == "ICE")
            //{
            //    ////10455=Feb15
            //    //string upperContract = securityAltID.Substring(0, 5);

            //    //g2.GetString(Tags.SecurityAltID);
            //    //OVS2 OPT Jan19 C1
            //    //var securityAltID = secuDef.SecurityAltID.getValue();//old  tt


            //    //DX Sep18 C60.5   SB Oct18 C10
            //    var securityAltIDStr = g2.GetString(Tags.SecurityAltID);
            //    //if(securityAltIDStr== "SB Oct18 C10")
            //    //{

            //    //}
            //    var array = securityAltIDStr.Split(' ');
            //    var upperContract = array[1];
            //    string upperMonth = upperContract.Substring(0, 3);
            //    codeBean.zdContractDate = upperContract.Substring(3) + ICEIpeMonthDict[upperMonth];
            //    codeBean.zdCode = codeBean.zdProduct + codeBean.zdContractDate;



            //}
            ////else if (exchange == "SGX")
            //// Changed by Rainer on 20150914 -begin
            ////else if ("SGX,CFE,jFCE,Eurex".IndexOf(exchange) > -1)
            //else
            //// Changed by Rainer on 20150914 -end
            //{
            //    // 200=201507
            //    codeBean.zdContractDate = secuDef.MaturityMonthYear.getValue().Substring(2);
            //    codeBean.zdCode = codeBean.zdProduct + codeBean.zdContractDate;
            //}
            ////DX_C1812 78
            //codeBean.zdCode += " " + strikePrice.ToString();

            #endregion


            //var c=   secuDef.GroupCount(Tags.NoSecurityAltID);
            Group g2 = secuDef.GetGroup(2, Tags.NoSecurityAltID);
            //g2.GetString(Tags.SecurityAltID);
            //OVS2 May20 P12.5
            var securityAltID = g2.GetString(Tags.SecurityAltID);
            string exchange = secuDef.SecurityExchange.getValue();
            ////执行价

            //执行价
            var strikePrice = secuDef.StrikePrice.getValue();
            if (exchange == "LME")
            {
                codeBean.zdContractDate = "3M";
                codeBean.zdCode = codeBean.zdProduct + codeBean.zdContractDate;
            }
            else
            {

                ////ICE 交易所未设置18223
                //if(secuDef.IsSetContractYearMonth())
                //{
                //    codeBean.zdContractDate = secuDef.ContractYearMonth.getValue().Substring(2);
                //    codeBean.zdCode = codeBean.zdProduct + codeBean.zdContractDate;
                //}


                //从455中解析
                try
                {
                    var securityAltIDStr = GetSecurityAltID(secuDef, 2);
                    var array = securityAltIDStr.Split(' ');
                    var upperContract = array[1];
                    string upperMonth = upperContract.Substring(0, 3);
                    codeBean.zdContractDate = upperContract.Substring(3) + ICEIpeMonthDict[upperMonth];
                    codeBean.zdCode = codeBean.zdProduct + codeBean.zdContractDate;
                }
                catch (Exception ex)
                {
                    TT.Common.NLogUtility.Error(ex.ToString());
                }
            }
            //DX_C1812 78
            codeBean.zdCode += " " + strikePrice.ToString();

            //if (secuDef.IsSetContractYearMonth())
            //{
            //    CheckContractYearMonth(secuDef, 2);
            //}



        }

        /// <summary>
        ///校验 tag18223的年月等于455中的年月
        /// </summary>
        /// <param name="secuDef"></param>
        /// <param name="index"></param>
        private static void CheckContractYearMonth(SecurityDefinition secuDef, int index)
        {
            var c = secuDef.GroupCount(Tags.NoSecurityAltID);
            Group g2 = secuDef.GetGroup(index, Tags.NoSecurityAltID);
            //g2.GetString(Tags.SecurityAltID);
            //OVS2 May20 P12.5
            var securityAltID = GetSecurityAltID(secuDef, index);

            //OVS2 May20 P12.5
            var monthYear = securityAltID.Split(' ')[1];
            var monthWord = monthYear.Substring(0, 3);

            var year = monthYear.Substring(3);
            try
            {
                var numYearMonth = $"{year}{ICEIpeMonthDict[monthWord]}";

                var contractYearMonth = secuDef.ContractYearMonth.getValue().Substring(2);
                if (numYearMonth != contractYearMonth)
                {
                    TT.Common.NLogUtility.Debug(secuDef.ToString());
                }
            }
            catch (Exception ex)
            {
                TT.Common.NLogUtility.Error(ex.ToString());
            }
        }

        private static string GetSecurityAltID(SecurityDefinition secuDef, int index)
        {
            var c = secuDef.GroupCount(Tags.NoSecurityAltID);
            Group g2 = secuDef.GetGroup(index, Tags.NoSecurityAltID);
            //g2.GetString(Tags.SecurityAltID);
            //OVS2 May20 P12.5
            var securityAltID = g2.GetString(Tags.SecurityAltID);

            //OVS2 May20 P12.5
            var monthYear = securityAltID.Split(' ')[1];
            var monthWord = monthYear.Substring(0, 3);
            if (!ICEIpeMonthDict.ContainsKey(monthWord))
            {
                index += 1;
                if (index > c)
                {
                    return "";
                }
                securityAltID = GetSecurityAltID(secuDef, index);
            }

            return securityAltID;
        }




        public static CodeBean getZDCodeInfoByUpperCode(string securityID)
        {
            if (tt2ZdMapping.ContainsKey(securityID))
                return tt2ZdMapping[securityID];
            else
                return null;
        }

        public static SecurityDefinition getUpperCodeInfoByZDCode(string zdCode, string exchangeCode)
        {
            if (zd2TTMapping.ContainsKey(zdCode))
                return zd2TTMapping[zdCode];
            else
                return null;
        }


        #region  倍率

        #region 初始化赔率配置
        /// <summary>
        /// ZD显示和TT的倍率配置,初始化合约之后调用
        /// </summary>
        /// <param name="sameEXE"></param>
        public static void initPrxFactor(bool sameEXE = false)
        {
            //Configuration config = null;
            //if (!sameEXE)
            //{
            //    string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TTMarketAdapter.exe.config");
            //    ExeConfigurationFileMap filemap = new ExeConfigurationFileMap();
            //    filemap.ExeConfigFilename = filePath;//配置文件路径  
            //    config = ConfigurationManager.OpenMappedExeConfiguration(filemap, ConfigurationUserLevel.None);
            //}


            if (PrdPrxFactorDict == null)
            {
                PrdPrxFactorDict = new Dictionary<string, decimal>();

                //if (GlobalData.allSecuriryList.Count == 0)
                //{
                //    initSecurityDefinitionFromFile();
                //}
                //添加tag:DisplayFactor(9787)换算
                GlobalData.allSecuriryList?.ForEach(p =>
                {
                    if (p.IsSetDisplayFactor())
                    {

                        string upperExchg = p.SecurityExchange.getValue();
                        string upperPrdCd = p.Symbol.getValue();
                        string zdProduct = upperPrdCd;
                        string uppperKey = upperExchg + "," + upperPrdCd;
                        if (mismatchPrdCd.ContainsKey(uppperKey))
                        {
                            zdProduct = mismatchPrdCd[uppperKey];
                        }
                        if (!PrdPrxFactorDict.ContainsKey(zdProduct))
                        {
                            PrdPrxFactorDict.Add(zdProduct, decimal.Parse(p.DisplayFactor.getValue()));
                        }
                    }
                });

                // CfgManager cfgManager = CfgManager.getInstance(CONFIG_FILE);
                // TT prd code:prx factor
                if (!string.IsNullOrEmpty(Configurations.DisplayPrxFactor))
                {
                    string[] arrPrxFactor = Configurations.DisplayPrxFactor.Split(';');
                    for (int i = 0; i < arrPrxFactor.Length; i++)
                    {
                        string[] prxFactorPair = arrPrxFactor[i].Split(':');
                        if (!PrdPrxFactorDict.ContainsKey(prxFactorPair[0]))
                        {
                            PrdPrxFactorDict.Add(prxFactorPair[0], decimal.Parse(prxFactorPair[1]));
                        }
                    }
                }
            }

            if (fractionalPrxFactorDict == null)
            {
                fractionalPrxFactorDict = new Dictionary<string, FractionalPrxBean>();

                //  CfgManager cfgManager = CfgManager.getInstance(CONFIG_FILE);
                //   string fractionalCfg =Configurations.FractionalPrxFactor: 

                //FractionalPrxFactor:ZC:ZS:ZW:ZR:ZO:XC:XW:XK,100,8
                if (string.IsNullOrEmpty(Configurations.FractionalPrxFactor))
                    return;

                string[] groupArr = Configurations.FractionalPrxFactor.Split(';');
                foreach (string oneGroup in groupArr)
                {
                    string[] itemArr = oneGroup.Split(',');

                    FractionalPrxBean fpb = new FractionalPrxBean();
                    fpb.factor = decimal.Parse(itemArr[1]);//100
                    fpb.denominator = decimal.Parse(itemArr[2]);//8

                    string[] products = itemArr[0].Split(':');
                    foreach (string product in products)
                    {
                        if (!fractionalPrxFactorDict.ContainsKey(product))
                        {
                            fractionalPrxFactorDict.Add(product, fpb);
                        }
                    }
                }
            }
        }
        #endregion

        #region  转换成ZD价格
        public static string toClientPrx(string inputPrx, string ttPrdCd)
        {
            return toClientPrx(Decimal.Parse(inputPrx), ttPrdCd);
        }

        /// <summary>
        /// 换算成直达的价格
        /// </summary>
        /// <param name="inputPrx"></param>
        /// <param name="ttPrdCd"></param>
        /// <returns></returns>
        public static string toClientPrx(decimal inputPrx, string ttPrdCd)
        {
            if (PrdPrxFactorDict.ContainsKey(ttPrdCd))
            {
                decimal prxFactor = PrdPrxFactorDict[ttPrdCd];
                if (prxFactor == 1)
                {
                    return inputPrx.ToString();
                }
                else
                {
                    inputPrx *= prxFactor;
                    return inputPrx.ToString();
                }

            }
            else if (fractionalPrxFactorDict.ContainsKey(ttPrdCd))
            {
                FractionalPrxBean fpb = fractionalPrxFactorDict[ttPrdCd];

                // To client
                //9722 -> 9.7225
                string strPrx = inputPrx.ToString();
                int dotIdx = strPrx.IndexOf('.');
                if (dotIdx > -1)
                {
                    strPrx = strPrx.Substring(0, dotIdx);
                }


                char fractionPrx = strPrx[strPrx.Length - 1];

                decimal dDecimal = decimal.Parse(fractionPrx.ToString());
                decimal tail = dDecimal / fpb.denominator;
                decimal data = decimal.Parse(strPrx.Substring(0, strPrx.Length - 1)) + tail;

                decimal result = data / fpb.factor;
                return result.ToString();

            }

            return inputPrx.ToString();
        }
        #endregion

        #region 转换成TT价格
        /// <summary>
        /// 换算成TT的价格
        /// </summary>
        /// <param name="inputPrx"></param>
        /// <param name="ttPrdCd"></param>
        /// <returns></returns>
        public static decimal toGlobexPrx(string inputPrx, string ttPrdCd)
        {
            decimal decInputPrx = decimal.Parse(inputPrx);

            if (PrdPrxFactorDict.ContainsKey(ttPrdCd))
            {
                decimal prxFactor = PrdPrxFactorDict[ttPrdCd];
                if (prxFactor == 1)
                {
                    return decInputPrx;
                }
                else
                {
                    decInputPrx /= prxFactor;
                    return decInputPrx;
                }
            }
            else if (fractionalPrxFactorDict.ContainsKey(ttPrdCd))
            {
                FractionalPrxBean fpb = fractionalPrxFactorDict[ttPrdCd];

                // To tt
                //9.7225 -> 9722 
                decimal data = decimal.Parse(inputPrx) * fpb.factor;
                int head = Decimal.ToInt32(data);
                decimal tail = (data - head) * fpb.denominator;

                string result = head.ToString() + tail.ToString();
                return decimal.Parse(result);

            }

            return decInputPrx;
        }
        #endregion

        #endregion



    }

}
