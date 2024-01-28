using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Query.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class updatevehiclereadmodel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CustomerCode",
                table: "VehicleRMs",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "CustomerId",
                table: "VehicleRMs",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "CustomerName",
                table: "VehicleRMs",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustomerCode",
                table: "VehicleRMs");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "VehicleRMs");

            migrationBuilder.DropColumn(
                name: "CustomerName",
                table: "VehicleRMs");
        }
    }
}
