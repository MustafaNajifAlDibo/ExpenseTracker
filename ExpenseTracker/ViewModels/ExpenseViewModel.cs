using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ExpenseTracker.Data;
using ExpenseTracker.Models;
using Microcharts;
using SkiaSharp;
using System.Collections.ObjectModel;

namespace ExpenseTracker.ViewModels {
    public partial class ExpenseViewModel : ObservableObject {

        private readonly ExpenseEntity expenseEntity;


        [ObservableProperty]
        public partial string? ExpenseName { get; set; }

        [ObservableProperty]
        public partial double ExpenseAmount { get; set; }

        [ObservableProperty]
        public partial string? ExpenseDate { get; set; }

        [ObservableProperty]
        public partial string? ExpenseCategory { get; set; }

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
        private async Task EditExpense() {

            if (string.IsNullOrEmpty(ExpenseName)
                || string.IsNullOrEmpty(ExpenseCategory)
                || ExpenseAmount <= 0) {
                await Shell.Current.DisplayAlertAsync("Warning", "Messing one or two text", "OK");
                return;
            }
            if (ExpenseCollection == null) {
                await Shell.Current.DisplayAlertAsync("Warning", "No items in the list", "OK");
                return;
            }

            if (SelectedExpense == null) {
                await Shell.Current.DisplayAlertAsync("Warning", "Messing item selection", "OK");
                return;
            }
            // Set new Expense
            var newExpense = new Expense() {
                Id = SelectedExpense.Id,
                Name = ExpenseName,
                Date = SelectedExpense.Date,
                Category = ExpenseCategory,
                Amount = ExpenseAmount,
            };
            await expenseEntity.UpdateDataAsync(newExpense);
            LoadData();

            SelectedExpense = await expenseEntity.FindAsync(newExpense.Id);

        }

        [RelayCommand]
        private async Task DeleteExpense() {

            if (SelectedExpense == null) {
                await Shell.Current.DisplayAlertAsync("Warning", "Messing item selection", "OK");
                return;
            }
            await expenseEntity.RemoveDataAsync(SelectedExpense);
            LoadData();

            // Reset Values
            ResetItemValues();
        }

        [RelayCommand]
        private async Task AddExpense() {

            if (string.IsNullOrEmpty(ExpenseName)
                || string.IsNullOrEmpty(ExpenseCategory)
                || ExpenseAmount <= 0) {
                await Shell.Current.DisplayAlertAsync("Warning", "Messing one or two Fields", "OK");
                return ;
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
            ResetItemValues();
        }


        public void SetData() {
            ExpenseName = SelectedExpense?.Name;
            ExpenseAmount = Convert.ToDouble(SelectedExpense?.Amount);
            ExpenseCategory = SelectedExpense?.Category;
        }

        public async void LoadData() {

            ExpenseCollection?.Clear();
            foreach (var expense in await expenseEntity.GetAllAsync()) {
                ExpenseCollection?.Add(expense);
            }
        }

        public List<ChartEntry> GetCategoryCharts() {

            // داخل دالة GetCategoryCharts في ViewModel
            string[] colors = {
        "#7C6AF7", // بنفسجي — اللون الأساسي للثيم
        "#5AACF5", // أزرق فاتح
        "#4CB87A", // أخضر
        "#F5706A", // أحمر/كورال
        "#F5A623", // برتقالي
        "#A78BFA", // بنفسجي فاتح
        "#F472B6", // وردي
        "#34D399", // أخضر فاتح
        "#60A5FA"  // أزرق
            };
            int i = 0;

            var groupedData = ExpenseCollection?
                .GroupBy(e => e.Category)
                .Select(g => new ChartEntry((float)g.Sum(e => e.Amount)) {
                    Label = g.Key,
                    ValueLabel = g.Sum(e => e.Amount).ToString("C"),
                    Color = SKColor.Parse(colors[i++ % colors.Length])
                })
                .ToList();

            return groupedData;
        }

        private void ResetItemValues() {
            ExpenseName = string.Empty;
            ExpenseAmount = 0;
            ExpenseDate = string.Empty;
            ExpenseCategory = string.Empty;
        }
    }
}
