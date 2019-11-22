using CloudBacktesting.ApiGateway.WebApi.Host.KestrelConfigurationModel;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Ocelot.DependencyInjection;
using System.IO;

namespace CloudBacktesting.ApiGateway.WebApi.Host
{
    public class Program
    {

        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            var settingsfile = File.ReadAllText("appsettings.json");
            var webhost = WebHost.CreateDefaultBuilder(args)
                     .ConfigureAppConfiguration((host, config) =>
                     {
                         config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                               .AddJsonFile($"appsettings.{host.HostingEnvironment.EnvironmentName}.json", optional: false, reloadOnChange: true)
                               .AddOcelot(host.HostingEnvironment);

                     })
                  .UseKestrel()
                  .UseUrls(JsonConvert.DeserializeObject<KestrelConfiguration>(settingsfile).BaseUrls)
                  .UseStartup<Startup>();
            return webhost;
        }
    }
}
