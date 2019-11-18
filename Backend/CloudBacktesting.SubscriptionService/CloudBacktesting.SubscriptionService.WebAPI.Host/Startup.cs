using Akka.Actor;
using CloudBacktesting.SubscriptionService.Domain.Aggregates.SubscriptionAccount;
using CloudBacktesting.SubscriptionService.Domain.Repositories.SubscriptionAccountQuery;
using CloudBacktesting.SubscriptionService.Domain.Repositories.SubscriptionAccounts;
using CloudBacktesting.SubscriptionService.Domain.Repositories.Subscriptions;
using CloudBacktesting.SubscriptionService.Domain.Sagas.SubscriptionAccountSaga;
using CloudBacktesting.SubscriptionService.Infra.Models;
using CloudBacktesting.SubscriptionService.Infra.Services;
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
            var aggregateSubscriptionAccountManager = actorSystem.ActorOf(Props.Create(() => new SubscriptionAccountManager()), "subscriptionaccount-manager");
            //var sagaManager = actorSystem.ActorOf(Props.Create(() => new SubscriptionAccountCreationSaga()), "subscriptionaccount-sagamanager");
            var subscriptionAccountStorage = actorSystem.ActorOf(Props.Create(() => new SubscriptionAccountsStorageHandler()), "subscription-storagehandler");
            var subscriptionStorage = actorSystem.ActorOf(Props.Create(() => new SubscriptionsStorageHandler()), "subscriptionaccount-storagehandler");

            // Add Actors to DI as ActorRefProvider<T>
            services
                .AddAkkatecture(actorSystem)
                .AddActorReference<SubscriptionAccountManager>(aggregateSubscriptionAccountManager)
                //.AddActorReference<SubscriptionAccountCreationSaga>(sagaManager)
                .AddActorReference<SubscriptionAccountsStorageHandler>(subscriptionAccountStorage)
                .AddActorReference<SubscriptionsStorageHandler>(subscriptionStorage);

            // Add Read Side 
            services
                .AddTransient<IQuerySubscriptionAccounts, SubscriptionAccountsQueryHandler>()
                .AddTransient<IQuerySubscriptions, SubscriptionsQueryHandler>();

            // requires using Microsoft.Extensions.Options
            services.Configure<SubscriptionDatabaseSettings>(
                Configuration.GetSection(nameof(SubscriptionDatabaseSettings)));

            services.AddSingleton<ISubscriptionDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<SubscriptionDatabaseSettings>>().Value);

            services.AddSingleton<SubscriptionServiceMongo>();
            services.AddControllers().AddNewtonsoftJson(options => options.UseMemberCasing());

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("V1", new Microsoft.OpenApi.Models.OpenApiInfo() { Title = "Subscription Api", Version = "V1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseSwagger();

            app.UseSwaggerUI(options => options.SwaggerEndpoint("/swagger/V1/swagger.json", "Subscription Api"));

            app.UseRouting();

            //app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
