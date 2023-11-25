using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OctaApi.Domain.Models;

namespace OctaApi.Persistence.Configs;

public class InvoicePaymentHistoryConfig : IEntityTypeConfiguration<InvoicePaymentHistory>
{
    public void Configure(EntityTypeBuilder<InvoicePaymentHistory> builder)
    {
        builder.ToTable("InvoicePaymentHistory");
        builder.HasKey(a => a.Id);
        builder.Property(a => a.InvoicePaymentId).IsRequired(true);
        builder.Property(a => a.UpdateDate).IsRequired(true);
        builder.Property(a => a.PaidAmount).IsRequired(true);
        builder.Property(a => a.TrackCode).IsRequired(false);
    }
}