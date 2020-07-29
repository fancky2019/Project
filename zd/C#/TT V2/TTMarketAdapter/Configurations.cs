using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using System.Linq;
using AuthCommon;
using System.Text.RegularExpressions;
using TT.Common;
using System.Diagnostics;

namespace TTMarketAdapter
{
    public class Configurations
    {

        public static readonly string QuickFixConfig;
        public static readonly string FIX42;
        public static readonly string SecurityDefinitionFuture;
        public static readonly string SecurityDefinitionOption;
        /// <summary>
        ///  先映射ZD的品种名称 配置格式：TT交易所,TT品种：ZD品种
        ///  根据 "TT交易所,TT品种" 找 "ZD的品种"
        /// </summary>
        public static readonly string MappingZDProducts;

        /// <summary>
        /// ZD交易所,ZD品种,倍率，开盘时间
        /// </summary>
        public static readonly string SupportedTradeVolumeProducts;
        /// <summary>
        /// 根据映射ZD的品种名称之后，映射ZD的交易所名称 配置格式：ZD交易所,ZD品种
        /// 根据 "ZD品种" 找 "ZD交易所"
        /// </summary>
        public static readonly string MappingZDExchanges;

        /// <summary>
        /// Future 期货的配置
        /// 配置格式:TT的交易所名称和品种名称
        /// </summary>
        public static readonly string TargetFutures;

        /// <summary>
        ///Spreads  配置格式: TT的交易所名称和品种名称
        /// </summary>
        public static readonly string TargetSpreads;
        /// <summary>
        /// 显示倍率
        /// </summary>
        public static string DisplayPrxFactor;
        /// <summary>
        /// 
        /// </summary>
        public static readonly string SessionAndPsw;
        /// <summary>
        /// 
        /// </summary>
        public static readonly string OnBehalfOfSubID;
        ///// <summary>
        ///// 
        ///// </summary>
        //public static readonly string DisorderSpread;
        /// <summary>
        /// 发送到二级行情UDP端口号
        /// </summary>
        public static readonly string MulticastPort;
        /// <summary>
        /// 发送到二级行情UDPIP，可用127.0.0.1代替
        /// </summary>
        public static readonly string MulticastIP;

        /// <summary>
        /// 发送到二级行情TCP端口号
        /// </summary>
        public static readonly string MulticastTCPPort;
        /// <summary>
        /// 发送到二级行情TCPIP，可用127.0.0.1代替
        /// </summary>
        public static readonly string MulticastTCPIP;
        public static readonly string NewMD;
        /// <summary>
        /// 是新版行情
        /// </summary>
        public static readonly bool NewMDBool;

        /// <summary>
        /// 程序启动时自动启动服务
        /// </summary>
        public static readonly string AutoStart;
        /// <summary>
        /// 定时清结算价功能数据库配置
        /// </summary>
        public static readonly string FutureConnectStr;
        /// <summary>
        /// 定时清结算价功能数据库配置
        /// </summary>
        public static readonly string ForeignShareStr;
        /// <summary>
        /// 之前每天下午重启，重启之前保存结算价
        /// 每天三点自动保存结算价
        /// </summary>
        public static readonly string SaveSettlementPriceTime;
        /// <summary>
        /// 触发点击stop服务时间，防止杀进程造成数据丢失
        /// </summary>
        public static readonly string RestartTime;

        /// <summary>
        ///Option 期权的配置
        ///  配置格式:TT的交易所名称和品种名称
        /// </summary>
        public static readonly string TargetOptions;
        //{
        //    get
        //    {
        //        return _instance.AppSettings.Settings["TargetOptions"]?.Value;
        //    }
        //}

        /// <summary>
        /// 压缩日志时间 0:0:0
        /// </summary>
        public static readonly string ZipLogTime;
        /// <summary>
        /// 是否记录发送给二级行情的数据日志
        /// </summary>
        public static readonly string LogSendMsg;// => _instance.AppSettings.Settings["LogSendMsg"]?.Value;


        /// <summary>
        /// 记录发送给二级行情的合约类型，多个用逗号分隔（FUT,MLEG,OPT）。
        /// </summary>
        public static readonly string LogSendMsgSecurityType;// => _instance.AppSettings.Settings["LogSendMsg"]?.Value;
        /// <summary>
        /// 记录发送给二级行情的合约类型，多个用逗号分隔（FUT,MLEG,OPT）。
        /// </summary>
        public static readonly List<string> LogSendMsgSecurityTypes;
        public static readonly string LogCacheSize;
        public static readonly string TimerInterval;

        /// <summary>
        /// 期权期货现货,配置格式：TT品种:期货商品,现货商品。条之间分号隔开
        /// </summary>
        public static readonly string OptFutSpot;
        /// <summary>
        /// 货币名称，配置格式：TT tag15值,ZD名称
        /// </summary>
        public static readonly string CurrencyName;
        /// <summary>
        /// 合约名称的汉字部分,配置格式：TT品种,合约名称的汉字部分
        /// </summary>
        public static readonly string ContractChineseName;
        /// <summary>
        ///期权对应标递的月份不是连续的,即期权月份是连续的（1-12月），其对应的期货合约月份不是连续的（1-12月）
        ///配置格式：TT期权名称:期货月份[期权月份(多个已逗号隔开)]+期货月份[期权月份(多个已逗号隔开)]
        ///配置实例:DX:3[1,2,3]+6[4,5,6]
        /// </summary>
        public static readonly string OptionFutureMonth;

        /// <summary>
        /// 兼容的老格式期权 配置格式：TT交易所,TT期权。多个以分号隔开
        /// </summary>
        public static readonly string CompatibleOption;



        /// <summary>
        /// 兼容老TT有持仓的期权：老TT合约的StrikePrice有多余零。配置格式：ZD合约代码,小数点位数
        /// </summary>
        public static readonly string OpenInterestContract;
        /// <summary>
        /// 
        /// </summary>
        public static readonly Dictionary<string, Dictionary<string, List<string>>> OptionFutureMonthDic;

        /// <summary>
        /// 期权期货现货,配置格式：TT品种:期货商品,现货商品。条之间分号隔开
        /// </summary>
        public static readonly List<(string TTProduct, string Future, string Spot)> OptFutSpotList;
        /// <summary>
        /// 货币名称，配置格式：TT tag15值,ZD名称
        /// </summary>
        public static readonly Dictionary<string, string> CurrencyNameDic;
        /// <summary>
        /// 合约名称的汉字部分,配置格式：TT品种,合约名称的汉字部分
        /// </summary>
        public static readonly List<(string TTProduct, string ContractChineseName)> ContractChineseNameList;

        public static List<(string TTProduct, string TTExchange, string ZDProduct)> MappingZDProductsTupleList { get; private set; }
        public static List<(string TTProduct, string TTExchange)> TargetFuturesTupleList { get; private set; }
        public static List<(string TTProduct, string TTExchange)> TargetSpreadsTupleList { get; private set; }
        public static List<(string TTProduct, string TTExchange)> TargetOptionsTupleList { get; private set; }
        /// <summary>
        /// ZD交易所,ZD品种,倍率，开盘时间
        /// </summary>
        public static List<(string ZDExchange, string ZDProduct, decimal Factor, string OpeningTime)> SupportedTradeVolumeProductsList { get; private set; }

        static Configurations()
        {

            try
            {
                string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TTMarketAdapter.exe.config");
                ExeConfigurationFileMap filemap = new ExeConfigurationFileMap();
                filemap.ExeConfigFilename = path;//配置文件路径  
                Configuration configuration = ConfigurationManager.OpenMappedExeConfiguration(filemap, ConfigurationUserLevel.None);
                //ProductCdMap = configuration.AppSettings.Settings["ProductCdMap"]?.Value;
                //Products = configuration.AppSettings.Settings["Products"]?.Value;
                QuickFixConfig = configuration.AppSettings.Settings["QuickFixConfig"]?.Value;
                FIX42 = configuration.AppSettings.Settings["FIX42"]?.Value;
                SecurityDefinitionFuture = configuration.AppSettings.Settings["SecurityDefinitionFuture"]?.Value;
                SecurityDefinitionOption = configuration.AppSettings.Settings["SecurityDefinitionOption"]?.Value;
                MappingZDProducts = configuration.AppSettings.Settings["MappingZDProducts"]?.Value;
                MappingZDExchanges = configuration.AppSettings.Settings["MappingZDExchanges"]?.Value;
                TargetFutures = configuration.AppSettings.Settings["TargetFutures"]?.Value;
                TargetSpreads = configuration.AppSettings.Settings["TargetSpreads"]?.Value;
                TargetOptions = configuration.AppSettings.Settings["TargetOptions"]?.Value;
                DisplayPrxFactor = configuration.AppSettings.Settings["DisplayPrxFactor"]?.Value;
                SessionAndPsw = configuration.AppSettings.Settings["SessionAndPsw"]?.Value;
                OnBehalfOfSubID = configuration.AppSettings.Settings["OnBehalfOfSubID"]?.Value;
                //DisorderSpread = configuration.AppSettings.Settings["DisorderSpread"]?.Value;
                MulticastPort = configuration.AppSettings.Settings["multicastPort"]?.Value;
                MulticastIP = configuration.AppSettings.Settings["multicastIP"]?.Value;

                MulticastTCPPort = configuration.AppSettings.Settings["multicastTCPPort"]?.Value;
                MulticastTCPIP = configuration.AppSettings.Settings["multicastTCPIP"]?.Value;
                NewMD = configuration.AppSettings.Settings["NewMD"]?.Value;
                NewMDBool = false;
                if (!string.IsNullOrEmpty(NewMD))
                {
                    bool.TryParse(NewMD, out NewMDBool);
                }

                AutoStart = configuration.AppSettings.Settings["AutoStart"]?.Value;
                FutureConnectStr = configuration.AppSettings.Settings["FutureConnectStr"]?.Value;
                ForeignShareStr = configuration.AppSettings.Settings["ForeignShareStr"]?.Value;
                SaveSettlementPriceTime = configuration.AppSettings.Settings["SaveSettlementPriceTime"]?.Value;
                RestartTime = configuration.AppSettings.Settings["RestartTime"]?.Value;


                ZipLogTime = configuration.AppSettings.Settings["ZipLogTime"]?.Value;
                LogSendMsg = configuration.AppSettings.Settings["LogSendMsg"]?.Value;
                LogSendMsgSecurityTypes = new List<string>();
                LogSendMsgSecurityType = configuration.AppSettings.Settings["LogSendMsgSecurityType"]?.Value;
                if (!string.IsNullOrEmpty(LogSendMsgSecurityType))
                {
                    LogSendMsgSecurityTypes = LogSendMsgSecurityType.Split(',').ToList();
                }
                LogCacheSize = configuration.AppSettings.Settings["LogCacheSize"]?.Value;
                TimerInterval = configuration.AppSettings.Settings["TimerInterval"]?.Value;
                OptFutSpot = configuration.AppSettings.Settings["OptFutSpot"]?.Value;
                CurrencyName = configuration.AppSettings.Settings["CurrencyName"]?.Value;
                ContractChineseName = configuration.AppSettings.Settings["ContractChineseName"]?.Value;
                OptionFutureMonth = configuration.AppSettings.Settings["OptionFutureMonth"]?.Value;
                CompatibleOption = configuration.AppSettings.Settings["CompatibleOption"]?.Value;
                OpenInterestContract = configuration.AppSettings.Settings["OpenInterestContract"]?.Value;
                OptionFutureMonthDic = new Dictionary<string, Dictionary<string, List<string>>>();

                if (!string.IsNullOrEmpty(OptionFutureMonth))
                {

                    var productOptionFutureMonthList = OptionFutureMonth.Split(';').ToList();
                    productOptionFutureMonthList.ForEach(p =>
                    {
                        //DX: 3[1, 2, 3] + 6[4, 5, 6] + 9[7, 8, 9] + 12[10, 11, 12];
                        var productOptionFutureMonth = p.Split(':').ToList();
                        var ttProduct = productOptionFutureMonth[0].Trim();
                        var optionFutureMonthList = productOptionFutureMonth[1].Trim().Split('+').ToList();
                        Dictionary<string, List<string>> futureOptionMonthDic = new Dictionary<string, List<string>>();
                        optionFutureMonthList.ForEach(o =>
                        {
                            //3[1, 2, 3]
                            var futureOptionMonthList = o.Split('[').ToList();
                            //3
                            var fututeMonth = futureOptionMonthList[0];
                            //1, 2, 3
                            var optionMonthStr = futureOptionMonthList[1].Replace("]", "");
                            var optionMonthList = optionMonthStr.Split(',').ToList();
                            futureOptionMonthDic.Add(fututeMonth, optionMonthList);
                        });
                        OptionFutureMonthDic.Add(ttProduct, futureOptionMonthDic);
                    });

                }

                SupportedTradeVolumeProducts = configuration.AppSettings.Settings["SupportedTradeVolumeProducts"]?.Value;

                TargetFuturesTupleList = new List<(string TTProduct, string TTExchange)>();

                if (!string.IsNullOrEmpty(TargetFutures))
                {
                    var itemArrays = TargetFutures.Split(';');
                    for (int i = 0; i < itemArrays.Length; i++)
                    {
                        var ttArray = itemArrays[i].Split(',');
                        var ttExchange = ttArray[0];
                        var ttProduct = ttArray[1];
                        TargetFuturesTupleList.Add((ttProduct, ttExchange));
                    }
                }

                TargetSpreadsTupleList = new List<(string TTProduct, string TTExchange)>();
                if (!string.IsNullOrEmpty(TargetSpreads))
                {
                    var itemArrays = TargetSpreads.Split(';');
                    for (int i = 0; i < itemArrays.Length; i++)
                    {
                        var ttArray = itemArrays[i].Split(',');
                        var ttExchange = ttArray[0];
                        var ttProduct = ttArray[1];
                        TargetSpreadsTupleList.Add((ttProduct, ttExchange));
                    }
                }

                TargetOptionsTupleList = new List<(string TTProduct, string TTExchange)>();
                if (!string.IsNullOrEmpty(TargetOptions))
                {
                    var itemArrays = TargetOptions.Split(';');
                    for (int i = 0; i < itemArrays.Length; i++)
                    {
                        var ttArray = itemArrays[i].Split(',');
                        var ttExchange = ttArray[0];
                        var ttProduct = ttArray[1];
                        TargetOptionsTupleList.Add((ttProduct, ttExchange));
                    }
                }




                MappingZDProductsTupleList = new List<(string TTProduct, string TTExchange, string ZDProduct)>();

                if (!string.IsNullOrEmpty(MappingZDProducts))
                {
                    var itemArrays = MappingZDProducts.Split(';');
                    for (int i = 0; i < itemArrays.Length; i++)
                    {
                        var ttZD = itemArrays[i].Split(':');
                        var zdProduct = ttZD[1];
                        var ttArray = ttZD[0].Split(',');
                        var ttExchange = ttArray[0];
                        var ttProduct = ttArray[1];
                        MappingZDProductsTupleList.Add((ttProduct, ttExchange, zdProduct));
                    }
                }
                OptFutSpotList = new List<(string TTProduct, string Future, string Spot)>();
                if (!string.IsNullOrEmpty(OptFutSpot))
                {
                    var itemsArr = OptFutSpot.Split(';');
                    for (int i = 0; i < itemsArr.Length; i++)
                    {
                        var arr = itemsArr[i].Split(':');
                        var futSpot = arr[1].Split(',');
                        OptFutSpotList.Add((arr[0], futSpot[0], futSpot[1]));
                    }
                }

                CurrencyNameDic = new Dictionary<string, string>();
                if (!string.IsNullOrEmpty(CurrencyName))
                {
                    var currencyNameList = CurrencyName.Split(';').ToList();
                    foreach (var item in currencyNameList)
                    {

                        var arr = item.Split(',');
                        if (!CurrencyNameDic.Keys.Contains(arr[0]))
                        {
                            CurrencyNameDic.Add(arr[0], arr[1]);
                        }

                    }
                }

                ContractChineseNameList = new List<(string TTProduct, string ContractChineseName)>();
                if (!string.IsNullOrEmpty(ContractChineseName))
                {
                    var itemsArr = ContractChineseName.Split(';');
                    for (int i = 0; i < itemsArr.Length; i++)
                    {

                        var arr = itemsArr[i].Split(',');
                        ContractChineseNameList.Add((arr[0], arr[1]));

                    }
                }

                SupportedTradeVolumeProductsList = new List<(string ZDExchange, string ZDProduct, decimal Factor, string OpeningTime)>();
                if (!string.IsNullOrEmpty(SupportedTradeVolumeProducts))
                {
                    var itemsArr = SupportedTradeVolumeProducts.Split(';');
                    for (int i = 0; i < itemsArr.Length; i++)
                    {
                        var arr = itemsArr[i].Split(',');
                        SupportedTradeVolumeProductsList.Add((arr[0], arr[1], decimal.Parse(arr[2]), arr[3]));
                    }
                }

            }
            catch (Exception ex)
            {
                NLogUtility.Error(ex.ToString());
            }

        }

        /// <summary>
        /// 根据直达的品种获取对应的TT的交易所、品种
        /// </summary>
        /// <returns></returns>
        public static (string TTExchange, string TTProduct) GetTTProductExchange(string zdProduct, SecurityTypeEnum securityType)
        {

            var tuple = MappingZDProductsTupleList.Find(p => p.ZDProduct == zdProduct);
            string ttProduct = zdProduct;
            if (!string.IsNullOrEmpty(tuple.ZDProduct))//找到映射
            {
                //ttProduct = tuple.TTProduct;
                return (tuple.TTExchange, tuple.TTProduct);
            }
            //如果没找到，说明ZD的Product名称和TT一样

            var ttExchange = "";
            switch (securityType)
            {
                case SecurityTypeEnum.FUT:
                    ttExchange = TargetFuturesTupleList.Find(p => p.TTProduct == ttProduct).TTExchange;
                    break;
                case SecurityTypeEnum.MLEG:
                    ttExchange = TargetSpreadsTupleList.Find(p => p.TTProduct == ttProduct).TTExchange;
                    break;
                case SecurityTypeEnum.OPT:
                    ttExchange = TargetOptionsTupleList.Find(p => p.TTProduct == ttProduct).TTExchange;
                    break;
                case SecurityTypeEnum.None:
                    ttExchange = TargetFuturesTupleList.Find(p => p.TTProduct == ttProduct).TTExchange;
                    if (string.IsNullOrEmpty(ttExchange))
                    {
                        ttExchange = TargetSpreadsTupleList.Find(p => p.TTProduct == ttProduct).TTExchange;
                        if (string.IsNullOrEmpty(ttExchange))
                        {
                            ttExchange = TargetOptionsTupleList.Find(p => p.TTProduct == ttProduct).TTExchange;
                        }
                    }
                    break;
            }
            return (ttExchange, ttProduct);
        }

        public static void UpdateConfig(string nodeName, string nodeValue, string configPath = "")
        {
            //var v = ConfigurationManager.AppSettings["CL_ORDER_ID"].ToString();
            if (string.IsNullOrEmpty(configPath))
            {
                configPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"{Process.GetCurrentProcess().ProcessName}.exe.config");
            }
            ExeConfigurationFileMap filemap = new ExeConfigurationFileMap();
            filemap.ExeConfigFilename = configPath;//配置文件路径  
            Configuration configuration = ConfigurationManager.OpenMappedExeConfiguration(filemap, ConfigurationUserLevel.None);
            configuration.AppSettings.Settings[nodeName].Value = nodeValue;
            //保存配置文件
            configuration.Save(ConfigurationSaveMode.Full);
            ConfigurationManager.RefreshSection("appSettings");// 刷新命名节，在下次检索它时将从磁盘重新读取它。记住应用程序要刷新节点
        }


        /// <summary>
        /// 根据tt的品种[交易所][SecurityType]获取ZD的交易、品种
        /// </summary>
        /// <param name="ttProduct"></param>
        /// <param name="ttExchange"></param>
        /// <param name="securityType">枚举SecurityTypeEnum</param>
        /// <returns></returns>
        public static (string ZDExchange, string ZDProduct) GetZDExchangeProduct(string ttProduct, string ttExchange = "", SecurityTypeEnum securityType = SecurityTypeEnum.None)
        {

            //出始认为和TT的一样
            string zdProduct = ttProduct;
            if (string.IsNullOrEmpty(ttExchange))
            {
                var configSource = string.Empty;
                switch (securityType)
                {
                    case SecurityTypeEnum.None:
                        configSource = !string.IsNullOrEmpty(TargetFutures) ? TargetFutures : configSource;
                        configSource = !string.IsNullOrEmpty(TargetSpreads) ? string.IsNullOrEmpty(configSource) ?
                                                                   TargetSpreads : configSource + ";" + TargetSpreads : configSource;
                        configSource = !string.IsNullOrEmpty(TargetOptions) ? string.IsNullOrEmpty(configSource) ?
                                                                   TargetOptions : configSource + ";" + TargetOptions : configSource;
                        break;
                    case SecurityTypeEnum.FUT:
                        configSource = TargetFutures;
                        break;
                    case SecurityTypeEnum.MLEG:
                        configSource = TargetSpreads;
                        break;
                    case SecurityTypeEnum.OPT:
                        configSource = TargetOptions;
                        break;
                }
                configSource = configSource.Trim();
                var targetFuturesArr = configSource.Split(';');
                for (int i = 0; i < targetFuturesArr.Length; i++)
                {
                    var exchangeProductArr = targetFuturesArr[i].Split(',');
                    if (exchangeProductArr[1] == ttProduct)
                    {
                        ttExchange = exchangeProductArr[0];
                        break;
                    }
                }
            }
            string zdExchangeName = ttExchange;
            //找到映射知道的品种名称
            string ttMapping = $"{ttExchange},{ttProduct}";
            var itemArr = MappingZDProducts.Split(';');
            for (int i = 0; i < itemArr.Length; i++)
            {
                var arr = itemArr[i].Split(':');
                if (arr[0] == ttMapping)
                {
                    zdProduct = arr[1];
                    break;
                }
            }

            //找到直达映射的交易所名称
            var zdProExcArr = MappingZDExchanges.Split(';');
            for (int i = 0; i < zdProExcArr.Length; i++)
            {
                var arr = zdProExcArr[i].Split(',');
                if (arr[1] == zdProduct)
                {
                    zdExchangeName = arr[0];
                    break;
                }
            }
            return (zdExchangeName, zdProduct);
        }


    }

    public enum SecurityTypeEnum : short
    {
        /*
        FUT: future
        MLEG: multi-leg
        OPT: option
        SPOT: EEX spot products
        CUR: currency
        TBOND: treasury bond
        CS: common stock
        NONE: No security type
         */
        /// <summary>
        /// 无
        /// </summary>
        None = 0,
        /// <summary>
        /// 期货
        /// </summary>
        FUT = 1,
        /// <summary>
        /// Mul-leg
        /// </summary>
        MLEG = 2,
        /// <summary>
        /// 期权
        /// </summary>
        OPT = 3
    }
}
