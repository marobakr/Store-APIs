using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Store.S_02.Core.Entities.Order;

namespace Store.S_02.Repository.Data.Configrations;

public class OrderConfigurations : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.Property(O => O.Subtotal).HasColumnType("decimal(18,2)");
        builder.Property(O => O.Status).HasConversion(OStatus => OStatus.ToString(),
            OStatus => (OrderStatus)Enum.Parse(typeof(OrderStatus), OStatus));
        builder.OwnsOne(O => O.ShipToAddress, SA => SA.WithOwner());
        builder.HasOne(O => O.DeliveryMethod).WithMany().HasForeignKey(O => O.DeliveryMethodId);
    }
}