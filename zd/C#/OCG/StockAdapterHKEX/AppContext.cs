using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Windows;


namespace StockAdapterHKEX
{
    public class AppContext
    {
        public TradeApp tradeApp = null;

        public string ConfigFile { get; set; }

        //public void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        //{
        //    Trace.WriteLine("Uncaught exception:");
        //    Trace.WriteLine(e.Exception.ToString());
        //}

        public void Application_Startup(
            object sender,
            EventArgs e,
            ICustomFixStrategy strategy)
        {
            /* // old QuickFix version
            
            // FIX app settings and related
            QuickFix.SessionSettings settings = new QuickFix.SessionSettings(this.ConfigFile);
            strategy.SessionSettings = settings;

            // FIX application setup
            QuickFix.MessageStoreFactory storeFactory = new QuickFix.FileStoreFactory(settings);
            QuickFix.LogFactory logFactory = new QuickFix.FileLogFactory(settings);
            tradeApp = new TradeApp(settings, strategy);

            QuickFix.IInitiator initiator = new QuickFix.Transport.SocketInitiator(tradeApp, storeFactory, settings, logFactory);
            tradeApp.Initiator = initiator;
            */

            // FIX app settings and related
            QuickFix.SessionSettings settings = new QuickFix.SessionSettings(this.ConfigFile);
            strategy.SessionSettings = settings;

            // FIX application setup
            QuickFix.IMessageStoreFactory storeFactory = new QuickFix.FileStoreFactory(settings);
            QuickFix.ILogFactory logFactory = new QuickFix.FileLogFactory(settings);
            tradeApp = new TradeApp(settings, strategy);

            QuickFix.IInitiator initiator = new QuickFix.Transport.SocketInitiator(tradeApp, storeFactory, settings, logFactory);
            tradeApp.Initiator = initiator;
           
        }

        public void connectGlobex()
        {
            try
            {
                Trace.WriteLine("Application exit.");
                tradeApp.Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public void disconnectGlobex()
        {
            try
            {
                Trace.WriteLine("Application exit.");
                tradeApp.Stop();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
