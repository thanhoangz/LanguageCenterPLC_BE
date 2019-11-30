using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LanguageCenterPLC.Data.EF;
using LanguageCenterPLC.Data.Entities;
using LanguageCenterPLC.Infrastructure.Enums;

namespace LanguageCenterPLC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InforLearnersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public InforLearnersController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/InforLearners
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InforLearner>>> GetInforLearners()
        {
            return await _context.InforLearners.ToListAsync();
        }

        // GET: api/InforLearners/5
        [HttpGet("{id}")]
        public async Task<ActionResult<InforLearner>> GetInforLearner(int id)
        {
            var inforLearner = await _context.InforLearners.FindAsync(id);

            if (inforLearner == null)
            {
                return NotFound();
            }

            return inforLearner;
        }

        // PUT: api/InforLearners/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInforLearner(int id, InforLearner inforLearner)
        {
            if (id != inforLearner.Id)
            {
                return BadRequest();
            }

            _context.Entry(inforLearner).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InforLearnerExists(id))
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

        // POST: api/InforLearners
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<InforLearner>> PostInforLearner(InforLearner inforLearner)
        {
            inforLearner.Sex = true;
            inforLearner.Status = Status.Active;
            inforLearner.Birthday = DateTime.Now;
            _context.InforLearners.Add(inforLearner);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetInforLearner", new { id = inforLearner.Id }, inforLearner);
        }

        // DELETE: api/InforLearners/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<InforLearner>> DeleteInforLearner(int id)
        {
            var inforLearner = await _context.InforLearners.FindAsync(id);
            if (inforLearner == null)
            {
                return NotFound();
            }

            _context.InforLearners.Remove(inforLearner);
            await _context.SaveChangesAsync();

            return inforLearner;
        }

        private bool InforLearnerExists(int id)
        {
            return _context.InforLearners.Any(e => e.Id == id);
        }
    }
}
