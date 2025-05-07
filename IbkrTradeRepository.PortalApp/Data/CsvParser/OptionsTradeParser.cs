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

            if(trades.Any())
            {
                // Todo records link to account and make sure all field are populated
                // Check option details!

                await _tradeRepository.AddTradesAsync(trades);
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
                Map(m => m.TradeDate).Name("Date/Time");
                Map(m => m.Quantity).Name("Quantity");
                Map(m => m.TradePrice).Name("T.Price");
                Map(m => m.Proceeds).Name("Proceeds");
                Map(m => m.Commission).Name("Comm/Fee");
                Map(m => m.Currency).Name("Currency");
                Map(m => m.TradeType).Constant(TradeType.Option);

                // TODO: Custom mapper for the Option Details!
                //Map(m => m.OptionDetails!.Multiplier).Name("Multiplier");
            }
        }
    }
}