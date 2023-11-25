using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OctaApi.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class someforgottendesignchanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "Count",
                table: "InvoiceInventoryItem",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<DateTime>(
                name: "RegisterDate",
                table: "InvoiceInventoryItem",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Count",
                table: "InvoiceInventoryItem");

            migrationBuilder.DropColumn(
                name: "RegisterDate",
                table: "InvoiceInventoryItem");
        }
    }
}
