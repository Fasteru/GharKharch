using AutoMapper;
using GharKharchaAPI.Data.Context;
using GharKharchaAPI.Data.Entities;
using GharKharchaAPI.Domain.Models;
using GharKharchaAPI.Domain.Models.DTO;
using GharKharchaAPI.Helper;
using Microsoft.EntityFrameworkCore;

namespace GharKharchaAPI.Authentication
{
    public class AuthService
    {
        private readonly AppDbContext _context;
        private readonly JwtHelper _jwtHelper;
        private readonly IMapper _mapper;

        public AuthService(AppDbContext context, JwtHelper jwtHelper, IMapper mapper)
        {
            _context = context;
            _jwtHelper = jwtHelper;
            _mapper = mapper;
        }

        // Register new family with admin user
        public async Task<AuthResponseDto> RegisterFamily(RegisterFamilyDto dto)
        {
            // Check if email already exists
            if (await _context.Users.AnyAsync(u => u.Email == dto.Email))
                throw new Exception("Email already registered!");

            // Create family
            var family = new Family
            {
                FamilyName = dto.FamilyName,
                CreatedAt = DateTime.Now
            };
            var fam = _mapper.Map<Family, FamilyEntity>(family);
            _context.Families.Add(fam);
            await _context.SaveChangesAsync();

            // Create admin user
            var user = new User
            {
                FamilyId = family.FamilyId,
                Name = dto.Name,
                Email = dto.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                Role = "admin",
                CreatedAt = DateTime.Now
            };
            var userData = _mapper.Map<User, UserEntity>(user);
            _context.Users.Add(userData);
            await _context.SaveChangesAsync();

            // Generate token
            var token = _jwtHelper.GenerateToken(user);

            return new AuthResponseDto
            {
                UserId = user.UserId,
                Name = user.Name,
                Email = user.Email,
                Role = user.Role,
                FamilyId = family.FamilyId,
                FamilyName = family.FamilyName,
                Token = token
            };
        }

        // Add member to existing family
        public async Task<AuthResponseDto> AddMember(AddMemberDto dto)
        {
            // Check if family exists
            var family = await _context.Families.FindAsync(dto.FamilyId)
                ?? throw new Exception("Family not found!");

            // Check if email already exists
            if (await _context.Users.AnyAsync(u => u.Email == dto.Email))
                throw new Exception("Email already registered!");

            // Create member
            var user = new User
            {
                FamilyId = dto.FamilyId,
                Name = dto.Name,
                Email = dto.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                Role = dto.Role,
                CreatedAt = DateTime.Now
            };
            var userData = _mapper.Map<User, UserEntity>(user);
            _context.Users.Add(userData);
            await _context.SaveChangesAsync();

            // Generate token
            var token = _jwtHelper.GenerateToken(user);

            return new AuthResponseDto
            {
                UserId = user.UserId,
                Name = user.Name,
                Email = user.Email,
                Role = user.Role,
                FamilyId = family.FamilyId,
                FamilyName = family.FamilyName,
                Token = token
            };
        }

        // Login
        public async Task<AuthResponseDto> Login(LoginDto dto)
        {
            // Find user by email
            var user = await _context.Users
                .Include(u => u.Family)
                .FirstOrDefaultAsync(u => u.Email == dto.Email)
                ?? throw new Exception("Invalid email or password!");

            // Verify password
            if (!BCrypt.Net.BCrypt.Verify(dto.Password, user.Password))
                throw new Exception("Invalid email or password!");

            // Generate token
            var userData = _mapper.Map<UserEntity, User>(user);
            var token = _jwtHelper.GenerateToken(userData);

            return new AuthResponseDto
            {
                UserId = user.UserId,
                Name = user.Name,
                Email = user.Email,
                Role = user.Role,
                FamilyId = user.FamilyId,
                FamilyName = user.Family.FamilyName,
                Token = token
            };
        }
    }
}
