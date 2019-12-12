using CloudBacktesting.SubscriptionService.Infra.Services;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CloudBacktesting.SubscriptionService.Infra.EmailSender
{
    public class SendGridMailNotificationService : IMailNotificationService
    {
        private readonly SendGridConfiguration configuration;

        public SendGridMailNotificationService(SendGridConfiguration configuration)
        {
            this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public Task Send(string sender, Receivers receviers, string subject, string message, CancellationToken cancellationToken = default)
        {
            var apiKey = configuration.ApiKey;
            var client = new SendGridClient(apiKey);
            var sendMessage = BuildMessage(sender, receviers, subject, message);
            return client.SendEmailAsync(sendMessage, cancellationToken);
        }

        private SendGridMessage BuildMessage(string sender, Receivers receviers, string subject, string message)
        {
            var sendGridMessage = new SendGridMessage()
            {
                From = new EmailAddress(sender),
                Subject = subject, 
                
            };
            sendGridMessage.AddTos(receviers.To.Select(to => new EmailAddress(to)).ToList());
            if(receviers.Bcc?.Any() ?? false)
            {
                sendGridMessage.AddBccs(receviers.Bcc.Select(to => new EmailAddress(to)).ToList());
            }
            if (receviers.Cc?.Any() ?? false)
            {
                sendGridMessage.AddBccs(receviers.Cc.Select(to => new EmailAddress(to)).ToList());
            }
            return sendGridMessage;
        }
    }

    public class SendGridConfiguration
    {
        public string ApiKey { get; set; }
    }
}
