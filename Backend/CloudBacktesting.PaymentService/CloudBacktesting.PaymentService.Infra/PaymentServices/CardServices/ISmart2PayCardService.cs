using System.Threading;
using System.Threading.Tasks;
using CloudBacktesting.PaymentService.Infra.Models;

namespace CloudBacktesting.PaymentService.Infra.PaymentServices.CardServices
{
    public interface ISmart2PayCardService
    {
        Task<bool> CreateAsync(string merchantTransactionId, string subscriber, Card cardDetails, string type, string currency, CancellationToken cancellationToken);
    }
}