using LanguageCenterPLC.Application.Interfaces;
using LanguageCenterPLC.Application.ViewModels.Categories;
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
    public class LanguageClassesController : ControllerBase
    {
        ILanguageClassService _languageClassService;

        public LanguageClassesController(ILanguageClassService languageClassService)
        {
            _languageClassService = languageClassService;
        }

        // GET: api/LanguageClasses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LanguageClassViewModel>>> GetLanguageClasses()
        {
            return await Task.FromResult(_languageClassService.GetAll());
        }

        //GET: api/LanguageClasses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LanguageClassViewModel>> GetLanguageClass(string id)
        {
            var languageClass = await _context.LanguageClasses.FindAsync(id);

            if (languageClass == null)
            {
                return NotFound();
            }

            return languageClass;
        }

        // PUT: api/LanguageClasses/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLanguageClass(string id, LanguageClass languageClass)
        {
            if (id != languageClass.Id)
            {
                return BadRequest();
            }

            _context.Entry(languageClass).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LanguageClassExists(id))
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

        // POST: api/LanguageClasses
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<LanguageClass>> PostLanguageClass(LanguageClass languageClass)
        {
            _context.LanguageClasses.Add(languageClass);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (LanguageClassExists(languageClass.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetLanguageClass", new { id = languageClass.Id }, languageClass);
        }

        // DELETE: api/LanguageClasses/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<LanguageClass>> DeleteLanguageClass(string id)
        {
            var languageClass = await _context.LanguageClasses.FindAsync(id);
            if (languageClass == null)
            {
                return NotFound();
            }

            _context.LanguageClasses.Remove(languageClass);
            await _context.SaveChangesAsync();

            return languageClass;
        }

        private bool LanguageClassExists(string id)
        {
            return _context.LanguageClasses.Any(e => e.Id == id);
        }
    }
}
