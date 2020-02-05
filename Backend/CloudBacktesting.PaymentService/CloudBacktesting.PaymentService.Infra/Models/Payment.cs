using CloudBacktesting.PaymentService.Infra.Models;

namespace CloudBacktesting.PaymentService.Infra.Models
{
    public class Payment
    {
        public string MerchantTransactionID { get; set; }
        public int Amount { get; set; }
        public string Currency { get; set; }
        public Card Card { get; set; }
        public bool Capture { get; set; }
    }
}