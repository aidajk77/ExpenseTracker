using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SampleCkWebApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RoleRenamed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "role",
                table: "Users",
                newName: "Role");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Role",
                table: "Users",
                newName: "role");
        }
    }
}
