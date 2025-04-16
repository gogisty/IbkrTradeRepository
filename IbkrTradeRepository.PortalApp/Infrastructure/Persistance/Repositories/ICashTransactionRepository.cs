using IbkrTradeRepository.PortalApp.Domain;

namespace IbkrTradeRepository.PortalApp.Infrastructure.Persistance.Repositories
{
    public interface ICashTransactionRepository
    {
        Task<IEnumerable<CashTransaction>> GetCashTransactionsByAccountIdAsync(Guid accountId);
        Task<CashTransaction?> GetCashTransactionByIdAsync(Guid id);
        Task AddCashTransactionAsync(CashTransaction cashTransaction);
        Task UpdateCashTransactionAsync(CashTransaction cashTransaction);
        Task RemoveCashTransactionAsync(Guid id);
    }
}
