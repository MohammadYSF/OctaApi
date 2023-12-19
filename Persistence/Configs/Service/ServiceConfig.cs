using Domain.Core;
using Domain.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Persistence.Configs.Service;
public class ServiceConfig : IEntityTypeConfiguration<ServiceAggregate>
{
    public void Configure(EntityTypeBuilder<ServiceAggregate> builder)
    {
        builder.OwnsOne(a => a.Code, b =>
      {
          b.Property(c => c.Value).HasColumnName("Code").IsRequired(true);
      });
        builder.OwnsOne(a => a.Name, b =>
        {
            b.Property(c => c.Value).HasColumnName("Name").IsRequired(true);
        });
        builder.OwnsOne(a => a.DefaultPrice, b =>
        {
            b.Property(c => c.Value).HasColumnName("DefaultPrice").IsRequired(false);
        });

        builder.Property(a => a.DefaultPricecHistory)
        .HasConversion(
            b => string.Join(',', b.Select(c => c.ToString())),
            b => b.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(a => (PriceHistory)a).ToList()
        );
    }
}
