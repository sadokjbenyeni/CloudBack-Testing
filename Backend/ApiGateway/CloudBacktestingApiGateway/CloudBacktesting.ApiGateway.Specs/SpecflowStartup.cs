using BoDi;
using System;
using TechTalk.SpecFlow;

namespace CloudBacktesting.ApiGateway.Specs
{
    [Binding]
    public class SpecflowStartup
    {
        private readonly ScenarioContext context;
        private readonly IObjectContainer container;

        public SpecflowStartup(ScenarioContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            this.container = context.ScenarioContainer ?? throw new ArgumentNullException(nameof(container));
        }

        [BeforeScenario]
        public void BeforeFeatureInit()
        {
            //var serviceProvider = container.CreateServiceProvider();
            //container.RegisterInstanceAs(serviceProvider);
            //context.Set(serviceProvider);
        }

    }

}
