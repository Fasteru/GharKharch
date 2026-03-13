using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GharKharchaAPI.Data.Entities
{
    public class ExpenseEntity
    {
        [Key]
        public int ExpenseId { get; set; }

        [Required]
        public int FamilyId { get; set; }

        [Required]
        public int ExpenseTypeId { get; set; }

        [Required]
        public int AddedByUserId { get; set; }

        public int? UpdatedByUserId { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }

        public string PaymentMode { get; set; } = string.Empty;
        public DateTime ExpenseDate { get; set; }
        public string? Notes { get; set; }
        public bool IsRecurring { get; set; } = false;
        public DateTime CreatedAt { get; set; }
        // Navigation
        public FamilyEntity Family { get; set; } = null!;
        public ExpenseTypeEntity ExpenseType { get; set; } = null!;
        public UserEntity AddedBy { get; set; } = null!;
        public UserEntity? UpdatedBy { get; set; }
    }
}
