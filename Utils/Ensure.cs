using System;

namespace DumDumPay.Utils
{
    public static class Ensure
    {
        public static string ArgumentNotNullOrEmpty(string argumentValue, string argumentName)
        {
            if (string.IsNullOrEmpty(argumentValue))
                throw new ArgumentException($"{argumentName} cannot be null or empty", argumentName);
            
            return argumentValue;
        }
    }
}