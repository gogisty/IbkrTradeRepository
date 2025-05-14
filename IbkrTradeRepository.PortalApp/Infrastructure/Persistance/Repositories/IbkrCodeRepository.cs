using IbkrTradeRepository.PortalApp.Domain;
using Microsoft.EntityFrameworkCore;

namespace IbkrTradeRepository.PortalApp.Infrastructure.Persistance.Repositories
{
    public class IbkrCodeRepository : IIbkrCodeRepository
    {
        private readonly PortfolioDbContext _dbContext;

        public IbkrCodeRepository(PortfolioDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task AddIbkrCodeAsync(IbkrCode ibkrCode)
        {
            _dbContext.IbkrCodes.Add(ibkrCode);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<IbkrCode>> GetAllIbkrCodesAsync()
        {
            return await _dbContext.IbkrCodes.ToListAsync();
        }

        public async Task<IbkrCode?> GetIbkrCodeByIdAsync(string code)
        {
            return await _dbContext.IbkrCodes
                .FirstOrDefaultAsync(t => t.Code == code);
        }

        public async Task RemoveIbkrCodeAsync(string code)
        {
            var entity = await _dbContext.IbkrCodes
                .FirstOrDefaultAsync(t => t.Code == code);

            if (entity != null)
            {
                _dbContext.IbkrCodes.Remove(entity);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
