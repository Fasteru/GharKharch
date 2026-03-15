using GharKharchaAPI.Application.Features;
using GharKharchaAPI.Domain.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace GharKharchaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BudgetController : ControllerBase
    {
        private readonly BudgetService _budgetService;

        public BudgetController(BudgetService budgetService)
        {
            _budgetService = budgetService;
        }

        // GET api/budget/{monthYear}
        [HttpGet("{monthYear}")]
        public async Task<IActionResult> GetBudgets(
            string monthYear,
            [FromQuery] int familyId)
        {
            try
            {
                var result = await _budgetService.GetBudgets(
                    familyId, monthYear);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // POST api/budget
        [HttpPost]
        public async Task<IActionResult> AddBudget(
            [FromBody] AddBudgetDto dto)
        {
            try
            {
                var result = await _budgetService.AddBudget(
                    dto.FamilyId, dto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // DELETE api/budget/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBudget(
            int id,
            [FromQuery] int familyId)
        {
            try
            {
                await _budgetService.DeleteBudget(
                    id, familyId);
                return Ok(new { message = "Budget deleted!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // GET api/budget/report/{monthYear}
        [HttpGet("report/{monthYear}")]
        public async Task<IActionResult> GetMonthlyReport(
            string monthYear,
            [FromQuery] int familyId)
        {
            try
            {
                var result = await _budgetService.GetMonthlyReport(
                    familyId, monthYear);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}