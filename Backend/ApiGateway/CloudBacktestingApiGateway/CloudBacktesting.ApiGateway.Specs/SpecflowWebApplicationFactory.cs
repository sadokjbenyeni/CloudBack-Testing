using System;
using BoDi;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Ocelot.DependencyInjection;
using TechTalk.SpecFlow;

namespace CloudBacktesting.ApiGateway.Specs
{
    public class SpecflowWebApplicationStartup : WebApplicationFactory<SpecflowAspnetCoreStartup>
    {
        private readonly IObjectContainer container;

        public IServiceCollection ServiceCollection { get; private set; }

        public SpecflowWebApplicationStartup(IObjectContainer container)
        {
            this.container = container ?? throw new ArgumentNullException(nameof(container));
        }

        protected override IHostBuilder CreateHostBuilder()
        {
            return Host.CreateDefaultBuilder(null)
                    .ConfigureWebHostDefaults(webBuilder =>
                    {
                        webBuilder.UseStartup<SpecflowAspnetCoreStartup>();
                        webBuilder.ConfigureAppConfiguration((host, config) =>
                    {
                        config.AddOcelot(host.HostingEnvironment);
                    });
                });
        }

        #region Web Builder Configuration
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
            //UseCurrentIOCConfiguration(serviceCollection);
            //InjectSubstituteServices(serviceCollection);
            //InjectSubstituteByContainer(serviceCollection);
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

        #endregion Web Builder Configuration
    }
}
