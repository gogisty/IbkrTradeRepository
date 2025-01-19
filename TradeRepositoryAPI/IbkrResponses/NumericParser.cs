using System.Globalization;

namespace TradeRepositoryAPI.IbkrResponses
{
    public static class NumericParser
    {
        public static T Parse<T>(string value) where T : struct, IConvertible
        {
            if (string.IsNullOrWhiteSpace(value))
                return default;

            var type = typeof(T);
            var styles = NumberStyles.AllowLeadingSign | NumberStyles.AllowThousands | NumberStyles.AllowDecimalPoint;

            if (type == typeof(float))
                return (T)(object)float.Parse(value, styles, CultureInfo.InvariantCulture);

            if (type == typeof(double))
                return (T)(object)double.Parse(value, styles, CultureInfo.InvariantCulture);

            if (type == typeof(decimal))
                return (T)(object)decimal.Parse(value, styles, CultureInfo.InvariantCulture);

            if (type == typeof(int))
                return (T)(object)int.Parse(value, styles, CultureInfo.InvariantCulture);

            throw new InvalidOperationException($"Unsupported type: {type.FullName}");
        }
    }
}
