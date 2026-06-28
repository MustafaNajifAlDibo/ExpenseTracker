using ExpenseTracker.Models;
using ExpenseTracker.ViewModels;
using Microcharts;
using SkiaSharp;

namespace ExpenseTracker.Views;

public partial class ExpensePage : ContentPage
{
	private readonly ExpenseViewModel expenseViewModel;
    private bool _isChartVisible = false;

    public ExpensePage(ExpenseViewModel vm)
	{
		InitializeComponent();

		BindingContext = vm;
		expenseViewModel = vm;

        this.Loaded += (s, e) => UpdateChart();
    }

    // في ExpensePage.xaml.cs
    protected override void OnAppearing() {
        base.OnAppearing();

        Shell.SetNavBarIsVisible(this, false);
    }

    private void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e) {
		expenseViewModel.SetData();
    }

    private async void OnFabTapped(object sender, TappedEventArgs e) {
        _isChartVisible = !_isChartVisible;

        // تحريك الـ FAB
        await FabButton.ScaleToAsync(0.85, 80);
        await FabButton.ScaleToAsync(1.0, 80);

        // تبديل الأيقونة
        FabIcon.Text = _isChartVisible ? "📋" : "📊";

        // إظهار / إخفاء
        if (_isChartVisible) {
            ListSection.IsVisible = false;
            ChartSection.IsVisible = true;
            await ChartSection.FadeToAsync(1, 200);
        } else {
            await ChartSection.FadeToAsync(0, 150);
            ChartSection.IsVisible = false;
            ListSection.IsVisible = true;
            await ListSection.FadeToAsync(1, 200);
        }
    }

    private void UpdateChart() {
        var entries = expenseViewModel.GetCategoryCharts();

        if (entries == null || entries.Count == 0)
            return;

        // 1. تحديث الرسم البياني
        ExpensesChart.Chart = new DonutChart {
            Entries = entries,
            BackgroundColor = SKColor.Parse("#1C1C26"),
            HoleRadius = 0.5f,
            LabelTextSize = 0f,
            GraphPosition = GraphPosition.Center,
            LabelMode = LabelMode.None
        };

        // 2. تحويل SKColor → MAUI Color للـ Legend
        var legendItems = entries.Select(e => new LegendItem {
            Label = e.Label,
            ValueLabel = e.ValueLabel,
            Color = Color.FromRgba(e.Color.Red, e.Color.Green, e.Color.Blue, e.Color.Alpha)
        }).ToList();

        LegendList.ItemsSource = legendItems;
    }
}