using EventFlow.AspNetCore.Extensions;
using EventFlow.AspNetCore.Middlewares;
using EventFlow.Extensions;
using EventFlow.MongoDB.Extensions;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using EventFlow.DependencyInjection.Extensions;
using CloudBacktesting.SubscriptionService.Domain.Aggregates.SubscriptionRequestAggregate.Events;
using CloudBacktesting.SubscriptionService.Domain.Repositories.SubscriptionRequestRepository;
using CloudBacktesting.SubscriptionService.WebAPI.Host.DatabaseSettings;
using CloudBacktesting.Infra.EventFlow.Queries;
using CloudBacktesting.Infra.EventFlow.MongoDb.Queries;
using CloudBacktesting.Infra.EventFlow.ReadStores;
using CloudBacktesting.SubscriptionService.Domain.Aggregates.SubscriptionAccountAggregate.Commands;
using CloudBacktesting.SubscriptionService.Domain.Repositories.SubscriptionAccountRepository;
using CloudBacktesting.SubscriptionService.Domain.Sagas.SubscriptionCreation;
using CloudBacktesting.SubscriptionService.RabbitMQ.EventManager.Publishers;
using CloudBacktesting.SubscriptionService.RabbitMQ.EventManager.Consumers;
using RabbitMQ.Client;
using Microsoft.AspNetCore.Authentication;
using CloudBacktesting.Infra.Security;
using Microsoft.AspNetCore.Authorization;
using CloudBacktesting.Infra.Security.Authorization;
using System;
using Microsoft.OpenApi.Models;
using System.Collections.Generic;
//using Microsoft.Extensions.Hosting;

namespace CloudBacktesting.SubscriptionService.WebAPI.Host
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
            services.AddMvc()
                //.SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                ;
            services.AddSingleton(new DecoderAuthenticationHandlerOptions() { HeaderName = "token" });

            services.AddAuthentication("cloudbacktestingAuthentication")
                    .AddScheme<AuthenticationSchemeOptions, CloudBacktestingAuthenticationHandler>("cloudbacktestingAuthentication", options => { });

            services.AddSingleton<IAuthorizationPolicyProvider, CloudBacktestingAuthorizationPolicyProvider>();

            // As always, handlers must be provided for the requirements of the authorization policies
            services.AddSingleton<IAuthorizationHandler, CloudBacktestingAuthorizationHandler>();

            services.AddAuthorization();
            services.AddApiVersioning();
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("V1", new Microsoft.OpenApi.Models.OpenApiInfo() { Title = "Subscription Api", Version = "V1" });
                OpenApiSecurityScheme securityScheme = new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Basic"
                };
                options.AddSecurityDefinition("Basic", securityScheme);
                options.AddSecurityRequirement(new OpenApiSecurityRequirement() { { securityScheme, new List<string>() } });
            });

            var configMongo = new SubscriptionDatabaseSettings();
            Configuration.Bind("SubscriptionDatabaseSettings", configMongo);
            Console.WriteLine($"Connecting to db connectionstring : {configMongo.ConnectionString} to the collection {configMongo.DatabaseName}");
            AddRabbitMQ(services);
            AddEventFlow(services, configMongo);
            services.AddCors(options =>
            options.AddPolicy("AllowAllOrigins", builder =>
            {
                builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
            }));
            //return services.BuildServiceProvider();
        }

        protected virtual void AddRabbitMQ(IServiceCollection services)
        {
            var endpoint = Configuration.GetSection("RabbitMq").GetValue<string>("endpoint");
            services.AddSingleton<IConnectionFactory>(con => new ConnectionFactory() { Endpoint = new AmqpTcpEndpoint(new Uri(endpoint)) });
            services.AddSingleton<IRabbitMQEventPublisher, RabbitMQEventPublisher>();
            services.AddHostedService<AccountCreatedListener>();
        }

        protected virtual IServiceCollection AddEventFlow(IServiceCollection services, SubscriptionDatabaseSettings configMongo)
        {

            if (UseEventFlowOptionsBuilder)
            {
                services.AddEventFlow(options => options.AddAspNetCore()
                                       .AddEvents(typeof(SubscriptionRequestCreatedEvent).Assembly)
                                       .AddCommands(typeof(SubscriptionAccountCreationCommand).Assembly, type => true)
                                       .AddCommandHandlers(typeof(SubscriptionAccountCreationCommandHandler).Assembly)
                                       .AddSagas(typeof(SubscriptionCreationSaga).Assembly)
                                       .AddSagaLocators(typeof(SubscriptionCreationSagaLocator).Assembly)
                                       //.AddEvents(typeof(SubscriptionRequestCreatedEvent))
                                       //.AddCommands(typeof(SubscriptionRequestCreationCommand))
                                       //.AddCommandHandlers(typeof(SubscriptionRequestCreationCommandHandler))
                                       .UseMongoDbEventStore()
                                       .ConfigureMongoDb(configMongo.ConnectionString, configMongo.DatabaseName)
                                       .UseConsoleLog()
                                       .UseMongoDbReadModel<SubscriptionAccountReadModel>()
                                       .UseMongoDbReadModel<SubscriptionRequestReadModel>()
                                       .AddQueryHandler<MongoDbFindReadModelQueryHandler<SubscriptionAccountReadModel>, FindReadModelQuery<SubscriptionAccountReadModel>, ICollectionReadModel<SubscriptionAccountReadModel>>()
                                       .AddQueryHandler<MongoDbFindReadModelQueryHandler<SubscriptionRequestReadModel>, FindReadModelQuery<SubscriptionRequestReadModel>, ICollectionReadModel<SubscriptionRequestReadModel>>()
                                       );
            }
            return services;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseCors("AllowAllOrigins");
            app.UseAuthentication();
            //app.UseAuthorization();
            if (!env.IsProduction())
            {
                app.UseSwagger();
                app.UseSwaggerUI(options => options.SwaggerEndpoint("/swagger/V1/swagger.json", "Subscription Api"));
            }
            app.UseHttpsRedirection();
            ConfigureEventFlow(app);

            app.UseMvc();
            //app.UseRouting();

            //app.UseAuthorization();

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllers();
            //});
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

