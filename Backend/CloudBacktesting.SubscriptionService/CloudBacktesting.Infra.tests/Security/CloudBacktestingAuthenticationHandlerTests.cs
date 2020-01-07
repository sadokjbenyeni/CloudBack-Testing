using CloudBacktesting.Infra.Security;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CloudBacktesting.Infra.Tests.Security
{
    [TestFixture]
    public class CloudBacktestingAuthenticationHandlerTests
    {
        [Test]
        public async Task Should_authenticate_user_when_token_is_valid()
        {
            var user = new UserIdentity()
            {
                Name = "Name",
                Email = "email@quanthouse.com",
                Roles = new[] { "Client", "Admin" },
                Additionals = new Dictionary<string, string>() { { "subcriptionaccountid", "id" } },
            };
            var engine = new CloudBacktestingAuthentificationEngine();
            var httpRequest = Substitute.ForPartsOf<HttpRequest>();
            var userStr = JsonConvert.SerializeObject(user);
            var token = Convert.ToBase64String(Encoding.UTF8.GetBytes(userStr));
            var header = new HeaderDictionaryTests(new Dictionary<string, StringValues>() { { "Authorization", $"Basic {token}" }, });
            httpRequest.Headers.Returns(header);
            var scheme = new AuthenticationScheme("test", "test", typeof(CloudBacktestingAuthenticationHandler));
            var result = await engine.HandleAsync(Substitute.For<ILogger>(), httpRequest, scheme);

            Assert.That(result.Succeeded, Is.True);
        }

        [Test]
        public async Task Should_have_claims_when_token_is_valid()
        {
            var user = new UserIdentity()
            {
                Name = "Name",
                Email = "email@quanthouse.com",
                Roles = new[] { "Client", "Admin" },
                Additionals = new Dictionary<string, string>() { { "subcriptionaccountid", "id" } },
            };
            var engine = new CloudBacktestingAuthentificationEngine();
            var httpRequest = Substitute.ForPartsOf<HttpRequest>();
            var userStr = JsonConvert.SerializeObject(user);
            var token = Convert.ToBase64String(Encoding.UTF8.GetBytes(userStr));
            var header = new HeaderDictionaryTests(new Dictionary<string, StringValues>() { { "Authorization", $"Basic {token}" }, });
            httpRequest.Headers.Returns(header);
            var scheme = new AuthenticationScheme("test", "test", typeof(CloudBacktestingAuthenticationHandler));
            var result = await engine.HandleAsync(Substitute.For<ILogger>(), httpRequest, scheme);

            Assert.That(result.Principal, Is.Not.Null);
            Assert.That(result.Principal.Identity.Name, Is.EqualTo(user.Name));
            Assert.That(result.Principal.HasClaim(claim => claim.Type == ClaimTypes.Role), Is.True);
            Assert.That(result.Principal.FindFirst(claim => claim.Type == ClaimTypes.Role).Value, Is.EqualTo(string.Join(", ", user.Roles)));
            Assert.That(result.Principal.HasClaim(claim => string.Equals(claim.Type, "subcriptionaccountid")), Is.True);
        }
    }

    public class HeaderDictionaryTests : Dictionary<string, StringValues>, IHeaderDictionary
    {
        public HeaderDictionaryTests()
        {

        }

        public HeaderDictionaryTests(IDictionary<string, StringValues> dictionary)
            : base(dictionary)
        {

        }

        public long? ContentLength { get; set; }
    }
}
