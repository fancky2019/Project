using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ZDFixService.Service.MemoryDataManager;
using ZDFixService.Utility;
using System.Linq;

namespace ZDFixService.Service.RepairOrders
{
    public class RepairOrder
    {
        private static readonly NLog.Logger _nLog = NLog.LogManager.GetCurrentClassLogger();
        ClintInToClintLog _clintInToClintLog;
        public RepairOrder()
        {
            _clintInToClintLog = new ClintInToClintLog();
        }
        public void  Repair(string clienInPath="")
        {
            var serializeTimeLogPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"logs/{ DateTime.Now.ToString("yyyy-MM")}/{DateTime.Now.ToString("yyyy-MM-dd")}/SerializeTime.log");
            var serializeTimes = TxtFile.ReadTxtFile(serializeTimeLogPath);
            var lastestLogTime = serializeTimes.LastOrDefault();
            if (string.IsNullOrEmpty(lastestLogTime))
            {
                _nLog.Info("SerializeTime.log没有持久化订单记录。");
                return;
            }
            //2020-09-09 17:45:30.985 20200909 17:45:30.985|20200909 17:45:30.985
            var serializeDuration = lastestLogTime.Substring(24);
            var duration = serializeDuration.Split('|');
            var startTime = DateTime.Parse(duration[0]);
            var endTime= DateTime.Parse(duration[1]);

            MemoryData.Init();
            if(string.IsNullOrEmpty(clienInPath))
            {
                var dateStr = DateTime.Now.ToString("yyyyMMdd");
                clienInPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"log/{dateStr}/ClientIn_{dateStr}.log");
            }

            var clientInDatas = _clintInToClintLog.ReadClientInData(clienInPath);
            var toClientPath = clienInPath.Replace("ClientIn", "ToClient");
            var toClientDatas = _clintInToClintLog.ReadToClientData(toClientPath);
         
        }
    }
}
