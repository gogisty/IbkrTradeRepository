using IbkrTradeRepository.PortalApp.Domain;
using Microsoft.EntityFrameworkCore;

namespace IbkrTradeRepository.PortalApp.Infrastructure.Persistance
{
    public class PortfolioDbContext : DbContext
    {
        public PortfolioDbContext(DbContextOptions<PortfolioDbContext> options) : base(options) { }

        public DbSet<Account> Accounts => Set<Account>();
        public DbSet<Trade> Trades => Set<Trade>();
        public DbSet<OptionTradeDetails> OptionTradeDetails => Set<OptionTradeDetails>();
        public DbSet<CashTransaction> CashTransactions => Set<CashTransaction>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PortfolioDbContext).Assembly);
        }
    }
}
