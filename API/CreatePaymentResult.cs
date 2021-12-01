using System.Net.Http;

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
            // TODO: Add validation
            PaReq = paReq;
            // TODO: Use Uri
            Url = url;
            // TODO: Add validation
            Method = method;
        }
    }
}