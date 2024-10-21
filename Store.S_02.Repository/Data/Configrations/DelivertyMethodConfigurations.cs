using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Store.S_02.Core.Entities.Order;

namespace Store.S_02.Repository.Data.Configrations;

public class DelivertyMethodConfigurations: IEntityTypeConfiguration<DelivertyMethod>
{
    public void Configure(EntityTypeBuilder<DelivertyMethod> builder)
    {
        builder.Property(DM => DM.Cost).HasColumnType("decimal(18,2)");
    }
}