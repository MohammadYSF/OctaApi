using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OctaApi.Domain.Models;

namespace OctaApi.Persistence.Configs;

public class InventoryItemConfig : IEntityTypeConfiguration<InventoryItem>
{
    public void Configure(EntityTypeBuilder<InventoryItem> builder)
    {
        builder.ToTable("InventoryItem");
        builder.HasKey(a => a.Id);
        builder.Property(a => a.IsActive).IsRequired(true);
        builder.Property(a => a.Name).HasMaxLength(255).IsRequired(true);
        builder.Property(a => a.Code).IsRequired(true);
        builder.Property(a => a.RegisterDate).IsRequired(true);
        builder.Property(a => a.CountLowerBound).IsRequired(false);
        builder.Property(a => a.BuyPrice).IsRequired(false);
        builder.Property(a => a.SellPrice).IsRequired(false);

        builder.HasMany(a => a.InventoryItemHistories)
            .WithOne(a => a.InventoryItem)
            .HasPrincipalKey(a => a.Id)
            .HasForeignKey(a => a.InventoryItemId);


        builder.HasMany(a => a.InvoiceInventoryItems)
            .WithOne(a => a.InventoryItem)
            .HasPrincipalKey(a => a.Id)
            .HasForeignKey(a => a.InventoryItemId);

    }
}


