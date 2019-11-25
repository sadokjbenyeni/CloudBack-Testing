using BoDi;
using CloudBacktesting.SubscriptionService.Domain.Aggregates.SubscriptionRequestAggregate.Commands;
using CloudBacktesting.SubscriptionService.Domain.Aggregates.SubscriptionRequestAggregate.Events;
using CloudBacktesting.SubscriptionService.Domain.Repositories.SubscriptionAccountRepository;
using CloudBacktesting.SubscriptionService.Domain.Repositories.SubscriptionRequestRepository;
using CloudBacktesting.SubscriptionService.Specs.Infra.Authentification;
using CloudBacktesting.SubscriptionService.WebAPI.Controllers;
using CloudBacktesting.SubscriptionService.WebAPI.Host;
using EventFlow;
using EventFlow.AspNetCore.Extensions;
using EventFlow.DependencyInjection.Extensions;
using EventFlow.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Web.Http;
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
            app.UseMvc(routes =>
            {
                routes.MapRoute(name: "default", template: "api/{controller=Home}/{action=Index}/{id?}");
            });
        }
        
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            base.ConfigureWebHost(builder);
            builder.ConfigureAppConfiguration((context, config) => config.AddJsonFile("appsettings.json"));
            builder.ConfigureServices(ConfigureServices);
        }

        private void ConfigureServices(IServiceCollection serviceCollection)
        {            
            ServiceCollection = serviceCollection;
            serviceCollection.AddSingleton(container);
            RegisterSpecflowDependecies(serviceCollection);            
            UseCurrentIOCConfiguration(serviceCollection);
            AddTransientBindingAttribute(serviceCollection);
            UseMapController(serviceCollection);
            UseEventFlowInMemory(serviceCollection);
            AddAuthentication(serviceCollection);
        }

        private void UseMapController(IServiceCollection serviceCollection)
        {
            
            //.Where(@type => type.BaseType == controllerBaseType)
            //.Where(@type => type.GetCustomAttribute(apiControllerAttributeType) != null && type.BaseType == controllerBaseType)
            ;
            foreach (var type in typeof(SubscriptionAccountController).Assembly.DefinedTypes.Where(@type => type.BaseType == typeof(ControllerBase)))
            {
                serviceCollection.AddTransient(type);
            }
            
        }

        private void UseEventFlowInMemory(IServiceCollection services)
        {
            services.AddEventFlow(options => options.AddAspNetCore()
                                       .AddEvents(typeof(SubscriptionRequestCreatedEvent).Assembly)
                                       .AddCommands(typeof(SubscriptionRequestCreationCommand).Assembly, type => true)
                                       .AddCommandHandlers(typeof(SubscriptionRequestCreationCommandHandler).Assembly)
                                       .UseConsoleLog()
                                       .UseInMemoryReadStoreFor<SubscriptionAccountReadModel>()
                                       .UseInMemoryReadStoreFor<SubscriptionRequestReadModel>()
                    );
        }
        
        private static void AddAuthentication(IServiceCollection serviceCollection)
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
            //var configurationStartup = Substitute.For<IConfiguration>();
            //serviceCollection.AddSingleton(configurationStartup);
            var config = serviceCollection.BuildServiceProvider().GetService<IConfiguration>();
            var startup = new Startup(config)
            {
                UseEventFlowOptionsBuilder = false
            };
            startup.ConfigureServices(serviceCollection);
            serviceCollection.AddSingleton(startup);
        }

        private static void RegisterSpecflowDependecies(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton(ResolveInObjectContainer<ScenarioContext>);
            serviceCollection.AddSingleton(ResolveInObjectContainer<FeatureContext>);
            serviceCollection.AddSingleton(ResolveInObjectContainer<TestThreadContext>);
        }

        private static void AddTransientBindingAttribute(IServiceCollection serviceCollection)
        {
            var controllerTypes = typeof(TestStartup)
                                        .Assembly
                                        .GetTypes()
                                        .Union(typeof(SubscriptionAccountController).Assembly.GetTypes())
                                        .Where(t => Attribute.IsDefined(t, typeof(BindingAttribute)));

            foreach (var type in controllerTypes)
            {
                serviceCollection.AddTransient(type);
            }
        }

        private static T ResolveInObjectContainer<T>(IServiceProvider provider)
        {
            var container = provider.GetRequiredService<IObjectContainer>();
            return container.Resolve<T>();
        }
    }

    
}
