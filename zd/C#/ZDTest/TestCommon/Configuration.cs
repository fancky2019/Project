using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestCommon
{
    /*
     * NuGet
     * Microsoft.Extensions.Configuration
     * Microsoft.Extensions.Configuration.FileExtensions
     * Microsoft.Extensions.Configuration.Json
     * */
    public class Configurations
    {
        static volatile IConfiguration _configuration;
        public static IConfiguration Configuration
        {
            get
            {
                return _configuration;
            }
            private set
            {
                _configuration = value; ;
            }
        }

        static Configurations()
        {
            LoadConfiguration();
        }

        internal static void LoadConfiguration()
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(AppDomain.CurrentDomain.BaseDirectory);
            //设置配置文件
            //builder.AddJsonFile("appsettings.json");
            //其中optional为是否为可选的，选择true则可以在没有appsettings.json的时候不抛出异常
            // reloadOnChange为时候支持热更新，true为支持
            builder.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            Configuration = builder.Build();


            //var str = Configurations.Configuration["ZDFixService:OrderIDFilePath"];
        }
    }
}
