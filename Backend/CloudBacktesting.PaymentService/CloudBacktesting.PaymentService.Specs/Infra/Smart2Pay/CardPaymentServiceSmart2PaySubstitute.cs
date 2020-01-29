using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using NSubstitute;
using S2p.RestClient.Sdk.Entities;
using S2p.RestClient.Sdk.Infrastructure;
using S2p.RestClient.Sdk.Services;

namespace CloudBacktesting.PaymentService.Specs.Infra.Smart2Pay
{
    public class CardPaymentServiceSmart2PaySubstitute : ICardPaymentService
    {

        public Task<ApiResult<ApiCardPaymentResponse>> AcceptChallengeAsync(long paymentId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return AcceptChallengeAsync(paymentId);
        }

        public Task<ApiResult<ApiCardPaymentResponse>> AcceptChallengeAsync(long paymentId)
        {
            var request = Substitute.ForPartsOf<HttpRequestMessage>(HttpMethod.Post, "spec.api.smart2pay.com/AcceptChallenge");
            var response = Substitute.ForPartsOf<HttpResponseMessage>(HttpStatusCode.OK);
            var card = new ApiCardPaymentResponse()
            {
                Payment = new CardPaymentResponse()
                {
                    ID = paymentId,
                    Card = new CardDetailsRequest()
                    {
                    },
                }
            };
            return Task.FromResult(ApiResult.Success<ApiCardPaymentResponse>(request, response, string.Empty, card));
        }

        public Task<ApiResult<ApiCardPaymentResponse>> AcceptChallengeAsync(long paymentId, string idempotencyToken, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return AcceptChallengeAsync(paymentId);
        }

        public Task<ApiResult<ApiCardPaymentResponse>> AcceptChallengeAsync(long paymentId, string idempotencyToken)
        {
            return AcceptChallengeAsync(paymentId);
        }

        public Task<ApiResult<ApiCardPaymentResponse>> CancelPaymentAsync(long paymentId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return CancelPaymentAsync(paymentId);
        }

        public Task<ApiResult<ApiCardPaymentResponse>> CancelPaymentAsync(long paymentId)
        {
            var request = Substitute.ForPartsOf<HttpRequestMessage>(HttpMethod.Post, "spec.api.smart2pay.com/AcceptChallenge");
            var response = Substitute.ForPartsOf<HttpResponseMessage>(HttpStatusCode.OK);
            var card = new ApiCardPaymentResponse()
            {
                Payment = new CardPaymentResponse()
                {
                    ID = paymentId,
                    Card = new CardDetailsRequest()
                    {
                    },
                    Status = new CardStateDetails()
                    {
                        ID = 0,
                        Info = "Canceled",

                    }
                },                
            };
            return Task.FromResult(ApiResult.Success<ApiCardPaymentResponse>(request, response, string.Empty, card));
        }

        public Task<ApiResult<ApiCardPaymentResponse>> CancelPaymentAsync(long paymentId, string idempotencyToken, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return CancelPaymentAsync(paymentId);
        }

        public Task<ApiResult<ApiCardPaymentResponse>> CancelPaymentAsync(long paymentId, string idempotencyToken)
        {
            return CancelPaymentAsync(paymentId);
        }

        public Task<ApiResult<ApiCardPaymentResponse>> CapturePaymentAsync(long paymentId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return CancelPaymentAsync(paymentId);
        }

        public Task<ApiResult<ApiCardPaymentResponse>> CapturePaymentAsync(long paymentId)
        {
            throw new System.NotImplementedException();
        }

        public Task<ApiResult<ApiCardPaymentResponse>> CapturePaymentAsync(long paymentId, string idempotencyToken, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<ApiResult<ApiCardPaymentResponse>> CapturePaymentAsync(long paymentId, string idempotencyToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<ApiResult<ApiCardPaymentResponse>> CapturePaymentAsync(long paymentId, long amount, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<ApiResult<ApiCardPaymentResponse>> CapturePaymentAsync(long paymentId, long amount)
        {
            throw new System.NotImplementedException();
        }

        public Task<ApiResult<ApiCardPaymentResponse>> CapturePaymentAsync(long paymentId, long amount, string idempotencyToken, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<ApiResult<ApiCardPaymentResponse>> CapturePaymentAsync(long paymentId, long amount, string idempotencyToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<ApiResult<ApiCardPaymentResponse>> CreatePaymentAsync(ApiCardPaymentRequest paymentRequest, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<ApiResult<ApiCardPaymentResponse>> CreatePaymentAsync(ApiCardPaymentRequest paymentRequest)
        {
            throw new System.NotImplementedException();
        }

        public Task<ApiResult<ApiCardPaymentResponse>> CreatePaymentAsync(ApiCardPaymentRequest paymentRequest, string idempotencyToken, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<ApiResult<ApiCardPaymentResponse>> CreatePaymentAsync(ApiCardPaymentRequest paymentRequest, string idempotencyToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<ApiResult<ApiCardPaymentResponse>> GetPaymentAsync(long paymentId, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<ApiResult<ApiCardPaymentResponse>> GetPaymentAsync(long paymentId)
        {
            throw new System.NotImplementedException();
        }

        public Task<ApiResult<ApiCardPaymentListResponse>> GetPaymentListAsync(CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<ApiResult<ApiCardPaymentListResponse>> GetPaymentListAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<ApiResult<ApiCardPaymentListResponse>> GetPaymentListAsync(CardPaymentsFilter filter, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<ApiResult<ApiCardPaymentListResponse>> GetPaymentListAsync(CardPaymentsFilter filter)
        {
            throw new System.NotImplementedException();
        }

        public Task<ApiResult<ApiCardPaymentStatusResponse>> GetPaymentStatusAsync(long paymentId, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<ApiResult<ApiCardPaymentStatusResponse>> GetPaymentStatusAsync(long paymentId)
        {
            throw new System.NotImplementedException();
        }

        public Task<ApiResult<ApiCardPaymentResponse>> RejectChallengeAsync(long paymentId, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<ApiResult<ApiCardPaymentResponse>> RejectChallengeAsync(long paymentId)
        {
            throw new System.NotImplementedException();
        }

        public Task<ApiResult<ApiCardPaymentResponse>> RejectChallengeAsync(long paymentId, string idempotencyToken, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<ApiResult<ApiCardPaymentResponse>> RejectChallengeAsync(long paymentId, string idempotencyToken)
        {
            throw new System.NotImplementedException();
        }
    }
}