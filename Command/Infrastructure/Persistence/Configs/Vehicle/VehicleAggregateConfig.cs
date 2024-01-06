using Command.Core.Domain.Vehicle;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Command.Infrastructure.Persistence.Configs.Vehicle;

public class VehicleAggregateConfig : IEntityTypeConfiguration<VehicleAggregate>
{
    public void Configure(EntityTypeBuilder<VehicleAggregate> builder)
    {
        builder.ToTable("Vehicle");
        builder.HasKey(a => a.Id);
        builder.OwnsOne(a => a.Name, b =>
        {
            b.Property(c => c.Value).HasMaxLength(255).IsRequired(true).HasColumnName("Name");
        });
        builder.OwnsOne(a => a.Plate, b =>
        {
            b.Property(c => c.Value).HasMaxLength(255).IsRequired(true).HasColumnName("Plate");
        });
        builder.OwnsOne(a => a.Code, b =>
        {
            b.Property(c => c.Value).HasMaxLength(255).IsRequired(true).HasColumnName("Code");
        });
        builder.OwnsOne(a => a.Color, b =>
        {
            b.Property(c => c.Value).IsRequired(true).HasColumnName("Color");
        });
    }
}
