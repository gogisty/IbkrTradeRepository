namespace IbkrTradeRepository.PortalApp.Data
{
    public interface ICsvFileProcessor
    {
        Task ParseAsync(Stream csvStream, string fileName);
    }
}
