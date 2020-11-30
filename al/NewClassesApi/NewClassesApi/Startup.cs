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
            //注入类
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
            #region 跨域

            services.AddCors(options =>
            {
                options.AddPolicy("AnyOrigin", 
                    builder => {
                        //支持多个域名端口，注意端口号后不要带/斜杆：比如localhost:8000/，是错的 
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

            //跨域:位置要在UseRouting()之后，UseEndpoints之前。
            app.UseCors("AnyOrigin");
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }



    }
}
