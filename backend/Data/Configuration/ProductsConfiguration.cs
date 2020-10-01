using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;

namespace Data.Configuration
{
    public class ProductsConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(product => product.Name).IsRequired().HasMaxLength(100);
            builder.Property(product => product.Description).IsRequired().HasMaxLength(256);
            builder.Property(product => product.Price).HasColumnType("decimal(18,2)");
            builder.Property(product => product.PictureURL).IsRequired();

            builder.HasOne(product => product.ProductBrand).WithMany().HasForeignKey(product => product.ProductBrandId);
            builder.HasOne(product => product.ProductType).WithMany().HasForeignKey(product => product.ProductTypeId);
        }
    }
}