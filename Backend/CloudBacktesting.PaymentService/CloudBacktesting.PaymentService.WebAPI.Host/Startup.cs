﻿using CloudBacktesting.Infra.Security;
using CloudBacktesting.Infra.Security.Authorization;
using CloudBacktesting.PaymentService.Domain.Aggregates.PaymentAccountAggregate.Commands;
using CloudBacktesting.PaymentService.Domain.Aggregates.PaymentAccountAggregate.Events;
using CloudBacktesting.PaymentService.Domain.Sagas.PaymentCreation;
using CloudBacktesting.PaymentService.Infra.Security;
using CloudBacktesting.PaymentService.WebAPI.Host.DatabaseSettings;
using EventFlow.AspNetCore.Extensions;
using EventFlow.AspNetCore.Middlewares;
using EventFlow.DependencyInjection.Extensions;
using EventFlow.Extensions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Serialization;

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
            //services.AddMvc();
            services.AddRazorPages();
            services.AddMvc().AddNewtonsoftJson();
            services.AddControllers().AddNewtonsoftJson();
            services.AddMvc().AddNewtonsoftJson(options => options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver());
            services.AddControllers();
            services.AddSingleton(new DecoderAuthentificationHandlerOptions() { HeaderName = "token" });

            services.AddAuthentication("cloudbacktestingAuthentication")
                    .AddScheme<AuthenticationSchemeOptions, CloudBacktestingAuthenticationHandler>("cloudbacktestingAuthentication", options => { });


            services.AddSingleton<IAuthorizationPolicyProvider, CloudBacktestingAuthorizationPolicyProvider>();
            services.AddSingleton<IAuthorizationHandler, CloudBacktestingAuthorizationHandler>();

            services.AddAuthorization();

            services.AddSwaggerGen(options => options.SwaggerDoc("V1", new Microsoft.OpenApi.Models.OpenApiInfo() { Title = "Payment Api", Version = "V1" }));

            var configMongo = new PaymentDatabaseSettings();
            Configuration.Bind("PaymentDatabaseSettings", configMongo);
            AddEventFlow(services, configMongo);
            services.AddCors(options =>
           options.AddPolicy("AllowAllOrigins", builder =>
           {
               builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
           }));
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
                                       //.UseMongoDbEventStore()
                                       //.ConfigureMongoDb(configMongo.ConnectionString, configMongo.DatabaseName)
                                       //.UseConsoleLog()
                                       //.UseMongoDbReadModel<PaymentAccountReadModel>()
                                       //.AddQueryHandler<MongoDbFindReadModelQueryHandler<PaymentAccountReadModel>, FindReadModelQuery<PaymentAccountReadModel>, ICollectionReadModel<PaymentAccountReadModel>>()
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
            else
            {
                app.UseHsts();
            }

            app.UseCors("AllowAllOrigins");
            app.UseAuthentication();
            app.UseSwagger();
            app.UseSwaggerUI(options => options.SwaggerEndpoint("/swagger/V1/swagger.json", "Payment Api"));
            app.UseHttpsRedirection();
            ConfigureEventFlow(app);

            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
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
