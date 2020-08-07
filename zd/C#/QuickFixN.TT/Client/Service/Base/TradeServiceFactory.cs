using Client.Service.PSHK;
using Client.Service.TT;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;
using Unity;

namespace Client.Service.Base
{
    public class TradeServiceFactory
    {
        public static ITradeService ITradeService { get; private set; }



        static TradeServiceFactory()
        {
            ITradeService = RegisterResolve();
        }

        static ITradeService RegisterResolve()
        {

            //var tpyes=  AppDomain.CurrentDomain.GetAssemblies()
            //.SelectMany(a => a.GetTypes().Where(t => t.GetInterfaces().Contains(typeof(ITradeService))))
            //.ToList();


            IUnityContainer container = new UnityContainer();

            //后面注册会覆盖前面的
            //container.RegisterType<ITradeService, TTTradeService>("TTTradeService");
            container.RegisterSingleton<ITradeService, TTTradeService>("TTTradeService");
            container.RegisterSingleton<ITradeService, PSHKTradeService>("PSHKTradeService");
            
            var tradeServiceName = ConfigurationManager.AppSettings["ITradeService"].ToString();
            //指定命名解析对象
            ITradeService tradeService = container.Resolve<ITradeService>(tradeServiceName);
            return tradeService;
        }


        internal static void Test()
        {

        }

    }
}
