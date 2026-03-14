using GharKharchaAPI.Application.Features;
using GharKharchaAPI.Domain.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GharKharchaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class BudgetController : ControllerBase
    {
        private readonly BudgetService _budgetService;

        public BudgetController(BudgetService budgetService)
        {
            _budgetService = budgetService;
        }

        private int GetFamilyId() =>
            int.Parse(User.FindFirst("FamilyId")!.Value);

        // GET api/budget/{monthYear}
        [HttpGet("{monthYear}")]
        public async Task<IActionResult> GetBudgets(string monthYear)
        {
            try
            {
                var result = await _budgetService.GetBudgets(
                    GetFamilyId(),
                    monthYear);
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
                    GetFamilyId(),
                    dto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // DELETE api/budget/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBudget(int id)
        {
            try
            {
                await _budgetService.DeleteBudget(
                    id,
                    GetFamilyId());
                return Ok(new { message = "Budget deleted successfully!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // GET api/budget/report/{monthYear}
        [HttpGet("report/{monthYear}")]
        public async Task<IActionResult> GetMonthlyReport(string monthYear)
        {
            try
            {
                var result = await _budgetService.GetMonthlyReport(
                    GetFamilyId(),
                    monthYear);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}