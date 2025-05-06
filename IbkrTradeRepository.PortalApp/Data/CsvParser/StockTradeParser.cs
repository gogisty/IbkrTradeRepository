
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using IbkrTradeRepository.PortalApp.Domain;
using IbkrTradeRepository.PortalApp.Infrastructure.Persistance.Repositories;
using System.Globalization;

namespace IbkrTradeRepository.PortalApp.Data.CsvParser
{
    public class StockTradeParser : ICsvParserAndSaveStrategy
    {
        private readonly ITradeRepository _tradeRepository;

        public StockTradeParser(ITradeRepository tradeRepository)
        {
            _tradeRepository = tradeRepository;
        }

        public async Task ParseAndSaveAsync(Stream csvStream, string fileName)
        {
            var records = this.Parse(csvStream);

            // Todo records link to account and make sure all field are populated

            await _tradeRepository.AddTradesAsync(records);
        }

        private IEnumerable<Trade> Parse(Stream csvStream)
        {
            using var reader = new StreamReader(csvStream);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

            csv.Context.RegisterClassMap<StockTradeMap>();
            return csv.GetRecords<Trade>().ToList();
        }

        private sealed class StockTradeMap : ClassMap<Trade>
        {
            public StockTradeMap()
            {
                Map(m => m.Symbol).Name("Symbol");
                Map(m => m.TradeDate).Name("Date/Time");
                Map(m => m.Quantity).Name("Quantity");
                Map(m => m.TradePrice).Name("T.Price");
                Map(m => m.Proceeds).Name("Proceeds");
                Map(m => m.Commission).Name("Comm/Fee");
                Map(m => m.Currency).Name("Currency");
                Map(m => m.TradeType).Constant(TradeType.Stock);
            }
        }
    }
}