using GharKharchaAPI.Data.Context;
using GharKharchaAPI.Data.Entities;
using GharKharchaAPI.Domain.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace GharKharchaAPI.Application.Features
{
    public class ExpenseService
    {
        private readonly AppDbContext _context;

        public ExpenseService(AppDbContext context)
        {
            _context = context;
        }

        // Get all expenses for a family
        public async Task<List<ExpenseResponseDto>> GetExpenses(
            int familyId,
            string? monthYear = null,
            int? expenseTypeId = null)
        {
            var query = _context.Expenses
                .Include(e => e.ExpenseType)
                .Include(e => e.AddedBy)
                .Include(e => e.UpdatedBy)
                .Where(e => e.FamilyId == familyId);

            // Filter by month year if provided
            if (!string.IsNullOrEmpty(monthYear))
            {
                var parts = monthYear.Split('-');
                var year = int.Parse(parts[0]);
                var month = int.Parse(parts[1]);
                query = query.Where(e =>
                    e.ExpenseDate.Year == year &&
                    e.ExpenseDate.Month == month);
            }

            // Filter by expense type if provided
            if (expenseTypeId.HasValue)
                query = query.Where(e => e.ExpenseTypeId == expenseTypeId);

            var expenses = await query
                .OrderByDescending(e => e.ExpenseDate)
                .ToListAsync();

            return expenses.Select(e => new ExpenseResponseDto
            {
                ExpenseId = e.ExpenseId,
                Title = e.Title,
                Amount = e.Amount,
                PaymentMode = e.PaymentMode,
                ExpenseDate = e.ExpenseDate,
                Notes = e.Notes,
                IsRecurring = e.IsRecurring,
                CreatedAt = e.CreatedAt,
                ExpenseTypeName = e.ExpenseType.Name,
                ExpenseTypeIcon = e.ExpenseType.Icon,
                ExpenseTypeColor = e.ExpenseType.ColorCode,
                AddedByName = e.AddedBy.Name,
                UpdatedByName = e.UpdatedBy?.Name
            }).ToList();
        }

        // Get single expense
        public async Task<ExpenseResponseDto> GetExpense(int expenseId, int familyId)
        {
            var e = await _context.Expenses
                .Include(e => e.ExpenseType)
                .Include(e => e.AddedBy)
                .Include(e => e.UpdatedBy)
                .FirstOrDefaultAsync(e =>
                    e.ExpenseId == expenseId &&
                    e.FamilyId == familyId)
                ?? throw new Exception("Expense not found!");

            return new ExpenseResponseDto
            {
                ExpenseId = e.ExpenseId,
                Title = e.Title,
                Amount = e.Amount,
                PaymentMode = e.PaymentMode,
                ExpenseDate = e.ExpenseDate,
                Notes = e.Notes,
                IsRecurring = e.IsRecurring,
                CreatedAt = e.CreatedAt,
                ExpenseTypeName = e.ExpenseType.Name,
                ExpenseTypeIcon = e.ExpenseType.Icon,
                ExpenseTypeColor = e.ExpenseType.ColorCode,
                AddedByName = e.AddedBy.Name,
                UpdatedByName = e.UpdatedBy?.Name
            };
        }

        // Add expense
        public async Task<ExpenseResponseDto> AddExpense(
            int familyId,
            int userId,
            AddExpenseDto dto)
        {
            var expense = new Expense
            {
                FamilyId = familyId,
                ExpenseTypeId = dto.ExpenseTypeId,
                AddedByUserId = userId,
                Title = dto.Title,
                Amount = dto.Amount,
                PaymentMode = dto.PaymentMode,
                ExpenseDate = dto.ExpenseDate,
                Notes = dto.Notes,
                IsRecurring = dto.IsRecurring,
                CreatedAt = DateTime.Now
            }; 
            _context.Expenses.Add(expense);
            await _context.SaveChangesAsync();

            return await GetExpense(expense.ExpenseId, familyId);
        }

        // Update expense
        public async Task<ExpenseResponseDto> UpdateExpense(
            int expenseId,
            int familyId,
            int userId,
            UpdateExpenseDto dto)
        {
            var expense = await _context.Expenses
                .FirstOrDefaultAsync(e =>
                    e.ExpenseId == expenseId &&
                    e.FamilyId == familyId)
                ?? throw new Exception("Expense not found!");

            expense.ExpenseTypeId = dto.ExpenseTypeId;
            expense.Title = dto.Title;
            expense.Amount = dto.Amount;
            expense.PaymentMode = dto.PaymentMode;
            expense.ExpenseDate = dto.ExpenseDate;
            expense.Notes = dto.Notes;
            expense.UpdatedByUserId = userId;

            await _context.SaveChangesAsync();

            return await GetExpense(expenseId, familyId);
        }

        // Delete expense
        public async Task DeleteExpense(int expenseId, int familyId)
        {
            var expense = await _context.Expenses
                .FirstOrDefaultAsync(e =>
                    e.ExpenseId == expenseId &&
                    e.FamilyId == familyId)
                ?? throw new Exception("Expense not found!");

            _context.Expenses.Remove(expense);
            await _context.SaveChangesAsync();
        }
    }
}
