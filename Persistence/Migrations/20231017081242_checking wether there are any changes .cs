using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OctaApi.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class checkingwetherthereareanychanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "DiscountAmount",
                table: "Invoice",
                type: "real",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "DiscountAmount",
                table: "Invoice",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(float),
                oldType: "real",
                oldNullable: true);
        }
    }
}
