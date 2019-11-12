using CloudBacktesting.ApiGateway.WebApi.Ocelot;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Ocelot.DependencyInjection;

namespace CloudBacktesting.ApiGateway.WebApi.Host
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateWebHostBuilder(string[] args) =>
            Microsoft.Extensions.Hosting.Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                  {
                      //webBuilder.UseStartup<OcelotStartup>();
                      webBuilder.ConfigureAppConfiguration((host, config) =>
                      {
                          config.AddOcelot(host.HostingEnvironment);
                      });
                  });
    }
}
