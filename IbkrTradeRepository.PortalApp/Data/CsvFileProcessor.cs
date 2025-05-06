using IbkrTradeRepository.PortalApp.Data.CsvParser;
using IbkrTradeRepository.PortalApp.Infrastructure.Persistance.Repositories;

namespace IbkrTradeRepository.PortalApp.Data
{
    public class CsvFileProcessor : ICsvFileProcessor
    {
        private readonly Dictionary<string, ICsvParserAndSaveStrategy> _strategies;

        public CsvFileProcessor(ICashTransactionRepository cashTransactionRepository, ITradeRepository tradeRepository)
        {
            _strategies = new Dictionary<string, ICsvParserAndSaveStrategy>
            {
                {"Date,Description,Amount,Currency,Type", new CashTransactionParser(cashTransactionRepository) },
                {"Symbol,Date/Time,Quantity,T.Price,Proceeds,Comm in EUR,MTM in EUR,Code,Currency,Type", new ForexTradeParser(tradeRepository) },
                {"Symbol,Date/Time,Quantity,T.Price,C.Price,Proceeds,Comm/Fee,Basis,Realized P/L,MTM P/L,Code,Currency,Type", new StockTradeParser(tradeRepository)},
                {"Symbol,Date/Time,Quantity,T.Price,C.Price,Proceeds,Comm/Fee,Basis,Realized P/L,MTM P/L,Code,Currency,Type,Multiplier", new OptionsTradeParser(tradeRepository)}
            };
        }

        public async Task ParseAsync(Stream csvStream, string fileName)
        {
            using var reader = new StreamReader(csvStream);
            var headerLine = await reader.ReadLineAsync();

            if (headerLine == null || !_strategies.TryGetValue(headerLine, out var strategy))
            {
                throw new InvalidOperationException("Unsupported CSV format.");
            }

            await strategy.ParseAndSaveAsync(csvStream, fileName);
        }
    }
}
