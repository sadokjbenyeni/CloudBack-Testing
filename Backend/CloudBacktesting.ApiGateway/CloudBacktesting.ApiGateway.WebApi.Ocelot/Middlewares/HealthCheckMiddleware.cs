using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace CloudBacktesting.ApiGateway.WebApi.Ocelot.Middlewares
{
    public class HealthCheckMiddleware
    {
        private readonly RequestDelegate _next;
        public HealthCheckMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext httpContext)
        {
            if (string.Equals(httpContext.Request.Path, "/ping", System.StringComparison.InvariantCultureIgnoreCase))
            {
                 httpContext.Response.StatusCode = 200;
                return httpContext.Response.CompleteAsync();
                //return Task.CompletedTask;
            }
            return this._next(httpContext);
        }
    }

    public static class HealthCheckMiddlewareExtension
    {
        public static IApplicationBuilder UseHealthCkeck(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<HealthCheckMiddleware>();
        }
    }
}
