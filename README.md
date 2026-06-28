# ExpenseTracker

A modern cross-platform **Expense Tracker** application built with **.NET MAUI** following the **MVVM** architecture. The application allows users to record, organize, edit, and delete daily expenses while storing data locally using **SQLite** and **Entity Framework Core**.

## Preview

The application provides a clean dark-themed interface designed for quick expense management.

### Features

* Add new expenses.
* Edit existing expenses.
* Delete expenses.
* View all recorded expenses.
* Categorize expenses.
* Local data persistence using SQLite.
* MVVM architecture with CommunityToolkit.Mvvm.
* Cross-platform support with .NET MAUI.
* Modern and responsive UI.

## Screenshots

> Add screenshots of the application here.

```
screenshots/
├── home.png
├── add-expense.png
└── edit-expense.png
```

## Technologies

* .NET 10 MAUI
* C#
* MVVM Pattern
* CommunityToolkit.Mvvm
* Entity Framework Core
* SQLite
* Microsoft.Data.Sqlite
* Microcharts.Maui *(Current version)*

## Project Structure

```
ExpenseTracker
│
├── Models
│
├── ViewModels
│
├── Views
│
├── Services
│
├── Data
│
├── Platforms
│
└── Resources
```

## Application Workflow

1. Enter the expense title.
2. Enter the expense amount.
3. Select a category.
4. Click **Add Expense**.
5. The expense is stored locally in the SQLite database.
6. Expenses are displayed immediately in the list.
7. Select any expense to edit or delete it.

## Architecture

```
                UI (Views)
                     │
                 Data Binding
                     │
             ExpenseViewModel
                     │
             ExpenseService
                     │
        Entity Framework Core
                     │
                 SQLite Database
```

## Database

The application uses **SQLite** as a local database through **Entity Framework Core**.

Example Expense model:

```csharp
public class Expense
{
    public int Id { get; set; }

    public string Title { get; set; }

    public double Amount { get; set; }

    public string Category { get; set; }

    public DateTime Date { get; set; }
}
```

## Getting Started

### Prerequisites

* Visual Studio 2022 (17.14 or later)
* .NET 10 SDK
* .NET MAUI workload
* Android Emulator or Windows Machine

### Clone the repository

```bash
git clone https://github.com/<username>/ExpenseTracker.git
```

### Build

```bash
dotnet build
```

### Run

```bash
dotnet maui run
```

## Implemented Features

* ✔ Add Expense
* ✔ Edit Expense
* ✔ Delete Expense
* ✔ Load Expenses
* ✔ Local SQLite Database
* ✔ Entity Framework Core Integration
* ✔ MVVM Architecture
* ✔ Responsive Dark UI

## Planned Features

* Expense statistics.
* Charts and analytics.
* Monthly reports.
* Search expenses.
* Filter by category.
* Export data to PDF.
* Export data to Excel.
* Data backup and restore.
* Cloud synchronization.
* Budget planning.
* Currency selection.
* Light/Dark theme switching.

## Dependencies

* CommunityToolkit.Mvvm
* Microsoft.EntityFrameworkCore
* Microsoft.EntityFrameworkCore.Sqlite
* Microsoft.Maui.Controls
* SQLitePCLRaw.bundle_e_sqlite3
* Microcharts.Maui

## Learning Objectives

This project demonstrates:

* Building cross-platform applications using .NET MAUI.
* Applying the MVVM architectural pattern.
* Implementing CRUD operations.
* Working with SQLite using Entity Framework Core.
* Using Commands and Data Binding.
* Managing local application data.
* Creating reusable UI components.

## License

This project is created for educational purposes and to practice building modern cross-platform applications using .NET MAUI.
