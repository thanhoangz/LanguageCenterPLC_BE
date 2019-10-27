using LanguageCenterPLC.Data.EF;
using LanguageCenterPLC.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LanguageCenterPLC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogStudyProcessesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public LogStudyProcessesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/LogStudyProcesses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LogStudyProcess>>> GetLogStudyProcesses()
        {
            return await _context.LogStudyProcesses.ToListAsync();
        }

        // GET: api/LogStudyProcesses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LogStudyProcess>> GetLogStudyProcess(int id)
        {
            var logStudyProcess = await _context.LogStudyProcesses.FindAsync(id);

            if (logStudyProcess == null)
            {
                return NotFound();
            }

            return logStudyProcess;
        }

        // PUT: api/LogStudyProcesses/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLogStudyProcess(int id, LogStudyProcess logStudyProcess)
        {
            if (id != logStudyProcess.Id)
            {
                return BadRequest();
            }

            _context.Entry(logStudyProcess).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LogStudyProcessExists(id))
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

        // POST: api/LogStudyProcesses
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<LogStudyProcess>> PostLogStudyProcess(LogStudyProcess logStudyProcess)
        {
            _context.LogStudyProcesses.Add(logStudyProcess);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLogStudyProcess", new { id = logStudyProcess.Id }, logStudyProcess);
        }

        // DELETE: api/LogStudyProcesses/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<LogStudyProcess>> DeleteLogStudyProcess(int id)
        {
            var logStudyProcess = await _context.LogStudyProcesses.FindAsync(id);
            if (logStudyProcess == null)
            {
                return NotFound();
            }

            _context.LogStudyProcesses.Remove(logStudyProcess);
            await _context.SaveChangesAsync();

            return logStudyProcess;
        }

        private bool LogStudyProcessExists(int id)
        {
            return _context.LogStudyProcesses.Any(e => e.Id == id);
        }
    }
}
