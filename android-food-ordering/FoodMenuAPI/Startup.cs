using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodMenuAPITest.Hubs;
using FoodMenuAPITest.Models;
using FoodMenuAPITest.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Serialization;

namespace FoodMenuAPI
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
            //Enable CORS
            services.AddCors(c =>
            {
                c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            });

            //JSON Serializer
            services.AddControllersWithViews().AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore)
            .AddNewtonsoftJson(options => options.SerializerSettings.ContractResolver
            = new DefaultContractResolver());

            //services.AddSignalR().AddJsonProtocol(options =>
            //{
            //    options.PayloadSerializerOptions.PropertyNamingPolicy = null;
            //});

            services.AddSignalR();

            services.AddTransient<ITemporaryInvoiceTableRepository, TemporaryInvoiceTableRepository>();
            //services.AddDbContext<AndroidFoodMenuDBContext>(options => options.UseSqlServer(Configuration.GetConnectionString("Database")));
            services.AddControllers();
            //services.AddSingleton<IFoodMenuApp, FoodMenuAppService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //Enable CORS
            app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

           
            //app.MapHub<ChatHub>("/chatHub");
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<ChatHub>("/chatHub");
                //app.UseSignalR(config =>
                //{
                //    config.MapHub<ChatHub>("/chatHub");
                //});
            });
        }
    }
}
