using EventFlow;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using EventFlow.Queries;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.TestHost;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore;

namespace CloudBacktesting.SubscriptionService.WebAPI.Tests
{
    public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override IWebHostBuilder CreateWebHostBuilder()
        {
            return WebHost.CreateDefaultBuilder()
                          .UseStartup<TStartup>();
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var serviceProvider = services.BuildServiceProvider();
                using (var scope = serviceProvider.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var logger = scopedServices.GetRequiredService<ILogger<CustomWebApplicationFactory<TStartup>>>();
                }
            });
            builder.ConfigureTestServices(services =>
            {                
                //ClearEventFlowDependencies(services);
                services.AddSingleton(Substitute.For<ICommandBus>());
                services.AddSingleton(Substitute.For<IQueryProcessor>());
                
                //services.AddAuthentication("subscriptionaccount-e76a105c-b10d-4953-8907-67b0d19a1e8c").AddTestAuth("subscriptionaccount-e76a105c-b10d-4953-8907-67b0d19a1e8c", options => options.ClaimsIssuer = "subscriptionaccount-e76a105c-b10d-4953-8907-67b0d19a1e8c");
                //services.AddAuthentication("subscriptionaccount-76ac3644-10e9-4e89-bd2d-9bd4fc7b5192").AddTestAuth("subscriptionaccount-76ac3644-10e9-4e89-bd2d-9bd4fc7b5192", options => options.ClaimsIssuer = "subscriptionaccount-76ac3644-10e9-4e89-bd2d-9bd4fc7b5192");

                //.AddScheme<AuthenticationSchemeOptions, TestAuthenticationHandler>("Test", options => { });
            });
        }

    }
}
