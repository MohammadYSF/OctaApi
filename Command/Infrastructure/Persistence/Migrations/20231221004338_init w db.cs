using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Command.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class initwdb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BuyInvoice",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    BuyDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Code = table.Column<int>(type: "integer", nullable: false),
                    SellerName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BuyInvoice", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Code = table.Column<int>(type: "integer", nullable: false),
                    FirstName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    LastName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    PhoneNumber = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Vehicles = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InventoryItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    BuyPriceHistory = table.Column<string>(type: "text", nullable: false),
                    SellPriceHistory = table.Column<string>(type: "text", nullable: false),
                    Code = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    BuyPrice = table.Column<long>(type: "bigint", nullable: true),
                    SellPrice = table.Column<long>(type: "bigint", nullable: true),
                    Count = table.Column<float>(type: "real", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    RegisterDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CountLowerBound = table.Column<float>(type: "real", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryItems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SellInvoices",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Code = table.Column<int>(type: "integer", nullable: false),
                    Customer = table.Column<Guid>(type: "uuid", nullable: true),
                    Vehicle = table.Column<Guid>(type: "uuid", nullable: true),
                    UseBuyPrice = table.Column<bool>(type: "boolean", nullable: false),
                    Discount = table.Column<long>(type: "bigint", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SellInvoices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Services",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Code = table.Column<int>(type: "integer", nullable: false),
                    DefaultPrice = table.Column<long>(type: "bigint", nullable: true),
                    DefaultPricecHistory = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vehicle",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Code = table.Column<int>(type: "integer", maxLength: 255, nullable: false),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Plate = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Color = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicle", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BuyInvoicecInventoryItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SellInvoiceId = table.Column<Guid>(type: "uuid", nullable: false),
                    InventoryItemId = table.Column<Guid>(type: "uuid", nullable: false),
                    Count = table.Column<float>(type: "real", nullable: false),
                    BuyInvoiceId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BuyInvoicecInventoryItems", x => new { x.Id, x.SellInvoiceId });
                    table.ForeignKey(
                        name: "FK_BuyInvoicecInventoryItems_BuyInvoice_BuyInvoiceId",
                        column: x => x.BuyInvoiceId,
                        principalTable: "BuyInvoice",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SellInvoicecInventoryItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SellInvoiceId = table.Column<Guid>(type: "uuid", nullable: false),
                    InventoryItemId = table.Column<Guid>(type: "uuid", nullable: false),
                    Count = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SellInvoicecInventoryItems", x => new { x.Id, x.SellInvoiceId });
                    table.ForeignKey(
                        name: "FK_SellInvoicecInventoryItems_SellInvoices_SellInvoiceId",
                        column: x => x.SellInvoiceId,
                        principalTable: "SellInvoices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SellInvoicePayments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SellInvoiceId = table.Column<Guid>(type: "uuid", nullable: false),
                    PaidDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    PaymentTrackCode = table.Column<string>(type: "text", nullable: false),
                    PaidAmount = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SellInvoicePayments", x => new { x.Id, x.SellInvoiceId });
                    table.ForeignKey(
                        name: "FK_SellInvoicePayments_SellInvoices_SellInvoiceId",
                        column: x => x.SellInvoiceId,
                        principalTable: "SellInvoices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SellInvoiceServices",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SellInvoiceId = table.Column<Guid>(type: "uuid", nullable: false),
                    ServiceId = table.Column<Guid>(type: "uuid", nullable: false),
                    ServicePrice = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SellInvoiceServices", x => new { x.Id, x.SellInvoiceId });
                    table.ForeignKey(
                        name: "FK_SellInvoiceServices_SellInvoices_SellInvoiceId",
                        column: x => x.SellInvoiceId,
                        principalTable: "SellInvoices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BuyInvoicecInventoryItems_BuyInvoiceId",
                table: "BuyInvoicecInventoryItems",
                column: "BuyInvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_SellInvoicecInventoryItems_SellInvoiceId",
                table: "SellInvoicecInventoryItems",
                column: "SellInvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_SellInvoicePayments_SellInvoiceId",
                table: "SellInvoicePayments",
                column: "SellInvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_SellInvoiceServices_SellInvoiceId",
                table: "SellInvoiceServices",
                column: "SellInvoiceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BuyInvoicecInventoryItems");

            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropTable(
                name: "InventoryItems");

            migrationBuilder.DropTable(
                name: "SellInvoicecInventoryItems");

            migrationBuilder.DropTable(
                name: "SellInvoicePayments");

            migrationBuilder.DropTable(
                name: "SellInvoiceServices");

            migrationBuilder.DropTable(
                name: "Services");

            migrationBuilder.DropTable(
                name: "Vehicle");

            migrationBuilder.DropTable(
                name: "BuyInvoice");

            migrationBuilder.DropTable(
                name: "SellInvoices");
        }
    }
}
