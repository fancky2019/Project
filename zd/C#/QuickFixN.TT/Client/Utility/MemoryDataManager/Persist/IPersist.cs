﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Utility.MemoryDataManager.Persist
{
    interface IPersist
    {
        void Persist();
        void Load();
    }
}
