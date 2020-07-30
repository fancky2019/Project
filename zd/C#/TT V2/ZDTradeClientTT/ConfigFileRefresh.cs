using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTMarketAdapter;

namespace ZDTradeClientTT
{
    class ConfigFileRefresh
    {

        public static ConfigFileRefresh Instance { get; }

        private object _lockObj = new object();
        private bool _enable;
        public bool Enable
        {
            get
            {
                lock (_lockObj)
                {
                    return _enable;
                }
            }
            set
            {
                lock (_lockObj)
                {
                    _enable = value;
                }
            }
        }

        System.Threading.Timer _timer = null;
        static ConfigFileRefresh()
        {
            Instance = new ConfigFileRefresh();
        }

        internal void Refresh(string fileFullName = "")
        {
            _timer = new System.Threading.Timer((param) =>
            {
                //if (string.IsNullOrEmpty(fileFullName))
                //{
                //    fileFullName = $"{System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName}.config";
                //}

                //ConfigurationManager.RefreshSection("appSettings");
                //ConfigurationManager.RefreshSection("connectionStrings");

                if (Enable)
                {
                    Configurations.Instance.Refresh();
                    ZDTradeClientTTConfiurations.Instance.Refresh();
                }

            }, null, 60 * 1000, 60 * 1000);
        }

    }
}
