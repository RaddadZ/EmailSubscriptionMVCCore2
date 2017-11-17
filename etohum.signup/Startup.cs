using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using etohum.signup.Entities;
using Microsoft.EntityFrameworkCore;
using etohum.signup.Services;
using Hangfire;

namespace etohum.signup
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        
        public void ConfigureServices(IServiceCollection services)
        {
            // using sql server to store serilized methods with thier parameters
            // Hangfire handles exceptions
            services.AddHangfire(x => x.UseSqlServerStorage(Configuration["ConnectionStrings:DefaultConnection"]));
            services.AddMvc();

            // using database and passing the connection string from appsettings.json
            services.AddDbContext<EtohumContext>(o => o.UseSqlServer(Configuration["ConnectionStrings:DefaultConnection"]));

            //registering services to be able to inject them
            // once per request
            services.AddScoped<IEtohumRepository, EtohumRepository>();
            // light and statless
            services.AddTransient<IMailService, LocalMailService>();
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
                app.UseExceptionHandler("/Subscribition/Error");
            }

            // Hangfire server runs under ISS
            app.UseHangfireServer();
            // Hangfire has optinal UI to keep track of background jobs and watch queues
            // should be could before UseMvc
            app.UseHangfireDashboard();

            // using wwwroot contents
            app.UseStaticFiles();

            // using conviction based routes
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Subscribition}/{action=SignUp}/{id?}");
            });
        }
    }
}
