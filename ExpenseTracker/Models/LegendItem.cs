using System;
using System.Collections.Generic;
using System.Text;

namespace ExpenseTracker.Models {
    // LegendItem.cs — model جديد بسيط
    public class LegendItem {
        public string? Label { get; set; }
        public string? ValueLabel { get; set; }
        public Color? Color { get; set; } // Microsoft.Maui.Graphics.Color
    }
}
