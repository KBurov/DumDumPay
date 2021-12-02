using System;
using System.Collections.Generic;
using System.Text;

namespace DumDumPay.API.Responses
{
    internal sealed class ErrorResponse
    {
        public IList<Error> errors { get; set; }

        public override string ToString()
        {
            if (errors == null || errors.Count == 0) return string.Empty;

            var builder = new StringBuilder();

            foreach (var error in errors)
                builder.Append($"{(builder.Length == 0 ? string.Empty : Environment.NewLine)}{error.type}: {error.message}");

            return builder.ToString();
        }
    }

    internal sealed class Error
    {
        public string type { get; set; }
        public string message { get; set; }
    }
}