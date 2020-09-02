using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TaskManagerApp.BusinessLogicLayer.Abstract;
using TaskManagerApp.BusinessLogicLayer.Concrete;
using TaskManagerApp.Core.CrossCuttingConcerns.Logging.LoggerProviders;
using TaskManagerApp.DataAccessLayer.Abstract;
using TaskManagerApp.DataAccessLayer.Concrete.EntityFramework;

namespace TaskManagerApp.WebUi
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
            services.AddMvc();
            services.AddControllersWithViews();


            services.AddAuthentication("SecurityScheme")
                         .AddCookie("SecurityScheme", options =>
                         {
                             options.LoginPath = "/Account/Login";
                             options.Cookie = new CookieBuilder
                             {
                          //Domain = "",
                          HttpOnly = false,
                                 Name = "Login.Security.Cookie",
                                 Path = "/",
                              

                             };
                         });

            services.AddScoped<IUserService, UserManager>();
            services.AddScoped<ITaskService, TaskManager>();
            services.AddScoped<ITaskTypeService, TaskTypeManager>();
            services.AddScoped<ITaskTypeDal, EfTaskTypeDal>();
            services.AddScoped<IUserDal, EfUserDal>();
            services.AddScoped<ITaskDal, EfTaskDal>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddProvider(new FileLoggerProvider($"{env.ContentRootPath}/log.txt"));
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
