using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Ocelot.DependencyInjection;

[assembly: HostingStartup(typeof(CloudBacktesting.ApiGateway.Specs.SpecflowAspnetCoreStartup))]
namespace CloudBacktesting.ApiGateway.Specs
{

    public class SpecflowAspnetCoreStartup/* : OcelotStartup*/
    {
        public SpecflowAspnetCoreStartup(IConfiguration configuration) /*: base(configuration)*/
        {
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<SpecflowAspnetCoreStartup>();
                    webBuilder.ConfigureAppConfiguration((host, config) =>
                    {
                        config.AddOcelot(host.HostingEnvironment);
                    });
                });
    }
}
