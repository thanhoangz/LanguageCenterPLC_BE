using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LanguageCenterPLC.Data.EF;
using LanguageCenterPLC.Data.Entities;
using LanguageCenterPLC.Areas.Identity.Pages.Account;

namespace LanguageCenterPLC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountForLearnersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AccountForLearnersController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/AccountForLearners
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AccountForLearner>>> GetAccountForLearners()
        {
            return await _context.AccountForLearners.ToListAsync();
        }

        // GET: api/AccountForLearners/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AccountForLearner>> GetAccountForLearner(int id)
        {
            var accountForLearner = await _context.AccountForLearners.FindAsync(id);

            if (accountForLearner == null)
            {
                return NotFound();
            }

            return accountForLearner;
        }

        // PUT: api/AccountForLearners/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAccountForLearner(int id, AccountForLearner accountForLearner)
        {
            if (id != accountForLearner.Id)
            {
                return BadRequest();
            }

            _context.Entry(accountForLearner).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccountForLearnerExists(id))
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

        // POST: api/AccountForLearners
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<AccountForLearner>> PostAccountForLearner(AccountForLearner accountForLearner)
        {
            _context.AccountForLearners.Add(accountForLearner);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAccountForLearner", new { id = accountForLearner.Id }, accountForLearner);
        }



        [HttpPost]
        [Route("login")]
        public async Task<Object> Login(string userName, string password )
        {
            var user = _context.AccountForLearners.Where(x => x.UserName == userName && x.Password == password).SingleOrDefault();
            if(user != null)
            {
                var leaner = _context.Learners.Where(x => x.Id == user.LearnerId).SingleOrDefault();

                return Task.FromResult(leaner);
            }
            return Task.FromResult("");
        }


        // DELETE: api/AccountForLearners/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<AccountForLearner>> DeleteAccountForLearner(int id)
        {
            var accountForLearner = await _context.AccountForLearners.FindAsync(id);
            if (accountForLearner == null)
            {
                return NotFound();
            }

            _context.AccountForLearners.Remove(accountForLearner);
            await _context.SaveChangesAsync();

            return accountForLearner;
        }

        private bool AccountForLearnerExists(int id)
        {
            return _context.AccountForLearners.Any(e => e.Id == id);
        }
    }
}
