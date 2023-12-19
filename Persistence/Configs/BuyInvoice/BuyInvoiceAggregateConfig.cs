using Domain.Invoice;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Persistence.Configs.BuyInvoice;

public class BuyInvoiceAggregateConfig : IEntityTypeConfiguration<BuyInvoiceAggregate>
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
        builder.OwnsMany(a => a.InventoryItems, b =>
        {
            b.ToTable("BuyInvoicecInventoryItems");
            b.WithOwner().HasForeignKey("BuyInvoiceId");
            b.Property(c => c.Id)
            .ValueGeneratedNever()
            ;
            b.HasKey("Id", "SellInvoiceId");
        });
    }
}
