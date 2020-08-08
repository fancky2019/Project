using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace ZDFixService
{
    class Configurations
    {
        public static IConfiguration Configuration { get; private set; }
        /*
         * NuGet
         * Microsoft.Extensions.Configuration
         * Microsoft.Extensions.Configuration.FileExtensions
         * Microsoft.Extensions.Configuration.Json
         * */
        static Configurations()
        {
            var builder = new ConfigurationBuilder();
            //将配置节点添加到内存
            //builder.AddInMemoryCollection();
            builder.SetBasePath(AppContext.BaseDirectory);
            //设置配置文件
            builder.AddJsonFile("appsettings.json");

            Configuration = builder.Build();
            //添加key到内存
            //configuration["con"] = "val";   
            var str = Configurations.Configuration["ZDFixService:OrderIDFilePath"];

        }
    }
}
