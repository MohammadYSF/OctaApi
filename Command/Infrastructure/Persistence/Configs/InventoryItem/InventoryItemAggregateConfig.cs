using Command.Core.Domain.Core;
using Command.Core.Domain.InventoryItem;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Command.Infrastructure.Persistence.Configs.InventoryItem;
public class InventoryItemAggregateConfig : IEntityTypeConfiguration<InventoryItemَAggregate>
{
    public void Configure(EntityTypeBuilder<InventoryItemَAggregate> builder)
    {
        builder.OwnsOne(a => a.Code, b =>
        {
            b.Property(c => c.Value).HasColumnName("Code").IsRequired(true);
        });
        builder.OwnsOne(a => a.Name, b =>
        {
            b.Property(c => c.Value).HasColumnName("Name").IsRequired(true);
        });
        builder.OwnsOne(a => a.Count, b =>
        {
            b.Property(c => c.Value).HasColumnName("Count").IsRequired(true);
        });
        builder.OwnsOne(a => a.BuyPrice, b =>
        {
            b.Property(c => c.Value).HasColumnName("BuyPrice").IsRequired(true);
        });
        builder.OwnsOne(a => a.SellPrice, b =>
        {
            b.Property(c => c.Value).HasColumnName("SellPrice").IsRequired(true);
        });
        builder.Property(a => a.BuyPriceHistory)
        .HasConversion(
            b => string.Join(',', b.Select(c => c.ToString())),
            b => b.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(a => (PriceHistory)a).ToList()
        );
        builder.Property(a => a.SellPriceHistory)
  .HasConversion(
      b => string.Join(',', b.Select(c => c.ToString())),
      b => b.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(a => (PriceHistory)a).ToList()
  );
    }
}
