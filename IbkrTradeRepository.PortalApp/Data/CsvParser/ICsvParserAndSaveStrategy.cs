namespace IbkrTradeRepository.PortalApp.Data.CsvParser
{
    public interface ICsvParserAndSaveStrategy
    {
        Task ParseAndSaveAsync(Stream csvStream, string fileName);
    }
}
