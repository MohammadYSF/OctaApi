using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Query.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class updatingreadmodels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "InventoryItemName",
                table: "SellInvoiceInventoryItemRMs",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InventoryItemName",
                table: "SellInvoiceInventoryItemRMs");
        }
    }
}
