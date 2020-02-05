using BoDi;
using CloudBacktesting.Infra.EventFlow.Queries;
using CloudBacktesting.Infra.EventFlow.Queries.InMemory;
using CloudBacktesting.Infra.EventFlow.ReadStores;
using CloudBacktesting.SubscriptionService.Domain.Aggregates.SubscriptionRequestAggregate.Commands;
using CloudBacktesting.SubscriptionService.Domain.Aggregates.SubscriptionRequestAggregate.Events;
using CloudBacktesting.SubscriptionService.Domain.Repositories.SubscriptionAccountRepository;
using CloudBacktesting.SubscriptionService.Domain.Repositories.SubscriptionRequestRepository;
using CloudBacktesting.SubscriptionService.Domain.Sagas.SubscriptionCreation;
using CloudBacktesting.SubscriptionService.WebAPI.Controllers;
using CloudBacktesting.SubscriptionService.WebAPI.Host;
using CloudBacktesting.SubscriptionService.WebAPI.Host.DatabaseSettings;
using EventFlow;
using EventFlow.AspNetCore.Extensions;
using EventFlow.DependencyInjection.Extensions;
using EventFlow.Extensions;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Web.Http;
using TechTalk.SpecFlow;

namespace CloudBacktesting.SubscriptionService.Specs.Host
{
    public class SpecWebApplicationFactory : WebApplicationFactory<BDDTestStartup>
    {
        private readonly IObjectContainer container;

        public SpecWebApplicationFactory(IObjectContainer container)
        {
            this.container = container ?? throw new ArgumentNullException(nameof(container));
        }


        protected override IWebHostBuilder CreateWebHostBuilder()
        {
            return WebHost.CreateDefaultBuilder()
                          .UseStartup<BDDTestStartup>();
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                services.AddSingleton(container);
                RegisterSpecflowDependecies(services);
                AddTransientBindingAttribute(services);
                UseMapController(services);
                UseEventFlowInMemory(services);
                var serviceProvider = services.BuildServiceProvider();
                using (var scope = serviceProvider.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var logger = scopedServices.GetRequiredService<ILogger<SpecWebApplicationFactory>>();
                }
            });
        }

        private void UseMapController(IServiceCollection serviceCollection)
        {
            foreach (var type in typeof(SubscriptionAccountController).Assembly.DefinedTypes.Where(@type => type.BaseType == typeof(ControllerBase)))
            {
                serviceCollection.AddTransient(type);
            }

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
        private void UseEventFlowInMemory(IServiceCollection services)
        {
            services.AddEventFlow(options => options.AddAspNetCore()
                                       .AddEvents(typeof(SubscriptionRequestCreatedEvent).Assembly)
                                       .AddCommands(typeof(SubscriptionRequestCreationCommand).Assembly, type => true)
                                       .AddCommandHandlers(typeof(SubscriptionRequestCreationCommandHandler).Assembly)
                                       .AddSagas(typeof(SubscriptionCreationSaga).Assembly)
                                       .AddSagaLocators(typeof(SubscriptionCreationSagaLocator).Assembly)
                                       .UseConsoleLog()
                                       .UseInMemoryReadStoreFor<SubscriptionAccountReadModel>()
                                       .UseInMemoryReadStoreFor<SubscriptionRequestReadModel>()
                                       .AddQueryHandler<InMemoryFindReadModelQueryHandler<SubscriptionAccountReadModel>, FindReadModelQuery<SubscriptionAccountReadModel>, ICollectionReadModel<SubscriptionAccountReadModel>>()
                                       .AddQueryHandler<InMemoryFindReadModelQueryHandler<SubscriptionRequestReadModel>, FindReadModelQuery<SubscriptionRequestReadModel>, ICollectionReadModel<SubscriptionRequestReadModel>>()

                    );
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

        internal static SpecWebApplicationFactory RegisterInstanceAs(IObjectContainer container)
        {
            var factory = new SpecWebApplicationFactory(container);
            container.RegisterInstanceAs((WebApplicationFactory<BDDTestStartup>)factory);
            return factory;
        }
    }

    public class BDDTestStartup : Startup
    {
        public BDDTestStartup(IConfiguration configuration) : base(configuration)
        {
            this.UseEventFlowOptionsBuilder = false;
        }

        protected override IServiceCollection AddEventFlow(IServiceCollection services, SubscriptionDatabaseSettings configMongo)
        {
            return services;
        }

        protected override void ConfigureEventFlow(IApplicationBuilder app)
        {

        }

        protected override void AddRabbitMQ(IServiceCollection services)
        {

        }
    }

    //public class SpecWebApplicationFactory : WebApplicationFactory<SpecWebApplicationFactory>
    //{
    //    private readonly IObjectContainer container;

    //    public SpecWebApplicationFactory(IObjectContainer container)
    //    {
    //        this.container = container;
    //    }

    //    public IServiceCollection ServiceCollection { get; private set; }

    //    protected override IWebHostBuilder CreateWebHostBuilder()
    //    {
    //        var builder = new WebHostBuilder();
    //        return builder.UseStartup<SpecWebApplicationFactory>()
    //                      .UseEnvironment("Development");
    //    }

    //    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    //    {
    //        app.UseMvc(routes =>
    //        {
    //            routes.MapRoute(name: "default", template: "api/{controller=Home}/{action=Index}/{id?}");
    //        });
    //    }

    //    protected override void ConfigureWebHost(IWebHostBuilder builder)
    //    {
    //        base.ConfigureWebHost(builder);
    //        builder.ConfigureAppConfiguration((context, config) => config.AddJsonFile("appsettings.json"));
    //        builder.ConfigureServices(ConfigureServices);
    //    }

    //    private void ConfigureServices(IServiceCollection serviceCollection)
    //    {            
    //        ServiceCollection = serviceCollection;
    //        AddAuthentication(serviceCollection);
    //        serviceCollection.AddSingleton(container);
    //        RegisterSpecflowDependecies(serviceCollection);            
    //        UseCurrentIOCConfiguration(serviceCollection);
    //        AddTransientBindingAttribute(serviceCollection);
    //        UseMapController(serviceCollection);
    //        UseEventFlowInMemory(serviceCollection);            
    //    }



    //    private void UseEventFlowInMemory(IServiceCollection services)
    //    {
    //                                   //.AddEvents(typeof(SubscriptionRequestCreatedEvent))
    //                                   //.AddCommands(typeof(SubscriptionRequestCreationCommand))
    //                                   //.AddCommandHandlers(typeof(SubscriptionRequestCreationCommandHandler))


    //        services.AddEventFlow(options => options.AddAspNetCore()
    //                                   .AddEvents(typeof(SubscriptionRequestCreatedEvent).Assembly)
    //                                   .AddCommands(typeof(SubscriptionRequestCreationCommand).Assembly, type => true)
    //                                   .AddCommandHandlers(typeof(SubscriptionRequestCreationCommandHandler).Assembly)
    //                                   .AddSagas(typeof(SubscriptionCreationSaga).Assembly)
    //                                   .AddSagaLocators(typeof(SubscriptionCreationSagaLocator).Assembly)
    //                                   .UseConsoleLog()
    //                                   .UseInMemoryReadStoreFor<SubscriptionAccountReadModel>()
    //                                   .UseInMemoryReadStoreFor<SubscriptionRequestReadModel>()
    //                                   // TODO Code InMemoryFindReadModelQueryHandler
    //                                   .AddQueryHandler<InMemoryFindReadModelQueryHandler<SubscriptionAccountReadModel>, FindReadModelQuery<SubscriptionAccountReadModel>, ICollectionReadModel<SubscriptionAccountReadModel>>()
    //                                   .AddQueryHandler<InMemoryFindReadModelQueryHandler<SubscriptionRequestReadModel>, FindReadModelQuery<SubscriptionRequestReadModel>, ICollectionReadModel<SubscriptionRequestReadModel>>()

    //                );
    //    }

    //    private static void AddAuthentication(IServiceCollection serviceCollection)
    //    {
    //        serviceCollection.AddAuthentication("Test")
    //                         .AddTestAuth(options => { });
    //    }

    //    private static void UseCurrentIOCConfiguration(IServiceCollection serviceCollection)
    //    {
    //        //var configurationStartup = Substitute.For<IConfiguration>();
    //        //serviceCollection.AddSingleton(configurationStartup);
    //        var config = serviceCollection.BuildServiceProvider().GetService<IConfiguration>();
    //        var startup = new Startup(config)
    //        {
    //            UseEventFlowOptionsBuilder = false
    //        };
    //        startup.ConfigureServices(serviceCollection);
    //        serviceCollection.AddSingleton(startup);
    //    }











    //}


}
