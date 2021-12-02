using System;
using System.IO;
using System.Net;
using System.Runtime.Serialization;
using System.Security;

namespace DumDumPay.Utils
{
    [Serializable]
    public class HttpException : Exception
    {
        private readonly HttpStatusCode _httpStatusCode;

        /// <summary>
        /// The HTTP response status code to return to the client.
        /// A non-zero HTTP code representing the exception or the <see cref="P:System.Exception.InnerException" /> code;
        /// otherwise, HTTP response status code <see cref="HttpStatusCode.InternalServerError"/> (500).
        /// </summary>
        public HttpStatusCode HttpCode => GetHttpStatusCode();

        /// <summary>
        /// The HTTP response body if exists.
        /// </summary>
        public string ResponseBody { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpException" /> class.
        /// </summary>
        public HttpException() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpException" /> class with a specified error message.
        /// </summary>
        /// <param name="message">the error message that explains the reason for the exception</param>
        public HttpException(string message) : base(message) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpException" /> class with a specified error message and
        /// <see cref="HttpStatusCode" />.
        /// </summary>
        /// <param name="httpStatusCode">the HTTP response status code sent to the client corresponding to this error</param>
        /// <param name="message">the error message that explains the reason for the exception</param>
        public HttpException(HttpStatusCode httpStatusCode, string message) : base(message)
        {
            _httpStatusCode = httpStatusCode;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpException" /> class with a specified error message and
        /// <see cref="HttpStatusCode" />.
        /// </summary>
        /// <param name="httpStatusCode">the HTTP response status code sent to the client corresponding to this error</param>
        /// <param name="responseBody">the HTTP response body</param>
        /// <param name="message">the error message that explains the reason for the exception</param>
        public HttpException(HttpStatusCode httpStatusCode, string responseBody, string message) : base(message)
        {
            _httpStatusCode = httpStatusCode;
            ResponseBody = responseBody;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpException" /> class with a specified error message
        /// and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">he error message that explains the reason for the exception</param>
        /// <param name="innerException">
        /// the exception that is the cause of the current exception;
        /// if the <paramref name="innerException" /> parameter is not a null reference,
        /// the current exception is raised in a catch block that handles the inner exception
        /// </param>
        public HttpException(string message, Exception innerException) : base(message, innerException) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpException" /> class with a specified error message
        /// and a reference to the inner exception that is the cause of this exception and <see cref="HttpStatusCode" />.
        /// </summary>
        /// <param name="httpStatusCode">the HTTP response status code sent to the client corresponding to this error</param>
        /// <param name="message">the error message that explains the reason for the exception</param>
        /// <param name="innerException">
        /// the exception that is the cause of the current exception;
        /// if the <paramref name="innerException" /> parameter is not a null reference,
        /// the current exception is raised in a catch block that handles the inner exception
        /// </param>
        public HttpException(HttpStatusCode httpStatusCode, string message, Exception innerException) :
            base(message, innerException)
        {
            _httpStatusCode = httpStatusCode;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpException" /> class with a specified error message
        /// and a reference to the inner exception that is the cause of this exception and <see cref="HttpStatusCode" />.
        /// </summary>
        /// <param name="httpStatusCode">the HTTP response status code sent to the client corresponding to this error</param>
        /// <param name="responseBody">the HTTP response body</param>
        /// <param name="message">the error message that explains the reason for the exception</param>
        /// <param name="innerException">
        /// the exception that is the cause of the current exception;
        /// if the <paramref name="innerException" /> parameter is not a null reference,
        /// the current exception is raised in a catch block that handles the inner exception
        /// </param>
        public HttpException(HttpStatusCode httpStatusCode, string responseBody, string message,
                             Exception innerException) : base(message, innerException)
        {
            _httpStatusCode = httpStatusCode;
            ResponseBody = responseBody;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpException" /> class with serialized data.
        /// </summary>
        /// <param name="info">the object that holds the serialized object data</param>
        /// <param name="context">the contextual information about the source or destination</param>
        [SecuritySafeCritical]
        protected HttpException(SerializationInfo info, StreamingContext context) : base(info, context) { }

        private HttpStatusCode GetHttpStatusCode()
        {
            return GetHttpCodeForException(this);
        }

        private static HttpStatusCode GetHttpCodeForException(Exception e)
        {
            switch (e) {
                case HttpException httpException:
                    if (httpException._httpStatusCode > 0)
                        return httpException._httpStatusCode;
                    break;
                case UnauthorizedAccessException _:
                    return HttpStatusCode.Unauthorized;
                case PathTooLongException _:
                    return HttpStatusCode.RequestUriTooLong;
            }

            return e.InnerException != null
                ? GetHttpCodeForException(e.InnerException)
                : HttpStatusCode.InternalServerError;
        }
    }
}