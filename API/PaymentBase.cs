namespace DumDumPay.API
{
    public abstract class PaymentBase
    {
        public string TransactionId { get; }

        public PaymentStatus Status { get; }

        public PaymentBase(string transactionId, PaymentStatus status)
        {
            // TODO: Add validation
            TransactionId = transactionId;
            // TODO: Add validation
            Status = status;
        }
    }
}