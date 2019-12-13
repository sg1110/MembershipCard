using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using MembershipCardSystem.DataStore;
using MembershipCardSystem.LogIn;
using MembershipCardSystem.Status;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.Examples;
using Swashbuckle.AspNetCore.SwaggerGen;

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
                ExpirationScanFrequency = TimeSpan.FromSeconds(10)
            }));

            services.AddSingleton<CachingPin>();

            services.AddSingleton(svc => new StatusSettings(
                Config.ApplicationConfiguration["application:version"],
                Config.ApplicationConfiguration["application:environment"]
            ));
            
            services.AddTransient<IMembershipCardRepository, MembershipCardRepository>();

            services.AddTransient<IDbConnection>( _ => new SqlConnection(Configuration["ConnectionString:MembershipCard"]));
            
           ConfigureSwaggerServices(services);



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
            
            
            app.UseSwagger(c => c.RouteTemplate = "membershipcard/{documentName}/_interface")
                .UseSwaggerUI(c =>
                {
                    c.RoutePrefix = "membershipcard/swagger";
                    c.SwaggerEndpoint("/membershipcard/v1/_interface", "Membership Card v1");
                });
        }

        private static void ConfigureSwaggerServices(IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Info
                {
                    Title = "Membership Card",
                    Version = "v1",
                    Description = "API set for membership card",
                });
                
                options.CustomOperationIds(apiDesc =>
                {
                    var methodName = apiDesc.TryGetMethodInfo(out var methodInfo)
                        ? methodInfo.Name
                        : apiDesc.HttpMethod;
                    return $"PS_{methodName}";
                });
                options.CustomSchemaIds(type => $"PS_{type.FullName}");
//                options.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>>
//                {
//                    {"Pin", Enumerable.Empty<string>()},
//                });
                options.EnableAnnotations();
                options.IgnoreObsoleteActions();
                options.IgnoreObsoleteProperties();
                options.OperationFilter<ExamplesOperationFilter>();
            });
        }
   }
}