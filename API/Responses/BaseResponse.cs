using System.Text.Json.Serialization;

namespace DumDumPay.API.Responses
{
    internal abstract class BaseResponse
    {
        [JsonPropertyName("transactionId")]
        public string TransactionId { get; set; }

        [JsonPropertyName("transactionStatus")]
        private string TransactionStatus1 { get; set; }

        [JsonPropertyName("status")]
        private string TransactionStatus2 { set => TransactionStatus1 = value; }

        [JsonIgnore]
        public PaymentStatus Status => TransactionStatus1.ToPaymentStatus();
    }
}