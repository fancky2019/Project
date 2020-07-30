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
        public static ZDTradeClientTTConfiurations Instance { get; }
        public string QuickFixConfig { get; private set; }
        public string FIX42 { get; private set; }
        public string SecurityDefinitionFuture { get; private set; }
        public string SecurityDefinitionOption { get; private set; }
        public string SecurityDefFile { get; private set; }


        /// <summary>
        /// tag 1
        /// </summary>
        public string Account { get; private set; }


        public string SessionAndPsw { get; private set; }


        public string Order_ID_Scope { get; private set; }

        public long MinClOrderID { get; private set; }
        public long MaxClOrderID { get; private set; }
        public string OrderID { get; private set; }
        public string PersistOrders { get; private set; }



        /// <summary>
        /// tag 116
        /// </summary>
        public string OnBehalfOfSubID { get; private set; }


        public string Gate_FUT_IP { get; private set; }

        public string Gate_FUT_Port { get; private set; }


        #region  THG Tags
        /// <summary>
        /// 公司简称前缀（THF用）
        /// </summary>
        public string Prefix { get; private set; }// => ConfigFile.AppSettings.Settings["Prefix"]?.Value;
        public string Pre_Agreed_Ref { get; private set; }//=> ConfigFile.AppSettings.Settings["Pre_Agreed_Ref"]?.Value;
        public string KenangaRef { get; private set; }//=> ConfigFile.AppSettings.Settings["KenangaRef"]?.Value;
        public string Prefix_Newwedge_Ref { get; private set; }//=> ConfigFile.AppSettings.Settings["Prefix_Newwedge_Ref"]?.Value;
        public string CME_SMPID { get; private set; }// => ConfigFile.AppSettings.Settings["CME_SMPID"]?.Value;
        public string CME_Instruction { get; private set; }//=> ConfigFile.AppSettings.Settings["CME_Instruction"]?.Value;
        public string SGX_ClearingAccountOverride { get; private set; }//=> ConfigFile.AppSettings.Settings["SGX_ClearingAccountOverride"]?.Value;
        /// <summary>
        ///FCStone、GHF
        /// </summary>
        public string ClearFirm { get; private set; }//=> ConfigFile.AppSettings.Settings["Company"]?.Value;

        public string RefreshTest { get; private set; }

        #endregion

        static ZDTradeClientTTConfiurations()
        {
            Instance = new ZDTradeClientTTConfiurations();
        }

        private  ZDTradeClientTTConfiurations()
        {
            Refresh();
        }

        public void Refresh()
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
                SecurityDefinitionOption = configuration.AppSettings.Settings["SecurityDefinitionOption"]?.Value;




                Account = configuration.AppSettings.Settings["Account"]?.Value;
                OnBehalfOfSubID = configuration.AppSettings.Settings["OnBehalfOfSubID"]?.Value;
                SessionAndPsw = configuration.AppSettings.Settings["SessionAndPsw"]?.Value;


                SecurityDefFile = configuration.AppSettings.Settings["SECURITY_DEF_FILE"]?.Value;
                OrderID = configuration.AppSettings.Settings["OrderID"]?.Value;
                PersistOrders = configuration.AppSettings.Settings["PersistOrders"]?.Value;


                Order_ID_Scope = configuration.AppSettings.Settings["Order_ID_Scope"]?.Value;

                if (string.IsNullOrEmpty(Order_ID_Scope))
                {
                    TT.Common.NLogUtility.Error("Order_ID_Scope is null!");
                }
                MinClOrderID = long.Parse(Order_ID_Scope.Split(',')[0]);
                MaxClOrderID = long.Parse(Order_ID_Scope.Split(',')[1]);



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
                ClearFirm = configuration.AppSettings.Settings["ClearFirm"]?.Value;

                RefreshTest = configuration.AppSettings.Settings["RefreshTest"]?.Value;
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
