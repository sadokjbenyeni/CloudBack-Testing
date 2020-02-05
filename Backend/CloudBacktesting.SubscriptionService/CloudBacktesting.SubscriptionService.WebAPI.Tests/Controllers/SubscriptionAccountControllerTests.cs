using CloudBacktesting.Infra.Security;
using CloudBacktesting.SubscriptionService.Domain.Aggregates.SubscriptionAccountAggregate;
using CloudBacktesting.SubscriptionService.Domain.Aggregates.SubscriptionAccountAggregate.Commands;
using CloudBacktesting.SubscriptionService.WebAPI.Host;
using CloudBacktesting.SubscriptionService.WebAPI.Models.Account.Client.SubscriptionAccount;
using EventFlow;
using EventFlow.Aggregates.ExecutionResults;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CloudBacktesting.SubscriptionService.WebAPI.Tests.Controllers
{
    [TestFixture]
    public class SubscriptionAccountControllerTests
    {
        [Test]
        public async Task Authentification_and_Authorization_works()
        {
            var user = new UserIdentity()
            {
                Name = "Name",
                Email = "email@quanthouse.com",
                Roles = new[] { "Connected", "Client" },
                Additionals = new Dictionary<string, string>() { { "subscriptionaccountid", $"subscriptionaccount-{Guid.NewGuid().ToString()}" } },
            };
            var userStr = JsonConvert.SerializeObject(user);
            var token = Convert.ToBase64String(Encoding.UTF8.GetBytes(userStr));

            var factory = new CustomWebApplicationFactory<TestStartup>();
            var options = new WebApplicationFactoryClientOptions();
            options.AllowAutoRedirect = true;
            options.BaseAddress = new Uri("https://localhost");
            var client = factory.CreateClient(options);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", token);
            //client.DefaultRequestHeaders.Add("token", $"token:{JsonConvert.SerializeObject(user)}");

            var response = await client.GetAsync("/api/v1/subscriptionaccount");
            Assert.That(response.IsSuccessStatusCode, Is.True, response.ReasonPhrase);

            client = factory.CreateClient(options);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", token);
            var content = new StringContent(JsonConvert.SerializeObject(new CreateSubscriptionAccountDto() { Subscriber = "Morgan" }), Encoding.UTF8, "application/json");
            response = await client.PostAsync("/api/v1/subscriptionaccount", content);
            Assert.That(response.IsSuccessStatusCode, Is.False, response.ReasonPhrase);

            user = new UserIdentity()
            {
                Name = "External Machine",
                Email = "support@quanthouse.com",
                Roles = new[] { "Connected", "Administrator" },
                Additionals = new Dictionary<string, string>() { { "subcriptionaccountid", $"subscriptionaccount-{Guid.NewGuid().ToString()}" } },
            };
            userStr = JsonConvert.SerializeObject(user);
            token = Convert.ToBase64String(Encoding.UTF8.GetBytes(userStr));
            client = factory.CreateClient(options);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", token);
            SetUpBusCommand(factory, new SuccessExecutionResult());
            response = await client.PostAsync("/api/v1/subscriptionaccount", content);
            Assert.That(response.IsSuccessStatusCode, Is.True, response.ReasonPhrase);

        }
        [Test]
        public async Task Should_success_call_post_when_user_is_admin()
        {
            var user = new UserIdentity()
            {
                Name = "Name",
                Email = "email@quanthouse.com",
                Roles = new[] { "Connected", "Administrator" },
                Additionals = new Dictionary<string, string>() { { "subcriptionaccountid", $"subscriptionaccount-{Guid.NewGuid().ToString()}" } },
            };
            var userStr = JsonConvert.SerializeObject(user);
            var token = Convert.ToBase64String(Encoding.UTF8.GetBytes(userStr));

            var factory = new CustomWebApplicationFactory<TestStartup>();
            var options = new WebApplicationFactoryClientOptions();
            options.AllowAutoRedirect = true;
            options.BaseAddress = new Uri("https://localhost");
            var client = factory.CreateClient(options);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", token);
            SetUpBusCommand(factory, new SuccessExecutionResult());
            var content = new StringContent(JsonConvert.SerializeObject(new CreateSubscriptionAccountDto() { Subscriber = "Morgan" }), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("/api/v1/subscriptionaccount", content);
            Assert.That(response.IsSuccessStatusCode, Is.True, response.ReasonPhrase);
        }

        //[Test]
        //[Explicit]
        //public async Task Should_success_call_post_when_user_is_system()
        //{
        //    var user = new UserIdentity()
        //    {
        //        Name = "Name",
        //        Email = "email@quanthouse.com",
        //        Roles = new[] { "Client", "System" },
        //        Additionals = new Dictionary<string, string>() { { "subcriptionaccountid", $"subscriptionaccount-{Guid.NewGuid().ToString()}" } },
        //    };
        //    var userStr = JsonConvert.SerializeObject(user);
        //    var token = Convert.ToBase64String(Encoding.UTF8.GetBytes(userStr));

        //    var factory = new CustomWebApplicationFactory<TestStartup>();
        //    var options = new WebApplicationFactoryClientOptions();
        //    options.AllowAutoRedirect = true;
        //    options.BaseAddress = new Uri("https://localhost");
        //    var client = factory.CreateClient(options);
        //    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", token);
        //    var content = new StringContent(JsonConvert.SerializeObject(new CreateSubscriptionAccountDto() { Subscriber = "Morgan" }), Encoding.UTF8, "application/json");
        //    var response = await client.PostAsync("/api/subscriptionaccount", content);
        //    Assert.That(response.IsSuccessStatusCode, Is.True);
        //}

        //[Test]
        //public async Task Should_fail_call_post_when_user_is_only_system()
        //{
        //    var user = new UserIdentity()
        //    {
        //        Name = "Name",
        //        Email = "email@quanthouse.com",
        //        Roles = new[] { "System" },
        //        Additionals = new Dictionary<string, string>() { { "subcriptionaccountid", $"subscriptionaccount-{Guid.NewGuid().ToString()}" } },
        //    };
        //    var userStr = JsonConvert.SerializeObject(user);
        //    var token = Convert.ToBase64String(Encoding.UTF8.GetBytes(userStr));

        //    var factory = new CustomWebApplicationFactory<TestStartup>();
        //    var options = new WebApplicationFactoryClientOptions();
        //    options.AllowAutoRedirect = true;
        //    options.BaseAddress = new Uri("https://localhost");
        //    var client = factory.CreateClient(options);
        //    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", token);
        //    var content = new StringContent(JsonConvert.SerializeObject(new CreateSubscriptionAccountDto() { Subscriber = "Morgan" }), Encoding.UTF8, "application/json");
        //    var response = await client.PostAsync("/api/subscriptionaccount", content);
        //    Assert.That(response.IsSuccessStatusCode, Is.True);
        //}

        [Test]
        public async Task Should_fail_call_post_when_user_is_not_admin_and_not_system()
        {
            var user = new UserIdentity()
            {
                Name = "Name",
                Email = "email@quanthouse.com",
                Roles = new[] { "Connected", "Client" },
                Additionals = new Dictionary<string, string>() { { "subcriptionaccountid", $"subscriptionaccount-{Guid.NewGuid().ToString()}" } },
            };
            var userStr = JsonConvert.SerializeObject(user);
            var token = Convert.ToBase64String(Encoding.UTF8.GetBytes(userStr));
            
            var factory = new CustomWebApplicationFactory<TestStartup>();            
            var options = new WebApplicationFactoryClientOptions();
            options.AllowAutoRedirect = true;
            options.BaseAddress = new Uri("https://localhost");
            var client = factory.CreateClient(options);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", token);
            var content = new StringContent(JsonConvert.SerializeObject(new CreateSubscriptionAccountDto() { Subscriber = "Morgan" }), Encoding.UTF8, "application/json");
            SetUpBusCommand(factory, new SuccessExecutionResult());
            var response = await client.PostAsync("/api/v1/subscriptionaccount", content);
            Assert.That(response.IsSuccessStatusCode, Is.False);
            Assert.That(response.ReasonPhrase, Is.EqualTo("Forbidden"));
        }

        private void SetUpBusCommand(CustomWebApplicationFactory<TestStartup> factory, IExecutionResult result)
        {
            var bus = (ICommandBus)factory.Server.Host.Services.GetService(typeof(ICommandBus));
            bus.PublishAsync(Arg.Any<SubscriptionAccountCreationCommand>(), Arg.Any<CancellationToken>()) //<SubscriptionAccount, SubscriptionAccountId, IExecutionResult>
                .Returns(Task.FromResult(result));
        }
    }
}
