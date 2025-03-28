using IbkrTradeRepository.PortalApp.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace IbkrTradeRepository.PortalApp.Infrastructure.Persistance.Configurations
{
    public class TradeConfiguration : IEntityTypeConfiguration<Trade>
    {
        public void Configure(EntityTypeBuilder<Trade> builder)
        {
            builder.ToTable("Trades");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.Ticker)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(t => t.UnderlyingTicker)
                .HasMaxLength(20);

            builder.Property(t => t.TradeType)
                .IsRequired()
                .HasConversion<string>()
                .HasMaxLength(10);

            builder.Property(t => t.EntryDate)
                .IsRequired();

            // DateTime? remains nullable

            builder.Property(t => t.Quantity)
                .IsRequired();

            // Configure decimal precision and scale
            builder.Property(t => t.OpenPrice)
                .IsRequired()
                .HasPrecision(18, 8);

            builder.Property(t => t.ClosePrice)
                .HasPrecision(18, 8);

            builder.Property(t => t.RealizedPnL)
                .HasPrecision(18, 8);

            builder.Property(t => t.Commission)
                .IsRequired()
                .HasPrecision(18, 8);

            // AccountId Foreign Key (defined relationship in AccountConfiguration)
            // builder.Property(t => t.AccountId).IsRequired(); // Ensured by relationship config

            // --- Relationships ---

            // Many-to-One: Trade -> Account (configured primarily in AccountConfiguration)
            // builder.HasOne(t => t.Account) ... // Redundant if configured fully in AccountConfiguration

            // One-to-One: Trade -> OptionTrade
            builder.HasOne(t => t.OptionDetails)
                .WithOne(o => o.Trade)
                .HasForeignKey<OptionTrade>(o => o.TradeId)
                .IsRequired(false);

            // One-to-Many: Trade -> TradeTransactions
            builder.HasMany(t => t.Transactions)
                .WithOne(tt => tt.Trade)
                .HasForeignKey(tt => tt.TradeId)
                .IsRequired();                
        }
    }
}
