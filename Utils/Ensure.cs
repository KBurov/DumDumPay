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

        public static TEnum ArgumentHasCorrectEnumValue<TEnum>(TEnum argumentValue, string argumentName)
            where TEnum : struct, Enum
        {
            if (!EnumerationValidator.IsDefined(argumentValue))
                throw new ArgumentException($"{argumentName} contains incorrect value", argumentName);
                    
            return argumentValue;
        }
    }
}