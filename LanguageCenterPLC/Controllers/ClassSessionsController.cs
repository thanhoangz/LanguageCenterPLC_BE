using LanguageCenterPLC.Data.EF;
using LanguageCenterPLC.Data.Entities;
using LanguageCenterPLC.Infrastructure.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LanguageCenterPLC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassSessionsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ClassSessionsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/ClassSessions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClassSession>>> GetClassSessions()
        {
            return await _context.ClassSessions.ToListAsync();
        }

        // GET: api/ClassSessions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ClassSession>> GetClassSession(int id)
        {
            var classSession = await _context.ClassSessions.FindAsync(id);

            if (classSession == null)
            {
                return NotFound();
            }

            return classSession;
        }

        // PUT: api/ClassSessions/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClassSession(int id, ClassSession classSession)
        {
            if (id != classSession.Id)
            {
                return BadRequest();
            }

            _context.Entry(classSession).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClassSessionExists(id))
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

        // POST: api/ClassSessions
        [HttpPut]
        [Route("put-list")]
        public void PutList(List<ClassSession> classSessionList)
        {
            _context.ClassSessions.UpdateRange(classSessionList);
            _context.SaveChanges();

        }

        [HttpPost]
        public async Task<ActionResult<ClassSession>> PostClassSession(ClassSession classSession)
        {
            _context.ClassSessions.Add(classSession);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetClassSession", new { id = classSession.Id }, classSession);
        }


        [HttpPost]
        [Route("class-session")]
        public async Task<Object> PostClassSessionClass(string classId, int timeShiftId, List<string> days)
        {
            var teachingScheduleByClass = _context.TeachingSchedules.Where(x => x.LanguageClassId == classId).SingleOrDefault();

            if (teachingScheduleByClass != null)
            {
                List<ClassSession> classSessions = new List<ClassSession>();
                TimeSpan Time = teachingScheduleByClass.ToDate - teachingScheduleByClass.FromDate;
                int totalDay = Time.Days;
                var learnDate = teachingScheduleByClass.FromDate;
                var timeshift = _context.TimeShifts.Where(x => x.Id == timeShiftId).SingleOrDefault();

                for (int i = 1; i <= totalDay; i++)
                {
                    if (days.Contains(learnDate.DayOfWeek.ToString()))
                    {
                        ClassSession classSession1 = new ClassSession();
                        classSession1.Date = learnDate;
                        classSession1.FromTime = timeshift.FromTime;
                        classSession1.ToTime = timeshift.ToTime;
                        classSession1.TeachingScheduleId = teachingScheduleByClass.Id;
                        classSessions.Add(classSession1);
                    }

                    learnDate = learnDate.AddDays(1);
                }
                teachingScheduleByClass.Status = Status.Active;
                _context.TeachingSchedules.Update(teachingScheduleByClass);
                _context.ClassSessions.AddRange(classSessions);
                await _context.SaveChangesAsync();

                return await Task.FromResult(classSessions);
            }
            return await Task.FromResult("No hope");
        }

        // DELETE: api/ClassSessions/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ClassSession>> DeleteClassSession(int id)
        {
            var classSession = await _context.ClassSessions.FindAsync(id);
            if (classSession == null)
            {
                return NotFound();
            }

            _context.ClassSessions.Remove(classSession);
            await _context.SaveChangesAsync();

            return classSession;
        }

        private bool ClassSessionExists(int id)
        {
            return _context.ClassSessions.Any(e => e.Id == id);
        }
    }
}
