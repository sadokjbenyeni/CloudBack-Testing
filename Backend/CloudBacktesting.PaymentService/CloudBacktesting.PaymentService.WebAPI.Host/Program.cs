using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System.IO;

namespace CloudBacktesting.PaymentService.WebAPI.Host
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = new HostBuilder()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseKestrel(serverOptions =>
                    {
                    })
                    .UseIISIntegration()
                    .UseStartup<Startup>();
                })
                .Build();

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
               Microsoft.Extensions.Hosting.Host.CreateDefaultBuilder(args)
        .ConfigureWebHostDefaults(webBuilder =>
        {
            webBuilder.ConfigureKestrel(serverOptions =>
            {
            })
            .ConfigureAppConfiguration((host, config) =>
            {
                config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                      .AddJsonFile($"appsettings.{host.HostingEnvironment.EnvironmentName}.json", optional: false, reloadOnChange: true);
            })
            .UseStartup<Startup>();
        });
    }
}
