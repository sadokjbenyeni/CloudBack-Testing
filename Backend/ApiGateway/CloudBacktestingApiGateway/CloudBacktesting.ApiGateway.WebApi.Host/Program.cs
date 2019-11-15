using CloudBacktesting.ApiGateway.WebApi.Ocelot;
using Microsoft.AspNetCore;
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

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                       .ConfigureAppConfiguration((host, config) =>
                       {
                           config.AddOcelot(host.HostingEnvironment);
                       }).UseStartup<Startup>();
    }
}
