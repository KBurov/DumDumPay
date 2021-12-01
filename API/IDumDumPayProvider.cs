namespace DumDumPay.API
{
    public interface IDumDumPayProvider
    {
        CreatePaymentResult Create(
            string orderId,
            decimal amount,
            string currency,
            string country,
            string cardNumber,
            string cardHolder,
            string cardExpiryDate,
            string cvv);

        PaymentState Confirm(string transactionId, string paReq);

        PaymentState Status(string transactionId);
    }
}