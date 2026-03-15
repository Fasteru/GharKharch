using GharKharchaAPI.Data.Context;
using GharKharchaAPI.Data.Entities;
using GharKharchaAPI.Domain.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace GharKharchaAPI.Application.Features
{
    public class BudgetService
    {
        private readonly AppDbContext _context;

        public BudgetService(AppDbContext context)
        {
            _context = context;
        }

        // Get all budgets for a family for a month
        public async Task<List<BudgetResponseDto>> GetBudgets(
            int familyId,
            string monthYear)
        {
            // Get budgets
            var budgets = await _context.Budgets
                .Include(b => b.ExpenseType)
                .Where(b =>
                    b.FamilyId == familyId &&
                    b.MonthYear == monthYear)
                .ToListAsync();

            // Get total spent per expense type for the month
            var parts = monthYear.Split('-');
            var year = int.Parse(parts[0]);
            var month = int.Parse(parts[1]);

            var expenses = await _context.Expenses
                .Where(e =>
                    e.FamilyId == familyId &&
                    e.ExpenseDate.Year == year &&
                    e.ExpenseDate.Month == month)
                .ToListAsync();

            return budgets.Select(b =>
            {
                // Calculate spent amount
                var spent = b.ExpenseTypeId.HasValue
                    ? expenses
                        .Where(e => e.ExpenseTypeId == b.ExpenseTypeId)
                        .Sum(e => e.Amount)
                    : expenses.Sum(e => e.Amount);

                var remaining = b.LimitAmount - spent;
                var percentage = b.LimitAmount > 0
                    ? (int)((spent / b.LimitAmount) * 100)
                    : 0;

                return new BudgetResponseDto
                {
                    BudgetId = b.BudgetId,
                    MonthYear = b.MonthYear,
                    LimitAmount = b.LimitAmount,
                    SpentAmount = spent,
                    RemainingAmount = remaining,
                    PercentageUsed = percentage,
                    ExpenseTypeName = b.ExpenseType?.Name,
                    ExpenseTypeIcon = b.ExpenseType?.Icon,
                    ExpenseTypeColor = b.ExpenseType?.ColorCode
                };
            }).ToList();
        }

        // Add budget
        public async Task<BudgetResponseDto> AddBudget(
    int familyId,
    AddBudgetDto dto)
        {
            Budget budget;

            // Check if budget already exists for same month + category
            var existing = await _context.Budgets
                .FirstOrDefaultAsync(b =>
                    b.FamilyId == familyId &&
                    b.MonthYear == dto.MonthYear &&
                    b.ExpenseTypeId == dto.ExpenseTypeId);

            if (existing != null)
            {
                // Update existing
                existing.LimitAmount = dto.LimitAmount;
                await _context.SaveChangesAsync();
                budget = existing;
            }
            else
            {
                // Create new
                budget = new Budget
                {
                    FamilyId = familyId,
                    ExpenseTypeId = dto.ExpenseTypeId,
                    MonthYear = dto.MonthYear,
                    LimitAmount = dto.LimitAmount,
                    CreatedAt = DateTime.Now
                };
                _context.Budgets.Add(budget);
                await _context.SaveChangesAsync();
            }

            // Calculate spent for this budget
            var parts = dto.MonthYear.Split('-');
            var year = int.Parse(parts[0]);
            var month = int.Parse(parts[1]);

            var expenses = await _context.Expenses
                .Where(e =>
                    e.FamilyId == familyId &&
                    e.ExpenseDate.Year == year &&
                    e.ExpenseDate.Month == month)
                .ToListAsync();

            var spent = budget.ExpenseTypeId.HasValue
                ? expenses
                    .Where(e => e.ExpenseTypeId == budget.ExpenseTypeId)
                    .Sum(e => e.Amount)
                : expenses.Sum(e => e.Amount);

            var remaining = budget.LimitAmount - spent;
            var percentage = budget.LimitAmount > 0
                ? (int)((spent / budget.LimitAmount) * 100)
                : 0;

            // Get expense type name if category budget
            string? expenseTypeName = null;
            string? expenseTypeIcon = null;
            string? expenseTypeColor = null;

            if (budget.ExpenseTypeId.HasValue)
            {
                var expenseType = await _context.ExpenseTypes
                    .FindAsync(budget.ExpenseTypeId);
                expenseTypeName = expenseType?.Name;
                expenseTypeIcon = expenseType?.Icon;
                expenseTypeColor = expenseType?.ColorCode;
            }

            return new BudgetResponseDto
            {
                BudgetId = budget.BudgetId,
                MonthYear = budget.MonthYear,
                LimitAmount = budget.LimitAmount,
                SpentAmount = spent,
                RemainingAmount = remaining,
                PercentageUsed = percentage,
                ExpenseTypeName = expenseTypeName,
                ExpenseTypeIcon = expenseTypeIcon,
                ExpenseTypeColor = expenseTypeColor
            };
        }

        // Delete budget
        public async Task DeleteBudget(int budgetId, int familyId)
        {
            var budget = await _context.Budgets
                .FirstOrDefaultAsync(b =>
                    b.BudgetId == budgetId &&
                    b.FamilyId == familyId)
                ?? throw new Exception("Budget not found!");

            _context.Budgets.Remove(budget);
            await _context.SaveChangesAsync();
        }

        // Get monthly report
        public async Task<MonthlyReportDto> GetMonthlyReport(
            int familyId,
            string monthYear)
        {
            var parts = monthYear.Split('-');
            var year = int.Parse(parts[0]);
            var month = int.Parse(parts[1]);

            // Get all expenses for month
            var expenses = await _context.Expenses
                .Include(e => e.ExpenseType)
                .Where(e =>
                    e.FamilyId == familyId &&
                    e.ExpenseDate.Year == year &&
                    e.ExpenseDate.Month == month)
                .ToListAsync();

            // Get overall budget
            var overallBudget = await _context.Budgets
                .FirstOrDefaultAsync(b =>
                    b.FamilyId == familyId &&
                    b.MonthYear == monthYear &&
                    b.ExpenseTypeId == null);

            var totalSpent = expenses.Sum(e => e.Amount);
            var totalBudget = overallBudget?.LimitAmount ?? 0;
            var totalSaved = totalBudget - totalSpent;
            var savingsPercentage = totalBudget > 0
                ? (int)((totalSaved / totalBudget) * 100)
                : 0;

            // Category breakdown
            var categoryBreakdown = expenses
                .GroupBy(e => e.ExpenseType)
                .Select(g =>
                {
                    var categoryBudget = _context.Budgets
                        .FirstOrDefault(b =>
                            b.FamilyId == familyId &&
                            b.MonthYear == monthYear &&
                            b.ExpenseTypeId == g.Key.ExpenseTypeId);

                    var spent = g.Sum(e => e.Amount);
                    var budget = categoryBudget?.LimitAmount ?? 0;
                    var percentage = budget > 0
                        ? (int)((spent / budget) * 100)
                        : 0;

                    return new CategoryReportDto
                    {
                        ExpenseTypeName = g.Key.Name,
                        ExpenseTypeIcon = g.Key.Icon,
                        ExpenseTypeColor = g.Key.ColorCode,
                        TotalSpent = spent,
                        Budget = budget,
                        PercentageUsed = percentage
                    };
                })
                .OrderByDescending(c => c.TotalSpent)
                .ToList();

            return new MonthlyReportDto
            {
                MonthYear = monthYear,
                TotalBudget = totalBudget,
                TotalSpent = totalSpent,
                TotalSaved = totalSaved,
                SavingsPercentage = savingsPercentage,
                CategoryBreakdown = categoryBreakdown
            };
        }
    }
}
