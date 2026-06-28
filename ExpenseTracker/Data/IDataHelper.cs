
namespace ExpenseTracker.Data {
    public interface IDataHelper<Table> {

        Task<List<Table>> GetAllAsync();
        Task<Table> FindAsync(int Id);
        Task AddDataAsync(Table table);
        Task UpdateDataAsync(Table table);
        Task RemoveDataAsync(Table table);
    }
}
