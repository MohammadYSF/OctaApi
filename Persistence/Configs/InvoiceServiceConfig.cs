using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OctaApi.Domain.Models;

namespace OctaApi.Persistence.Configs;

public class InvoiceServiceConfig : IEntityTypeConfiguration<InvoiceService>
{
    public void Configure(EntityTypeBuilder<InvoiceService> builder)
    {
        builder.ToTable("InvoiceServiceItem");
        builder.HasKey(a => a.Id);
        builder.Property(a => a.InvoiceId).IsRequired(true);
        builder.Property(a => a.ServiceId).IsRequired(true);
    }
}
