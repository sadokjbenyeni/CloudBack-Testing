using CloudBacktesting.PaymentService.Domain.Aggregates.BillingItemAggregate.Events;
using CloudBacktesting.PaymentService.Domain.Aggregates.PaymentMethodAggregate;
using CloudBacktesting.PaymentService.Domain.Aggregates.PaymentMethodAggregate.Commands;
using CloudBacktesting.PaymentService.Domain.Aggregates.PaymentMethodAggregate.Events;
using CloudBacktesting.PaymentService.Infra.Models;
using CloudBacktesting.PaymentService.Infra.PaymentServices.CardServices;
using EventFlow;
using EventFlow.Aggregates;
using EventFlow.Aggregates.ExecutionResults;
using S2p.RestClient.Sdk.Entities;
using S2p.RestClient.Sdk.Infrastructure;
using S2p.RestClient.Sdk.Infrastructure.Authentication;
using S2p.RestClient.Sdk.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CloudBacktesting.PaymentService.Domain.Aggregates.BillingItemAggregate
{
    public class BillingItem : AggregateRoot<BillingItem, BillingItemId>, IEmit<BillingItemCreatedEvent>, IEmit<PaymentMethodStatusCheckedEvent>
    {
        private const string URIPAYMENT = "https://securetest.smart2pay.com/payments";
        private const int SITEID = 1010;
        private const string APIKEY = "gabi";
        private string paymentMethodId;
        private string subscriptionRequestId;

        private readonly ICommandBus commandBus;


        public BillingItem(BillingItemId aggregateId) : base(aggregateId) { }

        public IExecutionResult Create(string paymentMethodId, string paymentMethodStatus)
        {

            Emit(new BillingItemCreatedEvent(this.Id.Value, paymentMethodId, "Creating", paymentMethodStatus, DateTime.UtcNow));
            return ExecutionResult.Success();
        }

        public IExecutionResult LinkSubscriptionToBilling(string subscriptionRequestId)
        {
            Emit(new SubscriptionRequestToBillingItemLinkedEvent(subscriptionRequestId));
            return ExecutionResult.Success();
        }

        public IExecutionResult LinkBillingToPayment(string itemId, string paymentMethodId, string paymentMethodStatus)
        {
            Emit(new BillingItemToPaymentMethodLinkedEvent(itemId, paymentMethodId, paymentMethodStatus));
            return ExecutionResult.Success();
        }

        public IExecutionResult GenerateInvoice(string invoiceId, string method, string client, string cardHolder, string address, string amount, DateTime invoiceDate)
        {
            Emit(new InvoiceGeneratedEvent(invoiceId, method, client, cardHolder, address, amount, invoiceDate));
            return ExecutionResult.Success();
        }

        public async Task<IExecutionResult> ExecutePayment(string merchantTransactionId, string billingItemId, string subscriber, Card cardDetails, string currency, double amount)
        {
            var baseAddress = new Uri(URIPAYMENT);

            IHttpClientBuilder httpClientBuilder = new HttpClientBuilder(() => new AuthenticationConfiguration
            {
                SiteId = SITEID,
                ApiKey = APIKEY
            });

            var httpClient = httpClientBuilder.Build();
            var paymentService = new CardPaymentService(httpClient, baseAddress);

            var service = new Smart2PayCardService(paymentService);

            var response = await service.CreateAsync(merchantTransactionId, subscriber, cardDetails, amount, currency, CancellationToken.None);
            Emit(new PaymentExecutedEvent(merchantTransactionId, billingItemId, subscriber, cardDetails, amount, currency));
            return ExecutionResult.Success();

        }


        public void Apply(BillingItemCreatedEvent @event) { }

        public void Apply(BillingItemToPaymentMethodLinkedEvent @event)
        {
            this.paymentMethodId = @event.PaymentMethodId;
        }

        public void Apply(InvoiceGeneratedEvent @event) { }

        public void Apply(SubscriptionRequestToBillingItemLinkedEvent @event)
        {
            this.subscriptionRequestId = @event.SubscriptionRequestId;
        }

        public void Apply(PaymentExecutedEvent @event) { }

        public void Apply(PaymentMethodStatusCheckedEvent @event) { }
    }
}
