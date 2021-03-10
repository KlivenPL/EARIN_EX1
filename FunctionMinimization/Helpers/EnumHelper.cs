using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace FunctionMinimization.Helpers
{
    public static class EnumHelper
    {
        private static Random random = new Random();

        public static string GetDescription(this Enum enumeration)
        {
            FieldInfo fi = enumeration.GetType().GetField(enumeration.ToString());

            if (fi.GetCustomAttributes(typeof(DescriptionAttribute), false) is DescriptionAttribute[] attributes && attributes.Any())
            {
                return attributes.First().Description;
            }

            return enumeration.ToString();
        }

        public static TEnum PickRandom<TEnum>(params TEnum[] except) where TEnum : IConvertible
        {
            if (!typeof(TEnum).IsEnum)
                throw new ArgumentException("T must be an enumerated type");

            var values = ((TEnum[])Enum.GetValues(typeof(TEnum))).Where(e => !except.Contains(e)).ToArray();
            if (values.Any())
            {
                return values[random.Next(0, values.Length)];
            }

            return default;
        }
    }
}

