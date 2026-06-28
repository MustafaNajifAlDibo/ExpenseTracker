using ExpenseTracker.Models;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Data {
    public class DBContext : DbContext {

        // Add Tables
        public DbSet<Expense> Expenses { get; set; }

        // Connection
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {

            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "AppDBExpeneTracker.db");
            optionsBuilder.UseSqlite($"Data Source={dbPath}");
        }
    }
}
