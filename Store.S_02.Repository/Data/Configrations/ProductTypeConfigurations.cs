using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Store.S_02.Core.Entities;

namespace Store.S_02.Repository.Data.Configrations;

public class ProductTypeConfigurations : IEntityTypeConfiguration<ProductType>
{
    public void Configure(EntityTypeBuilder<ProductType> builder)
    {
        builder.Property(P => P.Name).IsRequired();
    }
}