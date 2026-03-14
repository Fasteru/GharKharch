using System;
using System.Collections.Generic;
using System.Text;

namespace GharKharchaAPI.Domain.Models
{
    public class User
    {
        public int UserId { get; set; }
        public int FamilyId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Role { get; set; } = "member"; // admin/member
        public DateTime CreatedAt { get; set; }

        // Navigation
        public Family Family { get; set; } = null!;
    }
}
