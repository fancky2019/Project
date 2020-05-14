using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using TTMarketAdapter;

namespace ZDTradeClientTT
{
    /// <summary>
    /// 废弃类：兼容壳
    /// </summary>
    public class CfgManager
    {

        public string SessionAndPsw => ZDTradeClientTTConfiurations.SessionAndPsw;
        public string ClearFirm => ZDTradeClientTTConfiurations.ClearFirm;
        private static CfgManager cfgInstance = null;
        public CfgManager(string exeCfgPath)
        {
        }
        //private static CfgManager cfgInstance = null;
        //private static Configuration ConfigFile;

        //private bool ValidateString(string Value)
        //{
        //    //StringValidator sv = new StringValidator(1, 15, "");
        //    //try
        //    //{
        //    //    sv.Validate(Value);
        //    //}
        //    //catch (Exception e)
        //    //{
        //    //    return true;
        //    //}
        //    return false;
        //}


        public static CfgManager getInstance(string cfgPath = null)
        {
            if (cfgInstance == null)
            {
                cfgInstance = new CfgManager(cfgPath);
            }


            return cfgInstance;
        }

        //public string SecurityDefFile
        //{
        //    get { return ConfigFile.AppSettings.Settings["SECURITY_DEF_FILE"].Value; }
        //    set
        //    {
        //        if (!(ValidateString(value)))
        //        {
        //            ConfigFile.AppSettings.Settings["SECURITY_DEF_FILE"].Value = value;
        //        }
        //    }
        //}

        ////public string FaultTolerance
        ////{
        ////    get { return ConfigFile.AppSettings.Settings["FAULT_TOLERANCE"].Value; }
        ////    set
        ////    {
        ////        if (!(ValidateString(value)))
        ////        {
        ////            ConfigFile.AppSettings.Settings["FAULT_TOLERANCE"].Value = value;
        ////        }
        ////    }
        ////}

        ////public string FtServerIp
        ////{
        ////    get { return ConfigFile.AppSettings.Settings["FT_SERVER_IP"].Value; }
        ////    set
        ////    {
        ////        if (!(ValidateString(value)))
        ////        {
        ////            ConfigFile.AppSettings.Settings["FT_SERVER_IP"].Value = value;
        ////        }
        ////    }
        ////}

        ////public string FtServerPort
        ////{
        ////    get { return ConfigFile.AppSettings.Settings["FT_SERVER_PORT"].Value; }
        ////    set
        ////    {
        ////        if (!(ValidateString(value)))
        ////        {
        ////            ConfigFile.AppSettings.Settings["FT_SERVER_PORT"].Value = value;
        ////        }
        ////    }
        ////}

        ////public string MailServerName
        ////{
        ////    get { return ConfigFile.AppSettings.Settings["MAIL_SERVER_NAME"].Value; }
        ////    set
        ////    {
        ////        if (!(ValidateString(value)))
        ////        {
        ////            ConfigFile.AppSettings.Settings["MAIL_SERVER_NAME"].Value = value;
        ////        }
        ////    }
        ////}

        ////public string MailFrom
        ////{
        ////    get { return ConfigFile.AppSettings.Settings["MAIL_FROM"].Value; }
        ////    set
        ////    {
        ////        if (!(ValidateString(value)))
        ////        {
        ////            ConfigFile.AppSettings.Settings["MAIL_FROM"].Value = value;
        ////        }
        ////    }
        ////}

        ////public string MailTo
        ////{
        ////    get { return ConfigFile.AppSettings.Settings["MAIL_TO"].Value; }
        ////    set
        ////    {
        ////        if (!(ValidateString(value)))
        ////        {
        ////            ConfigFile.AppSettings.Settings["MAIL_TO"].Value = value;
        ////        }
        ////    }
        ////}

        ////public string MailSubject
        ////{
        ////    get { return ConfigFile.AppSettings.Settings["MAIL_SUBJECT"].Value; }
        ////    set
        ////    {
        ////        if (!(ValidateString(value)))
        ////        {
        ////            ConfigFile.AppSettings.Settings["MAIL_SUBJECT"].Value = value;
        ////        }
        ////    }
        ////}

        //public string HeartbeatInterval
        //{
        //    get { return ConfigFile.AppSettings.Settings["HEATBEAT_INTERVAL"].Value; }
        //    set
        //    {
        //        if (!(ValidateString(value)))
        //        {
        //            ConfigFile.AppSettings.Settings["HEATBEAT_INTERVAL"].Value = value;
        //        }
        //    }
        //}

        //public string SenderLocationID
        //{
        //    get { return ConfigFile.AppSettings.Settings["SenderLocationID"].Value; }
        //    set
        //    {
        //        if (!(ValidateString(value)))
        //        {
        //            ConfigFile.AppSettings.Settings["SenderLocationID"].Value = value;
        //        }
        //    }
        //}

        //public string ApplicationSystemName
        //{
        //    get { return ConfigFile.AppSettings.Settings["ApplicationSystemName"].Value; }
        //    set
        //    {
        //        if (!(ValidateString(value)))
        //        {
        //            ConfigFile.AppSettings.Settings["ApplicationSystemName"].Value = value;
        //        }
        //    }
        //}

        //public string TradingSystemVersion
        //{
        //    get { return ConfigFile.AppSettings.Settings["TradingSystemVersion"].Value; }
        //    set
        //    {
        //        if (!(ValidateString(value)))
        //        {
        //            ConfigFile.AppSettings.Settings["TradingSystemVersion"].Value = value;
        //        }
        //    }
        //}

        //public string ApplicationSystemVendor
        //{
        //    get { return ConfigFile.AppSettings.Settings["ApplicationSystemVendor"].Value; }
        //    set
        //    {
        //        if (!(ValidateString(value)))
        //        {
        //            ConfigFile.AppSettings.Settings["ApplicationSystemVendor"].Value = value;
        //        }
        //    }
        //}


        ///// <summary>
        ///// tag 1
        ///// </summary>
        //public string SenderSubID
        //{
        //    get { return ConfigFile.AppSettings.Settings["SenderSubID"].Value; }
        //    set
        //    {
        //        if (!(ValidateString(value)))
        //        {
        //            ConfigFile.AppSettings.Settings["SenderSubID"].Value = value;
        //        }
        //    }
        //}

        //public string SessionAndPsw
        //{
        //    get { return ConfigFile.AppSettings.Settings["SessionAndPsw"].Value; }
        //    set
        //    {
        //        if (!(ValidateString(value)))
        //        {
        //            ConfigFile.AppSettings.Settings["SessionAndPsw"].Value = value;
        //        }
        //    }
        //}

        //public string ManualOrderIndicator
        //{
        //    get { return ConfigFile.AppSettings.Settings["ManualOrderIndicator"].Value; }
        //    set
        //    {
        //        if (!(ValidateString(value)))
        //        {
        //            ConfigFile.AppSettings.Settings["ManualOrderIndicator"].Value = value;
        //        }
        //    }
        //}

        //public string ClOrderID
        //{
        //    get { return ConfigFile.AppSettings.Settings["CL_ORDER_ID"].Value; }
        //    set
        //    {
        //        if (!(ValidateString(value)))
        //        {
        //            ConfigFile.AppSettings.Settings["CL_ORDER_ID"].Value = value;
        //        }
        //    }
        //}

        //public string GTCOrderFile
        //{
        //    get { return ConfigFile.AppSettings.Settings["GTC_ORDER_FILE"].Value; }
        //    set
        //    {
        //        if (!(ValidateString(value)))
        //        {
        //            ConfigFile.AppSettings.Settings["GTC_ORDER_FILE"].Value = value;
        //        }
        //    }
        //}

        //public string DayOrderFile
        //{
        //    get { return ConfigFile.AppSettings.Settings["DAY_ORDER_FILE"].Value; }
        //    set
        //    {
        //        if (!(ValidateString(value)))
        //        {
        //            ConfigFile.AppSettings.Settings["DAY_ORDER_FILE"].Value = value;
        //        }
        //    }
        //}

        //public string DailyRestartTime
        //{
        //    get { return ConfigFile.AppSettings.Settings["DailyRestartTime"].Value; }
        //    set
        //    {
        //        if (!(ValidateString(value)))
        //        {
        //            ConfigFile.AppSettings.Settings["DailyRestartTime"].Value = value;
        //        }
        //    }
        //}

        ////ClearFirm
        //public string ClearFirm
        //{
        //    get { return ConfigFile.AppSettings.Settings["ClearFirm"].Value; }
        //    set
        //    {
        //        if (!(ValidateString(value)))
        //        {
        //            ConfigFile.AppSettings.Settings["ClearFirm"].Value = value;
        //        }
        //    }
        //}

        ////OtherSettings
        //public string OtherSettings
        //{
        //    get { return ConfigFile.AppSettings.Settings["OtherSettings"].Value; }
        //    set
        //    {
        //        if (!(ValidateString(value)))
        //        {
        //            ConfigFile.AppSettings.Settings["OtherSettings"].Value = value;
        //        }
        //    }
        //}

        ///// <summary>
        ///// tag 116
        ///// </summary>
        //public string OnBehalfOfSubID
        //{
        //    get { return ConfigFile.AppSettings.Settings["OnBehalfOfSubID"]?.Value; }
        //}

        //public string Gate_FUT_IP
        //{
        //    get { return ConfigFile.AppSettings.Settings["Gate_FUT_IP"]?.Value; }
        //}
        //public string Gate_FUT_Port
        //{
        //    get { return ConfigFile.AppSettings.Settings["Gate_FUT_Port"]?.Value; }
        //}

        //#region  THG Tags
        ///// <summary>
        ///// 公司简称前缀（THF用）
        ///// </summary>
        //public string Prefix => ConfigFile.AppSettings.Settings["Prefix"]?.Value;
        //public string Pre_Agreed_Ref => ConfigFile.AppSettings.Settings["Pre_Agreed_Ref"]?.Value;
        //public string KenangaRef => ConfigFile.AppSettings.Settings["KenangaRef"]?.Value;
        //public string Prefix_Newwedge_Ref => ConfigFile.AppSettings.Settings["Prefix_Newwedge_Ref"]?.Value;
        //public string CME_SMPID => ConfigFile.AppSettings.Settings["CME_SMPID"]?.Value;
        //public string CME_Instruction => ConfigFile.AppSettings.Settings["CME_Instruction"]?.Value;
        //public string SGX_ClearingAccountOverride => ConfigFile.AppSettings.Settings["SGX_ClearingAccountOverride"]?.Value;
        ///// <summary>
        /////FCStone、GHF
        ///// </summary>
        //public string Company => ConfigFile.AppSettings.Settings["Company"]?.Value;

        //#endregion

        //public void save()
        //{

        //    ConfigFile.Save();
        //}

        //public CfgManager(string exeCfgPath)
        //{
        //if (exeCfgPath == null)
        //{
        //    string applicationName = Environment.GetCommandLineArgs()[0];
        //    string exePath = Path.Combine(Environment.CurrentDirectory, applicationName);
        //    ConfigFile = ConfigurationManager.OpenExeConfiguration(exePath);
        //}
        //else
        //    ConfigFile = ConfigurationManager.OpenExeConfiguration(exeCfgPath);

        //if (ConfigFile.AppSettings.Settings.Count == 0)
        //{
        //    ConfigFile.AppSettings.Settings.Add("SenderLocationID", "KR");
        //    ConfigFile.AppSettings.Settings.Add("ApplicationSystemName", "ZD Trade System");
        //    ConfigFile.AppSettings.Settings.Add("TradingSystemVersion", "V1.0");
        //    ConfigFile.AppSettings.Settings.Add("ApplicationSystemVendor", "DA");
        //    ConfigFile.AppSettings.Settings.Add("SenderSubID", "ZD_001");
        //    ConfigFile.AppSettings.Settings.Add("OnBehalfOfSubID", "LRui");
        //    ConfigFile.AppSettings.Settings.Add("SessionAndPsw", "ZDDEV,12345678");
        //    ConfigFile.AppSettings.Settings.Add("ManualOrderIndicator", "Y");

        //    ConfigFile.AppSettings.Settings.Add("SECURITY_DEF_FILE", @"config\ZD_secdef.dat");
        //    ConfigFile.AppSettings.Settings.Add("GTC_ORDER_FILE", @"config\GTC_Order.dat");
        //    ConfigFile.AppSettings.Settings.Add("DAY_ORDER_FILE", @"config\DAY_Order.dat");
        //    ConfigFile.AppSettings.Settings.Add("HEATBEAT_INTERVAL", "30");

        //    ConfigFile.AppSettings.Settings.Add("CL_ORDER_ID", "8000089");

        //    //// 0: no mirror(no fault tolearance) 1:mirror server 2: mirror client
        //    //ConfigFile.AppSettings.Settings.Add("FAULT_TOLERANCE", "0");
        //    //ConfigFile.AppSettings.Settings.Add("FT_SERVER_IP", "192.168.1.59");
        //    //ConfigFile.AppSettings.Settings.Add("FT_SERVER_PORT", "9001");


        //    //ConfigFile.AppSettings.Settings.Add("MAIL_SERVER_NAME", "smtp.shanghaizhida.com");
        //    //ConfigFile.AppSettings.Settings.Add("MAIL_FROM", "jingxiaohui@shanghaizhida.com");
        //    //ConfigFile.AppSettings.Settings.Add("MAIL_TO", "jingxiaohui@shanghaizhida.com");
        //    //ConfigFile.AppSettings.Settings.Add("MAIL_SUBJECT", "System Generate Mail");

        //    ConfigFile.AppSettings.Settings.Add("DailyRestartTime", "5:32");
        //    ConfigFile.AppSettings.Settings.Add("ClearFirm", "GHFinancials");
        //    //FFT2&FFT3
        //    ConfigFile.AppSettings.Settings.Add("OtherSettings", "HDACIIP:CH99AHDACIIP;HDACNDC1:CH99AHDAD1;HDACNDC2:CH99AHDAD2;HDACOMNI:CH99AHDAOM");



        //    ConfigFile.AppSettings.Settings.Add("Gate_FUT_IP", "192.168.1.105");
        //    ConfigFile.AppSettings.Settings.Add("Gate_FUT_Port", "8606");

        //    //THF Tags
        //    ConfigFile.AppSettings.Settings.Add("Prefix", "ZD");
        //    ConfigFile.AppSettings.Settings.Add("Pre_Agreed_Ref", "GHFL");
        //    ConfigFile.AppSettings.Settings.Add("KenangaRef", "KenangaRef");
        //    ConfigFile.AppSettings.Settings.Add("Prefix_Newwedge_Ref", "Prefix_Newwedge_Ref");
        //    ConfigFile.AppSettings.Settings.Add("CME_SMPID", "CME_SMPID");
        //    ConfigFile.AppSettings.Settings.Add("CME_Instruction", "CME_Instruction");
        //    ConfigFile.AppSettings.Settings.Add("SGX_ClearingAccountOverride", "CH99AHDAOM");
        //    ConfigFile.AppSettings.Settings.Add("Company", "GHF");
        //    ConfigFile.Save();
        //}
        //}
    }
}
