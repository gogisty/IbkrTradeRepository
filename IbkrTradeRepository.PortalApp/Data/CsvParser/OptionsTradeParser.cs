using CsvHelper;
using CsvHelper.Configuration;
using IbkrTradeRepository.PortalApp.Domain;
using IbkrTradeRepository.PortalApp.Infrastructure.Persistance.Repositories;
using System.Globalization;

namespace IbkrTradeRepository.PortalApp.Data.CsvParser
{
    public class OptionsTradeParser : ICsvParserAndSaveStrategy
    {
        private readonly ITradeRepository _tradeRepository;
        private readonly IAccountRepository _accountRepository;

        public OptionsTradeParser(ITradeRepository tradeRepository, IAccountRepository accountRepository)
        {
            _tradeRepository = tradeRepository;
            _accountRepository = accountRepository;
        }

        public async Task ParseAndSaveAsync(Stream csvStream, string fileName)
        {
            var trades = await ParseAsync(csvStream);
            var accountNumber = fileName.Split('_');

            if (trades.Any())
            {
                await EnhanceTrades(trades, accountNumber);
                await _tradeRepository.AddTradesAsync(trades);
            }           
        }

        private async Task EnhanceTrades(IEnumerable<Trade> trades, string[] accountNumber)
        {
            var account = await _accountRepository.GetAccountByNumber(accountNumber[0]) ??
                throw new InvalidOperationException($"Account with number {accountNumber[0]} does not exist.");

            foreach (var trade in trades)
            {
                trade.Account = account;
                trade.AccountId = account.Id;
                trade.TradeDirection = trade.Quantity > 0 ? TradeDirection.Buy : TradeDirection.Sell;
            }
        }

        private async Task<IEnumerable<Trade>> ParseAsync(Stream csvStream)
        {
            using var reader = new StreamReader(csvStream);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            csv.Context.RegisterClassMap<OptionsTradeMap>();
            var trades = new List<Trade>();

            await foreach (var record in csv.GetRecordsAsync<Trade>())
            {
                trades.Add(record);
            }
            return trades;
        }

        private sealed class OptionsTradeMap : ClassMap<Trade>
        {
            public OptionsTradeMap()
            {
                Map(m => m.Symbol).Name("Symbol");
                Map(m => m.TradeDate).Name("Date/Time").TypeConverter<ForexTradeParser.DateTimeConverter>();
                Map(m => m.Quantity).Name("Quantity");
                Map(m => m.TradePrice).Name("T.Price");
                Map(m => m.Proceeds).Name("Proceeds");
                Map(m => m.Commission).Name("Comm/Fee");
                Map(m => m.Currency).Name("Currency");
                Map(m => m.Codes).Name("Code");
                Map(m => m.TradeType).Constant(TradeType.Option);
                Map(m => m.OptionDetails).Convert(x =>
                {
                    var multiplier = x.Row.GetField("Multiplier");
                    var symbol = x.Row.GetField("Symbol");

                    if (string.IsNullOrEmpty(symbol) || string.IsNullOrEmpty(multiplier))
                    { 
                        throw new Exception("OptionDetails empty!");
                    }

                    return CreateIndexOperation(symbol, multiplier);
                });
            }

            private OptionTradeDetails? CreateIndexOperation(string symbol, string multiplier)
            {
                var parts = symbol.Split([' '], StringSplitOptions.RemoveEmptyEntries);

                if (parts.Length < 4)
                {
                    throw new Exception($"Invalid symbol format: '{symbol}'. Expected format: 'Underlying Expiry Strike Type'.");
                }

                if (!DateOnly.TryParseExact(parts[1], "ddMMMyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var expirationDate))
                {
                    throw new Exception($"Error: Invalid expiration date format '{parts[1]}'. Symbol: '{symbol}'.");
                }

                var optionDetails = new OptionTradeDetails
                {
                    UnderlyingSymbol = parts[0],
                    ExpirationDate = expirationDate,
                    StrikePrice = decimal.TryParse(parts[2], NumberStyles.Any, CultureInfo.InvariantCulture, out var strikePrice) ? strikePrice : 0m,
                    Multiplier = int.TryParse(multiplier, out var mult) ? mult : 100,
                    OptionType = parts[3] == "C" ? OptionType.Call : OptionType.Put
                };

                return optionDetails;
            }
        }
    }
}