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
        private readonly IAccountRepository _accountRepository;

        public CashTransactionParser(ICashTransactionRepository cashTransactionRepository, IAccountRepository accountRepository)
        {          
            _cashTransactionRepository = cashTransactionRepository;
            _accountRepository = accountRepository;
        }

        public async Task ParseAndSaveAsync(Stream csvStream, string fileName)
        {
            var records = await ParseAsync(csvStream);
            var accountNumber = fileName.Split('_')[0];

            if (records.Any())
            {
                await ValidateAndSaveTransactionAsync(records, accountNumber);
            }            
        }

        private async Task ValidateAndSaveTransactionAsync(IEnumerable<CashTransaction> cashTransactions, string accountNumber)
        {
            var account = await _accountRepository.GetAccountByNumber(accountNumber) ?? throw new InvalidOperationException($"Account with number {accountNumber} does not exist.");

            foreach (var transaction in cashTransactions)
            {
                transaction.Account = account;
                transaction.AccountId = account.Id;
            }

            await _cashTransactionRepository.AddCashTransactionsAsync(cashTransactions);
        }

        private async Task<IEnumerable<CashTransaction>> ParseAsync(Stream csvStream)
        {
            using var reader = new StreamReader(csvStream);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

            csv.Context.RegisterClassMap<CashTransactionMap>();

            var records = new List<CashTransaction>();
            
            await foreach (var record in csv.GetRecordsAsync<CashTransaction>())
            {
                records.Add(record);
            }

            return records;
        }

        private sealed class CashTransactionMap : ClassMap<CashTransaction>
        {
            public CashTransactionMap()
            {
                Map(m => m.Id).Constant(new Guid());
                Map(m => m.TransactionDate).Name("Date").TypeConverter<DateOnlyConverter>();
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

        private sealed class DateOnlyConverter : ITypeConverter
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
                throw new InvalidCastException($"Invalid value to DateTimeConverter. Value: '{text}'");
            }

            public string ConvertToString(object value, IWriterRow row, MemberMapData memberMapData)
            {
                return ((DateTime)value).ToString("yyyy-MM-dd");
            }
        }
    }
}
