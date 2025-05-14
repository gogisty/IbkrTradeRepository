using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using IbkrTradeRepository.PortalApp.Domain;

namespace IbkrTradeRepository.PortalApp.Data.CsvParser
{
    public partial class CashTransactionParser
    {
        public class TransactionTypeConverter<T> : EnumConverter
        {
            public TransactionTypeConverter() : base(typeof(T)) { }

            public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
            {
                if (string.IsNullOrWhiteSpace(text))
                {
                    throw new InvalidCastException($"Enum value cannot be null or empty. Type: {typeof(T)}");
                }

                if (typeof(T) == typeof(CashTransactionType))
                {
                    if (!Enum.TryParse<CashTransactionType>(text, true, out var cashTransactionType))
                    {
                        throw new InvalidCastException($"Invalid value to EnumConverter for CashTransactionType. Value: '{text}'");
                    }
                    return cashTransactionType;
                }
                
                return base.ConvertFromString(text, row, memberMapData);
            }
        }
    }
}
