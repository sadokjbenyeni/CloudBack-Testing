using System;
using System.Collections.Generic;
using System.Text;

namespace CloudBacktesting.AuthentificationService
{
    public class BasicAuthentificationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly IClientService clientService;

        public BasicAuthentificationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options,
                                            ILoggerFactory logger,
                                            UrlEncoder encoder,
                                            ISystemClock clock,
                                            IClientService clientService) : base(options, logger, encoder, clock)
        {
            this.clientService = clientService;
        }

        protected override async Task<AuthentificateResult> HandleAuthentificateAsync()
        {
            if (!Request.Headers.ContainsKey("Authorization"))
                return AuthentificationResult.Fail("Missing Authorization Header");

            Client client = null;
            try
            {
                var authHeader = AuthentificationHeaderValue.Parse(Request.Headers["Authorization"]);
                var credentialBytes = Convert.FromBase64String(authHeader.Parameter);
                var token = Encoding.UTF8.GetString(credentialBytes);
                client = await this.clientService.Authentificate(token);
            }
            catch
            {
                return AuthentificateResult.Fail("Invalid Authorization Header");
            }

            var claims = new[]
            {
                new Claim(ClaimTypes.Identifier, client.Id.ToString()),
                new Claim(ClaimTypes.Token, client.Token),
            };

            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new ClaimsPrincipal(indentity);
            var ticket = new AuthentificationTicket(principal, Scheme.Name);

            return AuthentificationResult.Success(ticket);
        }
    }
}
