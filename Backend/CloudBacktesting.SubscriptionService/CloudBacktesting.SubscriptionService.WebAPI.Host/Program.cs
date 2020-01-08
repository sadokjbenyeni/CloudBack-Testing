using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace CloudBacktesting.SubscriptionService.WebAPI.Host
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args)
                .Build()
                .Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                   .ConfigureAppConfiguration((host, config) =>
                   {
                       config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                             .AddJsonFile($"appsettings.{host.HostingEnvironment.EnvironmentName}.json", optional: false, reloadOnChange: true);
                   })
                   .UseStartup<Startup>();
    }
}
