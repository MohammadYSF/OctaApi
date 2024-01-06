using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Query.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class initreaddb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BuyInvoiceRMs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    BuyInvoiceId = table.Column<Guid>(type: "uuid", nullable: false),
                    BuyInvoiceCode = table.Column<string>(type: "text", nullable: false),
                    BuyInvoiceCreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    TotalPrice = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BuyInvoiceRMs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CustomerRMs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uuid", nullable: false),
                    CustomerFirstName = table.Column<string>(type: "text", nullable: true),
                    CustomerLastName = table.Column<string>(type: "text", nullable: true),
                    CustomerCode = table.Column<string>(type: "text", nullable: true),
                    CustomerPhoneNumber = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerRMs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CustomerVehicleRMs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uuid", nullable: false),
                    VehicleId = table.Column<Guid>(type: "uuid", nullable: false),
                    VehicleName = table.Column<string>(type: "text", nullable: true),
                    VehicleCode = table.Column<string>(type: "text", nullable: true),
                    VehiclePlate = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerVehicleRMs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InventoryItemRMs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    InventoryItemId = table.Column<Guid>(type: "uuid", nullable: false),
                    InventoryItemCode = table.Column<string>(type: "text", nullable: true),
                    InventoryItemName = table.Column<string>(type: "text", nullable: true),
                    InventoryItemCount = table.Column<float>(type: "real", nullable: false),
                    InventoryItemBuyPrice = table.Column<long>(type: "bigint", nullable: false),
                    InventoryItemSellPrice = table.Column<long>(type: "bigint", nullable: false),
                    FromDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ToDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryItemRMs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SellInvoiceDescriptionRMs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SellInvoiceId = table.Column<Guid>(type: "uuid", nullable: false),
                    Kilometer1 = table.Column<long>(type: "bigint", nullable: false),
                    Kilometer2 = table.Column<long>(type: "bigint", nullable: false),
                    Kilometer3 = table.Column<long>(type: "bigint", nullable: false),
                    Kilometer4 = table.Column<long>(type: "bigint", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SellInvoiceDescriptionRMs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SellInvoiceInventoryItemRMs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SellInvoiceId = table.Column<Guid>(type: "uuid", nullable: false),
                    InventoryItemId = table.Column<Guid>(type: "uuid", nullable: false),
                    InventoryItemCode = table.Column<string>(type: "text", nullable: true),
                    Count = table.Column<float>(type: "real", nullable: false),
                    BuyPrice = table.Column<long>(type: "bigint", nullable: false),
                    SellPrice = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SellInvoiceInventoryItemRMs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SellInvoicePaymentRMs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SellInvoiceId = table.Column<Guid>(type: "uuid", nullable: false),
                    PaidAmount = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SellInvoicePaymentRMs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SellInvoiceRMs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SellInvoiceId = table.Column<Guid>(type: "uuid", nullable: false),
                    SellInvoiceCode = table.Column<string>(type: "text", nullable: true),
                    SellInvoiceDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CustomerName = table.Column<string>(type: "text", nullable: true),
                    CustomerCode = table.Column<string>(type: "text", nullable: true),
                    VehicleName = table.Column<string>(type: "text", nullable: true),
                    VehicleCode = table.Column<string>(type: "text", nullable: true),
                    TotalPrice = table.Column<long>(type: "bigint", nullable: false),
                    TotalPriceWhenUsingBuyPrices = table.Column<long>(type: "bigint", nullable: false),
                    Tax = table.Column<long>(type: "bigint", nullable: false),
                    Discount = table.Column<long>(type: "bigint", nullable: false),
                    ToPay = table.Column<long>(type: "bigint", nullable: false),
                    ToPayWhenUsingBuyPrices = table.Column<long>(type: "bigint", nullable: false),
                    UseBuyPrice = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SellInvoiceRMs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SellInvoiceServiceRMs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SellInvoiceId = table.Column<Guid>(type: "uuid", nullable: false),
                    ServiceId = table.Column<Guid>(type: "uuid", nullable: false),
                    ServiceCode = table.Column<string>(type: "text", nullable: true),
                    DefaultPrice = table.Column<long>(type: "bigint", nullable: false),
                    Price = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SellInvoiceServiceRMs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ServiceRMs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ServiceId = table.Column<Guid>(type: "uuid", nullable: false),
                    ServiceCode = table.Column<string>(type: "text", nullable: true),
                    ServiceName = table.Column<string>(type: "text", nullable: true),
                    ServiceDefaultPrice = table.Column<long>(type: "bigint", nullable: false),
                    FromDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ToDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceRMs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VehicleRMs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    VehicleId = table.Column<Guid>(type: "uuid", nullable: false),
                    VehicleCode = table.Column<string>(type: "text", nullable: false),
                    VehicleName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleRMs", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BuyInvoiceRMs");

            migrationBuilder.DropTable(
                name: "CustomerRMs");

            migrationBuilder.DropTable(
                name: "CustomerVehicleRMs");

            migrationBuilder.DropTable(
                name: "InventoryItemRMs");

            migrationBuilder.DropTable(
                name: "SellInvoiceDescriptionRMs");

            migrationBuilder.DropTable(
                name: "SellInvoiceInventoryItemRMs");

            migrationBuilder.DropTable(
                name: "SellInvoicePaymentRMs");

            migrationBuilder.DropTable(
                name: "SellInvoiceRMs");

            migrationBuilder.DropTable(
                name: "SellInvoiceServiceRMs");

            migrationBuilder.DropTable(
                name: "ServiceRMs");

            migrationBuilder.DropTable(
                name: "VehicleRMs");
        }
    }
}
