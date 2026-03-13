using System;
using System.Collections.Generic;
using System.Text;

namespace GharKharchaAPI.Domain.Models.DTO
{
    // Register new family (first member = admin)
    public class RegisterFamilyDto
    {
        public string FamilyName { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    // Add member to existing family
    public class AddMemberDto
    {
        public int FamilyId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Role { get; set; } = "member";
    }

    // Login
    public class LoginDto
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    // Response after login/register
    public class AuthResponseDto
    {
        public int UserId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public int FamilyId { get; set; }
        public string FamilyName { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
    }
}
