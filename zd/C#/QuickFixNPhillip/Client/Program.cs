using Client.FixUtility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    static class Program
    {
        /*
         * github:https://github.com/kennystone/quickfixn
         */
        private static readonly NLog.Logger _nLog = NLog.LogManager.GetCurrentClassLogger();
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);


            FrmTradeClient frmTradeClient = new FrmTradeClient();
            AppDomain.CurrentDomain.UnhandledException += (sender, e) =>
            {
                _nLog.Error(e.ToString());
                frmTradeClient.btnStop_Click(null, null);
            };

            Application.ThreadException += (sender, e) =>
            {
                _nLog.Error(e.ToString());
                frmTradeClient.btnStop_Click(null, null);
            };
            Application.Run(frmTradeClient);

        }
    }
}
