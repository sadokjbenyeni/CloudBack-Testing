using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System.IO;

namespace CloudBacktesting.PaymentService.WebAPI.Host
{
    public class Program
    {
        //public static void Main(string[] args)
        //{
        //    CreateWebHostBuilder(args)
        //        .Build()
        //        .Run();
        //}

        //public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
        //    WebHost.CreateDefaultBuilder(args)
        //           .UseStartup<Startup>();
        //public static void Main(string[] args)
        //{
        //    CreateHostBuilder(args).Build().Run();
        //}

        //public static IHostBuilder CreateHostBuilder(string[] args) =>
        //    Microsoft.Extensions.Hosting.Host.CreateDefaultBuilder(args)
        //        .ConfigureWebHostDefaults(webBuilder =>
        //        {
        //            webBuilder.UseStartup<Startup>();
        //        });

        public static IHostBuilder CreateHostBuilder(string[] args) =>
               Microsoft.Extensions.Hosting.Host.CreateDefaultBuilder(args)
        .ConfigureWebHostDefaults(webBuilder =>
        {
            webBuilder.ConfigureKestrel(serverOptions =>
            {
                // Set properties and call methods on options
            })
            .UseStartup<Startup>();
        });

        public static void Main(string[] args)
        {
            var host = new HostBuilder()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseKestrel(serverOptions =>
                    {
                // Set properties and call methods on options
                    })
                    .UseIISIntegration()
                    .UseStartup<Startup>();
                })
                .Build();

            host.Run();
        }
    }
}
