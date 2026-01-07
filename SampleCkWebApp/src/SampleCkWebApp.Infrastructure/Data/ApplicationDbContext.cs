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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Transaction>()
                .Property(t => t.Type)
                .HasConversion<string>();
            // many-to-many key
            modelBuilder.Entity<UserSaving>()
                .HasKey(uc => new { uc.UserId, uc.SavingId });
        }
    }
}