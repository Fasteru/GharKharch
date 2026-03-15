using GharKharchaAPI.Data;
using GharKharchaAPI.Data.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GharKharchaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExpenseTypeController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ExpenseTypeController(AppDbContext context)
        {
            _context = context;
        }

        // GET api/expensetype
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var types = await _context.ExpenseTypes
                    .OrderBy(t => t.Name)
                    .ToListAsync();
                return Ok(types);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}