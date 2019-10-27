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
    public class LogTuitionRecordsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public LogTuitionRecordsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/LogTuitionRecords
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LogTuitionRecord>>> GetLogTuitionRecords()
        {
            return await _context.LogTuitionRecords.ToListAsync();
        }

        // GET: api/LogTuitionRecords/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LogTuitionRecord>> GetLogTuitionRecord(int id)
        {
            var logTuitionRecord = await _context.LogTuitionRecords.FindAsync(id);

            if (logTuitionRecord == null)
            {
                return NotFound();
            }

            return logTuitionRecord;
        }

        // PUT: api/LogTuitionRecords/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLogTuitionRecord(int id, LogTuitionRecord logTuitionRecord)
        {
            if (id != logTuitionRecord.Id)
            {
                return BadRequest();
            }

            _context.Entry(logTuitionRecord).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LogTuitionRecordExists(id))
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

        // POST: api/LogTuitionRecords
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<LogTuitionRecord>> PostLogTuitionRecord(LogTuitionRecord logTuitionRecord)
        {
            _context.LogTuitionRecords.Add(logTuitionRecord);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLogTuitionRecord", new { id = logTuitionRecord.Id }, logTuitionRecord);
        }

        // DELETE: api/LogTuitionRecords/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<LogTuitionRecord>> DeleteLogTuitionRecord(int id)
        {
            var logTuitionRecord = await _context.LogTuitionRecords.FindAsync(id);
            if (logTuitionRecord == null)
            {
                return NotFound();
            }

            _context.LogTuitionRecords.Remove(logTuitionRecord);
            await _context.SaveChangesAsync();

            return logTuitionRecord;
        }

        private bool LogTuitionRecordExists(int id)
        {
            return _context.LogTuitionRecords.Any(e => e.Id == id);
        }
    }
}
