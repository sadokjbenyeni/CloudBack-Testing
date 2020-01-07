using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Text;

namespace CloudBacktesting.Infra.Security
{

    public class CloudBacktestingAuthentificationEngine
    {
        
        public Task<AuthenticateResult> HandleAsync(ILogger logger, HttpRequest request, AuthenticationScheme scheme)
        {
            if (!request.Headers.ContainsKey("Authorization"))
            {
                return Task.FromResult(AuthenticateResult.Fail("Missing Authorization Header"));
            }
            var token = AuthenticationHeaderValue.Parse(request.Headers["Authorization"]).Parameter;
            if (string.IsNullOrEmpty(token))
            {
                var errorId = Guid.NewGuid().ToString();
                logger.LogWarning($"[Authenticate, Failed] [ {errorId} | Token '{token}' is not recognized");
                return Task.FromResult(AuthenticateResult.Fail($"Invalid Token, error identifier: '{errorId}'"));
            }
            var user = DecodeHeaderAuthenticate(Encoding.UTF8.GetString(Convert.FromBase64String(token)));
            var claims = user.Additionals.Select(kp => new Claim(kp.Key, kp.Value))
                                              .Union(new[] {
                                                    new Claim(ClaimTypes.Email, user.Email),
                                                    new Claim(ClaimTypes.Name, user.Name),
                                                    new Claim(ClaimTypes.Role, string.Join(", ", user.Roles)),
                                                    new Claim("Connected", "Connected")
                                              })
                                              .Union(user.Roles.Select(role => new Claim(role, role))).ToArray();
            var identity = new ClaimsIdentity(claims, scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, scheme.Name);
            return Task.FromResult(AuthenticateResult.Success(ticket));
        }

        private UserIdentity DecodeHeaderAuthenticate(string token)
        {
            return JsonConvert.DeserializeObject<UserIdentity>(token);
        }
    }
}
