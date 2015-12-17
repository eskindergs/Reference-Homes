using CodedHomes.Models;
using System.Data.Entity.ModelConfiguration;

namespace CodedHomes.Data.Configuration
{
    public class HomeConfiguration : EntityTypeConfiguration<Home>
    {
        public HomeConfiguration()
        {
            this.Property(p => p.StreetAddress)
                .IsRequired().HasMaxLength(100);

            this.Property(p => p.StreetAddress2)
                .IsOptional().HasMaxLength(100);

            this.Property(p => p.City)
                .IsRequired().HasMaxLength(50);

            this.Property(p => p.ZipCode)
                .IsRequired();

            this.Property(p => p.ImageName)
                .HasMaxLength(100);

            this.Property(p => p.CreatedOn)
                .IsRequired().HasColumnType("datetime");

            this.Property(p => p.ModifiedOn)
                .IsRequired().HasColumnType("datetime");
        }
    }
}
