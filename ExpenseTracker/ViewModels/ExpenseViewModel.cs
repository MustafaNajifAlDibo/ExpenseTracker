using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ExpenseTracker.Data;
using ExpenseTracker.Models;
using System.Collections.ObjectModel;

namespace ExpenseTracker.ViewModels {
    public partial class ExpenseViewModel : ObservableObject {

        private readonly ExpenseEntity expenseEntity;


        [ObservableProperty]
        public partial string? ExpenseName {  get; set; }

        [ObservableProperty]
        public partial double ExpenseAmount {  get; set; }

        [ObservableProperty]
        public partial string? ExpenseDate {  get; set; }

        [ObservableProperty]
        public partial string? ExpenseCategory {  get; set; }


        [ObservableProperty]
        public partial Expense? SelectedExpense { get; set; }

        [ObservableProperty]
        public partial ObservableCollection<Expense>? ExpenseCollection { get; set; }

        public ExpenseViewModel() {
            ExpenseCollection = new ObservableCollection<Expense>();
            expenseEntity = new ExpenseEntity();
            LoadData();
        }

        [RelayCommand]
        private async Task<bool> EditExpense() {

            if (string.IsNullOrEmpty(ExpenseName)
                || string.IsNullOrEmpty(ExpenseCategory)
                || ExpenseAmount <= 0) {
                await Shell.Current.DisplayAlertAsync("Warning", "Messing one or two text", "OK");
                return false;
            }
            if (ExpenseCollection != null) {

                if (SelectedExpense != null) {

                    // Set new Expense
                    var newExpense = new Expense() {
                        Id = SelectedExpense.Id,
                        Name = ExpenseName,
                        Date = ExpenseDate,
                        Category = ExpenseCategory,
                        Amount = ExpenseAmount,
                    };
                    await expenseEntity.UpdateDataAsync(newExpense);
                    LoadData();

                    SelectedExpense = await expenseEntity.FindAsync(newExpense.Id);
                }
            }
            return true;
        }

        [RelayCommand]
        private async Task<bool> DeleteExpense() {

            if (SelectedExpense != null) {

                await expenseEntity.RemoveDataAsync(SelectedExpense);
                LoadData();

                // Reset Values
                ExpenseName = string.Empty;
                ExpenseAmount = 0;
                ExpenseDate = string.Empty;
                ExpenseCategory = string.Empty;
                return true;
            } else return false;
        }

        [RelayCommand]
        private async Task<bool> AddExpense() {

            if (string.IsNullOrEmpty(ExpenseName)
                ||string.IsNullOrEmpty(ExpenseCategory)
                ||ExpenseAmount <= 0) {
                await Shell.Current.DisplayAlertAsync("Warning", "Messing one or two Fields", "OK");
                return false;
            }

            // for DB Test
            var expense = new Expense {
                Name = ExpenseName,
                Date = DateTime.Now.ToString("yyyy-MM-dd"),
                Category = ExpenseCategory,
                Amount = ExpenseAmount,
            };
            await expenseEntity.AddDataAsync(expense);
            LoadData();

            // Reset Values
            ExpenseName = string.Empty;
            ExpenseAmount = 0;
            ExpenseDate = string.Empty;
            ExpenseCategory = string.Empty;

            return true;
        }


        public void SetData() {
            ExpenseName = SelectedExpense?.Name;
            ExpenseAmount = Convert.ToDouble(SelectedExpense?.Amount);
            ExpenseDate = SelectedExpense?.Date;
            ExpenseCategory = SelectedExpense?.Category;
        }

        public async void LoadData() {

            ExpenseCollection?.Clear();
            foreach (var expense in await expenseEntity.GetAllAsync()) {
                ExpenseCollection?.Add(expense);
            }
        }
    }
}
