using System;
using System.Collections.Generic;
using System.Text;

namespace GharKharchaAPI.Domain.Models
{
    public class RecurringExpense
    {
        public int RecurringId { get; set; }
        public int FamilyId { get; set; }
        public int ExpenseTypeId { get; set; }
        public int AddedByUserId { get; set; }
        public string Title { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public string PaymentMode { get; set; } = string.Empty;
        public string Frequency { get; set; } = string.Empty; // Monthly/Weekly
        public DateTime StartDate { get; set; }
        public DateTime NextDueDate { get; set; }
        public bool IsActive { get; set; } = true;

        // Navigation
        public Family Family { get; set; } = null!;
        public ExpenseType ExpenseType { get; set; } = null!;
        public User AddedBy { get; set; } = null!;
    }
}
