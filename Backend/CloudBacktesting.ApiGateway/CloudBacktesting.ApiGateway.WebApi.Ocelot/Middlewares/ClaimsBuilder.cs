
using CloudBacktesting.ApiGateway.WebApi.Ocelot.Models;
using CloudBacktesting.ApiGateway.WebApi.Ocelot.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IdentityModel.Tokens.Jwt;
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
                try
                {
                    var tokenhandler = new JwtSecurityTokenHandler();
                    tokenhandler.ValidateToken(token, new TokenValidationParameters
                    {
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("QFCL0UDB@CKESTING2019")),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                    }, out SecurityToken validatedToken);
                    var persistenttoken = ((JwtSecurityToken)validatedToken).Payload["token"].ToString();
                    var user = await _userService.GetuserByTokenAsync(persistenttoken);
                    if (user != null)
                    {
                        var bindeduser = JsonConvert.SerializeObject(BindToUserTokenFormat(user));
                        var byteuser = Encoding.UTF8.GetBytes(bindeduser);
                        var encodeduser = System.Convert.ToBase64String(byteuser);
                        httpContext.Request.Headers.Remove("Authorization");
                        httpContext.Request.Headers.Add("Authorization", "Basic " + encodeduser);
                        var appIdentity = BuildClaimsIdentity(user);
                        httpContext.User.AddIdentity(appIdentity);
                    }

                }
                catch (SecurityTokenExpiredException)
                {
                    httpContext.Response.StatusCode = 401;
                    await httpContext.Response.WriteAsync(JsonConvert.SerializeObject(new { error = "token expired" }));
                }
                catch (SecurityTokenInvalidSignatureException)
                {
                    httpContext.Response.StatusCode = 401;
                    await httpContext.Response.WriteAsync(JsonConvert.SerializeObject(new { error = "invalid signature" }));
                }
                catch (Exception ex)
                {
                    httpContext.Response.StatusCode = 401;
                    await httpContext.Response.WriteAsync(JsonConvert.SerializeObject(new { error = ex.Message }));
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
                    SubscriptionAccountId = userreceived.SubscriptionAccountId,
                    PaymentAccountId = userreceived.PaymentAccountId
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
