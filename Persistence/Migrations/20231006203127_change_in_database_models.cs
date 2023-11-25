using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OctaApi.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class change_in_database_models : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BuyFactorCode",
                table: "InventoryItemHistory");

            migrationBuilder.DropColumn(
                name: "SellerName",
                table: "InventoryItemHistory");

            migrationBuilder.DropColumn(
                name: "BuyFactorCode",
                table: "InventoryItem");

            migrationBuilder.DropColumn(
                name: "SellerName",
                table: "InventoryItem");

            migrationBuilder.AddColumn<long>(
                name: "DefaultPrice",
                table: "ServiceHistory",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "DefaultPrice",
                table: "Service",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "Price",
                table: "InvoiceService",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "TrackCode",
                table: "InvoicePayments",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Code",
                table: "Invoice",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "SerllerName",
                table: "Invoice",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Invoice",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "UseBuyPrice",
                table: "Invoice",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "InvoicePaymentHistories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    InvoicePaymentId = table.Column<Guid>(type: "uuid", nullable: false),
                    TrackCode = table.Column<string>(type: "text", nullable: true),
                    PaidAmount = table.Column<long>(type: "bigint", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoicePaymentHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InvoicePaymentHistories_InvoicePayments_InvoicePaymentId",
                        column: x => x.InvoicePaymentId,
                        principalTable: "InvoicePayments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InvoicePaymentHistories_InvoicePaymentId",
                table: "InvoicePaymentHistories",
                column: "InvoicePaymentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InvoicePaymentHistories");

            migrationBuilder.DropColumn(
                name: "DefaultPrice",
                table: "ServiceHistory");

            migrationBuilder.DropColumn(
                name: "DefaultPrice",
                table: "Service");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "InvoiceService");

            migrationBuilder.DropColumn(
                name: "TrackCode",
                table: "InvoicePayments");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "Invoice");

            migrationBuilder.DropColumn(
                name: "SerllerName",
                table: "Invoice");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Invoice");

            migrationBuilder.DropColumn(
                name: "UseBuyPrice",
                table: "Invoice");

            migrationBuilder.AddColumn<string>(
                name: "BuyFactorCode",
                table: "InventoryItemHistory",
                type: "character varying(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SellerName",
                table: "InventoryItemHistory",
                type: "character varying(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "BuyFactorCode",
                table: "InventoryItem",
                type: "character varying(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SellerName",
                table: "InventoryItem",
                type: "character varying(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");
        }
    }
}
