using IbkrTradeRepository.PortalApp.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IbkrTradeRepository.PortalApp.Infrastructure.Persistance.Configurations
{
    public class OptionTradeConfiguration : IEntityTypeConfiguration<OptionTrade>
    {
        public void Configure(EntityTypeBuilder<OptionTrade> builder)
        {
            builder.ToTable("OptionTrades");

            builder.HasKey(o => o.TradeId);

            // Enum stored as string (P or C) not sure if the max lenght has to be 4
            builder.Property(o => o.OptionType)
                .IsRequired()
                .HasConversion<string>()
                .HasMaxLength(4);

            builder.Property(o => o.StrikePrice)
                .IsRequired()
                .HasPrecision(18, 8);

            builder.Property(o => o.ExpirationDate)
                .IsRequired();

            builder.Property(o => o.Multiplier)
                .IsRequired()
                .HasDefaultValue(100);
        }
    }
}
