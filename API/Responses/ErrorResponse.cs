using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace DumDumPay.API.Responses
{
    internal sealed class ErrorResponse
    {
        [JsonPropertyName("errors")]
        public IList<Error> Errors { get; set; }

        public override string ToString()
        {
            if (Errors == null || Errors.Count == 0) return string.Empty;

            var builder = new StringBuilder();

            foreach (var error in Errors)
                builder.Append($"{(builder.Length == 0 ? string.Empty : Environment.NewLine)}{error.Type}: {error.Message}");

            return builder.ToString();
        }
    }

    internal sealed class Error
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; }
    }
}