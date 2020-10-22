using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZDTest
{
    public class MemoryData
    {
        public static ConcurrentDictionary<string, User> Users { get; set; }
    }

    public class User
    {
        public string ClientNo { get; set; }
        public bool  Login { get; set; }
    }
}
