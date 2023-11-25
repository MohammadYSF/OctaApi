using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OctaApi.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class changeinnamings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceService_Invoice_InvoiceId",
                table: "InvoiceService");

            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceService_Service_ServiceId",
                table: "InvoiceService");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InvoiceService",
                table: "InvoiceService");

            migrationBuilder.DropColumn(
                name: "IssueDate",
                table: "Invoice");

            migrationBuilder.RenameTable(
                name: "InvoiceService",
                newName: "InvoiceServiceItem");

            migrationBuilder.RenameIndex(
                name: "IX_InvoiceService_ServiceId",
                table: "InvoiceServiceItem",
                newName: "IX_InvoiceServiceItem_ServiceId");

            migrationBuilder.RenameIndex(
                name: "IX_InvoiceService_InvoiceId",
                table: "InvoiceServiceItem",
                newName: "IX_InvoiceServiceItem_InvoiceId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdateDate",
                table: "VehicleHistory",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "RegisterDate",
                table: "Vehicle",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdateDate",
                table: "ServiceHistory",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "RegisterDate",
                table: "Service",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastPaymentDate",
                table: "InvoicePayments",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdateDate",
                table: "InvoicePaymentHistories",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "RegisterDate",
                table: "Invoice",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateDate",
                table: "Invoice",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdateDate",
                table: "InventoryItemHistory",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "RegisterDate",
                table: "InventoryItem",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdateDate",
                table: "CustomerHistory",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "RegisterDate",
                table: "Customer",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InvoiceServiceItem",
                table: "InvoiceServiceItem",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceServiceItem_Invoice_InvoiceId",
                table: "InvoiceServiceItem",
                column: "InvoiceId",
                principalTable: "Invoice",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceServiceItem_Service_ServiceId",
                table: "InvoiceServiceItem",
                column: "ServiceId",
                principalTable: "Service",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceServiceItem_Invoice_InvoiceId",
                table: "InvoiceServiceItem");

            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceServiceItem_Service_ServiceId",
                table: "InvoiceServiceItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InvoiceServiceItem",
                table: "InvoiceServiceItem");

            migrationBuilder.DropColumn(
                name: "UpdateDate",
                table: "Invoice");

            migrationBuilder.RenameTable(
                name: "InvoiceServiceItem",
                newName: "InvoiceService");

            migrationBuilder.RenameIndex(
                name: "IX_InvoiceServiceItem_ServiceId",
                table: "InvoiceService",
                newName: "IX_InvoiceService_ServiceId");

            migrationBuilder.RenameIndex(
                name: "IX_InvoiceServiceItem_InvoiceId",
                table: "InvoiceService",
                newName: "IX_InvoiceService_InvoiceId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdateDate",
                table: "VehicleHistory",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "RegisterDate",
                table: "Vehicle",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdateDate",
                table: "ServiceHistory",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "RegisterDate",
                table: "Service",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastPaymentDate",
                table: "InvoicePayments",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdateDate",
                table: "InvoicePaymentHistories",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "RegisterDate",
                table: "Invoice",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AddColumn<DateTime>(
                name: "IssueDate",
                table: "Invoice",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdateDate",
                table: "InventoryItemHistory",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "RegisterDate",
                table: "InventoryItem",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdateDate",
                table: "CustomerHistory",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "RegisterDate",
                table: "Customer",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InvoiceService",
                table: "InvoiceService",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceService_Invoice_InvoiceId",
                table: "InvoiceService",
                column: "InvoiceId",
                principalTable: "Invoice",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceService_Service_ServiceId",
                table: "InvoiceService",
                column: "ServiceId",
                principalTable: "Service",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
