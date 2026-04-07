using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SampleCkWebApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddCodeToSaving : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Code",
                table: "Saving",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Code",
                table: "Saving");
        }
    }
}
