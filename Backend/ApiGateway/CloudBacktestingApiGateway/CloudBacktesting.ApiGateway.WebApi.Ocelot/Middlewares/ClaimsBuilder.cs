
using CloudBacktesting.ApiGateway.WebApi.Ocelot.Models;
using CloudBacktesting.ApiGateway.WebApi.Ocelot.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CloudBacktesting.ApiGateway.WebApi.Ocelot.Middlewares
{
    public class ClaimsBuilder
    {
        private readonly IUserService _userService;
        private readonly RequestDelegate _next;
        public ClaimsBuilder(RequestDelegate next, IUserService userService)
        {
            _next = next;
            _userService = userService;
        }
        public async Task Invoke(HttpContext httpContext)
        {
            StringValues token;
            var HasToken = httpContext.Request.Headers.TryGetValue("Authorization", out token);
            if (HasToken)
            {
                var user = await _userService.GetuserByTokenAsync(token);
                if (user != null)
                {
                    var byteuser = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(BindToUserTokenFormat(user)));
                    var encodeduser = System.Convert.ToBase64String(byteuser);
                    httpContext.Request.Headers.Remove("Authorization");
                    httpContext.Request.Headers.Add("Authorization", "Basic " + encodeduser);
                    var appIdentity = BuildClaimsIdentity(user);
                    httpContext.User.AddIdentity(appIdentity);
                }
            }
            await _next(httpContext);

        }
        private ClaimsIdentity BuildClaimsIdentity(UserReceivedData user)
        {

            var claims = new List<Claim>();
            //state 1 => Activated Account
            //state 0 => Inactivated

            claims.Add(new Claim("State", user.State.ToString()));
            claims.Add(new Claim("Role", user.Role.ToList().Contains("Administrator") ? "Administrator" : "Client"));
            claims.Add(new Claim("IsLogin", user.IsLogin.ToString(), ClaimValueTypes.Boolean));
            var appIdentity = new ClaimsIdentity(claims);
            return appIdentity;

        }

        private UserTokenFormat BindToUserTokenFormat(UserReceivedData userreceived)
        {
            return new UserTokenFormat
            {
                Name = userreceived.Name,
                Email = userreceived.Email,
                IsLogin = userreceived.IsLogin,
                Role = userreceived.Role,
                State = userreceived.State,
                Additionals = new Additionals
                {
                     Subscriptionaccountid= userreceived.SubscriptionAccountId 
                }
            };
        }
    }

    public static class ClaimsBuilderMiddlewareExtensions
    {
        public static IApplicationBuilder UseClaimsBuilder(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ClaimsBuilder>();
        }
    }
}
