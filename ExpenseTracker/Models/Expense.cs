
using System.ComponentModel.DataAnnotations;

namespace ExpenseTracker.Models {
    public class Expense {

        [Key]
        public int Id { get; set; }        // المعرف الفريد
        public string? Name { get; set; }   // اسم المصروف
        public double Amount { get; set; } // القيمة
        public string? Date { get; set; }   // التاريخ
        public string? Category { get; set; } // التصنيف
    }
}
