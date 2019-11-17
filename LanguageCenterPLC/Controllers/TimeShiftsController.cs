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
    public class TimeShiftsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TimeShiftsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/TimeShifts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TimeShift>>> GetTimeShifts()
        {
            return await _context.TimeShifts.ToListAsync();
        }

        // GET: api/TimeShifts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TimeShift>> GetTimeShift(int id)
        {
            var timeShift = await _context.TimeShifts.FindAsync(id);

            if (timeShift == null)
            {
                return NotFound();
            }

            return timeShift;
        }

        // PUT: api/TimeShifts/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTimeShift(int id, TimeShift timeShift)
        {
            if (id != timeShift.Id)
            {
                return BadRequest();
            }

            _context.Entry(timeShift).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TimeShiftExists(id))
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

        // POST: api/TimeShifts
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<TimeShift>> PostTimeShift(TimeShift timeShift)
        {
            _context.TimeShifts.Add(timeShift);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTimeShift", new { id = timeShift.Id }, timeShift);
        }

        // DELETE: api/TimeShifts/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TimeShift>> DeleteTimeShift(int id)
        {
            var timeShift = await _context.TimeShifts.FindAsync(id);
            if (timeShift == null)
            {
                return NotFound();
            }

            _context.TimeShifts.Remove(timeShift);
            await _context.SaveChangesAsync();

            return timeShift;
        }

        private bool TimeShiftExists(int id)
        {
            return _context.TimeShifts.Any(e => e.Id == id);
        }
    }
}
