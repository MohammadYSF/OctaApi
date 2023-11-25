using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OctaApi.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Miscellaneouscustomerseed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "Id", "Code", "FirstName", "IsActive", "LastName", "PhoneNumber", "RegisterDate" },
                values: new object[] { new Guid("245db4b9-4aed-43e5-a02a-001202523e86"), 12345678, "مشتری متفرقه", true, "", "00000000000", new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Customer",
                keyColumn: "Id",
                keyValue: new Guid("245db4b9-4aed-43e5-a02a-001202523e86"));
        }
    }
}
