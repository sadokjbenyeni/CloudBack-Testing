using CloudBacktesting.SubscriptionService.WebAPI.Models;
using CloudBacktesting.SubscriptionService.WebAPI.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace CloudBacktesting.SubscriptionService.WebAPI.Host
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
            services.Configure<SubscriptionDatabaseSettings>(
                Configuration.GetSection(nameof(SubscriptionDatabaseSettings)));

            services.AddSingleton<ISubscriptionDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<SubscriptionDatabaseSettings>>().Value);

            services.AddSingleton<SubscriptionAccountService>();

            services.AddControllers().AddNewtonsoftJson(options => options.UseMemberCasing());
        }
        //public void ConfigureServices(IServiceCollection services)
        //{
        //    // requires using Microsoft.Extensions.Options
        //    services.Configure<SubscriptionDatabaseSettings>(
        //        Configuration.GetSection(nameof(SubscriptionDatabaseSettings)));

        //    services.AddSingleton<ISubscriptionDatabaseSettings>(sp =>
        //        sp.GetRequiredService<IOptions<SubscriptionDatabaseSettings>>().Value);

        //    services.AddSingleton<SubscriptionService>();

        //    services.AddControllers();
        //}

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

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
