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
        public async Task<List<Object>> GetLogSystems()
        {
            List<Object> result = new List<object>();
            foreach (var item in _context.LogSystems.OrderByDescending(x=>x.DateCreated))
            {
                var Ob = new
                {
                    UserName = _context.AppUsers.Find(item.UserId).FullName,
                    Infor = item
                };

                result.Add(Ob);

            }
            return await Task.FromResult(result);
        }

        [HttpPost]
        [Route("searchLog")]
        public async Task<List<Object>> SearchLogSystem(Guid userId, DateTime toDate, DateTime fromDate)
        {
            List<Object> result = new List<object>();
            var list = _context.LogSystems.Where(x => x.UserId == userId &&  x.DateCreated >= toDate && x.DateCreated <= fromDate).OrderByDescending(x => x.DateCreated);
            foreach (var item in list)
            {
                var Ob = new
                {
                    UserName = _context.AppUsers.Find(item.UserId).FullName,
                    Infor = item
                };

                result.Add(Ob);

            }
            return await Task.FromResult(result);
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
            logSystem.DateCreated = DateTime.Now;
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

        [HttpPost]
        [Route("get-studyprocess-by-learnerId")]
        public async Task<List<Object>> getStudyProcessBtLearnerId(string learnerId)
        {
            var resultList = new List<Object>();
            var studyProcessList = _context.LogSystems.Where(x => x.IsStudyProcessLog == true && x.LearnerId == learnerId).OrderBy(x => x.StudyProcessId).ToList();
            var number = 0;
            foreach (var itemX in studyProcessList)
            {
                // lấy danh sách điểm danh
                var diemDanhByClassId = _context.AttendanceSheets.Where(x => x.LanguageClassId == itemX.ClassId).ToList();             
                foreach (var itemY in diemDanhByClassId)
                {     // lấy ra chi tiết điểm danh                               
                    var diemDanh = _context.AttendanceSheetDetails.Where(x => x.LearnerId == learnerId && x.AttendanceSheetId == itemY.Id).SingleOrDefault();
                    if ( diemDanh != null)
                    {
                        number++;
                    }
                }

                // lấy tên lớp
                var classObj = _context.LanguageClasses.Find(itemX.ClassId);
                // lấy tên người dùng
                var userObj = _context.AppUsers.Find(itemX.UserId);

                var logQTHT = new
                {
                    id = itemX.Id,
                    date = itemX.DateCreated,
                    content = itemX.Content,                
                    userName = userObj.FullName,
                    className = classObj.Name,
                    mumberSessions = number,
                };
                resultList.Add(logQTHT);
            }
            return await Task.FromResult(resultList);
        }
    }
}
