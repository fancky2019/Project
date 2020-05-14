using QuickFix.FIX42;
using System.Collections.Generic;
using TTMarketAdapter.Model;

namespace TTMarketAdapter.Utilities
{
    public static class GlobalData
    {
        internal  static List<SecurityDefinition> allSecuriryList = new List<SecurityDefinition>();
        //    public static OrderBookMgr orderBookMgr = null;

        internal static HashSet<uint> optionSecurityIDSet = new HashSet<uint>();

        public static Dictionary<string, string> ICEMonthDict { get; private set; }
        public static List<OrderModel> OrderModelList { get; private set; }
        static GlobalData()
        {
            ICEMonthDict = new Dictionary<string, string>();
            ICEMonthDict.Add("Jan", "01");
            ICEMonthDict.Add("Feb", "02");
            ICEMonthDict.Add("Mar", "03");
            ICEMonthDict.Add("Apr", "04");
            ICEMonthDict.Add("May", "05");
            ICEMonthDict.Add("Jun", "06");
            ICEMonthDict.Add("Jul", "07");
            ICEMonthDict.Add("Aug", "08");
            ICEMonthDict.Add("Sep", "09");
            ICEMonthDict.Add("Oct", "10");
            ICEMonthDict.Add("Nov", "11");
            ICEMonthDict.Add("Dec", "12");
            OrderModelList = OrderModel.FromFile();
        }
    }
}
