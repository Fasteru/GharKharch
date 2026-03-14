using GharKharchaAPI.Application.Features;
using GharKharchaAPI.Domain.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GharKharchaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ExpenseController : ControllerBase
    {
        private readonly ExpenseService _expenseService;

        public ExpenseController(ExpenseService expenseService)
        {
            _expenseService = expenseService;
        }

        // Get family ID from JWT token
        private int GetFamilyId() =>
            int.Parse(User.FindFirst("FamilyId")!.Value);

        // Get user ID from JWT token
        private int GetUserId() =>
            int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        // GET api/expense
        [HttpGet]
        public async Task<IActionResult> GetExpenses(
            [FromQuery] string? monthYear,
            [FromQuery] int? expenseTypeId)
        {
            try
            {
                var result = await _expenseService.GetExpenses(
                    GetFamilyId(),
                    monthYear,
                    expenseTypeId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // GET api/expense/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetExpense(int id)
        {
            try
            {
                var result = await _expenseService.GetExpense(
                    id,
                    GetFamilyId());
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // POST api/expense
        [HttpPost]
        public async Task<IActionResult> AddExpense(
            [FromBody] AddExpenseDto dto)
        {
            try
            {
                var result = await _expenseService.AddExpense(
                    GetFamilyId(),
                    GetUserId(),
                    dto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // PUT api/expense/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateExpense(
            int id,
            [FromBody] UpdateExpenseDto dto)
        {
            try
            {
                var result = await _expenseService.UpdateExpense(
                    id,
                    GetFamilyId(),
                    GetUserId(),
                    dto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // DELETE api/expense/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExpense(int id)
        {
            try
            {
                await _expenseService.DeleteExpense(
                    id,
                    GetFamilyId());
                return Ok(new { message = "Expense deleted successfully!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}