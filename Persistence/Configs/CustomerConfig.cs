using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OctaApi.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OctaApi.Persistence.Configs;
public class CustomerConfig : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.ToTable("Customer");
        builder.HasKey(a => a.Id);
        builder.Property(a => a.FirstName).HasMaxLength(255).IsRequired(true);
        builder.Property(a => a.LastName).HasMaxLength(255).IsRequired(true);
        builder.Property(a => a.PhoneNumber).HasMaxLength(255).IsRequired(true);
        builder.Property(a => a.Code).IsRequired(true);
        builder.Property(a => a.RegisterDate).IsRequired(true);

        builder.HasMany(a => a.Invoices)
            .WithOne(a => a.Customer)
            .HasPrincipalKey(a => a.Id)
            .HasForeignKey(a => a.CustomerId).IsRequired(false);
        
        builder.HasMany(a => a.CustomerHistories)
            .WithOne(a => a.Customer)            
            .HasPrincipalKey(a => a.Id)            
            .HasForeignKey(a => a.CustomerId);

        builder.HasData(new Customer
        {
            Code = 12345678,
            Id = Guid.Parse("245db4b9-4aed-43e5-a02a-001202523e86"),
            FirstName = "مشتری متفرقه",
            LastName = "",
            IsActive = true,
            PhoneNumber = "00000000000",
            RegisterDate = DateTime.Parse("2023-01-01")
        });
    }
}
