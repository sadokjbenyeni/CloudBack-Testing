using BoDi;
using CloudBacktesting.Infra.Security;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
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
            this.startup = SpecWebApplicationFactory.RegisterInstanceAs(container);
            container.RegisterFactoryAs<ITestHttpClientFactory>(objContainer => new TestHttpClientFactory(startup));
            container.RegisterFactoryAs(objContainer => new TestHttpClientFactory(startup).Create());
            return this;
        }


        public IServiceProvider Build()
        {
            container.Resolve<HttpClient>();
            container.RegisterInstanceAs(startup.Server.Host.Services);
            return startup.Server.Host.Services;
        }
    }

    public interface ITestHttpClientFactory
    {
        HttpClient Create(UserIdentity user = null);
    }

    public class TestHttpClientFactory : ITestHttpClientFactory
    {
        private readonly SpecWebApplicationFactory startup;

        public TestHttpClientFactory(SpecWebApplicationFactory startup)
        {
            this.startup = startup ?? throw new ArgumentNullException(nameof(startup));
        }

        public HttpClient Create(UserIdentity user = null)
        {
            var client = startup.CreateClient();
            client.BaseAddress = new Uri("https://localhost");
            if (user == null) 
            {
                return client;
            }
            client.DefaultRequestHeaders.Authorization = BuildAuthorizationValue(user);
            return client;
        }

        private AuthenticationHeaderValue BuildAuthorizationValue(UserIdentity user)
        {
            var userStr = JsonConvert.SerializeObject(user);
            var token = Convert.ToBase64String(Encoding.UTF8.GetBytes(userStr));
            return new AuthenticationHeaderValue("Basic", token);
        }
    }

    //public class TestHttpClientFactory : IHttpClientFactory
    //{
    //    private readonly SpecWebApplicationFactory startup;

    //    public TestHttpClientFactory(SpecWebApplicationFactory startup)
    //    {
    //        this.startup = startup ?? throw new ArgumentNullException(nameof(startup));
    //    }

    //    public HttpClient Create(IEnumerable<Claim> claims = null)
    //    {
    //        var client = startup.CreateClient(new WebApplicationFactoryClientOptions
    //        {
    //            AllowAutoRedirect = false,
    //        });
    //        claims = claims ?? Enumerable.Empty<Claim>();
    //        var claimsParameters = string.Join(",", claims.Select(c => $"{c.Subject}: {c.Value}")) ?? "test_parameter";
    //        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Test", claimsParameters );
    //        return client;
    //    }
    //}
}
