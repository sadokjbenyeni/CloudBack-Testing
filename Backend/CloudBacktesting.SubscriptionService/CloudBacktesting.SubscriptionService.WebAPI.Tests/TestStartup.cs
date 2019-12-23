using CloudBacktesting.SubscriptionService.WebAPI.Host;
using CloudBacktesting.SubscriptionService.WebAPI.Host.DatabaseSettings;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudBacktesting.SubscriptionService.WebAPI.Tests
{
    public class TestStartup : Startup
    {
        public TestStartup(IConfiguration configuration) : base(configuration)
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
}
