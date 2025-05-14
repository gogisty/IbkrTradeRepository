using IbkrTradeRepository.PortalApp.Domain;
using Microsoft.EntityFrameworkCore;

namespace IbkrTradeRepository.PortalApp.Infrastructure.Persistance.Repositories
{
    public class CashTransactionRepository : ICashTransactionRepository
    {
        private readonly PortfolioDbContext _context;

        public CashTransactionRepository(PortfolioDbContext context)
        {
            _context = context;
        }

        public async Task AddCashTransactionAsync(CashTransaction cashTransaction)
        {
            await _context.CashTransactions.AddAsync(cashTransaction);
            await _context.SaveChangesAsync();
        }

        public async Task AddCashTransactionsAsync(IEnumerable<CashTransaction> cashTransactions)
        {
            await _context.CashTransactions.AddRangeAsync(cashTransactions);
            await _context.SaveChangesAsync();
        }

        public async Task<CashTransaction?> GetCashTransactionByIdAsync(Guid id)
        {
            return await _context.CashTransactions
                .FirstOrDefaultAsync(ct => ct.Id == id);
        }

        public async Task<IEnumerable<CashTransaction>> GetCashTransactionsByAccountIdAsync(Guid accountId)
        {
            return await _context.CashTransactions
                .Where(ct => ct.AccountId == accountId)
                .ToListAsync();
        }

        public async Task RemoveCashTransactionAsync(Guid id)
        {
            var cashTransaction = await GetCashTransactionByIdAsync(id);
            if (cashTransaction != null)
            {
                _context.CashTransactions.Remove(cashTransaction);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateCashTransactionAsync(CashTransaction cashTransaction)
        {
            _context.CashTransactions.Update(cashTransaction);
            await _context.SaveChangesAsync();
        }
    }
}
