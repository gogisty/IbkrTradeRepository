using IbkrTradeRepository.PortalApp.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IbkrTradeRepository.PortalApp.Infrastructure.Persistance.Configurations
{
    public class TradeTransactionConfiguration : IEntityTypeConfiguration<TradeTransaction>
    {
        public void Configure(EntityTypeBuilder<TradeTransaction> builder)
        {
            builder.ToTable("TradeTransactions");

            builder.HasKey(tt => tt.Id);

            builder.Property(tt => tt.ExecutionDate)
                .IsRequired();

            builder.Property(tt => tt.Quantity)
                .IsRequired();

            builder.Property(tt => tt.Currency)
                .IsRequired()
                .HasMaxLength(3)
                .HasDefaultValue("EUR");

            builder.Property(tt => tt.Price)
                .IsRequired()
                .HasPrecision(18, 8);

            builder.Property(tt => tt.OrderType)
                .IsRequired()
                .HasMaxLength(20)
                .HasDefaultValue("Market");
        }
    }
}
