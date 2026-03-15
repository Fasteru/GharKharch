using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GharKharchaAPI.Data.Entities
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        public int FamilyId { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;

        public string Role { get; set; } = "member";

        public DateTime CreatedAt { get; set; }

        // Navigation
        public Family Family { get; set; } = null!;
    }
}
