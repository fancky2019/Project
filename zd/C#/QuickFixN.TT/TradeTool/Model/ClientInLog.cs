using CommonClassLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeTool.Model
{
    public class ClientInLog
    {
        public DateTime LogTime { get; set; }

        public string SystemCode
        {
            get
            {
                return this.NetInfo.systemCode;
            }
        }

        public string Command
        {
            get
            {
                return this.NetInfo.code;
            }
        }
        public NetInfo NetInfo { get; set; }
    }
}
