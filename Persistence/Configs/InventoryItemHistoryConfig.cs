using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OctaApi.Domain.Models;

namespace OctaApi.Persistence.Configs;

public class InventoryItemHistoryConfig : IEntityTypeConfiguration<InventoryItemHistory>
{
    public void Configure(EntityTypeBuilder<InventoryItemHistory> builder)
    {
        builder.ToTable("InventoryItemHistory");
        builder.HasKey(a => a.Id);
        builder.Property(a => a.InventoryItemId).IsRequired(true);
        builder.Property(a => a.IsActive).IsRequired(true);
        builder.Property(a => a.Name).HasMaxLength(255).IsRequired(true);
        builder.Property(a => a.Code).IsRequired(true);
        builder.Property(a => a.UpdateDate).IsRequired(true);
        builder.Property(a => a.CountLowerBound).IsRequired(false);
        builder.Property(a => a.BuyPrice).IsRequired(false);
        builder.Property(a => a.SellPrice).IsRequired(false);        
    }
}


