using ExpenseTracker.Data;
using ExpenseTracker.ViewModels;
using ExpenseTracker.Views;
using Microsoft.Extensions.Logging;

namespace ExpenseTracker {
    public static class MauiProgram {
        public static MauiApp CreateMauiApp() {

            // Create Database
            DBContext dBContext = new DBContext();
            SQLitePCL.Batteries.Init();
            _= dBContext.SetupDatabase();
            dBContext.Database.EnsureCreated();

            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts => {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            builder.Services.AddSingleton<ExpensePage>();
            builder.Services.AddSingleton<ExpenseViewModel>();

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
