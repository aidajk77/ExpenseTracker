using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SampleCkWebApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddAllTimeAmountToCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "AllTimeAmount",
                table: "Categories",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "CurrentAmount",
                table: "Budgets",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AllTimeAmount",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "CurrentAmount",
                table: "Budgets");
        }
    }
}
