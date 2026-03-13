using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GharKharchaAPI.Data.Entities
{
    public class BudgetEntity
    {
        [Key]
        public int BudgetId { get; set; }

        [Required]
        public int FamilyId { get; set; }

        public int? ExpenseTypeId { get; set; }

        [Required]
        public string MonthYear { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18,2)")]
        public decimal LimitAmount { get; set; }

        public DateTime CreatedAt { get; set; }

        // Navigation
        public FamilyEntity Family { get; set; } = null!;
        public ExpenseTypeEntity? ExpenseType { get; set; }
    }
}
