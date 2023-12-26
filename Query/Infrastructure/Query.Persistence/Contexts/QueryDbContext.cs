using Application.ReadModels;
using Microsoft.EntityFrameworkCore;

namespace Query.Persistence.Contexts;
public class QueryDbContext : DbContext
{
    public QueryDbContext(DbContextOptions options) : base(options)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }
    public virtual DbSet<CustomerRM> CustomerRMs { get; set; }
    public virtual DbSet<ServiceRM> ServiceRMs { get; set; }
    public virtual DbSet<InventoryItemRM> InventoryItemRMs { get; set; }
    public virtual DbSet<VehicleRM> VehicleRMs { get; set; }
    public virtual DbSet<CustomerVehicleRM> CustomerVehicleRMs{ get; set; }
    public virtual DbSet<SellInvoiceRM> SellInvoiceRMs { get; set; }
    public virtual DbSet<SellInvoiceServiceRM> SellInvoiceServiceRMs { get; set; }
    public virtual DbSet<SellInvoiceInventoryItemRM> SellInvoiceInventoryItemRMs { get; set; }
    public virtual DbSet<BuyInvoiceRM> BuyInvoiceRMs { get; set; }
}
