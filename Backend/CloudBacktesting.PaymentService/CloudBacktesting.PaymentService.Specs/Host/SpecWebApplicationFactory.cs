using BoDi;
using CloudBacktesting.Infra.EventFlow.Queries;
using CloudBacktesting.Infra.EventFlow.Queries.InMemory;
using CloudBacktesting.Infra.EventFlow.ReadStores;
using CloudBacktesting.PaymentService.Domain.Aggregates.PaymentAccountAggregate.Commands;
using CloudBacktesting.PaymentService.Domain.Aggregates.PaymentAccountAggregate.Events;
using CloudBacktesting.PaymentService.Domain.Repositories.PaymentAccountRepository;
using CloudBacktesting.PaymentService.Domain.Repositories.PaymentMethodRepository;
using CloudBacktesting.PaymentService.Domain.Sagas.PaymentCreation;
using CloudBacktesting.PaymentService.WebAPI.Controllers;
using CloudBacktesting.PaymentService.WebAPI.Host;
using CloudBacktesting.PaymentService.WebAPI.Host.DatabaseSettings;
using EventFlow.AspNetCore.Extensions;
using EventFlow.DependencyInjection.Extensions;
using EventFlow.Extensions;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using TechTalk.SpecFlow;

namespace CloudBacktesting.PaymentService.Specs.Host
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
            foreach (var type in typeof(PaymentAccountController).Assembly.DefinedTypes.Where(@type => type.BaseType == typeof(ControllerBase)))
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
                                                .AddEvents(typeof(PaymentAccountCreatedEvent).Assembly)
                                                .AddCommands(typeof(PaymentAccountCreationCommand).Assembly, type => true)
                                                .AddCommandHandlers(typeof(PaymentAccountCreationCommandHandler).Assembly)
                                                .AddSagas(typeof(PaymentCreationSaga).Assembly)
                                                .AddSagaLocators(typeof(PaymentCreationSagaLocator).Assembly)
                                                .UseConsoleLog()
                                                .UseInMemoryReadStoreFor<PaymentAccountReadModel>()
                                                .UseInMemoryReadStoreFor<PaymentMethodReadModel>()
                                                .AddQueryHandler<InMemoryFindReadModelQueryHandler<PaymentAccountReadModel>, FindReadModelQuery<PaymentAccountReadModel>, ICollectionReadModel<PaymentAccountReadModel>>()
                                                .AddQueryHandler<InMemoryFindReadModelQueryHandler<PaymentMethodReadModel>, FindReadModelQuery<PaymentMethodReadModel>, ICollectionReadModel<PaymentMethodReadModel>>()

                    );
        }

        private static void AddTransientBindingAttribute(IServiceCollection serviceCollection)
        {
            var controllerTypes = typeof(SpecWebApplicationFactory)
                                        .Assembly
                                        .GetTypes()
                                        .Union(typeof(PaymentAccountController).Assembly.GetTypes())
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

        protected override IServiceCollection AddEventFlow(IServiceCollection services, PaymentDatabaseSettings configMongo)
        {
            return services;
        }

        protected override void ConfigureEventFlow(IApplicationBuilder app)
        {

        }

        //protected override void AddRabbitMQ(IServiceCollection services)
        //{

        //}
    }
}
