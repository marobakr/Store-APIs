using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Store.S_02.Core.Entities;

namespace Store.S_02.Repository.Data.Configrations;

public class ProductConfigurations: IEntityTypeConfiguration<Products>
{
    public void Configure(EntityTypeBuilder<Products> builder)
    {
        builder.Property(p => p.Name).HasMaxLength(200).IsRequired();
        builder.Property(p => p.Description).IsRequired();
        builder.Property(p => p.Price).HasColumnType("decimal(18,2)");
        builder.HasOne(P => P.Brand)
            .WithMany()
            .HasForeignKey(P => P.BrandId)
            .OnDelete(DeleteBehavior.SetNull);
        
        builder.HasOne(P => P.Type)
            .WithMany()
            .HasForeignKey(P => P.TypeId)
            .OnDelete(DeleteBehavior.SetNull);


        builder.Property(P => P.BrandId).IsRequired(false);
        builder.Property(P => P.TypeId).IsRequired(false);

    }
}