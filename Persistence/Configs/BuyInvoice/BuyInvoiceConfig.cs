using Domain.Invoice;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Persistence.Configs.BuyInvoice;

public class BuyInvoiceConfig : IEntityTypeConfiguration<BuyInvoiceAggregate>
{
    public void Configure(EntityTypeBuilder<BuyInvoiceAggregate> builder)
    {
        builder.ToTable("BuyInvoice");
        builder.HasKey(a => a.Id);
        builder.OwnsOne(a => a.SellerName, b =>
        {
            b.Property(c => c.Value).HasMaxLength(255).IsRequired(true).HasColumnName("SellerName");
        });
        builder.OwnsOne(a => a.BuyDate, b =>
        {
            b.Property(c => c.Value).IsRequired(true).HasColumnName("BuyDate");
        });
   
        builder.Property(a => a.InventoryItems)
            .HasConversion(
                b => string.Join(',', b),
                b => b.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(Guid.Parse).ToList()
             )
            .HasColumnName("InventoryItems");

    }
}
