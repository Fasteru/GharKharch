using GharKharchaAPI.Authentication;
using GharKharchaAPI.Domain.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace GharKharchaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        // POST api/auth/register
        [HttpPost("register")]
        public async Task<IActionResult> RegisterFamily(
            [FromBody] RegisterFamilyDto dto)
        {
            try
            {
                var result = await _authService.RegisterFamily(dto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // POST api/auth/add-member
        [HttpPost("add-member")]
        public async Task<IActionResult> AddMember(
            [FromBody] AddMemberDto dto)
        {
            try
            {
                var result = await _authService.AddMember(dto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // POST api/auth/login
        [HttpPost("login")]
        public async Task<IActionResult> Login(
            [FromBody] LoginDto dto)
        {
            try
            {
                var result = await _authService.Login(dto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}