using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data; // ← DbContext の名前空間に合わせる
using WebApplication1.Models; // ← Employee モデルの名前空間に合わせる

namespace server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EmployeeController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/employee
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees()
        {
            return await _context.Employees.ToListAsync();
        }

        // GET: api/employee/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployee(int id)
        {
            var emp = await _context.Employees.FindAsync(id);
            if (emp == null) return NotFound();
            return emp;
        }

        // POST: api/employee
        [HttpPost]
        public async Task<ActionResult<Employee>> PostEmployee(Employee emp)
        {
            _context.Employees.Add(emp);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetEmployee), new { id = emp.Id }, emp);
        }

        // PUT: api/employee/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployee(int id, Employee emp)
        {
            if (id != emp.Id) return BadRequest();
            _context.Entry(emp).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/employee/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var emp = await _context.Employees.FindAsync(id);
            if (emp == null) return NotFound();
            _context.Employees.Remove(emp);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
