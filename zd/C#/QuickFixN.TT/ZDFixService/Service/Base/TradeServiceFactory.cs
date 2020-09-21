using ServiceStack;
using System;
using System.Linq;
using System.Reflection;
using Unity;
using ZDFixService.Service.PSHK;
using ZDFixService.Service.TT;
using ZDFixService.Utility;

namespace ZDFixService.Service.Base
{
    public class TradeServiceFactory
    {
        public static ITradeService ITradeService { get; private set; }



        static TradeServiceFactory()
        {
            UnityRegister.Register();
            var tradeServiceName = Configurations.Configuration["ZDFixService:ITradeService"];
            ITradeService = UnityRegister.Resolve<ITradeService>(tradeServiceName);// RegisterResolve();
        }

        static ITradeService RegisterResolve()
        {

            ////程序集太多会有问题
            ////var types=  AppDomain.CurrentDomain.GetAssemblies()
            ////.SelectMany(a => a.GetTypes().Where(t => t.GetInterfaces().Contains(typeof(ITradeService))))
            ////.ToList();

            //ITradeService tradeService = null;
            //Assembly assembly = Assembly.GetAssembly(typeof(ITradeService));
            //var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            //var types = assembly.GetTypes();
            //var tradeServiceName = Configurations.Configuration["ZDFixService:ITradeService"];
            //var currentService = types.Where(p => p.Name == tradeServiceName).FirstOrDefault();
            //if (currentService != null)
            //{
            //    //ServiceStack 拓展CreateInstance()
            //    tradeService = (ITradeService)currentService.CreateInstance();
            //    //tradeService = (ITradeService)Activator.CreateInstance(currentService);
            //    var serviceName = tradeService.GetType().Name;
            //}


            IUnityContainer container = new UnityContainer();

            //后面注册会覆盖前面的
            //container.RegisterType<ITradeService, TTTradeService>("TTTradeService");
            container.RegisterSingleton<ITradeService, TTTradeService>("TTTradeService");
            container.RegisterSingleton<ITradeService, PSHKTradeService>("PSHKTradeService");

            var tradeServiceName = Configurations.Configuration["ZDFixService:ITradeService"];
            //指定命名解析对象
            ITradeService tradeService = container.Resolve<ITradeService>(tradeServiceName);
            return tradeService;
        }


        static void Test()
        {
            //程序集太多会有问题
            //var types=  AppDomain.CurrentDomain.GetAssemblies()
            //.SelectMany(a => a.GetTypes().Where(t => t.GetInterfaces().Contains(typeof(ITradeService))))
            //.ToList();

            ITradeService tradeService = null;
            Assembly assembly = Assembly.GetAssembly(typeof(ITradeService));
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            var types = assembly.GetTypes();
            var tradeServiceName = Configurations.Configuration["ZDFixService:ITradeService"];
            var currentService = types.Where(p => p.Name == tradeServiceName).FirstOrDefault();
            if (currentService != null)
            {
                //tradeService = (ITradeService)currentService.CreateInstance();
                tradeService = currentService.CreateInstance<ITradeService>();
                var serviceName = tradeService.GetType().Name;
            }


        }

    }
}
