using BoDi;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;

namespace CloudBacktesting.ApiGateway.Specs
{
    internal static class ScenarioDependenciesLoader
    {
        public static IServiceProvider CreateServiceProvider(this IObjectContainer container)
        {
            var applicationFactory = new SpecflowWebApplicationStartup(container);
            container.RegisterInstanceAs((WebApplicationFactory<SpecflowAspnetCoreStartup>)applicationFactory);
            container.RegisterFactoryAs(objContainer => applicationFactory.CreateClient());
            container.Resolve<HttpClient>();
            var provider = applicationFactory.ServiceCollection.BuildServiceProvider();
            container.RegisterInstanceAs<IServiceProvider>(provider);
            return provider;
        }

    }
}
