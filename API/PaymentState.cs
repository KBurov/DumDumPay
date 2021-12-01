namespace DumDumPay.API
{
    public class PaymentState : PaymentBase
    {
        public decimal Amount { get; }
        
        public string Currency { get; }
        
        public string OrderId { get; }
        
        public string LastFourDigits { get; }
        
        public PaymentState(
            string transactionId,
            PaymentStatus status,
            decimal amount,
            string currency,
            string orderId,
            string lastFourDigits) : base(transactionId, status)
        {
            Amount = amount;
            // TODO: Add validation for correct ISO code
            Currency = currency;
            // TODO: Use Guid instead of string value
            OrderId = orderId;
            // TODO: Add validation
            LastFourDigits = lastFourDigits;
        }
    }
}