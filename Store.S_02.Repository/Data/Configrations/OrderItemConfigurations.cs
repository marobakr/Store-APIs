using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Store.S_02.Core.Entities.Order;

namespace Store.S_02.Repository.Data.Configrations;

public class OrderItemConfigurations:  IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.OwnsOne(OI => OI.Product, P => P.WithOwner());
        builder.Property(P => P.Price).HasColumnType("decimal(18,2)");
    }
}