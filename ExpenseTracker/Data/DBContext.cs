using ExpenseTracker.Models;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Data {
    public class DBContext : DbContext {

        // Add Tables
        public DbSet<Expense> Expenses { get; set; }

        // Connection
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {

            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "AppDBExpenseTracker.db");
            optionsBuilder.UseSqlite($"Data Source={dbPath}");
        }

        public async Task SetupDatabase() {
            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "AppDBExpenseTracker.db");

            // إذا لم يكن الملف موجوداً، ننسخه من الـ Resources
            if (!File.Exists(dbPath)) {
                using var stream = await FileSystem.OpenAppPackageFileAsync("AppDBExpenseTracker.db");
                using var fileStream = File.Create(dbPath);
                await stream.CopyToAsync(fileStream);
            }
        }
    }
}
