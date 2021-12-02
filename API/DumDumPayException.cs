using System;
using System.Net;
using System.Runtime.Serialization;
using System.Security;

using DumDumPay.Utils;

namespace DumDumPay.API
{
    [Serializable]
    public class DumDumPayException : HttpException
    {
        public DumDumPayException() { }

        public DumDumPayException(string message) : base(message) { }

        public DumDumPayException(HttpStatusCode httpStatusCode, string message) : base(httpStatusCode, message) { }

        public DumDumPayException(HttpStatusCode httpStatusCode, string responseBody, string message)
            : base(httpStatusCode, responseBody, message) { }

        public DumDumPayException(string message, Exception innerException)
            : base(message, innerException) { }

        public DumDumPayException(HttpStatusCode httpStatusCode, string message, Exception innerException) :
            base(httpStatusCode, message, innerException) { }

        public DumDumPayException(HttpStatusCode httpStatusCode, string responseBody, string message,
                                  Exception innerException)
            : base(httpStatusCode, responseBody, message, innerException) { }

        [SecuritySafeCritical]
        protected DumDumPayException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}