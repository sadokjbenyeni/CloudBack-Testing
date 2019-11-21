using CloudBacktesting.SubscriptionService.Domain.Aggregates.SubscriptionAccountAggregate.Commands;
using CloudBacktesting.SubscriptionService.Domain.Aggregates.SubscriptionAccountAggregate.Events;
using System;
using EventFlow.AspNetCore.Extensions;
using EventFlow.AspNetCore.Middlewares;
using EventFlow.Extensions;
using EventFlow.MongoDB.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using EventFlow.DependencyInjection.Extensions;
using CloudBacktesting.SubscriptionService.Domain.Repositories.SubscriptionAccountRepository;

namespace CloudBacktesting.SubscriptionService.WebAPI.Host
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddSwaggerGen(options => options.SwaggerDoc("V1", new Microsoft.OpenApi.Models.OpenApiInfo() { Title = "Subscription Api", Version = "V1" }));

            services.AddEventFlow(options => options.AddAspNetCore()
                                                   .AddEvents(typeof(SubscriptionAccountCreatedEvent))
                                                   .AddCommands(typeof(SubscriptionAccountCreationCommand))
                                                   .AddCommandHandlers(typeof(SubscriptionAccountCreationCommandHandler))
                                                   .UseMongoDbEventStore()
                                                   .ConfigureMongoDb("mongodb://localhost:27017", "SubscriptionDb")
                                                   .UseConsoleLog()
                                                   //.UseInMemoryReadStoreFor<ExampleReadModel>()
                                                   .UseMongoDbReadModel<SubscriptionAccountReadModel>()
                                );

            return services.BuildServiceProvider();
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
