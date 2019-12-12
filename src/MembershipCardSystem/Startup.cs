using System;
using System.Data;
using System.Data.SqlClient;
using MembershipCardSystem.DataStore;
using MembershipCardSystem.LogIn;
using MembershipCardSystem.Status;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MembershipCardSystem
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            
            
            services.AddSingleton<IMemoryCache>( _ =>new MemoryCache(new MemoryCacheOptions
            {
                ExpirationScanFrequency = TimeSpan.FromSeconds(10),
                CompactionPercentage = 0.75
            }));

            services.AddSingleton<CachingPin>();
            
            
            
            services.AddSingleton(svc => new StatusSettings(
                Config.ApplicationConfiguration["application:version"],
                Config.ApplicationConfiguration["application:environment"]
            ));
            
            services.AddTransient<IMembershipCardRepository, MembershipCardRepository>();

            services.AddTransient<IDbConnection>( _ => new SqlConnection(Configuration["ConnectionString:MembershipCard"]));
            
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}