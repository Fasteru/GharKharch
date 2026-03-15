using GharKharchaAPI.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace GharKharchaAPI.Data.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<Family> Families { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<ExpenseType> ExpenseTypes { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<RecurringExpense> RecurringExpenses { get; set; }
        public DbSet<Budget> Budgets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Family → Members
            modelBuilder.Entity<User>()
                .HasOne(u => u.Family)
                .WithMany(f => f.Members)
                .HasForeignKey(u => u.FamilyId)
                .OnDelete(DeleteBehavior.Restrict);

            // Expense → Family
            modelBuilder.Entity<Expense>()
                .HasOne(e => e.Family)
                .WithMany(f => f.Expenses)
                .HasForeignKey(e => e.FamilyId)
                .OnDelete(DeleteBehavior.Restrict);

            // Expense → ExpenseType
            modelBuilder.Entity<Expense>()
                .HasOne(e => e.ExpenseType)
                .WithMany(et => et.Expenses)
                .HasForeignKey(e => e.ExpenseTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            // Expense → AddedBy User
            modelBuilder.Entity<Expense>()
                .HasOne(e => e.AddedBy)
                .WithMany()
                .HasForeignKey(e => e.AddedByUserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Expense → UpdatedBy User
            modelBuilder.Entity<Expense>()
                .HasOne(e => e.UpdatedBy)
                .WithMany()
                .HasForeignKey(e => e.UpdatedByUserId)
                .OnDelete(DeleteBehavior.Restrict);

            // RecurringExpense → Family
            modelBuilder.Entity<RecurringExpense>()
                .HasOne(r => r.Family)
                .WithMany()
                .HasForeignKey(r => r.FamilyId)
                .OnDelete(DeleteBehavior.Restrict);

            // RecurringExpense → ExpenseType
            modelBuilder.Entity<RecurringExpense>()
                .HasOne(r => r.ExpenseType)
                .WithMany()
                .HasForeignKey(r => r.ExpenseTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            // RecurringExpense → AddedBy User
            modelBuilder.Entity<RecurringExpense>()
                .HasOne(r => r.AddedBy)
                .WithMany()
                .HasForeignKey(r => r.AddedByUserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Budget → Family
            modelBuilder.Entity<Budget>()
                .HasOne(b => b.Family)
                .WithMany(f => f.Budgets)
                .HasForeignKey(b => b.FamilyId)
                .OnDelete(DeleteBehavior.Restrict);

            // Budget → ExpenseType (nullable)
            modelBuilder.Entity<Budget>()
                .HasOne(b => b.ExpenseType)
                .WithMany(et => et.Budgets)
                .HasForeignKey(b => b.ExpenseTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            // Seed default ExpenseTypes
            modelBuilder.Entity<ExpenseType>().HasData(
                new ExpenseType { ExpenseTypeId = 1, Name = "Food & Groceries", Icon = "🛒", ColorCode = "#FF6B6B", IsDefault = true },
                new ExpenseType { ExpenseTypeId = 2, Name = "Rent & EMI", Icon = "🏠", ColorCode = "#4D96FF", IsDefault = true },
                new ExpenseType { ExpenseTypeId = 3, Name = "Transport", Icon = "⛽", ColorCode = "#FFD93D", IsDefault = true },
                new ExpenseType { ExpenseTypeId = 4, Name = "Utilities", Icon = "⚡", ColorCode = "#6BCB77", IsDefault = true },
                new ExpenseType { ExpenseTypeId = 5, Name = "Entertainment", Icon = "🎬", ColorCode = "#A855F7", IsDefault = true },
                new ExpenseType { ExpenseTypeId = 6, Name = "Medical", Icon = "💊", ColorCode = "#FF6B6B", IsDefault = true },
                new ExpenseType { ExpenseTypeId = 7, Name = "Education", Icon = "📚", ColorCode = "#4D96FF", IsDefault = true },
                new ExpenseType { ExpenseTypeId = 8, Name = "Shopping", Icon = "🛍️", ColorCode = "#FFD93D", IsDefault = true }
            );
        }
    }
}
