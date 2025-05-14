using CsvHelper;
using CsvHelper.Configuration;
using IbkrTradeRepository.PortalApp.Domain;
using IbkrTradeRepository.PortalApp.Infrastructure.Persistance.Repositories;
using System.Globalization;

namespace IbkrTradeRepository.PortalApp.Data.CsvParser
{
    public partial class ForexTradeParser : ICsvParserAndSaveStrategy
    {
        private readonly ITradeRepository _tradeRepository;
        private readonly IAccountRepository _accountRepository;

        public ForexTradeParser(ITradeRepository tradeRepository, IAccountRepository accountRepository)
        {
            _tradeRepository = tradeRepository;
            _accountRepository = accountRepository;
        }

        public async Task ParseAndSaveAsync(Stream csvStream, string fileName)
        {
            var records = await ParseAsync(csvStream);


            if (records.Any())
            {
                var accountNumber = fileName.Split('_')[0];
                await EnhanceTradesAndSaveAsync(records, accountNumber);
            }            
        }

        private async Task EnhanceTradesAndSaveAsync(IEnumerable<Trade> trades, string accountNumber)
        {
            var account = await _accountRepository.GetAccountByNumber(accountNumber) ?? 
                throw new InvalidOperationException($"Account with number {accountNumber} does not exist.");

            foreach (var trade in trades)
            {
                trade.Account = account;
                trade.AccountId = account.Id;
                trade.TradeDirection = trade.Quantity > 0 ? TradeDirection.Buy : TradeDirection.Sell;
            }

            await _tradeRepository.AddTradesAsync(trades);
        }

        private async Task<IEnumerable<Trade>> ParseAsync(Stream csvStream)
        {
            using var reader = new StreamReader(csvStream);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            csv.Context.RegisterClassMap<ForexTradeMap>();
            var trades = new List<Trade>(); 

            await foreach (var record in csv.GetRecordsAsync<Trade>())
            {
                trades.Add(record);
            }

            return trades;
        }

        private sealed class ForexTradeMap : ClassMap<Trade>
        {
            public ForexTradeMap()
            {
                Map(m => m.Symbol).Name("Symbol");
                Map(m => m.TradeDate).Name("Date/Time").TypeConverter<DateTimeConverter>();
                Map(m => m.Quantity).Name("Quantity");
                Map(m => m.TradePrice).Name("T.Price");
                Map(m => m.Proceeds).Name("Proceeds");
                Map(m => m.Commission).Name("Comm in EUR");
                Map(m => m.Currency).Name("Currency");
                Map(m => m.Codes).Name("Code");
                Map(m => m.TradeType).Constant(TradeType.Forex);
            }
        }
    }
}