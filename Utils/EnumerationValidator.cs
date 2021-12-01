using System;
using System.Linq;
using System.Reflection;

namespace DumDumPay.Utils
{
    public static class EnumerationValidator
    {
        public static bool IsDefined<TEnum>(TEnum value) where TEnum : struct, Enum
        {
            var defined = Enum.IsDefined(value);

            if (!defined && IsEnumTypeFlags(typeof(TEnum)))
                return IsDefinedCombined(value);

            return defined;
        }

        private static bool IsEnumTypeFlags(ICustomAttributeProvider enumType)
        {
            var attributes = enumType.GetCustomAttributes(typeof(FlagsAttribute), true);

            return attributes.Length > 0;
        }

        private static bool IsDefinedCombined<TEnum>(TEnum value) where TEnum : struct, Enum
        {
            var valueToCheck = Convert.ToInt64(value);
            var mask = Enum.GetValues(typeof(TEnum))
                           .Cast<long>()
                           .Aggregate(0L, (current, enumValue) => current | enumValue);

            return (mask & valueToCheck) == valueToCheck;
        }
    }
}