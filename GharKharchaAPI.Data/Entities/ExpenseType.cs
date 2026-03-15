using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GharKharchaAPI.Data.Entities
{
    public class ExpenseType
    {
        [Key]
        public int ExpenseTypeId { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        public string Icon { get; set; } = string.Empty;
        public string ColorCode { get; set; } = string.Empty;
        public bool IsDefault { get; set; } = true;

        // Navigation
        public ICollection<Expense> Expenses { get; set; } = new List<Expense>();
        public ICollection<Budget> Budgets { get; set; } = new List<Budget>();
    }
}
