using IbkrTradeRepository.PortalApp.Domain;

namespace IbkrTradeRepository.PortalApp.Infrastructure.Persistance.Repositories
{
    public interface IAccountRepository
    {
        Task AddAccountAsync(Account account);
        Task<Account?> GetAccountByIdAsync(Guid id);
        Task<Account?> GetAccountByNumber(string accountNumber);
        Task<bool> AccountExists(string accountNumber);
        Task<IEnumerable<Account>> GetAllAccountsAsync();
        Task UpdateAccountAsync(Account account);
        Task DeleteAccountAsync(Guid id);
    }
}
