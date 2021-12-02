using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;

using DumDumPay.Utils;

namespace DumDumPay.API
{
    public class DumDumPayProvider : IDumDumPayProvider
    {
        private const string CreateResource = "api/payment/create";
        private const string ConfirmResource = "api/payment/confirm";
        private const string StatusResource = "api/payment/{0}/status";
        
        private string EndPoint { get; }
        private IDictionary<string, string> Headers { get; }
        private IHttpHelper HttpHelper { get; }
        
        #region IDumDumPayProvider implementation

        public CreatePaymentResult Create(
            string orderId,
            decimal amount,
            string currency,
            string country,
            string cardNumber,
            string cardHolder,
            string cardExpiryDate,
            string cvv)
        {
            return ProcessHttpRequest(() =>
            {
                var baseUri = new Uri(EndPoint);
                var uri = new Uri(baseUri, CreateResource);
                var postData = new
                {
                    orderId,
                    amount,
                    currency,
                    country,
                    cardNumber,
                    cardHolder,
                    cardExpiryDate,
                    cvv
                };
                var json = JsonSerializer.Serialize(postData);

                var response = HttpHelper.Post(
                                               uri,
                                               json,
                                               expectedResponseStatusCode: HttpStatusCode.OK,
                                               headers: Headers);
            });
        }

        public PaymentState Confirm(string transactionId, string paReq)
        {
            //
        }

        public PaymentState Status(string transactionId)
        {
            //
        }
        #endregion

        public DumDumPayProvider(
            string endPoint,
            string merchantId,
            string secretKey,
            IHttpHelper httpHelper = null,
            int timeoutInSeconds = 100)
        {
            if (timeoutInSeconds < 0)
                throw new ArgumentException($"{nameof(timeoutInSeconds)} cannot be less then 0", nameof(timeoutInSeconds));
            
            EndPoint = Ensure.ArgumentNotNullOrEmpty(endPoint, nameof(endPoint));
            Headers = new Dictionary<string, string>
            {
                {"mechant-id", merchantId},
                {"secret-key", secretKey}
            };
            HttpHelper = httpHelper ?? new HttpHelper(timeoutInSeconds);
        }

        private static T ProcessHttpRequest<T>(Func<T> func)
        {
            try {
                return func();
            }
            catch (HttpException ex) {
                //
            }
            catch (Exception ex) {
                //
            }
        }
    }
}