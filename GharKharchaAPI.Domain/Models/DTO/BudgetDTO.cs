using System;
using System.Collections.Generic;
using System.Text;

namespace GharKharchaAPI.Domain.Models.DTO
{
    // Add/Update budget
    public class AddBudgetDto
    {
        public int? ExpenseTypeId { get; set; }
        public string MonthYear { get; set; } = string.Empty;
        public decimal LimitAmount { get; set; }
    }

    // Return budget data
    public class BudgetResponseDto
    {
        public int BudgetId { get; set; }
        public string MonthYear { get; set; } = string.Empty;
        public decimal LimitAmount { get; set; }
        public decimal SpentAmount { get; set; }
        public decimal RemainingAmount { get; set; }
        public int PercentageUsed { get; set; }
        public string? ExpenseTypeName { get; set; }
        public string? ExpenseTypeIcon { get; set; }
        public string? ExpenseTypeColor { get; set; }
    }

    // Monthly report
    public class MonthlyReportDto
    {
        public string MonthYear { get; set; } = string.Empty;
        public decimal TotalBudget { get; set; }
        public decimal TotalSpent { get; set; }
        public decimal TotalSaved { get; set; }
        public int SavingsPercentage { get; set; }
        public List<CategoryReportDto> CategoryBreakdown { get; set; } = new();
    }

    // Category wise report
    public class CategoryReportDto
    {
        public string ExpenseTypeName { get; set; } = string.Empty;
        public string ExpenseTypeIcon { get; set; } = string.Empty;
        public string ExpenseTypeColor { get; set; } = string.Empty;
        public decimal TotalSpent { get; set; }
        public decimal Budget { get; set; }
        public int PercentageUsed { get; set; }
    }
}
