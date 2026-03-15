using System;
using System.Collections.Generic;
using System.Text;

namespace GharKharchaAPI.Domain.Models.DTO
{
    // Add new expense
    public class AddExpenseDto
    {
        public int FamilyId { get; set; }
        public int UserId { get; set; }
        public int ExpenseTypeId { get; set; }
        public string Title { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public string PaymentMode { get; set; } = string.Empty;
        public DateTime ExpenseDate { get; set; }
        public string? Notes { get; set; }
        public bool IsRecurring { get; set; }
    }

    // Update existing expense
    public class UpdateExpenseDto
    {
        public int FamilyId { get; set; }
        public int UserId { get; set; }
        public int ExpenseTypeId { get; set; }
        public string Title { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public string PaymentMode { get; set; } = string.Empty;
        public DateTime ExpenseDate { get; set; }
        public string? Notes { get; set; }
        public bool IsRecurring { get; set; }
    }

    // Return expense data
    public class ExpenseResponseDto
    {
        public int ExpenseId { get; set; }
        public string Title { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public string PaymentMode { get; set; } = string.Empty;
        public DateTime ExpenseDate { get; set; }
        public string? Notes { get; set; }
        public bool IsRecurring { get; set; }
        public DateTime CreatedAt { get; set; }
        public string ExpenseTypeName { get; set; } = string.Empty;
        public string ExpenseTypeIcon { get; set; } = string.Empty;
        public string ExpenseTypeColor { get; set; } = string.Empty;
        public string AddedByName { get; set; } = string.Empty;
        public string? UpdatedByName { get; set; }
    }
}
