using ExpenseTracker.Models;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Expense> Expenses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Expense>().HasData(
                new Expense { Id = 1, Amount = 230, Category = "Electronics", Description = "Motherboard" },
                new Expense { Id = 2, Amount = 410, Category = "Electronics", Description = "CPU" },
                new Expense { Id = 3, Amount = 40, Category = "Peripherals", Description = "Keyboard" }
                );
        }
    }
}
