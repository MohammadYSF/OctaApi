using Domain.Customer;
using Domain.Invoice;
using Domain.SellInvoice;
using Domain.Service;
using Domain.Vehicle;
using Microsoft.EntityFrameworkCore;
using OctaApi.Domain.InventoryItem;
using Persistence.Configs.BuyInvoice;
using Persistence.Configs.Customer;
using Persistence.Configs.InventoryItem;
using Persistence.Configs.SellInvoice;
using Persistence.Configs.Service;
using Persistence.Configs.Vehicle;
namespace OctaApi.Persistence.Contexts;
public class WriteDbContext : DbContext
{
    public WriteDbContext(DbContextOptions options) : base(options)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }
    public virtual DbSet<CustomerAggregate> Customers { get; set; }
    public virtual DbSet<VehicleAggregate> Vehicles { get; set; }
    public virtual DbSet<InventoryItemَAggregate> InventoryItems { get; set; }
    public virtual DbSet<ServiceAggregate> Services { get; set; }
    public virtual DbSet<BuyInvoiceAggregate> BuyInvoices { get; set; }
    public virtual DbSet<SellInvoiceAggregate> SellInvoices { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new CustomerAggregateConfig());
        modelBuilder.ApplyConfiguration(new VehicleAggregateConfig());
        modelBuilder.ApplyConfiguration(new InventoryItemAggregateConfig());
        modelBuilder.ApplyConfiguration(new ServiceAggregateConfig());
        modelBuilder.ApplyConfiguration(new BuyInvoiceAggregateConfig());
        modelBuilder.ApplyConfiguration(new SellInvoiceAggregateConfig());
    }
}
