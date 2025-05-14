using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;

namespace IbkrTradeRepository.PortalApp.Data.CsvParser
{
    public partial class ForexTradeParser
    {
        public class DateTimeConverter : ITypeConverter
        {
            public object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
            {
                if (string.IsNullOrWhiteSpace(text))
                {
                    throw new InvalidCastException($"Date/Time Value cannot be null or empty.");
                }
                
                if (DateTime.TryParse(text, out var date))
                {
                    return DateTime.SpecifyKind(date, DateTimeKind.Utc);
                }

                throw new InvalidCastException($"Invalid value to DateTimeConverter. Value: '{text}'");
            }

            public string ConvertToString(object value, IWriterRow row, MemberMapData memberMapData)
            {
                return ((DateTime)value).ToString("yyyy-MM-dd HH:mm:ss");
            }
        }
    }
}