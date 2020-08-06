using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;

namespace Client.Service
{
    public class TradeServiceFactory
    {
        public static ITradeService ITradeService { get; private set; }

        static TradeServiceFactory()
        {
            ITradeService = new TTTradeService();
        }


    }
}
