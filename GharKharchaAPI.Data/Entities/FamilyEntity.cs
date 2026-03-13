

using System.ComponentModel.DataAnnotations;

namespace GharKharchaAPI.Data.Entities
{
    public class FamilyEntity
    {
        [Key]
        public int FamilyId { get; set; }

        [Required]
        public string FamilyName { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }

        // Navigation
        public ICollection<UserEntity> Members { get; set; } = new List<UserEntity>();
        public ICollection<ExpenseEntity> Expenses { get; set; } = new List<ExpenseEntity>();
        public ICollection<BudgetEntity> Budgets { get; set; } = new List<BudgetEntity>();
    }
}