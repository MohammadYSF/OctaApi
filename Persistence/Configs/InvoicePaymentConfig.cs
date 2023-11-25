using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OctaApi.Domain.Models;

namespace OctaApi.Persistence.Configs;

public class InvoicePaymentConfig : IEntityTypeConfiguration<InvoicePayment>
{
    public void Configure(EntityTypeBuilder<InvoicePayment> builder)
    {
        builder.ToTable("InvoicePayment");
        builder.HasKey(a => a.Id);
        builder.Property(a => a.InvoiceId).IsRequired(true);
        builder.Property(a => a.LastPaymentDate).IsRequired(true);
        builder.Property(a => a.PaidAmount).IsRequired(true);

        builder.Property(a => a.TrackCode).IsRequired(false);

        builder.HasMany(a => a.InvoicePaymentHistories)
.WithOne(a => a.InvoicePayment)
.HasPrincipalKey(a => a.Id)
.HasForeignKey(a => a.InvoicePaymentId);
    }
}