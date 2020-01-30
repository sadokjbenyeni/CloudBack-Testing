using CloudBacktesting.Infra.EventFlow.MongoDb.Queries;
using CloudBacktesting.Infra.EventFlow.Queries;
using CloudBacktesting.Infra.EventFlow.ReadStores;
using CloudBacktesting.Infra.Security;
using CloudBacktesting.Infra.Security.Authorization;
using CloudBacktesting.PaymentService.Domain.Aggregates.PaymentAccountAggregate.Commands;
using CloudBacktesting.PaymentService.Domain.Aggregates.PaymentAccountAggregate.Events;
using CloudBacktesting.PaymentService.Domain.Repositories.BillingItemRepository;
using CloudBacktesting.PaymentService.Domain.Repositories.PaymentAccountRepository;
using CloudBacktesting.PaymentService.Domain.Repositories.PaymentMethodRepository;
using CloudBacktesting.PaymentService.Domain.Sagas.PaymentCreation;
using CloudBacktesting.PaymentService.Infra.Security;
using CloudBacktesting.PaymentService.RabbitMQ.EventManager.Consumers;
using CloudBacktesting.PaymentService.RabbitMQ.EventManager.Publishers;
using CloudBacktesting.PaymentService.WebAPI.Host.DatabaseSettings;
using EventFlow.AspNetCore.Extensions;
using EventFlow.AspNetCore.Middlewares;
using EventFlow.DependencyInjection.Extensions;
using EventFlow.Extensions;
using EventFlow.MongoDB.Extensions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using RabbitMQ.Client;
using System;

namespace CloudBacktesting.PaymentService.WebAPI.Host
{
    public class Startup
    {

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public bool UseEventFlowOptionsBuilder { get; set; } = true;
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddAuthentication("cloudbacktestingAuthentication")
                    .AddScheme<AuthenticationSchemeOptions, CloudBacktestingAuthenticationHandler>("cloudbacktestingAuthentication", options => { });
            services.AddSingleton<IAuthorizationPolicyProvider, CloudBacktestingAuthorizationPolicyProvider>();
            services.AddSingleton<IAuthorizationHandler, CloudBacktestingAuthorizationHandler>();
            services.AddAuthorization();
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("V1", new Microsoft.OpenApi.Models.OpenApiInfo() { Title = "Payment Api", Version = "V1" });
                options.AddSecurityDefinition("Basic", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Basic"
                });
            });
            var configMongo = new PaymentDatabaseSettings();
            Console.WriteLine($"Connecting to db connectionstring : {configMongo.ConnectionString} to the collection {configMongo.DatabaseName}");
            Configuration.Bind("PaymentDatabaseSettings", configMongo);
            AddRabbitMQ(services);
            AddEventFlow(services, configMongo);
        }
        protected virtual void AddRabbitMQ(IServiceCollection services)
        {
            var endpoint = Configuration.GetSection("RabbitMq").GetValue<string>("endpoint");
            services.AddSingleton<IConnectionFactory>(con => new ConnectionFactory() { Endpoint = new AmqpTcpEndpoint(new Uri(endpoint)) });
            services.AddSingleton<IRabbitMQEventPublisher, RabbitMQEventPublisher>();
            services.AddHostedService<AccountCreatedListener>();
        }
        protected virtual IServiceCollection AddEventFlow(IServiceCollection services, PaymentDatabaseSettings configMongo)
        {
            if (UseEventFlowOptionsBuilder)
            {
                services.AddEventFlow(options => options.AddAspNetCore()
                                       .AddEvents(typeof(PaymentAccountCreatedEvent).Assembly)
                                       .AddCommands(typeof(PaymentAccountCreationCommand).Assembly, type => true)
                                       .AddCommandHandlers(typeof(PaymentAccountCreationCommandHandler).Assembly)
                                       .AddSagas(typeof(PaymentCreationSaga).Assembly)
                                       .AddSagaLocators(typeof(PaymentCreationSagaLocator).Assembly)
                                       .UseMongoDbEventStore()
                                       .ConfigureMongoDb(configMongo.ConnectionString, configMongo.DatabaseName)
                                       .UseConsoleLog()
                                       .UseMongoDbReadModel<PaymentAccountReadModel>()
                                       .UseMongoDbReadModel<PaymentMethodReadModel>()
                                       .UseMongoDbReadModel<BillingItemReadModel>()
                                       .AddQueryHandler<MongoDbFindReadModelQueryHandler<PaymentAccountReadModel>, FindReadModelQuery<PaymentAccountReadModel>, ICollectionReadModel<PaymentAccountReadModel>>()
                                       .AddQueryHandler<MongoDbFindReadModelQueryHandler<PaymentMethodReadModel>, FindReadModelQuery<PaymentMethodReadModel>, ICollectionReadModel<PaymentMethodReadModel>>()
                                       .AddQueryHandler<MongoDbFindReadModelQueryHandler<BillingItemReadModel>, FindReadModelQuery<BillingItemReadModel>, ICollectionReadModel<BillingItemReadModel>>()
                                       );
            }
            return services;
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            if (!env.IsProduction())
            {
                app.UseSwagger();
                app.UseSwaggerUI(options => options.SwaggerEndpoint("/swagger/V1/swagger.json", "Payment Api"));
            }
            
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            ConfigureEventFlow(app);
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        protected virtual void ConfigureEventFlow(IApplicationBuilder app)
        {
            if (!UseEventFlowOptionsBuilder)
            {
                return;
            }
            app.UseMiddleware<CommandPublishMiddleware>();
        }
    }
}
