using IbkrTradeRepository.PortalApp.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IbkrTradeRepository.PortalApp.Infrastructure.Persistance.Configurations
{
    public class OptionTradeDetailsConfiguration : IEntityTypeConfiguration<OptionTradeDetails>
    {
        public void Configure(EntityTypeBuilder<OptionTradeDetails> builder)
        {
            builder.HasKey(optionDetails => optionDetails.TradeId);

            builder.Property(optionDetails => optionDetails.StrikePrice)
                .HasColumnType("decimal(18,4)");

            builder.Property(optionDetails => optionDetails.ExpirationDate).IsRequired();

            builder.Property(optionDetails => optionDetails.OptionType)
                .HasConversion<string>();

            builder.Property(optionDetails => optionDetails.UnderlyingSymbol)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(optionDetails => optionDetails.Multiplier)
                .HasDefaultValue(100);

            builder.ToTable("OptionTradeDetails");
        }
    }
}
