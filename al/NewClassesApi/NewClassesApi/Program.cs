using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace NewClassesApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            NewConfiguration();
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>().UseUrls("http://*:5000;https://*:5001"); 
                });

        public static void NewConfiguration()
        {
            var builder = new ConfigurationBuilder();
            //将配置节点添加到内存
            //builder.AddInMemoryCollection();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            //设置配置文件
            builder.AddJsonFile("appsettings.json");
            //builder.AddJsonFile("appsettingsextention.json");

            IConfiguration configuration = builder.Build();
            ////添加key到内存
            ////configuration["con"] = "val";   
            //var str = configuration["ConnectionStrings:WMSConnectionString:ConnectionString"];
            //var stt = configuration["Swagger:Enable"];
            ////获取的都是string:   索引：string this[string key]
            //var bo = configuration["Swagger:EnableBool"];
            //var num = configuration["Swagger:EnableCount"];
            var str = configuration["NewClassesConStr"];
        }
    }
}
