using BoDi;
using CloudBacktesting.SubscriptionService.Specs.Infra.Authentification;
using CloudBacktesting.SubscriptionService.WebAPI.Controllers;
using CloudBacktesting.SubscriptionService.WebAPI.Host;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace CloudBacktesting.SubscriptionService.Specs.Host
{
    public class SpecWebApplicationFactory : WebApplicationFactory<SpecWebApplicationFactory>
    {
        private readonly IObjectContainer container;

        public SpecWebApplicationFactory(IObjectContainer container)
        {
            this.container = container;
        }
       
        public IServiceCollection ServiceCollection { get; private set; }

        protected override IWebHostBuilder CreateWebHostBuilder()
        {
            var builder = new WebHostBuilder();
            return builder.UseStartup<SpecWebApplicationFactory>()
                          .UseEnvironment("Development");
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //app.UseWebSockets();
            //app.UseAuthentication();
            //app.UseStaticFiles();
            //app.UseSpaStaticFiles();

            //app.UseMvc(routes =>
            //{
            //    routes.MapRoute(
            //        name: "default",
            //        template: "api/v{version}/{controller=Home}/{action=Index}/{id?}");
            //});

            //app.UseSpa(spa =>
            //{
            //    spa.Options.SourcePath = "ClientApp";
            //});

        }


        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            base.ConfigureWebHost(builder);
            builder.ConfigureServices(ConfigureServices);
        }

        private void ConfigureServices(IServiceCollection serviceCollection)
        {
            ServiceCollection = serviceCollection;
            RegisterSpecflowDependecies(serviceCollection);
            serviceCollection.AddSingleton(container);
            UseCurrentIOCConfiguration(serviceCollection);
            InjectSubstituteServices(serviceCollection);
            InjectSubstituteByContainer(serviceCollection);
        }

        //private static void InjectSubstituteByContainer(IUnityContainer container)
        private static void InjectSubstituteByContainer(IServiceCollection serviceCollection)
        {
            var controllerTypes = typeof(TestStartup)
                                                .Assembly
                                                .GetTypes()
                                                .Union(typeof(SubscriptionRequestController).Assembly.GetTypes())
                                                .Where(t => Attribute.IsDefined(t, typeof(BindingAttribute)));

            foreach (var type in controllerTypes)
            {
                serviceCollection.AddTransient(type);
            }
        }

        private static void InjectSubstituteServices(IServiceCollection serviceCollection)
        {
            serviceCollection
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = "Test Scheme";
                    options.DefaultChallengeScheme = "Test Scheme";
                })
                .AddTestAuth(options => { });
        }

        private static void UseCurrentIOCConfiguration(IServiceCollection serviceCollection)
        {
            var configurationStartup = Substitute.For<IConfiguration>();
            serviceCollection.AddSingleton(configurationStartup);
            var startup = new Startup(configurationStartup);
            startup.ConfigureServices(serviceCollection);
            serviceCollection.AddSingleton(startup);
        }

        private static void RegisterSpecflowDependecies(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton(ResolveInObjectContainer<ScenarioContext>);
            serviceCollection.AddSingleton(ResolveInObjectContainer<FeatureContext>);
            serviceCollection.AddSingleton(ResolveInObjectContainer<TestThreadContext>);
        }

        private static T ResolveInObjectContainer<T>(IServiceProvider provider)
        {
            var container = provider.GetRequiredService<IObjectContainer>();
            return container.Resolve<T>();
        }


    }

    
}
