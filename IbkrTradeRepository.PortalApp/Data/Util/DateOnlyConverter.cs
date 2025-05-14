using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;

namespace IbkrTradeRepository.PortalApp.Data.CsvParser
{
    public partial class CashTransactionParser
    {
        public class DateOnlyConverter : ITypeConverter
        {
            public object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
            {
                if (string.IsNullOrWhiteSpace(text))
                {
                    throw new InvalidCastException($"DateOnly value cannot be null or empty.");
                }
                if (DateOnly.TryParse(text, out var date))
                {
                    return date;
                }
                throw new InvalidCastException($"Invalid value to DateOnlyConverter. Value: '{text}'");
            }

            public string ConvertToString(object value, IWriterRow row, MemberMapData memberMapData)
            {
                return ((DateOnly)value).ToString("yyyy-MM-dd");
            }
        }
    }
}
