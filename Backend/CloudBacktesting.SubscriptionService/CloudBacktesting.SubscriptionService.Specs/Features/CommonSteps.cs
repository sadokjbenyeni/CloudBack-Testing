using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace CloudBacktesting.SubscriptionService.Specs.Features
{
    [Binding]
    public class CommonSteps
    {

        [Given(@"(.*) is authentificated")]
        public void GivenMorganIsAuthentificated(string user)
        {

        }

    }
}
