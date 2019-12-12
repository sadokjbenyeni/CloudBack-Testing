using BoDi;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
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
            container.RegisterFactoryAs<IHttpClientFactory>(objContainer => new TestHttpClientFactory(startup));
            container.RegisterFactoryAs(objContainer => new TestHttpClientFactory(startup).Create());
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

    public interface IHttpClientFactory
    {
        HttpClient Create(IEnumerable<Claim> claims = null);
    }

    public class TestHttpClientFactory : IHttpClientFactory
    {
        private readonly SpecWebApplicationFactory startup;

        public TestHttpClientFactory(SpecWebApplicationFactory startup)
        {
            this.startup = startup ?? throw new ArgumentNullException(nameof(startup));
        }

        public HttpClient Create(IEnumerable<Claim> claims = null)
        {
            var client = startup.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false,
            });
            claims = claims ?? Enumerable.Empty<Claim>();
            var claimsParameters = string.Join(",", claims.Select(c => $"{c.Subject}: {c.Value}")) ?? "test_parameter";
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Test", claimsParameters );
            return client;
        }
    }
}
