using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LanguageCenterPLC.Data.EF;
using LanguageCenterPLC.Data.Entities;
using LanguageCenterPLC.Application.ViewModels.Finances;
using AutoMapper;
using LanguageCenterPLC.Infrastructure.Enums;

namespace LanguageCenterPLC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FunctionsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public FunctionsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Functions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Function>>> GetFunctions()
        {
            return await _context.Functions.ToListAsync();
        }

        // GET: api/Functions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Function>> GetFunction(string id)
        {
            var function = await _context.Functions.FindAsync(id);

            if (function == null)
            {
                return NotFound();
            }

            return function;
        }

     
        [HttpGet]
        [Route("get-functions-group")]
        public async Task<ActionResult<List<FunctionViewModel>>> GetAllFunction()
        {
            var functions = await _context.Functions.Where(x => (string.IsNullOrEmpty(x.ParentId))).ToListAsync();
          
            var functionsViewModel = Mapper.Map<List<FunctionViewModel>>(functions);
            foreach (var item in functionsViewModel)
            {
                if (string.IsNullOrEmpty(item.ParentId))
                {
                    var childFunctions = _context.Functions.Where(x => x.ParentId == item.Id && x.Status == Status.Active).ToList();
                    var childFunctionsViewModel = Mapper.Map<List<FunctionViewModel>>(childFunctions);
                    item.ChildFunctionViewModels = childFunctionsViewModel;
                }
            }
            return await Task.FromResult(functionsViewModel);
        }

        // PUT: api/Functions/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFunction(string id, Function function)
        {
            if (id != function.Id)
            {
                return BadRequest();
            }

            _context.Entry(function).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FunctionExists(id))
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

        // POST: api/Functions
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Function>> PostFunction(Function function)
        {
            _context.Functions.Add(function);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (FunctionExists(function.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetFunction", new { id = function.Id }, function);
        }

        // DELETE: api/Functions/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Function>> DeleteFunction(string id)
        {
            var function = await _context.Functions.FindAsync(id);
            if (function == null)
            {
                return NotFound();
            }

            _context.Functions.Remove(function);
            await _context.SaveChangesAsync();

            return function;
        }

        private bool FunctionExists(string id)
        {
            return _context.Functions.Any(e => e.Id == id);
        }
    }
}
