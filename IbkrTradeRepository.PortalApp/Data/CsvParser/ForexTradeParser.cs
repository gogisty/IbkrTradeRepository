using CsvHelper;
using CsvHelper.Configuration;
using IbkrTradeRepository.PortalApp.Domain;
using IbkrTradeRepository.PortalApp.Infrastructure.Persistance.Repositories;
using System.Globalization;

namespace IbkrTradeRepository.PortalApp.Data.CsvParser
{
    public class ForexTradeParser : ICsvParserAndSaveStrategy
    {
        private readonly ITradeRepository _tradeRepository;

        public ForexTradeParser(ITradeRepository tradeRepository)
        {
            _tradeRepository = tradeRepository;
        }

        public async Task ParseAndSaveAsync(Stream csvStream, string fileName)
        {
            var records = await ParseAsync(csvStream);

            if (records.Any())
            {
                // Todo records link to account and make sure all field are populated

                await _tradeRepository.AddTradesAsync(records);
            }            
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
                Map(m => m.TradeDate).Name("Date/Time");
                Map(m => m.Quantity).Name("Quantity");
                Map(m => m.TradePrice).Name("T.Price");
                Map(m => m.Proceeds).Name("Proceeds");
                Map(m => m.Commission).Name("Comm in EUR");
                Map(m => m.Currency).Name("Currency");
                Map(m => m.TradeType).Constant(TradeType.Forex);
            }
        }
    }
}