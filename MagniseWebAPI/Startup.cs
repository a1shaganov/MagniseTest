using MagniseWebAPI.Storages;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;

namespace MagniseWebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            DataStorage.Instance.Initialize("https://platform.fintacharts.com/",
                "wss://platform.fintacharts.com/api/streaming/ws/v1/realtime?token=",
                new Dictionary<String, String>()
                {
                    { "Token", "identity/realms/fintatech/protocol/openid-connect/token" },
                    { "Values", "api/instruments/v1/instruments" },
                    { "Providers", "api/instruments/v1/providers" },
                    { "Exchanges", "api/instruments/v1/exchanges" },
                }, "r_test@fintatech.com", "kisfiz-vUnvy9-sopnyv");
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
