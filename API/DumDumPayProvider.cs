using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;

using DumDumPay.API.Responses;
using DumDumPay.Utils;

namespace DumDumPay.API
{
    public class DumDumPayProvider : IDumDumPayProvider
    {
        private const string CreateResource = "api/payment/create";
        private const string ConfirmResource = "api/payment/confirm";
        private const string StatusResource = "api/payment/{0}/status";

        private const string UnknownErrorMessage = "Unknown error during processing DumDumPay request/response";

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

                var responseBody = HttpHelper.Post(
                                               uri,
                                               json,
                                               expectedResponseStatusCode: HttpStatusCode.OK,
                                               headers: Headers);
                var createResponse = JsonSerializer.Deserialize<CreateResponse>(responseBody);

                return new CreatePaymentResult(createResponse.GetTransactionId(),
                                               createResponse.GetTransactionStatus(),
                                               createResponse.GetPaReq(),
                                               createResponse.GetUrl(),
                                               createResponse.GetMethod());
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
                throw new ArgumentException($"{nameof(timeoutInSeconds)} cannot be less then 0",
                                            nameof(timeoutInSeconds));

            EndPoint = Ensure.ArgumentNotNullOrEmpty(endPoint, nameof(endPoint));
            Headers = new Dictionary<string, string>
            {
                { "mechant-id", merchantId },
                { "secret-key", secretKey }
            };
            HttpHelper = httpHelper ?? new HttpHelper(timeoutInSeconds);
        }

        private static T ProcessHttpRequest<T>(Func<T> func)
        {
            try {
                return func();
            }
            catch (HttpException ex) {
                switch (ex.HttpCode) {
                    case HttpStatusCode.Forbidden:
                    case HttpStatusCode.BadRequest:
                        throw new
                            DumDumPayException(GetExceptionMessage("Error during processing DumDumPay request", ex),
                                               ex);
                    case HttpStatusCode.Unauthorized:
                        throw new DumDumPayException(GetExceptionMessage("Unauthorized access to DumDumPay API", ex),
                                                     ex);
                    default:
                        throw new DumDumPayException(ex.HttpCode, $"{UnknownErrorMessage}: {ex}", ex);
                }
            }
            catch (Exception ex) {
                throw new DumDumPayException($"{UnknownErrorMessage}: {ex}", ex);
            }
        }

        private static string GetExceptionMessage(string messageHeader, HttpException ex)
        {
            try {
                if (!string.IsNullOrEmpty(ex.ResponseBody)) {
                    var errors = JsonSerializer.Deserialize<ErrorResponse>(ex.ResponseBody);

                    return $"{messageHeader}: {errors}";
                }
            }
            catch {
                // ignored
            }

            return messageHeader;
        }
    }
}