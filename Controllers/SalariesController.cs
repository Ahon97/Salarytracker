using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Salary_tracker.Models;

namespace Salary_tracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalariesController : ControllerBase
    {
        private readonly SalaryTrackerContext _context;
        readonly log4net.ILog _log4net;

        public SalariesController(SalaryTrackerContext context)
        {
            _context = context;
            _log4net = log4net.LogManager.GetLogger(typeof(SalariesController));
        }

        // GET: api/Salaries
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Salary>>> GetSalary()
        {
            return await _context.Salary.ToListAsync();
        }

        // GET: api/Salaries/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Salary>> GetSalary(int id)
        {
            var salary = await _context.Salary.FindAsync(id);

            if (salary == null)
            {
                return NotFound();
            }

            return salary;
        }

        // PUT: api/Salaries/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSalary(int id, Salary salary)
        {
            if (id != salary.TransactionId)
            {
                return BadRequest();
            }

            _context.Entry(salary).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SalaryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Salaries
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Salary>> PostSalary(Salary salary)
        {
            _context.Salary.Add(salary);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (SalaryExists(salary.TransactionId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetSalary", new { id = salary.TransactionId }, salary);
        }

        // DELETE: api/Salaries/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Salary>> DeleteSalary(int id)
        {
            var salary = await _context.Salary.FindAsync(id);
            if (salary == null)
            {
                return NotFound();
            }

            _context.Salary.Remove(salary);
            await _context.SaveChangesAsync();

            return salary;
        }

        private bool SalaryExists(int id)
        {
            return _context.Salary.Any(e => e.TransactionId == id);
        }
    }
}
