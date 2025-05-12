using IbkrTradeRepository.PortalApp.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IbkrTradeRepository.PortalApp.Infrastructure.Persistance.Configurations
{
    public class IbkrCodeConfiguration : IEntityTypeConfiguration<IbkrCode>
    {
        public void Configure(EntityTypeBuilder<IbkrCode> builder)
        {
            builder.HasKey(c => c.Code);

            builder.Property(c => c.Description)
                .IsRequired();
        }
    }
}
