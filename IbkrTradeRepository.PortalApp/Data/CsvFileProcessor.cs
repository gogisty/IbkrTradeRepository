using IbkrTradeRepository.PortalApp.Data.CsvParser;
using IbkrTradeRepository.PortalApp.Infrastructure.Persistance.Repositories;
using System.Text;

namespace IbkrTradeRepository.PortalApp.Data
{
    public class CsvFileProcessor : ICsvFileProcessor
    {
        private readonly Dictionary<string, ICsvParserAndSaveStrategy> _strategies;

        public CsvFileProcessor(ICashTransactionRepository cashTransactionRepository, ITradeRepository tradeRepository, IAccountRepository accountRepository)
        {
            _strategies = new Dictionary<string, ICsvParserAndSaveStrategy>
            {
                {"Date,Description,Amount,Currency,Type", new CashTransactionParser(cashTransactionRepository, accountRepository) },
                {"Symbol,Date/Time,Quantity,T.Price,Proceeds,Comm in EUR,MTM in EUR,Code,Currency,Type", new ForexTradeParser(tradeRepository) },
                {"Symbol,Date/Time,Quantity,T.Price,C.Price,Proceeds,Comm/Fee,Basis,Realized P/L,MTM P/L,Code,Currency,Type", new StockTradeParser(tradeRepository, accountRepository)},
                {"Symbol,Date/Time,Quantity,T.Price,C.Price,Proceeds,Comm/Fee,Basis,Realized P/L,MTM P/L,Code,Currency,Type,Multiplier", new OptionsTradeParser(tradeRepository, accountRepository)},
                {"AccountNumber,BaseCurrency,BrokerName", new AccountInfoParser(accountRepository)}
            };
        }

        public async Task ParseAsync(Stream csvStream, string fileName)
        {
            string headerLine;            
            using (var headerReader = new StreamReader(csvStream, Encoding.UTF8, detectEncodingFromByteOrderMarks: true, bufferSize: 1024, leaveOpen: true))
            {
                headerLine = await headerReader.ReadLineAsync();
            }

            if (headerLine == null || !_strategies.TryGetValue(headerLine, out var strategy))
            {
                throw new InvalidOperationException("Unsupported CSV format.");
            }
           
            if (csvStream.CanSeek)
            {
                csvStream.Position = 0;
            }
            else
            {                
                throw new InvalidOperationException("Stream is not seekable. Cannot re-process after reading header for strategy selection. Ensure the stream is buffered (e.g., into a MemoryStream) before calling this method.");
            }

            await strategy.ParseAndSaveAsync(csvStream, fileName);
        }
    }
}
