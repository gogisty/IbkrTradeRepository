using IbkrTradeRepository.PortalApp.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace IbkrTradeRepository.PortalApp.Infrastructure.Persistance.Configurations
{
    public class TradeConfiguration : IEntityTypeConfiguration<Trade>
    {
        public void Configure(EntityTypeBuilder<Trade> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Symbol)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(t => t.Currency)
                .IsRequired()
                .HasMaxLength(3);

            builder.Property(t => t.TradeType)
                .HasConversion<string>()
                .IsRequired();

            builder.HasOne(t => t.Account)
                .WithMany(a => a.Trades)
                .HasForeignKey(t => t.AccountId);

            builder.HasOne(t => t.OptionDetails)
                .WithOne(optionDetails => optionDetails.Trade)
                .HasForeignKey<OptionTradeDetails>(optionDetails => optionDetails.TradeId);

            builder.ToTable("Trades");
        }
    }
}
