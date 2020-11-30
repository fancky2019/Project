using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace NewClassesApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //ע����
            services.AddDbContext<NewClassesDbContext>(options =>
            {
                options.UseSqlServer(Configuration["NewClassesConStr"]);
            });
            //IIS
            services.Configure<IISOptions>(options =>
            {
                options.AutomaticAuthentication = false;
                options.ForwardClientCertificate = false;

            });
            #region ����

            services.AddCors(options =>
            {
                options.AddPolicy("AnyOrigin", 
                    builder => {
                        //֧�ֶ�������˿ڣ�ע��˿ںź�Ҫ��/б�ˣ�����localhost:8000/���Ǵ�� 
                        //.WithOrigins("https://127.0.0.1:44329", "http://127.0.0.1:44329", "http://localhost:8021", "http://localhost:8081")

                        builder.AllowAnyOrigin().
                                AllowAnyMethod().
                                AllowAnyHeader(); 
                    });
            });

            #endregion
    

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();
           
            app.UseAuthorization();

            //����:λ��Ҫ��UseRouting()֮��UseEndpoints֮ǰ��
            app.UseCors("AnyOrigin");
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }



    }
}
