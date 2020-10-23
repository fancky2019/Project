using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ViewModel
{
    public class MemoryData
    {
        public static ConcurrentDictionary<string, User> Users { get; set; }
        static MemoryData()
        {
            Users = new ConcurrentDictionary<string, User>();
        }
    }


}
