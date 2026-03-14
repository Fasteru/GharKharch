

namespace GharKharchaAPI.Domain.Models
{
    public class Family
    {
        public int FamilyId { get; set; }
        public string FamilyName { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }

        // Navigation
        public ICollection<User> Members { get; set; } = new List<User>();
        public ICollection<Expense> Expenses { get; set; } = new List<Expense>();
        public ICollection<Budget> Budgets { get; set; } = new List<Budget>();
    }
}