using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Ocelot.DependencyInjection;
using System;

namespace CloudBacktesting.ApiGateway.WebApi.Host
{
    public class Program
    {

        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        =>
             WebHost.CreateDefaultBuilder(args)
                     .ConfigureAppConfiguration((host, config) =>
                     {
                         Console.WriteLine($"The Hosting environment name is : {host.HostingEnvironment.EnvironmentName}");
                         config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                               .AddJsonFile($"appsettings.{host.HostingEnvironment.EnvironmentName}.json", optional: false, reloadOnChange: true)
                               .AddOcelot(host.HostingEnvironment);

                     })
                  //.UseKestrel()
                  //.UseUrls(JsonConvert.DeserializeObject<KestrelConfiguration>(settingsfile).BaseUrls)
                  .UseStartup<Startup>();
        
    }
}
