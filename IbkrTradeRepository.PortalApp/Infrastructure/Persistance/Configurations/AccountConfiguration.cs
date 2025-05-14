using IbkrTradeRepository.PortalApp.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace IbkrTradeRepository.PortalApp.Infrastructure.Persistance.Configurations
{
    public class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.ToTable("Accounts");

            builder.HasKey(a => a.Id);

            builder.Property(a => a.BrokerName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(a => a.AccountNumber)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(a => a.BaseCurrency)
                .IsRequired()
                .HasMaxLength(3)
                .HasDefaultValue("EUR");

            // One-to-Many: Account -> Trades
            builder.HasMany(a => a.Trades)
                .WithOne(t => t.Account)
                .HasForeignKey(t => t.AccountId)
                .IsRequired();

            builder.HasMany(a => a.CashTransactions)
                .WithOne(ct => ct.Account)
                .HasForeignKey(ct => ct.AccountId)
                .IsRequired();
        }
    }
}
