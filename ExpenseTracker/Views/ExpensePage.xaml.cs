using ExpenseTracker.ViewModels;

namespace ExpenseTracker.Views;

public partial class ExpensePage : ContentPage
{
	private readonly ExpenseViewModel expenseViewModel;
	public ExpensePage(ExpenseViewModel vm)
	{
		InitializeComponent();

		BindingContext = vm;
		expenseViewModel = vm;
	}

    private void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e) {
		expenseViewModel.SetData();
    }
}