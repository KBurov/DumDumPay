using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace DumDumPay.Utils
{
    /// <inheritdoc />
    public class HttpHelper : IHttpHelper
    {
        private int TimeoutInSeconds { get; }

        #region IHttpHelper implementation
        public string Post(
            string url,
            string postData = null,
            Encoding preferredEncoding = null,
            string contentType = "application/json",
            HttpStatusCode expectedResponseStatusCode = HttpStatusCode.OK,
            IDictionary<string, string> headers = null)
        {
            if (string.IsNullOrEmpty(url))
                throw new ArgumentException($"{nameof(url)} cannot be null or empty", nameof(url));

            var uri = new Uri(url);

            return Post(uri, postData, preferredEncoding, contentType, expectedResponseStatusCode, headers);
        }

        public string Post(
            Uri uri,
            string postData = null,
            Encoding preferredEncoding = null,
            string contentType = "application/json",
            HttpStatusCode expectedResponseStatusCode = HttpStatusCode.OK,
            IDictionary<string, string> headers = null)
        {
            if (uri == null)
                throw new ArgumentNullException(nameof(uri), $"{nameof(uri)} cannot be null");

            var request = (HttpWebRequest) WebRequest.Create(uri);

            request.Timeout = TimeoutInSeconds * 1000;

            if (headers?.Count > 0)
                foreach (var kvp in headers)
                    request.Headers.Add(kvp.Key, kvp.Value);

            return InternalPost(request, postData, preferredEncoding, contentType, expectedResponseStatusCode);
        }

        private static string InternalPost(
            WebRequest request,
            string postData,
            Encoding preferredEncoding,
            string contentType,
            HttpStatusCode expectedResponseStatusCode)
        {
            request.Method = WebRequestMethods.Http.Post;
            request.ContentType = contentType;

            if (!string.IsNullOrEmpty(postData))
                using (var streamWriter = new StreamWriter(request.GetRequestStream())) {
                    streamWriter.Write(postData);
                }

            return InternalProcessReceiveStream(request, preferredEncoding, expectedResponseStatusCode);
        }

        private static string InternalProcessReceiveStream(
            WebRequest request,
            Encoding preferredEncoding,
            HttpStatusCode expectedResponseStatusCode)
        {
            HttpStatusCode? responseStatusCode = null;
            string responseBody;
            var exceptionMessage = $"Could not process {request.RequestUri}";

            try {
                using (var response = (HttpWebResponse) request.GetResponse()) {
                    responseStatusCode = response.StatusCode;

                    using (var responseStream = response.GetResponseStream()) {
                        using (var reader =
                            preferredEncoding != null || !string.IsNullOrEmpty(response.CharacterSet)
                                ? new StreamReader(responseStream,
                                                   preferredEncoding ?? Encoding.GetEncoding(response.CharacterSet))
                                : new StreamReader(responseStream)) {
                            responseBody = reader.ReadToEnd();

                            if (response.StatusCode == expectedResponseStatusCode)
                                return responseBody;
                        }
                    }
                }
            }
            catch (WebException ex) {
                try {
                    if (ex.Response is HttpWebResponse response) {
                        responseStatusCode = response.StatusCode;

                        using (var responseStream = response.GetResponseStream()) {
                            using (var reader =
                                preferredEncoding != null || !string.IsNullOrEmpty(response.CharacterSet)
                                    ? new StreamReader(responseStream,
                                                       preferredEncoding ??
                                                       Encoding.GetEncoding(response.CharacterSet))
                                    : new StreamReader(responseStream)) {
                                responseBody = reader.ReadToEnd();
                            }

                            throw new HttpException(response.StatusCode, responseBody, exceptionMessage);
                        }
                    }
                }
                catch (HttpException) {
                    throw;
                }
                catch {
                    // ignored
                }

                if (responseStatusCode.HasValue)
                    throw new HttpException(responseStatusCode.Value, exceptionMessage, ex);

                throw new HttpException(exceptionMessage, ex);
            }
            catch (Exception ex) {
                if (responseStatusCode.HasValue)
                    throw new HttpException(responseStatusCode.Value, exceptionMessage, ex);

                throw new HttpException(exceptionMessage, ex);
            }

            if (responseStatusCode.HasValue)
                throw new HttpException(responseStatusCode.Value, responseBody, exceptionMessage);

            throw new HttpException(exceptionMessage);
        }
        #endregion
        
        static HttpHelper()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        }

        public HttpHelper(int timeoutInSeconds = 100)
        {
            if (timeoutInSeconds < 0)
                throw new ArgumentException($"{nameof(timeoutInSeconds)} cannot be less then 0",
                                            nameof(timeoutInSeconds));

            TimeoutInSeconds = timeoutInSeconds;
        }
    }
}