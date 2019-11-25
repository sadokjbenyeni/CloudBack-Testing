using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using Microsoft.Extensions.DependencyInjection;
using CloudBacktesting.SubscriptionService.Specs.Host;

namespace CloudBacktesting.SubscriptionService.Specs
{
    [Binding]
    public class TestStartup
    {
        private readonly ScenarioContext context;

        public TestStartup(ScenarioContext context)
        {
            this.context = context;
        }

        [BeforeScenario]
        public void BeforeScenario()
        {
            var serviceProvider = new BehaviorDependenciesBuilder()
                                    .CreateServiceCollection(context.ScenarioContainer)
                                    .Build();
            context.Set(serviceProvider);
            
        }
    }
}
