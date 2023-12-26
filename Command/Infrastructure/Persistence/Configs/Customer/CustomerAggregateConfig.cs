using Command.Core.Domain.Customer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Command.Infrastructure.Persistence.Configs.Customer;
public class CustomerAggregateConfig : IEntityTypeConfiguration<CustomerAggregate>
{
    public void Configure(EntityTypeBuilder<CustomerAggregate> builder)
    {
        builder.ToTable("Customer");
        builder.HasKey(a => a.Id);
        builder.OwnsOne(a => a.FirstName, b =>
        {
            b.Property(c => c.Value).HasMaxLength(255).IsRequired(true).HasColumnName("FirstName");
        });
        builder.OwnsOne(a => a.LastName, b =>
        {
            b.Property(c => c.Value).HasMaxLength(255).IsRequired(true).HasColumnName("LastName");
        });
        builder.OwnsOne(a => a.PhoneNumber, b =>
        {
            b.Property(c => c.Value).HasMaxLength(255).IsRequired(true).HasColumnName("PhoneNumber");
        });
        builder.OwnsOne(a => a.Code, b =>
        {
            b.Property(c => c.Value).IsRequired(true).HasColumnName("Code");
        });
        builder.Property(a => a.Vehicles)
            .HasConversion(
                b => string.Join(',', b),
                b => b.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(Guid.Parse).ToList()
             )
            .HasColumnName("Vehicles");

    }
}
