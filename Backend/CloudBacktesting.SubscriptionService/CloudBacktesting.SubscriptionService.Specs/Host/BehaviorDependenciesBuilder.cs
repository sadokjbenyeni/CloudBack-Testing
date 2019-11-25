using BoDi;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CloudBacktesting.SubscriptionService.Specs.Host
{
    public class BehaviorDependenciesBuilder
    {
        private IObjectContainer container;
        private SpecWebApplicationFactory startup;

        
        public BehaviorDependenciesBuilder CreateServiceCollection(IObjectContainer container)
        {
            if (container is null)
            {
                throw new ArgumentNullException(nameof(container));
            }
            this.container = container;
            this.startup = new SpecWebApplicationFactory(container);
            container.RegisterInstanceAs((WebApplicationFactory<SpecWebApplicationFactory>)startup);
            container.RegisterFactoryAs(objContainer => startup.CreateClient());
            return this;
        }


        public IServiceProvider Build()
        {
            container.Resolve<HttpClient>();
            var provider = startup.ServiceCollection.BuildServiceProvider();
            container.RegisterInstanceAs<IServiceProvider>(provider);
            return provider;
        }
    }
}
