using System.Net.Http;
using System.Text.Json.Serialization;

namespace DumDumPay.API.Responses
{
    internal sealed class CreateResponse
    {
        [JsonPropertyName("result")]
        public CreateResponseInternal Result { get; set; }
    }

    internal sealed class CreateResponseInternal : BaseResponse
    {
        [JsonIgnore] private HttpMethod _method;

        [JsonPropertyName("paReq")]
        public string PaReq { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }

        [JsonPropertyName("method")]
        private string MethodInt { get; set; }

        [JsonIgnore]
        public HttpMethod Method => _method ??= new HttpMethod(MethodInt);
    }
}