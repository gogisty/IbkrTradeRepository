using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using IbkrTradeRepository.PortalApp.Domain;
using IbkrTradeRepository.PortalApp.Infrastructure.Persistance.Repositories;
using System.Globalization;

namespace IbkrTradeRepository.PortalApp.Data.CsvParser
{
    public class CashTransactionParser : ICsvParserAndSaveStrategy
    {
        private readonly ICashTransactionRepository _cashTransactionRepository;

        public CashTransactionParser(ICashTransactionRepository cashTransactionRepository)
        {
            this._cashTransactionRepository = cashTransactionRepository;
        }

        public async Task ParseAndSaveAsync(Stream csvStream, string fileName)
        {
            var records = Parse(csvStream);

            // TODO: Map to account from the file name
            await _cashTransactionRepository.AddCashTransactionsAsync(records);
        }

        private IEnumerable<CashTransaction> Parse(Stream csvStream)
        {
            using var reader = new StreamReader(csvStream);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

            csv.Context.RegisterClassMap<CashTransactionMap>();
            return csv.GetRecords<CashTransaction>().ToList();
        }

        private sealed class CashTransactionMap : ClassMap<CashTransaction>
        {
            public CashTransactionMap()
            {
                Map(m => m.TransactionDate).Name("Date");
                Map(m => m.Description).Name("Description");
                Map(m => m.Amount).Name("Amount");
                Map(m => m.Currency).Name("Currency");
                Map(m => m.TransactionType).Name("Type").TypeConverter<TransactionTypeConverter<CashTransactionType>>();
            }
        }

        private sealed class TransactionTypeConverter<T> : EnumConverter
        {
            public TransactionTypeConverter() : base(typeof(T)) { }

            public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
            {
                if (!Enum.TryParse(text, out CashTransactionType cashTransactionType))
                {                    
                    // If an invalid value is found in the CSV for the Aggregate column, throw an exception...
                    throw new InvalidCastException($"Invalid value to EnumConverter. Type: {typeof(T)} Value: {text}");
                }

                return cashTransactionType;
            }
        }
    }
}
