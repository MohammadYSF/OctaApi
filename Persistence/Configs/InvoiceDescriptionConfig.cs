using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OctaApi.Domain.Models;

namespace OctaApi.Persistence.Configs;

public class InvoiceDescriptionConfig : IEntityTypeConfiguration<InvoiceDescription>
{
    public void Configure(EntityTypeBuilder<InvoiceDescription> builder)
    {
        builder.ToTable("InvoiceDescription");
        builder.HasKey(a => a.Id);
        builder.Property(a => a.Value).HasMaxLength(255).IsRequired(true);
        builder.Property(a => a.DescriptionItemId).IsRequired(true);
        builder.Property(a => a.InvoiceId).IsRequired(true);
    }
}

