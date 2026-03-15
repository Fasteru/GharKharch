using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GharKharchaAPI.Data.Entities
{
    public class RecurringExpense
    {
        [Key]
        public int RecurringId { get; set; }

        [Required]
        public int FamilyId { get; set; }

        [Required]
        public int ExpenseTypeId { get; set; }

        [Required]
        public int AddedByUserId { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }

        public string PaymentMode { get; set; } = string.Empty;
        public string Frequency { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime NextDueDate { get; set; }
        public bool IsActive { get; set; } = true;

        // Navigation
        public Family Family { get; set; } = null!;
        public ExpenseType ExpenseType { get; set; } = null!;
        public User AddedBy { get; set; } = null!;
    }
}
