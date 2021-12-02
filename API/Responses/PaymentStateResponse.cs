using System.Text.Json.Serialization;

namespace DumDumPay.API.Responses
{
    internal sealed class PaymentStateResponse
    {
        [JsonPropertyName("result")]
        public PaymentStateResponseInternal Result { get; set; }
    }

    internal sealed class PaymentStateResponseInternal : BaseResponse
    {
        [JsonPropertyName("amount")]
        public decimal Amount { get; set; }
        
        [JsonPropertyName("currency")]
        public string Currency { get; set; }
        
        [JsonPropertyName("orderId")]
        public string OrderId { get; set; }
        
        [JsonPropertyName("lastFourDigits")]
        public string LastFourDigits { get; set; }
    }
}