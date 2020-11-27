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
            //�����ýڵ���ӵ��ڴ�
            //builder.AddInMemoryCollection();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            //���������ļ�
            builder.AddJsonFile("appsettings.json");
            //builder.AddJsonFile("appsettingsextention.json");

            IConfiguration configuration = builder.Build();
            ////���key���ڴ�
            ////configuration["con"] = "val";   
            //var str = configuration["ConnectionStrings:WMSConnectionString:ConnectionString"];
            //var stt = configuration["Swagger:Enable"];
            ////��ȡ�Ķ���string:   ������string this[string key]
            //var bo = configuration["Swagger:EnableBool"];
            //var num = configuration["Swagger:EnableCount"];
            var str = configuration["NewClassesConStr"];
        }
    }
}
