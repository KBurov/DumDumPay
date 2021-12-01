using System.Net.Http;

using DumDumPay.Utils;

namespace DumDumPay.API
{
    public class CreatePaymentResult : PaymentBase
    {
        public string PaReq { get; }
        
        public string Url { get; }
        
        public HttpMethod Method { get; }

        public CreatePaymentResult(
            string transactionId,
            PaymentStatus status,
            string paReq,
            string url,
            HttpMethod method) : base(transactionId, status)
        {
            PaReq = Ensure.ArgumentNotNullOrEmpty(paReq, nameof(paReq));
            // TODO: Use Uri
            Url = Ensure.ArgumentNotNullOrEmpty(url, nameof(url));
            // TODO: Add validation
            Method = method;
        }
    }
}