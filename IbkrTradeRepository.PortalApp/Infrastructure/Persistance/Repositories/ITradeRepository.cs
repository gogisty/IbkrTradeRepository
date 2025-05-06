using IbkrTradeRepository.PortalApp.Domain;

namespace IbkrTradeRepository.PortalApp.Infrastructure.Persistance.Repositories
{
    public interface ITradeRepository
    {
        Task<IEnumerable<Trade>> GetTradesByAccountIdAsync(Guid accountId);
        Task<Trade?> GetTradeByIdAsync(Guid id);
        Task AddTradeAsync(Trade trade);
        Task AddTradesAsync(IEnumerable<Trade> trades);
        Task UpdateTradeAsync(Trade trade);
        Task RemoveTradeAsync(Guid id);
    }
}
