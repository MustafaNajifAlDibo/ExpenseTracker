using ExpenseTracker.Models;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Data {
    public class ExpenseEntity : IDataHelper<Expense> {

        private DBContext _dBContext;

        public ExpenseEntity() {
            _dBContext = new DBContext();
        }

        public async Task AddDataAsync(Expense table) {
            await _dBContext.Expenses.AddAsync(table);
            await _dBContext.SaveChangesAsync();
        }

        public async Task<Expense> FindAsync(int Id) {
            return await _dBContext.Expenses.FindAsync(Id);
        }

        public async Task<List<Expense>> GetAllAsync() {
            return await _dBContext.Expenses.ToListAsync();
        }

        public async Task RemoveDataAsync(Expense table) {
            _dBContext.Expenses.Remove(table);
            await _dBContext.SaveChangesAsync();
        }

        public async Task RemoveDataAtAsync(int Id) {
            Expense? expense = await FindAsync(Id);
            await RemoveDataAsync(expense);
        }

        public async Task UpdateDataAsync(Expense table) {
            // Check if entity is already tracked
            var tracked = _dBContext.ChangeTracker
                                    .Entries<Expense>()
                                    .FirstOrDefault(e => e.Entity.Id == table.Id);

            if (tracked != null) {
                // Entity is tracked — update values directly
                tracked.CurrentValues.SetValues(table);
            } else {
                // Entity is not tracked — attach and mark as modified
                _dBContext.Expenses.Update(table);
            }
            await _dBContext.SaveChangesAsync();
        }
    }
}
