
using CloudBacktesting.ApiGateway.WebApi.Ocelot.Models;
using CloudBacktesting.ApiGateway.WebApi.Ocelot.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Security.Claims;
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
                    var stringuser = JsonConvert.SerializeObject(user);
                    //httpContext.Request.Headers.Remove("Authorization");
                    httpContext.Request.Headers.Remove("Authorization");
                    httpContext.Request.Headers.Add("Authorization", stringuser);
                    var appIdentity = BuildClaimsIdentity(user);
                    httpContext.User.AddIdentity(appIdentity);
                }
            }
            await _next(httpContext);

        }
        private ClaimsIdentity BuildClaimsIdentity(User user)
        {

            var claims = new List<Claim>();
            //state 1 => Activated Account
            //state 0 => Inactivated 
            claims.Add(new Claim("State", user.State.ToString()));
            claims.Add(new Claim("IsLogin", user.IsLogin.ToString(), ClaimValueTypes.Boolean));
            var appIdentity = new ClaimsIdentity(claims);
            return appIdentity;

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
