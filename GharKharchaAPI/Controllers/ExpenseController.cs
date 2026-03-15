using GharKharchaAPI.Application.Features;
using GharKharchaAPI.Domain.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace GharKharchaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExpenseController : ControllerBase
    {
        private readonly ExpenseService _expenseService;

        public ExpenseController(ExpenseService expenseService)
        {
            _expenseService = expenseService;
        }

        // GET api/expense
        [HttpGet]
        public async Task<IActionResult> GetExpenses(
            [FromQuery] int familyId,
            [FromQuery] string? monthYear,
            [FromQuery] int? expenseTypeId)
        {
            try
            {
                var result = await _expenseService.GetExpenses(
                    familyId,
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
        public async Task<IActionResult> GetExpense(
            int id,
            [FromQuery] int familyId)
        {
            try
            {
                var result = await _expenseService.GetExpense(
                    id, familyId);
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
                    dto.FamilyId,
                    dto.UserId,
                    dto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    message = ex.Message,
                    inner = ex.InnerException?.Message,
                    detail = ex.InnerException?.InnerException?.Message
                });
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
                    dto.FamilyId,
                    dto.UserId,
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
        public async Task<IActionResult> DeleteExpense(
            int id,
            [FromQuery] int familyId)
        {
            try
            {
                await _expenseService.DeleteExpense(
                    id, familyId);
                return Ok(new { message = "Expense deleted!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}