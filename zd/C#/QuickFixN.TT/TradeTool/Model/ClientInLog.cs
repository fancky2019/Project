using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeTool.Model
{
    class ClientInLog
    {
        public DateTime LogTime { get; set; }
        public string Content { get; set; }
        public string SystemCode { get; set; }
        public string CustomerNo { get; set; }
    }
}
