using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SampleCkWebApp.Infrastructure.Data.DataSeeding
{
    public class TransactionSeeder
    {
        public static List<Transaction> GenerateTestTransactions(int userId, int count = 10000)
        {
            var transactions = new List<Transaction>();
            var random = new Random(42); // Fixed seed for reproducibility
            var categoryIds = new[] { 18, 19, 20, 21, 22 }; // Your category IDs
            var paymentMethodIds = new[] { 1 }; // Your payment method IDs
            var savingIds = new[] { 6, 7 };

            for (int i = 0; i < count; i++)
            {
                // Distribute transactions across last 2 years
                var daysAgo = random.Next(0, 730);
                var transactionDate = DateTime.UtcNow.AddDays(-daysAgo);
                
                var type = (TransactionType)(i % 3); // 0=Income, 1=Expense, 2=Saving

                // ✅ FIXED: Different logic based on transaction type
                int? categoryId = null;
                int? savingId = null;

                if (type == TransactionType.SAVING)
                {
                    // ✅ SAVING type: has SavingId, NO CategoryId
                    savingId = savingIds[random.Next(savingIds.Length)];
                    categoryId = null;
                }
                else
                {
                    // ✅ INCOME & EXPENSE types: have CategoryId, NO SavingId
                    categoryId = categoryIds[random.Next(categoryIds.Length)];
                    savingId = null;
                }

                transactions.Add(new Transaction
                {
                    UserId = userId,
                    Type = type,
                    Amount = (decimal)(random.NextDouble() * 1000 + 10), // 10-1010
                    CategoryId = categoryId,           // ✅ Only for INCOME/EXPENSE
                    SavingId = savingId,               // ✅ Only for SAVING
                    PaymentMethodId = paymentMethodIds[random.Next(paymentMethodIds.Length)],
                    Date = transactionDate,
                    Description = $"Test transaction {i + 1}",
                    CreatedAt = transactionDate
                });
            }

            return transactions;
        }
    }
}