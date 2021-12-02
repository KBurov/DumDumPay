using System;
using System.Collections.Generic;

using DumDumPay.Utils;

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

    internal static class PaymentStatusStringConverter
    {
        private static readonly IDictionary<string, PaymentStatus> _paymentStatusStringValues =
            new Dictionary<string, PaymentStatus>
            {
                {"Init", PaymentStatus.Init},
                {"Pending", PaymentStatus.Pending},
                {"Approved", PaymentStatus.Approved},
                {"Declined", PaymentStatus.Declined},
                {"DeclinedDueToInvalidCreditCard", PaymentStatus.DeclinedDueToInvalidCreditCard}
            };
        public static PaymentStatus ToPaymentStatus(this string str)
        {
            Ensure.ArgumentNotNullOrEmpty(str, nameof(str));
            
            foreach (var kvp in _paymentStatusStringValues)
                if (string.Equals(kvp.Key, str, StringComparison.InvariantCultureIgnoreCase))
                    return kvp.Value;

            throw new ArgumentException("Incorrect PaymentStatus value");
        }
    }
}