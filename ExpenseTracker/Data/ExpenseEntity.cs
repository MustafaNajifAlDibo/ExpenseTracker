using ExpenseTracker.Models;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Data {
    public class ExpenseEntity : IDataHelper<Expense> {

        private DBContext dBContext;

        public ExpenseEntity() {
            dBContext = new DBContext();
        }

        public async Task AddDataAsync(Expense table) {
            await dBContext.Expenses.AddAsync(table);
            await dBContext.SaveChangesAsync();
        }

        public async Task<Expense> FindAsync(int Id) {
            return await dBContext.Expenses.FindAsync(Id);
        }

        public async Task<List<Expense>> GetAllAsync() {
            return await dBContext.Expenses.ToListAsync();
        }

        public async Task RemoveDataAsync(Expense table) {
            dBContext.Expenses.Remove(table);
            await dBContext.SaveChangesAsync();
        }

        public async Task UpdateDataAsync(Expense table) {
            dBContext = new DBContext();
            dBContext.Expenses.Update(table);
            await dBContext.SaveChangesAsync();
        }
    }
}
