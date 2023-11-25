using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OctaApi.Domain.Models;

namespace OctaApi.Persistence.Configs;

public class VehicleConfig : IEntityTypeConfiguration<Vehicle>
{
    public void Configure(EntityTypeBuilder<Vehicle> builder)
    {
        builder.ToTable("Vehicle");
        builder.HasKey(a => a.Id);
        builder.Property(a => a.Name).HasMaxLength(255).IsRequired(true);
        builder.Property(a => a.Plate).HasMaxLength(15).IsRequired(true);
        builder.Property(a => a.Color).HasMaxLength(255).IsRequired(true);
        builder.Property(a => a.Code).IsRequired(true);
        builder.Property(a => a.RegisterDate).IsRequired(true);

        builder.HasMany(a => a.VehicleHistory)
            .WithOne(a => a.Vehicle)
            .HasPrincipalKey(a => a.Id)
            .HasForeignKey(a => a.VehicleId);

        builder.HasMany(a => a.Invoices)
        .WithOne(a => a.Vehicle)
        .HasPrincipalKey(a => a.Id)
        .HasForeignKey(a => a.VehicleId).IsRequired(false);

    }
}
