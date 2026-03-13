using System;
using System.Collections.Generic;
using System.Text;

namespace GharKharchaAPI.Domain.Models
{
    public class Budget
    {
        public int BudgetId { get; set; }
        public int FamilyId { get; set; }
        public int? ExpenseTypeId { get; set; } // nullable = overall budget
        public string MonthYear { get; set; } = string.Empty; // "2026-03"
        public decimal LimitAmount { get; set; }
        public DateTime CreatedAt { get; set; }

        // Navigation
        public Family Family { get; set; } = null!;
        public ExpenseType? ExpenseType { get; set; }
    }
}
