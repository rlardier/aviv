using AVIV.Domain.Entities.Advertisement;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AVIV.Infrastructure.Data.Config
{
    public class AdvertisementConfiguration : IEntityTypeConfiguration<Advertisement>
    {
        public void Configure(EntityTypeBuilder<Advertisement> builder)
        {
            builder
                .Property(t => t.Id)
                .ValueGeneratedOnAdd();

            builder
                .Property(t => t.Type)
                .HasConversion<string>();
        }
    }
}
