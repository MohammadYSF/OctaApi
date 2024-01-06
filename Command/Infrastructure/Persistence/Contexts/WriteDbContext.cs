using Command.Core.Domain.Customer;
using Command.Core.Domain.InventoryItem;
using Command.Core.Domain.Invoice;
using Command.Core.Domain.SellInvoice;
using Command.Core.Domain.Service;
using Command.Core.Domain.Vehicle;
using Command.Infrastructure.Persistence.Configs.BuyInvoice;
using Command.Infrastructure.Persistence.Configs.Customer;
using Command.Infrastructure.Persistence.Configs.InventoryItem;
using Command.Infrastructure.Persistence.Configs.SellInvoice;
using Command.Infrastructure.Persistence.Configs.Service;
using Command.Infrastructure.Persistence.Configs.Vehicle;
using Microsoft.EntityFrameworkCore;
namespace Command.Infrastructure.Persistence.Contexts;
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
