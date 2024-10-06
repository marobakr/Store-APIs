using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Store.S_02.Core.Entities;

namespace Store.S_02.Repository.Data.Configrations;

public class ProductBrandConfigurations: IEntityTypeConfiguration<ProductBrand>
{
    public void Configure(EntityTypeBuilder<ProductBrand> builder)
    {
        builder.Property(P => P.Name).IsRequired();
    }
}