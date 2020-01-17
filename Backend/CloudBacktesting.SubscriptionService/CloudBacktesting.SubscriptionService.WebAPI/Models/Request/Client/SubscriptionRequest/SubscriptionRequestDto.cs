namespace CloudBacktesting.SubscriptionService.WebAPI.Models.Request.Client.SubscriptionRequest
{
    public class CreateSubscriptionRequestDto
    {
        public string SubscriptionAccountId { get; set; }
        public string Type { get; set; }
        public string PaymentMethodId { get; set; }
    }
    public class UpdateSubscriptionRequestDto
    {
        public string Id { get; set; }
        public string SubscriptionAccountId { get; set; }
        public string Status { get; set; }
        public string Subscriber { get; set; }
        public string Type { get; set; }
    }
}
