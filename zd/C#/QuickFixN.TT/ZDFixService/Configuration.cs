using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace ZDFixService
{
    /*
     * NuGet
     * Microsoft.Extensions.Configuration
     * Microsoft.Extensions.Configuration.FileExtensions
     * Microsoft.Extensions.Configuration.Json
     * */
    class Configurations
    {
        internal static IConfiguration Configuration { get; private set; }

        static Configurations()
        {
            LoadConfiguration();
        }

        private static void LoadConfiguration()
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(AppContext.BaseDirectory);
            //设置配置文件
            builder.AddJsonFile("appsettings.json");
            Configuration = builder.Build();
            //var str = Configurations.Configuration["ZDFixService:OrderIDFilePath"];
        }
    }
}
