using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace DumDumPay.Utils
{
    /// <summary>
    ///     Helper methods to GET/POST HTTP methods and process received HTML pages.
    /// </summary>
    public interface IHttpHelper
    {
        /// <summary>
        ///     Implements HTTP GET method.
        /// </summary>
        /// <param name="url">the URI that identifies the Internet resource</param>
        /// <param name="preferredEncoding">the preferred encoding for result</param>
        /// <param name="contentType">the value of the <see langword="Content-type" /> HTTP header</param>
        /// <param name="expectedResponseStatusCode">the expected <see cref="HttpStatusCode" /> for correct GET method response</param>
        /// <param name="headers">the additional custom HTTP headers</param>
        /// <returns>A result of HTTP GET method for provided parameters</returns>
        string Get(
            string url,
            Encoding preferredEncoding = null,
            string contentType = "application/json",
            HttpStatusCode expectedResponseStatusCode = HttpStatusCode.OK,
            IDictionary<string, string> headers = null);

        /// <summary>
        ///     Implements HTTP GET method.
        /// </summary>
        /// <param name="uri">the <see cref="Uri" /> containing the URI of the requested resource</param>
        /// <param name="preferredEncoding">the preferred encoding for result</param>
        /// <param name="contentType">the value of the <see langword="Content-type" /> HTTP header</param>
        /// <param name="expectedResponseStatusCode">the expected <see cref="HttpStatusCode" /> for correct GET method response</param>
        /// <param name="headers">the additional custom HTTP headers</param>
        /// <returns>A result of HTTP GET method for provided parameters</returns>
        string Get(
            Uri uri,
            Encoding preferredEncoding = null,
            string contentType = "application/json",
            HttpStatusCode expectedResponseStatusCode = HttpStatusCode.OK,
            IDictionary<string, string> headers = null);

        /// <summary>
        ///     Implements HTTP POST method.
        /// </summary>
        /// <param name="url">the URI that identifies the Internet resource</param>
        /// <param name="postData">the data which will be sent in POST</param>
        /// <param name="preferredEncoding">the preferred encoding for result</param>
        /// <param name="contentType">the value of the <see langword="Content-type" /> HTTP header</param>
        /// <param name="expectedResponseStatusCode">the expected <see cref="HttpStatusCode" /> for correct POST method response</param>
        /// <param name="headers">the additional custom HTTP headers</param>
        /// <returns>A result of HTTP POST method for provided parameters</returns>
        string Post(
            string url,
            string postData = null,
            Encoding preferredEncoding = null,
            string contentType = "application/json",
            HttpStatusCode expectedResponseStatusCode = HttpStatusCode.OK,
            IDictionary<string, string> headers = null);

        /// <summary>
        ///     Implements HTTP POST method.
        /// </summary>
        /// <param name="uri">the <see cref="Uri" /> containing the URI of the requested resource</param>
        /// <param name="postData">the data which will be sent in POST</param>
        /// <param name="preferredEncoding">the preferred encoding for result</param>
        /// <param name="contentType">the value of the <see langword="Content-type" /> HTTP header</param>
        /// <param name="expectedResponseStatusCode">the expected <see cref="HttpStatusCode" /> for correct POST method response</param>
        /// <param name="headers">the additional custom HTTP headers</param>
        /// <returns>A result of HTTP POST method for provided parameters</returns>
        string Post(
            Uri uri,
            string postData = null,
            Encoding preferredEncoding = null,
            string contentType = "application/json",
            HttpStatusCode expectedResponseStatusCode = HttpStatusCode.OK,
            IDictionary<string, string> headers = null);
    }
}