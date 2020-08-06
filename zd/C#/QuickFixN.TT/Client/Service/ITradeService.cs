using CommonClassLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Service
{
   public interface ITradeService
    {
        event Action<string> ExecutionReport;
        event Action<string> Logon;
        event Action<string> Logout;
        void Start();
        void Stop();

        void Order(NetInfo netInfo);
    }
}
