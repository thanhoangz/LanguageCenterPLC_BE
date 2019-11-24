using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LanguageCenterPLC.Data.EF;
using LanguageCenterPLC.Data.Entities;

namespace LanguageCenterPLC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogSystemsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public LogSystemsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/LogSystems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LogSystem>>> GetLogSystems()
        {
            return await _context.LogSystems.ToListAsync();
        }

        // GET: api/LogSystems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LogSystem>> GetLogSystem(long id)
        {
            var logSystem = await _context.LogSystems.FindAsync(id);

            if (logSystem == null)
            {
                return NotFound();
            }

            return logSystem;
        }

        // PUT: api/LogSystems/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLogSystem(long id, LogSystem logSystem)
        {
            if (id != logSystem.Id)
            {
                return BadRequest();
            }

            _context.Entry(logSystem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LogSystemExists(id))
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

        // POST: api/LogSystems
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<LogSystem>> PostLogSystem(LogSystem logSystem)
        {
            _context.LogSystems.Add(logSystem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLogSystem", new { id = logSystem.Id }, logSystem);
        }

        // DELETE: api/LogSystems/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<LogSystem>> DeleteLogSystem(long id)
        {
            var logSystem = await _context.LogSystems.FindAsync(id);
            if (logSystem == null)
            {
                return NotFound();
            }

            _context.LogSystems.Remove(logSystem);
            await _context.SaveChangesAsync();

            return logSystem;
        }

        private bool LogSystemExists(long id)
        {
            return _context.LogSystems.Any(e => e.Id == id);
        }
    }
}
