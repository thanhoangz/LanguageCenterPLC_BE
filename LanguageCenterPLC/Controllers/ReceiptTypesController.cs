using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LanguageCenterPLC.Data.EF;
using LanguageCenterPLC.Data.Entities;
using LanguageCenterPLC.Application.Interfaces;

namespace LanguageCenterPLC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReceiptTypesController : ControllerBase
    {
        //private readonly AppDbContext _context;
        private readonly IReceiptTypeService _receiptTypeService;
        public ReceiptTypesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/ReceiptTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReceiptType>>> GetReceiptTypes()
        {
            return await _context.ReceiptTypes.ToListAsync();
        }

        // GET: api/ReceiptTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ReceiptType>> GetReceiptType(int id)
        {
            var receiptType = await _context.ReceiptTypes.FindAsync(id);

            if (receiptType == null)
            {
                return NotFound();
            }

            return receiptType;
        }

        // PUT: api/ReceiptTypes/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReceiptType(int id, ReceiptType receiptType)
        {
            if (id != receiptType.Id)
            {
                return BadRequest();
            }

            _context.Entry(receiptType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReceiptTypeExists(id))
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

        // POST: api/ReceiptTypes
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<ReceiptType>> PostReceiptType(ReceiptType receiptType)
        {
            _context.ReceiptTypes.Add(receiptType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetReceiptType", new { id = receiptType.Id }, receiptType);
        }

        // DELETE: api/ReceiptTypes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ReceiptType>> DeleteReceiptType(int id)
        {
            var receiptType = await _context.ReceiptTypes.FindAsync(id);
            if (receiptType == null)
            {
                return NotFound();
            }

            _context.ReceiptTypes.Remove(receiptType);
            await _context.SaveChangesAsync();

            return receiptType;
        }

        private bool ReceiptTypeExists(int id)
        {
            return _context.ReceiptTypes.Any(e => e.Id == id);
        }
    }
}
