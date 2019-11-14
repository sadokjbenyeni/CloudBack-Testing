using Akka.Actor;
using CloudBacktesting.SubscriptionService.Domain.Model;
using CloudBacktesting.SubscriptionService.Domain.Repositories.SubscriptionAccountQuery;
using CloudBacktesting.SubscriptionService.WebAPI.Models;
using CloudBacktesting.SubscriptionService.WebAPI.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

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
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson(options => options.UseMemberCasing());

            // TODO: Add in services all dependencies            

            var actorSystem = ActorSystem.Create("subscription-api-system");
            var aggregateSubscriptionAccountManager = actorSystem.ActorOf(Props.Create(() => new SubscriptionAccountManager()),"subscriptionaccount-manager");
            // var sagaManager = actorSystem.ActorOf(Props.Create(() => new ResourceCreationSagaManager(() => new ResourceCreationSaga())),"resourcecreation-sagamanager");
            var subscriptionAccountStorage = actorSystem.ActorOf(Props.Create(() => new SubscriptionAccountStorageHandler()), "resource-storagehandler");
            var subscriptionStorage = actorSystem.ActorOf(Props.Create(() => new SubscriptionStorageHandler()), "operation-storagehandler");
            
            // Add Actors to DI as ActorRefProvider<T>
            services
                .AddAkkatecture(actorSystem)
                .AddActorReference<SubscriptionAccountManager>(aggregateSubscriptionAccountManager)
                // .AddActorReference<ResourceCreationSagaManager>(sagaManager)
                .AddActorReference<SubscriptionAccountStorageHandler>(subscriptionAccountStorage)
                .AddActorReference<SubscriptionStorageHandler>(subscriptionStorage);
            
            // Add Read Side 
            services
                .AddTransient<IQuerySubscriptionAccount, QuerySubscriptionAccount>()
                .AddTransient<IQuerySubscription, QuerySubscription>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
