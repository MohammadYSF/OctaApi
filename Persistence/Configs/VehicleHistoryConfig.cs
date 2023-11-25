using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OctaApi.Domain.Models;

namespace OctaApi.Persistence.Configs;

public class VehicleHistoryConfig : IEntityTypeConfiguration<VehicleHistory>
{
    public void Configure(EntityTypeBuilder<VehicleHistory> builder)
    {
        builder.ToTable("VehicleHistory");
        builder.HasKey(a => a.Id);
        builder.Property(a => a.VehicleId).IsRequired(true);

        builder.Property(a => a.Name).HasMaxLength(255).IsRequired(true);
        builder.Property(a => a.Plate).HasMaxLength(15).IsRequired(true);
        builder.Property(a => a.Color).HasMaxLength(255).IsRequired(true);
        builder.Property(a => a.Code).IsRequired(true);
        builder.Property(a => a.UpdateDate).IsRequired(true);
    }
}