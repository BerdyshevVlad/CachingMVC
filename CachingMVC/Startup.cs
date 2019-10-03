using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CachingMVC.Models.Context;
using CachingMVC.Repositories;
using CachingMVC.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CachingMVC
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

            string connectionString = @"Server=localhost;Database=mobilestoredb;Trusted_Connection=True;";
            // внедрение зависимости Entity Framework
            services.AddDbContext<MobileContext>(options =>
                options.UseSqlServer(connectionString));
            // внедрение зависимости ProductService
            services.AddTransient<ProductRepository>();
            services.AddTransient<PostRepository>();
            services.AddTransient<CashService>();
            // добавление кэширования
            services.AddMemoryCache();


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
