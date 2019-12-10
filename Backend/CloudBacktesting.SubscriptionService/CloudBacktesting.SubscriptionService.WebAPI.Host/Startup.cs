using System;
using EventFlow.AspNetCore.Extensions;
using EventFlow.AspNetCore.Middlewares;
using EventFlow.Extensions;
using EventFlow.MongoDB.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using EventFlow.DependencyInjection.Extensions;
using CloudBacktesting.SubscriptionService.Domain.Aggregates.SubscriptionRequestAggregate.Events;
using CloudBacktesting.SubscriptionService.Domain.Aggregates.SubscriptionRequestAggregate.Commands;
using CloudBacktesting.SubscriptionService.Domain.Repositories.SubscriptionRequestRepository;
using CloudBacktesting.SubscriptionService.WebAPI.Host.DatabaseSettings;
using CloudBacktesting.Infra.EventFlow.Queries;
using CloudBacktesting.Infra.EventFlow.MongoDb.Queries;
using CloudBacktesting.Infra.EventFlow.ReadStores;
using CloudBacktesting.SubscriptionService.Domain.Aggregates.SubscriptionAccountAggregate.Commands;
using CloudBacktesting.SubscriptionService.Domain.Repositories.SubscriptionAccountRepository;
using CloudBacktesting.SubscriptionService.Domain.Sagas.SubscriptionCreation;

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
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                //.SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                ;

            services.AddSwaggerGen(options => options.SwaggerDoc("V1", new Microsoft.OpenApi.Models.OpenApiInfo() { Title = "Subscription Api", Version = "V1" }));

            var configMongo = new SubscriptionDatabaseSettings();
            Configuration.Bind("SubscriptionDatabaseSettings", configMongo);
            AddEventFlow(services, configMongo);
            services.AddCors(options =>
            options.AddPolicy("AllowAllOrigins", builder =>
            {
                builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
            }));
            return services.BuildServiceProvider();
        }

        private IServiceCollection AddEventFlow(IServiceCollection services, SubscriptionDatabaseSettings configMongo)
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
            app.UseSwagger();
            app.UseSwaggerUI(options => options.SwaggerEndpoint("/swagger/V1/swagger.json", "Subscription Api"));
            app.UseHttpsRedirection();
            app.UseMiddleware<CommandPublishMiddleware>();
            app.UseMvc();
            //app.UseRouting();

            //app.UseAuthorization();

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllers();
            //});
        }
    }
}
