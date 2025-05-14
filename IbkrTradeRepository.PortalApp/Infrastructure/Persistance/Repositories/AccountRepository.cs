using IbkrTradeRepository.PortalApp.Domain;
using Microsoft.EntityFrameworkCore;

namespace IbkrTradeRepository.PortalApp.Infrastructure.Persistance.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly PortfolioDbContext _dbContext;

        public AccountRepository(PortfolioDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> AccountExists(string accountNumber)
        {
            return await _dbContext.Accounts
                .AnyAsync(a => a.AccountNumber == accountNumber);
        }

        public async Task AddAccountAsync(Account account)
        {
            await _dbContext.Accounts.AddAsync(account);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAccountAsync(Guid id)
        {
            var account = _dbContext.Accounts.Find(id);
            if (account != null)
            {
                _dbContext.Accounts.Remove(account);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<Account?> GetAccountByIdAsync(Guid id)
        {
            return await _dbContext.Accounts
                .Include(a => a.CashTransactions)
                .Include(a => a.Trades)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<Account?> GetAccountByNumber(string accountNumber)
        {
            return await _dbContext.Accounts
                .Include(a => a.CashTransactions)
                .Include(a => a.Trades)
                .FirstOrDefaultAsync(a => a.AccountNumber == accountNumber);
        }

        public async Task<IEnumerable<Account>> GetAllAccountsAsync()
        {
            return await _dbContext.Accounts
                .Include(a => a.CashTransactions)
                .Include(a => a.Trades)
                .ToListAsync();
        }

        public async Task UpdateAccountAsync(Account account)
        {
            _dbContext.Accounts.Update(account);
            await _dbContext.SaveChangesAsync();
        }
    }
}
