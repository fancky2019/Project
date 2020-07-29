using Client.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Utility
{
    class MemoryDataManager
    {
        private static readonly NLog.Logger _nLog = NLog.LogManager.GetCurrentClassLogger();
        
        internal static ConcurrentDictionary<string, Order> Orders { get; set; }
        //static Dictionary<string, long> _systemCodeCliOrderID = null;

        static long _beginOrderId = 0;
        static long _endOrderId = 0;
        internal static long LastOrderID { get; private set; }



        static MemoryDataManager()
        {
            Orders = new ConcurrentDictionary<string, Order>();
            //_systemCodeCliOrderID = new Dictionary<string, long>();


            var cliOrderIDScope = ConfigurationManager.AppSettings["CliOrderIDScope"].ToString();
            if (string.IsNullOrEmpty(cliOrderIDScope))
            {
                _nLog.Error("Order_ID_Scope is null!");
            }
            _beginOrderId = long.Parse(cliOrderIDScope.Split(',')[0]);
            _endOrderId = long.Parse(cliOrderIDScope.Split(',')[1]);
            LastOrderID = 0;

            var datas = TxtFile.ReadTxtFile(ConfigurationManager.AppSettings["OrderIDFilePath"].ToString());
            if (datas.Count > 0)
            {
                LastOrderID = long.Parse(datas[0]);
            }

        }


        public static long GetNextClOrderID()
        {

            LastOrderID++;
            LastOrderID = LastOrderID <= _beginOrderId ? _beginOrderId + 1 : LastOrderID;
            LastOrderID = LastOrderID >= _endOrderId ? _beginOrderId + 1 : LastOrderID;

            if (Orders.Values.Select(p => p.CurrentCliOrderID).ToList().Contains(LastOrderID.ToString()))
            {
                GetNextClOrderID();
            }

            return LastOrderID;
        }



        //static internal void SetSystemCodeCurrentCliOrderID(string systemCode, long currentClientOrderID)
        //{
        //    if(_systemCodeCliOrderID.ContainsKey(systemCode))
        //    {
        //        _systemCodeCliOrderID[systemCode] = currentClientOrderID;
        //    }
        //    else
        //    {
        //        _systemCodeCliOrderID.Add(systemCode, currentClientOrderID);
        //    }
        //}


    }
}
