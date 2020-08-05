using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Utility.MemoryDataManager.Persist
{
    class SQLitePersist : IPersist
    {
        private static readonly NLog.Logger _nLog = NLog.LogManager.GetCurrentClassLogger();
        private object _lockObj = new object();

        public void Load()
        {
            throw new NotImplementedException();
        }

    
        public void Persist()
        {
            lock (_lockObj)
            {
            }
        }
    }
}
