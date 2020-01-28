
using CloudBacktesting.ApiGateway.WebApi.Ocelot.Middlewares;
using CloudBacktesting.ApiGateway.WebApi.Ocelot.Services;
using Consul;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Provider.Consul;
using System;

namespace CloudBacktesting.ApiGateway.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddSingleton<IConsulClient, ConsulClient>(p => new ConsulClient(consulConfig =>
            {
                var address = $"http://{Configuration.GetSection("ConsulEndpoint").GetValue<string>("IPAdress")}:{Configuration.GetSection("ConsulEndpoint").GetValue<string>("Port")}";
                Console.WriteLine($"consul endpoint is ${address}");
                consulConfig.Address = new Uri(address);
            }));
            services.AddTransient<IUserService, UserService>();
            services.AddOcelot(Configuration).AddConsul();
            services.AddCors(options =>
            {
                options.AddPolicy("allOrigins",
                builder =>
                {
                    builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public async void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseRouting();
            app.UseCors("allOrigins");
            app.UseHealthCkeck();
            app.UseClaimsBuilder();
            await app.UseOcelot();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });
                endpoints.MapControllers();
            });
        }

    }
}
