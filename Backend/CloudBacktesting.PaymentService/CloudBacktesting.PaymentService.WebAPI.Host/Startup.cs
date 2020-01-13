﻿using CloudBacktesting.Infra.EventFlow.MongoDb.Queries;
using CloudBacktesting.Infra.EventFlow.Queries;
using CloudBacktesting.Infra.EventFlow.ReadStores;
using CloudBacktesting.Infra.Security;
using CloudBacktesting.Infra.Security.Authorization;
using CloudBacktesting.PaymentService.Domain.Aggregates.PaymentAccountAggregate.Commands;
using CloudBacktesting.PaymentService.Domain.Aggregates.PaymentAccountAggregate.Events;
using CloudBacktesting.PaymentService.Domain.Repositories.PaymentAccountRepository;
using CloudBacktesting.PaymentService.Domain.Sagas.PaymentCreation;
using CloudBacktesting.PaymentService.Infra.Security;
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
            configMongo.ConnectionString = "mongodb://localhost:27017";
            configMongo.DatabaseName = "PaymentDb";
            //Configuration.Bind("PaymentDatabaseSettings", configMongo);
            AddEventFlow(services, configMongo);
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
                                       ////.AddEvents(typeof(PaymentMethodCreatedEvent))
                                       ////.AddCommands(typeof(PaymentMethodCreationCommand))
                                       ////.AddCommandHandlers(typeof(PaymentMethodCreationCommandHandler))
                                       .UseMongoDbEventStore()
                                       .ConfigureMongoDb(configMongo.ConnectionString, configMongo.DatabaseName)
                                       .UseConsoleLog()
                                       .UseMongoDbReadModel<PaymentAccountReadModel>()
                                       .AddQueryHandler<MongoDbFindReadModelQueryHandler<PaymentAccountReadModel>, FindReadModelQuery<PaymentAccountReadModel>, ICollectionReadModel<PaymentAccountReadModel>>()
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
            app.UseSwagger();
            app.UseSwaggerUI(options => options.SwaggerEndpoint("/swagger/V1/swagger.json", "Payment Api"));
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
