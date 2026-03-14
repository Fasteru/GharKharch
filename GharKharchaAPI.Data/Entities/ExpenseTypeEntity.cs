using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GharKharchaAPI.Data.Entities
{
    public class ExpenseTypeEntity
    {
        [Key]
        public int ExpenseTypeId { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        public string Icon { get; set; } = string.Empty;
        public string ColorCode { get; set; } = string.Empty;
        public bool IsDefault { get; set; } = true;

        // Navigation
        public ICollection<ExpenseEntity> Expenses { get; set; } = new List<ExpenseEntity>();
        public ICollection<BudgetEntity> Budgets { get; set; } = new List<BudgetEntity>();
    }
}
