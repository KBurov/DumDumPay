using System;
using System.Collections.Generic;
using System.Net.Http;

using DumDumPay.Utils;

namespace DumDumPay.API.Responses
{
    internal sealed class CreateResponse
    {
        public Dictionary<string, string> result { get; set; }
    }

    internal static class CreateResponseExtensions
    {
        private const string ErrorMessageTemplate = "Payment creation response does not contain {0} value";
        
        public static string GetTransactionId(this CreateResponse response)
        {
            return GetValueByKey(response, "transactionId");
        }

        public static PaymentStatus GetTransactionStatus(this CreateResponse response)
        {
            var str = GetValueByKey(response, "transactionStatus");

            return str.ToPaymentStatus();
        }

        public static string GetPaReq(this CreateResponse response)
        {
            return GetValueByKey(response, "paReq");
        }

        public static string GetUrl(this CreateResponse response)
        {
            return GetValueByKey(response, "url");
        }

        public static HttpMethod GetMethod(this CreateResponse response)
        {
            var str = GetValueByKey(response, "method");

            return new HttpMethod(str);
        }

        private static string GetValueByKey(CreateResponse response, string key)
        {
            Ensure.ArgumentNotNull(response, nameof(response));
            
            if (response.result.ContainsKey(key))
                return response.result[key];

            throw new ArgumentException(string.Format(ErrorMessageTemplate, key));
        }
    }
}