using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace ZDTradeClientTT
{
    public class ZDTradeClientTTConfiurations
    {
        public static readonly string QuickFixConfig;
        public static readonly string FIX42;
        public static readonly string SecurityDefinitionFuture;
        public static readonly string SecurityDefinitionOption;
        public static string SecurityDefFile;

        public static string HeartbeatInterval;
        

        public static string SenderLocationID;
        

        public static string ApplicationSystemName;
       

        public static string TradingSystemVersion;
       

        public static string ApplicationSystemVendor;
        


        /// <summary>
        /// tag 1
        /// </summary>
        public static string SenderSubID;
        

        public static string SessionAndPsw;
        

        public static string ManualOrderIndicator;

        public static string Order_ID_Scope;

        public static long MinClOrderID;

        public static long MaxClOrderID;

        public static string ClOrderID;
       

        public static string GTCOrderFile;
       

        public static string DayOrderFile;
        

        public static string DailyRestartTime;
       

        //ClearFirm
        public static string ClearFirm;
        

        //OtherSettings
        public static string OtherSettings;
        

        /// <summary>
        /// tag 116
        /// </summary>
        public static string OnBehalfOfSubID;


        public static string Gate_FUT_IP;

        public static string Gate_FUT_Port;
       

        #region  THG Tags
        /// <summary>
        /// 公司简称前缀（THF用）
        /// </summary>
        public static readonly string Prefix;// => ConfigFile.AppSettings.Settings["Prefix"]?.Value;
        public static readonly string Pre_Agreed_Ref;//=> ConfigFile.AppSettings.Settings["Pre_Agreed_Ref"]?.Value;
        public static readonly string KenangaRef;//=> ConfigFile.AppSettings.Settings["KenangaRef"]?.Value;
        public static readonly string Prefix_Newwedge_Ref;//=> ConfigFile.AppSettings.Settings["Prefix_Newwedge_Ref"]?.Value;
        public static readonly string CME_SMPID;// => ConfigFile.AppSettings.Settings["CME_SMPID"]?.Value;
        public static readonly string CME_Instruction;//=> ConfigFile.AppSettings.Settings["CME_Instruction"]?.Value;
        public static readonly string SGX_ClearingAccountOverride;//=> ConfigFile.AppSettings.Settings["SGX_ClearingAccountOverride"]?.Value;
        /// <summary>
        ///FCStone、GHF
        /// </summary>
        public static readonly string Company;//=> ConfigFile.AppSettings.Settings["Company"]?.Value;

        #endregion

        static ZDTradeClientTTConfiurations()
        {

            try
            {
                string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ZDTradeClientTT.exe.config");
                ExeConfigurationFileMap filemap = new ExeConfigurationFileMap();
                filemap.ExeConfigFilename = path;//配置文件路径  
                Configuration configuration = ConfigurationManager.OpenMappedExeConfiguration(filemap, ConfigurationUserLevel.None);
                //ProductCdMap = configuration.AppSettings.Settings["ProductCdMap"]?.Value;
                //Products = configuration.AppSettings.Settings["Products"]?.Value;


                QuickFixConfig = configuration.AppSettings.Settings["QuickFixConfig"]?.Value;
                FIX42 = configuration.AppSettings.Settings["FIX42"]?.Value;
                SecurityDefinitionFuture = configuration.AppSettings.Settings["SecurityDefinitionFuture"]?.Value;
                SecurityDefinitionOption= configuration.AppSettings.Settings["SecurityDefinitionOption"]?.Value;
                SenderLocationID = configuration.AppSettings.Settings["SenderLocationID"]?.Value;
                ApplicationSystemName = configuration.AppSettings.Settings["ApplicationSystemName"]?.Value;
                TradingSystemVersion = configuration.AppSettings.Settings["TradingSystemVersion"]?.Value;
                ApplicationSystemVendor = configuration.AppSettings.Settings["ApplicationSystemVendor"]?.Value;
                SenderSubID = configuration.AppSettings.Settings["SenderSubID"]?.Value;
                OnBehalfOfSubID = configuration.AppSettings.Settings["OnBehalfOfSubID"]?.Value;
                SessionAndPsw= configuration.AppSettings.Settings["SessionAndPsw"]?.Value;
                ManualOrderIndicator= configuration.AppSettings.Settings["ManualOrderIndicator"]?.Value;

                SecurityDefFile = configuration.AppSettings.Settings["SECURITY_DEF_FILE"]?.Value;
                GTCOrderFile = configuration.AppSettings.Settings["GTC_ORDER_FILE"]?.Value;
                DayOrderFile= configuration.AppSettings.Settings["DAY_ORDER_FILE"]?.Value;
                HeartbeatInterval= configuration.AppSettings.Settings["HEATBEAT_INTERVAL"]?.Value;

                Order_ID_Scope = configuration.AppSettings.Settings["Order_ID_Scope"]?.Value;

                if (string.IsNullOrEmpty(Order_ID_Scope))
                {
                    TT.Common.NLogUtility.Error("Order_ID_Scope is null!");
                }
                MinClOrderID = long.Parse(Order_ID_Scope.Split(',')[0]);
                MaxClOrderID = long.Parse(Order_ID_Scope.Split(',')[1]);
                ClOrderID = configuration.AppSettings.Settings["CL_ORDER_ID"]?.Value;

                //// 0: no mirror(no fault tolearance) 1:mirror server 2: mirror client
                //ConfigFile.AppSettings.Settings.Add("FAULT_TOLERANCE", "0");
                //ConfigFile.AppSettings.Settings.Add("FT_SERVER_IP", "192.168.1.59");
                //ConfigFile.AppSettings.Settings.Add("FT_SERVER_PORT", "9001");


                //ConfigFile.AppSettings.Settings.Add("MAIL_SERVER_NAME", "smtp.shanghaizhida.com");
                //ConfigFile.AppSettings.Settings.Add("MAIL_FROM", "jingxiaohui@shanghaizhida.com");
                //ConfigFile.AppSettings.Settings.Add("MAIL_TO", "jingxiaohui@shanghaizhida.com");
                //ConfigFile.AppSettings.Settings.Add("MAIL_SUBJECT", "System Generate Mail");

                DailyRestartTime = configuration.AppSettings.Settings["DailyRestartTime"]?.Value;
                ClearFirm =configuration.AppSettings.Settings["ClearFirm"]?.Value;
                //FFT2&FFT3
                OtherSettings = configuration.AppSettings.Settings["OtherSettings"]?.Value;


                Gate_FUT_IP = configuration.AppSettings.Settings["Gate_FUT_IP"]?.Value;
                Gate_FUT_Port = configuration.AppSettings.Settings["Gate_FUT_Port"]?.Value;


                //GHF Tags
                Prefix = configuration.AppSettings.Settings["Prefix"]?.Value;
                Pre_Agreed_Ref = configuration.AppSettings.Settings["Pre_Agreed_Ref"]?.Value;
                KenangaRef = configuration.AppSettings.Settings["KenangaRef"]?.Value;
                Prefix_Newwedge_Ref = configuration.AppSettings.Settings["Prefix_Newwedge_Ref"]?.Value;
                CME_SMPID = configuration.AppSettings.Settings["CME_SMPID"]?.Value;
                CME_Instruction = configuration.AppSettings.Settings["CME_Instruction"]?.Value;
                SGX_ClearingAccountOverride = configuration.AppSettings.Settings["SGX_ClearingAccountOverride"]?.Value;
                Company = configuration.AppSettings.Settings["Company"]?.Value;
            }
            catch (Exception ex)
            {
                TT.Common.NLogUtility.Error(ex.ToString());
            }
        }

       public static void UpdateConfig(string configPath, string nodeName, string nodeValue)
        {
            //var v = ConfigurationManager.AppSettings["CL_ORDER_ID"].ToString();

            ExeConfigurationFileMap filemap = new ExeConfigurationFileMap();
            filemap.ExeConfigFilename = configPath;//配置文件路径  
            Configuration configuration = ConfigurationManager.OpenMappedExeConfiguration(filemap, ConfigurationUserLevel.None);
            configuration.AppSettings.Settings[nodeName].Value = nodeValue;
            //保存配置文件
            configuration.Save(ConfigurationSaveMode.Full);
            ConfigurationManager.RefreshSection("appSettings");// 刷新命名节，在下次检索它时将从磁盘重新读取它。记住应用程序要刷新节点
        }

        //private static Configuration ConfigFile;
        ///// <summary>
        ///// 窗体关闭前，调用保存此配置
        ///// </summary>
        //public void save()
        //{
        //    ConfigFile.Save();
        //}

        ///// <summary>
        ///// 兼容单例的ConfigFile
        ///// </summary>
        ///// <param name="nodeName"></param>
        ///// <param name="nodeValue"></param>
        ///// <param name="configPath"></param>
        //public void UpdateConfigCompatibility(string nodeName, string nodeValue, string configPath = "")
        //{
        //    //var v = ConfigurationManager.AppSettings["CL_ORDER_ID"].ToString();
        //    if (string.IsNullOrEmpty(configPath))
        //    {
        //        configPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"{Process.GetCurrentProcess().ProcessName}.exe.config");
        //    }
        //    ExeConfigurationFileMap filemap = new ExeConfigurationFileMap();
        //    filemap.ExeConfigFilename = configPath;//配置文件路径  
        //                                           //  Configuration configuration = ConfigurationManager.OpenMappedExeConfiguration(filemap, ConfigurationUserLevel.None);
        //    ConfigFile.AppSettings.Settings[nodeName].Value = nodeValue;
        //    //保存配置文件
        //    ConfigFile.Save(ConfigurationSaveMode.Full);
        //    ConfigurationManager.RefreshSection("appSettings");// 刷新命名节，在下次检索它时将从磁盘重新读取它。记住应用程序要刷新节点
        //}
    }
}
