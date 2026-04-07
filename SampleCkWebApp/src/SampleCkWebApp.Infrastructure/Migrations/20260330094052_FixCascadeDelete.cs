using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SampleCkWebApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixCascadeDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Saving_SavingId",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_UserSavings_Saving_SavingId",
                table: "UserSavings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Saving",
                table: "Saving");

            migrationBuilder.RenameTable(
                name: "Saving",
                newName: "Savings");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Savings",
                table: "Savings",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Savings_SavingId",
                table: "Transactions",
                column: "SavingId",
                principalTable: "Savings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserSavings_Savings_SavingId",
                table: "UserSavings",
                column: "SavingId",
                principalTable: "Savings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Savings_SavingId",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_UserSavings_Savings_SavingId",
                table: "UserSavings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Savings",
                table: "Savings");

            migrationBuilder.RenameTable(
                name: "Savings",
                newName: "Saving");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Saving",
                table: "Saving",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Saving_SavingId",
                table: "Transactions",
                column: "SavingId",
                principalTable: "Saving",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserSavings_Saving_SavingId",
                table: "UserSavings",
                column: "SavingId",
                principalTable: "Saving",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
