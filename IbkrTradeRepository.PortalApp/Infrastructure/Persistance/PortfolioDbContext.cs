using IbkrTradeRepository.PortalApp.Domain;
using Microsoft.EntityFrameworkCore;

namespace IbkrTradeRepository.PortalApp.Infrastructure.Persistance
{
    public class PortfolioDbContext : DbContext
    {
        private readonly IConfiguration _appSettings;

        public PortfolioDbContext(DbContextOptions<PortfolioDbContext> options, IConfiguration appSettings) : base(options)
        {
            _appSettings = appSettings;
        }

        public DbSet<Account> Accounts => Set<Account>();
        public DbSet<Trade> Trades => Set<Trade>();
        public DbSet<OptionTradeDetails> OptionTradeDetails => Set<OptionTradeDetails>();
        public DbSet<CashTransaction> CashTransactions => Set<CashTransaction>();
        public DbSet<IbkrCode> IbkrCodes => Set<IbkrCode>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PortfolioDbContext).Assembly);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
            => optionsBuilder.UseNpgsql(_appSettings.GetConnectionString("PortfolioDb"))
                .UseSeeding((context, _) =>
                {
                    context.Set<IbkrCode>().AddRange(IbkrCodeSeedData.CodesSeedData);
                    context.SaveChanges();
                });
    }
}
