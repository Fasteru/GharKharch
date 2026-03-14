using System;
using System.Collections.Generic;
using System.Text;

namespace GharKharchaAPI.Domain.Models
{
    public class Expense
    {
        public int ExpenseId { get; set; }
        public int FamilyId { get; set; }
        public int ExpenseTypeId { get; set; }
        public int AddedByUserId { get; set; }
        public int? UpdatedByUserId { get; set; }
        public string Title { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public string PaymentMode { get; set; } = string.Empty;
        public DateTime ExpenseDate { get; set; }
        public string? Notes { get; set; }
        public bool IsRecurring { get; set; } = false;
        public DateTime CreatedAt { get; set; }

        // Navigation
        public Family Family { get; set; } = null!;
        public ExpenseType ExpenseType { get; set; } = null!;
        public User AddedBy { get; set; } = null!;
        public User? UpdatedBy { get; set; }
    }
}
