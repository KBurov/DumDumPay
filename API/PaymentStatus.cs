namespace DumDumPay.API
{
    public enum PaymentStatus
    {
        Init,
        Pending,
        Approved,
        Declined,
        DeclinedDueToInvalidCreditCard
    }
}