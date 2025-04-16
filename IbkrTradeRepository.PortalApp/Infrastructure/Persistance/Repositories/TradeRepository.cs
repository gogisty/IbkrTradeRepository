using IbkrTradeRepository.PortalApp.Domain;
using Microsoft.EntityFrameworkCore;

namespace IbkrTradeRepository.PortalApp.Infrastructure.Persistance.Repositories
{
    public class TradeRepository : ITradeRepository
    {
        private readonly PortfolioDbContext _dbContext;

        public TradeRepository(PortfolioDbContext context)
        {
            _dbContext = context;
        }

        public async Task<IEnumerable<Trade>> GetTradesByAccountIdAsync(Guid accountId)
        {
            return await _dbContext.Trades
                .Where(t => t.AccountId == accountId)
                .Include(t => t.OptionDetails)
                .Include(t => t.Transactions)
                .ToListAsync();
        }

        public async Task<Trade?> GetTradeByIdAsync(Guid id)
        {
            return await _dbContext.Trades
                .Include(t => t.OptionDetails)
                .Include(t => t.Transactions)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task AddTradeAsync(Trade trade)
        {
            await _dbContext.Trades.AddAsync(trade);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateTradeAsync(Trade trade)
        {
            _dbContext.Trades.Update(trade);
            await _dbContext.SaveChangesAsync();
        }

        public async Task RemoveTradeAsync(Guid id)
        {
            var trade = await GetTradeByIdAsync(id);
            if (trade != null)
            {
                _dbContext.Trades.Remove(trade);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
