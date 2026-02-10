using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SampleCkWebApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeAllTimeAmountToNotBeNull : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "AllTimeAmount",
                table: "Categories",
                type: "numeric",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "numeric",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "AllTimeAmount",
                table: "Categories",
                type: "numeric",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric");
        }
    }
}
