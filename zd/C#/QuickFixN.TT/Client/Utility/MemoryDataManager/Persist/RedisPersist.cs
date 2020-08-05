using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Utility.MemoryDataManager.Persist
{
    class RedisPersist : IPersist
    {
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
