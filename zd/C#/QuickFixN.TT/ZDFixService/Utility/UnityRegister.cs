using CommonClassLib;
using System;
using System.Collections.Generic;
using System.Text;
using Unity;
using ZDFixService.Service.Base;
using ZDFixService.Service.PSHK;
using ZDFixService.Service.TT;
using ZDFixService.Utility.Queue;

namespace ZDFixService.Utility
{
    class UnityRegister
    {
        static IUnityContainer _container = null;
        static UnityRegister()
        {
            _container = new UnityContainer();
        }
        internal static void Register()
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


            //IUnityContainer container = new UnityContainer();

            //后面注册会覆盖前面的
            //container.RegisterType<ITradeService, TTTradeService>("TTTradeService");
            _container.RegisterSingleton<ITradeService, TTTradeService>("TTTradeService");
            _container.RegisterSingleton<ITradeService, PSHKTradeService>("PSHKTradeService");

            _container.RegisterSingleton<IMessageQueue<NetInfo>, MemoryQueue<NetInfo>>("MemoryQueue");
            _container.RegisterSingleton<IMessageQueue<NetInfo>, RabbitMQQueue<NetInfo>>("RabbitMQQueue");
            _container.RegisterSingleton<IMessageQueue<NetInfo>, RedisQueue<NetInfo>>("RedisQueue");

        }
        internal static T Resolve<T>(string typeName)
        {
            //var tradeServiceName = Configurations.Configuration["ZDFixService:ITradeService"];
            //指定命名解析对象
            return _container.Resolve<T>(typeName);
        }
    }
}
