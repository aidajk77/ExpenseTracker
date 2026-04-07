using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;

namespace SampleCkWebApp.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext 
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }
        public DbSet<Budget> Budgets { get; set; }
        public DbSet<UserSaving> UserSavings { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<Saving> Savings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //  Transaction Type conversion
            modelBuilder.Entity<Transaction>()
                .Property(t => t.Type)
                .HasConversion<string>();

            //  UserSaving composite key
            modelBuilder.Entity<UserSaving>()
                .HasKey(uc => new { uc.UserId, uc.SavingId });

            //  Configure cascade delete: Saving -> UserSavings
            modelBuilder.Entity<UserSaving>()
                .HasOne(us => us.Saving)
                .WithMany(s => s.UserSavings)
                .HasForeignKey(us => us.SavingId)
                .OnDelete(DeleteBehavior.Cascade);

            //  Configure cascade delete: User -> UserSavings
            modelBuilder.Entity<UserSaving>()
                .HasOne(us => us.User)
                .WithMany(u => u.UserSavings)
                .HasForeignKey(us => us.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            //  Configure cascade delete: Saving -> Transactions
            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.Saving)
                .WithMany(s => s.Transactions)
                .HasForeignKey(t => t.SavingId)
                .OnDelete(DeleteBehavior.Cascade);

            //  Configure cascade delete: User -> Transactions
            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.User)
                .WithMany(u => u.Transactions)
                .HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            //  Configure cascade delete: Category -> Budgets 
            modelBuilder.Entity<Budget>()
                .HasOne(b => b.Category)
                .WithMany(c => c.Budgets)
                .HasForeignKey(b => b.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            //  Configure cascade delete: User -> Categories
            modelBuilder.Entity<Category>()
                .HasOne(c => c.User)
                .WithMany(u => u.Categories)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}