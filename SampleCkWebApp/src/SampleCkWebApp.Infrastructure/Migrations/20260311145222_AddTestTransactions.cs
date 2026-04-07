using Microsoft.EntityFrameworkCore.Migrations;
using SampleCkWebApp.Infrastructure.Data.DataSeeding;

#nullable disable

namespace SampleCkWebApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddTestTransactions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var testTransactions = TransactionSeeder.GenerateTestTransactions(userId: 1, count: 10000);
            
            foreach (var transaction in testTransactions)
            {
                migrationBuilder.InsertData(
                    table: "Transactions",
                    columns: new[] { "UserId", "Type", "Amount", "CategoryId", "SavingId", "PaymentMethodId", "Date", "Description", "CreatedAt" },
                    values: new object[] { 
                        transaction.UserId,
                        transaction.Type.ToString(),
                        transaction.Amount,
                        transaction.CategoryId,
                        transaction.SavingId,
                        transaction.PaymentMethodId,
                        transaction.Date,
                        transaction.Description,
                        transaction.CreatedAt
                    });
            }

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
              migrationBuilder.Sql(@"
                DELETE FROM Transactions 
                WHERE Description LIKE 'Test transaction%';
            ");
        }
    }
}
