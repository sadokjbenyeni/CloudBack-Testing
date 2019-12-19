using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace CloudBacktesting.PaymentService.Infra.Security
{
    public class CloudBacktestingAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly CloudBacktestingAuthentificationEngine authenticateService;

        public CloudBacktestingAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock)
            : base(options, logger, encoder, clock)
        {
            authenticateService = new CloudBacktestingAuthentificationEngine();
        }
        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            return authenticateService.HandleAsync(Logger, Request, Scheme);
        }
    }
}
