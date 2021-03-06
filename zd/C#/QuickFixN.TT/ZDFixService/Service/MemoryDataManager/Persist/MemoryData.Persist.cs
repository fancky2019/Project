﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDFixService.Service.MemoryDataManager.Persist;
using ZDFixService;

namespace ZDFixService.Service.MemoryDataManager
{
    partial class MemoryData
    {

        public static volatile bool AppStop;
        internal static IPersist IPersist { get; private set; }

        static System.Threading.Timer _timer = null;

        private static void InitPersist()
        {
            //持久化方式：SQLite,REDIS,FILE
            var persistType = Configurations.Configuration["ZDFixService:Persist:PersistType"].ToString().ToUpper();

            switch (persistType)
            {
                case "SQLITE":
                    IPersist = new SQLitePersist();
                    break;
                case "REDIS":
                    IPersist = new RedisPersist();
                    break; ;
                case "FILE":
                default:
                    IPersist = new FilePersist();
                    break;
            }

            IPersist.Load();
            PersistTimer();
        }

        internal static void PersistTimer()
        {
            var persistInterval = int.Parse(Configurations.Configuration["ZDFixService:Persist:PersistInterval"].ToString());
            _timer = new System.Threading.Timer((param) =>
            {
                if (!AppStop)
                {
                    RemoveExpireDayOrder();
                    IPersist.Persist();
                }
            }, null, persistInterval * 1000, persistInterval * 1000);
        }
    }
}
