using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Query.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class updatereadmodelss : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ServiceName",
                table: "SellInvoiceServiceRMs",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ServiceName",
                table: "SellInvoiceServiceRMs");
        }
    }
}
