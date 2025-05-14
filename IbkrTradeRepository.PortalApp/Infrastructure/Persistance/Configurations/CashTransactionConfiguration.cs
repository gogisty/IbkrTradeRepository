using IbkrTradeRepository.PortalApp.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IbkrTradeRepository.PortalApp.Infrastructure.Persistance.Configurations
{
    public class CashTransactionConfiguration : IEntityTypeConfiguration<CashTransaction>
    {
        public void Configure(EntityTypeBuilder<CashTransaction> builder)
        {
            builder.ToTable("CashTransactions");

            builder.HasKey(ct => ct.Id);

            builder.Property(ct => ct.TransactionDate)
                .IsRequired();

            builder.Property(ct => ct.Amount)
                .IsRequired()
                .HasPrecision(18, 8);

            builder.Property(ct => ct.Currency)
                .IsRequired()
                .HasMaxLength(3)
                .HasDefaultValue("EUR");

            builder.Property(ct => ct.TransactionType)
                .IsRequired()
                .HasConversion<string>()
                .HasMaxLength(20);

            builder.Property(ct => ct.Description)
                .HasMaxLength(255);
        }
    }
}
