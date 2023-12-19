using Domain.BuyInvoice.ValueObjects;
using Domain.Customer;
using Domain.SellInvoice;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configs.SellInvoice;
public class SellInvoiceAggregateConfig : IEntityTypeConfiguration<SellInvoiceAggregate>
{
    public void Configure(EntityTypeBuilder<SellInvoiceAggregate> builder)
    {

        builder.OwnsOne(a => a.Code, b =>
        {
            b.Property(c => c.Value).HasColumnName("Code").IsRequired(true);
        });
        builder.OwnsOne(a => a.Description, b =>
        {
            b.Property(c => c.Value).HasColumnName("Description");
        });
        builder.OwnsOne(a => a.Discount, b =>
        {
            b.Property(c => c.Value).HasColumnName("Discount").IsRequired(false);
        });
        builder.OwnsOne(a => a.CreateDate, b =>
        {
            b.Property(c => c.Value).HasColumnName("CreateDate").IsRequired(true);
        });
        builder.OwnsMany(a => a.Payments, b =>
        {
            b.ToTable("SellInvoicePayments");            
            b.WithOwner().HasForeignKey("SellInvoiceId");
            b.Property(c => c.Id)
            .ValueGeneratedNever()
            ;
            b.HasKey("Id", "SellInvoiceId");
        });
        builder.OwnsMany(a => a.Services, b =>
        {
            b.ToTable("SellInvoiceServices");
            b.WithOwner().HasForeignKey("SellInvoiceId");
            b.Property(c => c.Id)
            .ValueGeneratedNever()
            ;
            b.HasKey("Id", "SellInvoiceId");
        });
        builder.OwnsMany(a => a.InventoryItems, b =>
        {
            b.ToTable("SellInvoicecInventoryItems");
            b.WithOwner().HasForeignKey("SellInvoiceId");
            b.Property(c => c.Id)
            .ValueGeneratedNever()
            ;
            b.HasKey("Id", "SellInvoiceId");
        });
    }
}
