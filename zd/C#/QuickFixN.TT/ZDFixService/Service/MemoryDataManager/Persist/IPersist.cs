using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZDFixService.Service.MemoryDataManager.Persist
{
    interface IPersist
    {
        void Persist();
        void Load();
    }
}
