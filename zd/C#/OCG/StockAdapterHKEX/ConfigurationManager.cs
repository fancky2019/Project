using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using System.Diagnostics;
using System.Linq;

namespace StockAdapterHKEX
{
    public class CfgManager
    {

        private static CfgManager cfgInstance = null;
        private static Configuration ConfigFile;

        private bool ValidateString(string Value)
        {
            //StringValidator sv = new StringValidator(1, 15, "");
            //try
            //{
            //    sv.Validate(Value);
            //}
            //catch (Exception e)
            //{
            //    return true;
            //}
            return false;
        }


        public static CfgManager getInstance(string cfgPath)
        {
            if (cfgInstance == null)
            {
                cfgInstance = new CfgManager(cfgPath);
            }

            return cfgInstance;
        }

        public string SecurityDefFile
        {
            get { return ConfigFile.AppSettings.Settings["SECURITY_DEF_FILE"]?.Value; }
            set
            {
                if (!(ValidateString(value)))
                {
                    ConfigFile.AppSettings.Settings["SECURITY_DEF_FILE"].Value = value;
                }
            }
        }



        public string HeartbeatInterval
        {
            get { return ConfigFile.AppSettings.Settings["HEATBEAT_INTERVAL"]?.Value; }
            set
            {
                if (!(ValidateString(value)))
                {
                    ConfigFile.AppSettings.Settings["HEATBEAT_INTERVAL"].Value = value;
                }
            }
        }

        public string SenderLocationID
        {
            get { return ConfigFile.AppSettings.Settings["SenderLocationID"]?.Value; }
            set
            {
                if (!(ValidateString(value)))
                {
                    ConfigFile.AppSettings.Settings["SenderLocationID"].Value = value;
                }
            }
        }

        public string ApplicationSystemName
        {
            get { return ConfigFile.AppSettings.Settings["ApplicationSystemName"].Value; }
            set
            {
                if (!(ValidateString(value)))
                {
                    ConfigFile.AppSettings.Settings["ApplicationSystemName"].Value = value;
                }
            }
        }

        public string TradingSystemVersion
        {
            get { return ConfigFile.AppSettings.Settings["TradingSystemVersion"].Value; }
            set
            {
                if (!(ValidateString(value)))
                {
                    ConfigFile.AppSettings.Settings["TradingSystemVersion"].Value = value;
                }
            }
        }

        public string ApplicationSystemVendor
        {
            get { return ConfigFile.AppSettings.Settings["ApplicationSystemVendor"].Value; }
            set
            {
                if (!(ValidateString(value)))
                {
                    ConfigFile.AppSettings.Settings["ApplicationSystemVendor"].Value = value;
                }
            }
        }


        public string SenderSubID
        {
            get { return ConfigFile.AppSettings.Settings["SenderSubID"].Value; }
            set
            {
                if (!(ValidateString(value)))
                {
                    ConfigFile.AppSettings.Settings["SenderSubID"].Value = value;
                }
            }
        }

        public string SessionAndPsw
        {
            get { return ConfigFile.AppSettings.Settings["SessionAndPsw"].Value; }
            set
            {
                if (!(ValidateString(value)))
                {
                    ConfigFile.AppSettings.Settings["SessionAndPsw"].Value = value;
                }
            }
        }

        public string AlterPasswordDays
        {
            get
            {
                return ConfigFile.AppSettings.Settings["AlterPasswordDays"]?.Value;
            }
            set
            {
                ConfigFile.AppSettings.Settings["AlterPasswordDays"].Value = value;

            }
        }

        //public string NewPasswd
        //{
        //    get { return ConfigFile.AppSettings.Settings["NewPasswd"].Value; }
        //    set
        //    {
        //        if (!(ValidateString(value)))
        //        {
        //            ConfigFile.AppSettings.Settings["NewPasswd"].Value = value;
        //        }
        //    }
        //}

        public string BERequestID
        {
            get { return ConfigFile.AppSettings.Settings["BERequestID"].Value; }
            set
            {
                if (!(ValidateString(value)))
                {
                    ConfigFile.AppSettings.Settings["BERequestID"].Value = value;
                }
            }
        }

        public string SenderCompID
        {
            get { return ConfigFile.AppSettings.Settings["SenderCompID"].Value; }
            set
            {
                if (!(ValidateString(value)))
                {
                    ConfigFile.AppSettings.Settings["SenderCompID"].Value = value;
                }
            }
        }

        public string ManualOrderIndicator
        {
            get { return ConfigFile.AppSettings.Settings["ManualOrderIndicator"].Value; }
            set
            {
                if (!(ValidateString(value)))
                {
                    ConfigFile.AppSettings.Settings["ManualOrderIndicator"].Value = value;
                }
            }
        }

        public string RSAPublicKeyPem
        {
            get { return ConfigFile.AppSettings.Settings["RSAPublicKeyPem"].Value; }
            set
            {
                if (!(ValidateString(value)))
                {
                    ConfigFile.AppSettings.Settings["RSAPublicKeyPem"].Value = value;
                }
            }
        }

        public string ExpectedMsgSeqNumDir
        {
            get { return ConfigFile.AppSettings.Settings["ExpectedMsgSeqNumDir"].Value; }
            set
            {
                if (!(ValidateString(value)))
                {
                    ConfigFile.AppSettings.Settings["ExpectedMsgSeqNumDir"].Value = value;
                }
            }
        }

        public string ClOrderID
        {
            get { return ConfigFile.AppSettings.Settings["CL_ORDER_ID"].Value; }
            set
            {
                if (!(ValidateString(value)))
                {
                    ConfigFile.AppSettings.Settings["CL_ORDER_ID"].Value = value;
                }
            }
        }

        public string GTCOrderFile
        {
            get { return ConfigFile.AppSettings.Settings["GTC_ORDER_FILE"].Value; }
            set
            {
                if (!(ValidateString(value)))
                {
                    ConfigFile.AppSettings.Settings["GTC_ORDER_FILE"].Value = value;
                }
            }
        }

        public string DayOrderFile
        {
            get { return ConfigFile.AppSettings.Settings["DAY_ORDER_FILE"].Value; }
            set
            {
                if (!(ValidateString(value)))
                {
                    ConfigFile.AppSettings.Settings["DAY_ORDER_FILE"].Value = value;
                }
            }
        }

        public string DailyRestartTime
        {
            get { return ConfigFile.AppSettings.Settings["DailyRestartTime"].Value; }
            set
            {
                if (!(ValidateString(value)))
                {
                    ConfigFile.AppSettings.Settings["DailyRestartTime"].Value = value;
                }
            }
        }

        public string PreOpenTime
        {
            get { return ConfigFile.AppSettings.Settings["PreOpenTime"].Value; }
            set
            {
                if (!(ValidateString(value)))
                {
                    ConfigFile.AppSettings.Settings["PreOpenTime"].Value = value;
                }
            }
        }
        //
        private TimeSpan _preOpenTime1Start = default(TimeSpan);
        private TimeSpan _preOpenTime1End = default(TimeSpan);
        private TimeSpan _preOpenTime2Start = default(TimeSpan);
        private TimeSpan _preOpenTime2End = default(TimeSpan);
        private bool? _isPreOpenTime;
        public bool IsPreOpenTime
        {
            get
            {
                if (_isPreOpenTime == null)
                {
                    if (string.IsNullOrEmpty(PreOpenTime))
                    {
                        throw new Exception("PreOpenTime is not configured!");
                    }
                    var timeArray = PreOpenTime.Split(';');

                    var startEndTime1 = timeArray[0].Split('-').ToList();
                    var startHourMinute1 = startEndTime1[0].Split(':').ToList();
                    _preOpenTime1Start = new TimeSpan(int.Parse(startHourMinute1[0]), int.Parse(startHourMinute1[1]), 0);
                    var endHourMinute1 = startEndTime1[1].Split(':').ToList();
                    _preOpenTime1End = new TimeSpan(int.Parse(endHourMinute1[0]), int.Parse(endHourMinute1[1]), 0);



                    var startEndTime2 = timeArray[1].Split('-').ToList();
                    var startHourMinute2 = startEndTime2[0].Split(':').ToList();
                    _preOpenTime2Start = new TimeSpan(int.Parse(startHourMinute2[0]), int.Parse(startHourMinute2[1]), 0);
                    var endHourMinute2 = startEndTime2[1].Split(':').ToList();
                    _preOpenTime2End = new TimeSpan(int.Parse(endHourMinute2[0]), int.Parse(endHourMinute2[1]), 0);



                }
                TimeSpan timeSpan = new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, 0);
                if ((timeSpan > _preOpenTime1Start && timeSpan < _preOpenTime1End) ||
                  (timeSpan > _preOpenTime2Start && timeSpan < _preOpenTime2End))
                {
                    _isPreOpenTime = true;
                }
                else
                {
                    _isPreOpenTime = false;
                }
                return _isPreOpenTime.Value;
            }
        }

        /// <summary>
        /// 窗体关闭前，调用保存此配置
        /// </summary>
        public void save()
        {
            ConfigFile.Save();
        }

        public CfgManager(string exeCfgPath)
        {
            if (exeCfgPath == null)
            {
                string applicationName = Environment.GetCommandLineArgs()[0];
                string exePath = Path.Combine(Environment.CurrentDirectory, applicationName);
                ConfigFile = ConfigurationManager.OpenExeConfiguration(exePath);
            }
            else
                ConfigFile = ConfigurationManager.OpenExeConfiguration(exeCfgPath);

            //string applicationName = Environment.GetCommandLineArgs()[0];
            //string exePath = Path.Combine(Environment.CurrentDirectory, applicationName);
            //ConfigFile = ConfigurationManager.OpenExeConfiguration(exePath);

            if (ConfigFile.AppSettings.Settings.Count == 0)
            {
                ConfigFile.AppSettings.Settings.Add("SenderLocationID", "CN");
                ConfigFile.AppSettings.Settings.Add("ApplicationSystemName", "ZD Trade System");
                ConfigFile.AppSettings.Settings.Add("TradingSystemVersion", "V1.0");
                ConfigFile.AppSettings.Settings.Add("ApplicationSystemVendor", "DA");
                ConfigFile.AppSettings.Settings.Add("SenderSubID", "DA1");
                ConfigFile.AppSettings.Settings.Add("SessionAndPsw", "7533,Aa123456");
                ConfigFile.AppSettings.Settings.Add("AlterPasswordDays", "70");
                //ConfigFile.AppSettings.Settings.Add("NewPasswd", "SHZd7890");
                ConfigFile.AppSettings.Settings.Add("BERequestID", "800102");
                ConfigFile.AppSettings.Settings.Add("SenderCompID", "CO70130001");
                ConfigFile.AppSettings.Settings.Add("ManualOrderIndicator", "Y");
                ConfigFile.AppSettings.Settings.Add("RSAPublicKeyPem", @"config\hkex_ocg_public_key_ete.pem");
                ConfigFile.AppSettings.Settings.Add("ExpectedMsgSeqNumDir", @"store");


                ConfigFile.AppSettings.Settings.Add("SECURITY_DEF_FILE", @"config\USSTOCKINFO_20150422.txt");
                ConfigFile.AppSettings.Settings.Add("GTC_ORDER_FILE", @"config\GTC_Order.dat");
                ConfigFile.AppSettings.Settings.Add("DAY_ORDER_FILE", @"config\DAY_Order.dat");
                ConfigFile.AppSettings.Settings.Add("HEATBEAT_INTERVAL", "30");

                ConfigFile.AppSettings.Settings.Add("CL_ORDER_ID", "10000814");

                ConfigFile.AppSettings.Settings.Add("DailyRestartTime", "5:32");
                ConfigFile.AppSettings.Settings.Add("PreOpenTime", "9:00-9:30;16:00-16:30");
                ConfigFile.Save();
            }
        }


        /// <summary>
        /// 此方法和单例的对象保存的冲突，报已修改的异常
        /// </summary>
        /// <param name="nodeName"></param>
        /// <param name="nodeValue"></param>
        /// <param name="configPath"></param>
        public void UpdateConfig(string nodeName, string nodeValue, string configPath = "")
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
        /// 兼容单例的ConfigFile
        /// </summary>
        /// <param name="nodeName"></param>
        /// <param name="nodeValue"></param>
        /// <param name="configPath"></param>
        public void UpdateConfigCompatibility(string nodeName, string nodeValue, string configPath = "")
        {
            //var v = ConfigurationManager.AppSettings["CL_ORDER_ID"].ToString();
            if (string.IsNullOrEmpty(configPath))
            {
                configPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"{Process.GetCurrentProcess().ProcessName}.exe.config");
            }
            ExeConfigurationFileMap filemap = new ExeConfigurationFileMap();
            filemap.ExeConfigFilename = configPath;//配置文件路径  
                                                   //  Configuration configuration = ConfigurationManager.OpenMappedExeConfiguration(filemap, ConfigurationUserLevel.None);
            ConfigFile.AppSettings.Settings[nodeName].Value = nodeValue;
            //保存配置文件
            ConfigFile.Save(ConfigurationSaveMode.Full);
            ConfigurationManager.RefreshSection("appSettings");// 刷新命名节，在下次检索它时将从磁盘重新读取它。记住应用程序要刷新节点
        }
    }
}
