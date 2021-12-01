using DumDumPay.Utils;

namespace DumDumPay.API
{
    public abstract class PaymentBase
    {
        public string TransactionId { get; }

        public PaymentStatus Status { get; }

        public PaymentBase(string transactionId, PaymentStatus status)
        {
            TransactionId = Ensure.ArgumentNotNullOrEmpty(transactionId, nameof(transactionId));
            // TODO: Add validation
            Status = status;
        }
    }
}