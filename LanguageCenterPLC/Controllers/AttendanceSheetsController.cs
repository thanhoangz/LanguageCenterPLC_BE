using AutoMapper;
using LanguageCenterPLC.Application.ViewModels.Timekeepings;
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
    public class AttendanceSheetsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AttendanceSheetsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/AttendanceSheets
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AttendanceSheet>>> GetAttendanceSheets()
        {
            return await _context.AttendanceSheets.ToListAsync();
        }

        // GET: api/AttendanceSheets/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AttendanceSheet>> GetAttendanceSheet(int id)
        {
            var attendanceSheet = await _context.AttendanceSheets.FindAsync(id);

            if (attendanceSheet == null)
            {
                return NotFound();
            }

            return attendanceSheet;
        }

        [HttpGet]
        [Route("attendance/{id}")]
        public async Task<ActionResult<AttendanceSheet>> GetAttendanceSheetByClass(string id, DateTime date)
        {
            var attendanceSheet = await _context.AttendanceSheets.Where(x => x.LanguageClassId == id && x.Date.Day == date.Date.Day && x.Date.Month == date.Month && x.Date.Year == date.Year).SingleOrDefaultAsync();

            if (attendanceSheet == null)
            {
                return NotFound();
            }

            return attendanceSheet;
        }

        [HttpGet]
        [Route("getByDate")]
        public async Task<ActionResult<AttendanceSheet>> GetAttendanceSheetByDate(string classId, DateTime date)
        {
            var attendanceSheet =  _context.AttendanceSheets.Where(x => x.LanguageClassId == classId && x.Date.Day == date.Day && x.Date.Month == date.Month && x.Date.Year == date.Year).SingleOrDefault();

            if (attendanceSheet == null)
            {
                return NotFound();
            }

            return await Task.FromResult(attendanceSheet);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutAttendanceSheet(int id, AttendanceSheet attendanceSheet)
        {
            if (id != attendanceSheet.Id)
            {
                return BadRequest();
            }

            _context.Entry(attendanceSheet).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AttendanceSheetExists(id))
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

        // POST: api/AttendanceSheets

        [HttpPost]
        public async Task<ActionResult<AttendanceSheetViewModel>> PostAttendanceSheet(AttendanceSheetViewModel attendanceSheet)
        {
            var _class = _context.LanguageClasses.Where(x => x.Id == attendanceSheet.LanguageClassId).SingleOrDefault();
            attendanceSheet.WageOfLecturer = Convert.ToDecimal( _class.WageOfLecturer);
            attendanceSheet.WageOfTutor = Convert.ToDecimal(_class.WageOfTutor);
            attendanceSheet.DateCreated = DateTime.Now;
            attendanceSheet.Status = Status.Active;
            var result = Mapper.Map<AttendanceSheetViewModel, AttendanceSheet>(attendanceSheet);
            _context.AttendanceSheets.Add(result);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAttendanceSheet", new { id = attendanceSheet.Id }, attendanceSheet);
        }

        [HttpPost]
        [Route("add-attendance-list")]
        public void PostAttendanceSheetList(Guid userId, int lecturerId, int tutorId,string classId, int month, int year)
        {
            // tạo hết 1 loạt các chi tiết điểm danh theo các buổi học 

            var classSessions = (from cl in _context.LanguageClasses
                                 join ts in _context.TeachingSchedules on cl.Id equals ts.LanguageClassId
                                 join s in _context.ClassSessions on ts.Id equals s.TeachingScheduleId
                                 where cl.Id == classId && s.Date.Month == month && s.Date.Year == year
                                 select s).OrderBy(x => x.Date).ToList();

            //lấy ra các học viên trong lớp đó.
            var learners = (from l in _context.Learners
                            join st in _context.StudyProcesses on l.Id equals st.LearnerId
                            where st.LanguageClassId == classId && l.Status == Status.Active && st.Status == Status.Active
                            select l).ToList();

            var _class = _context.LanguageClasses.Where(x => x.Id == classId).SingleOrDefault();

            List<AttendanceSheet> attendanceSheets = new List<AttendanceSheet>();


            foreach (var cs in classSessions)
            {
                AttendanceSheet attendanceSheet = new AttendanceSheet();

                attendanceSheet.WageOfLecturer = Convert.ToDecimal(_class.WageOfLecturer);
                attendanceSheet.WageOfTutor = Convert.ToDecimal(_class.WageOfTutor);
                attendanceSheet.DateCreated = DateTime.Now;
                attendanceSheet.Status = Status.Active;
                attendanceSheet.AppUserId = userId;
                attendanceSheet.LecturerId = lecturerId;
                attendanceSheet.TutorId = tutorId;
                attendanceSheet.LanguageClassId = classId;

                attendanceSheet.Date = cs.Date;
                attendanceSheets.Add(attendanceSheet);
            }
            _context.AttendanceSheets.AddRange(attendanceSheets);
            _context.SaveChanges();


            List<AttendanceSheetDetail> attendanceSheetDetails = new List<AttendanceSheetDetail>();

            foreach (var attendance in attendanceSheets)
            {
                foreach (var learner in learners)
                {
                    AttendanceSheetDetail attendanceSheetDetail = new AttendanceSheetDetail();
                    attendanceSheetDetail.Status = Status.InActive;
                    attendanceSheetDetail.LanguageClassId = classId;
                    attendanceSheetDetail.DateCreated = attendance.Date;
                    attendanceSheetDetail.AttendanceSheetId = attendance.Id;

                    attendanceSheetDetail.LearnerId = learner.Id;

                    attendanceSheetDetails.Add(attendanceSheetDetail);
                }

            }
            _context.AttendanceSheetDetails.AddRange(attendanceSheetDetails);
            _context.SaveChanges();

        }



        // DELETE: api/AttendanceSheets/5SqlException: Cannot insert explicit value for identity column in table 'AttendanceSheetDetails' when IDENTITY_INSERT is set to OFF.

        [HttpDelete("{id}")]
        public async Task<ActionResult<AttendanceSheet>> DeleteAttendanceSheet(int id)
        {
            var attendanceSheet = await _context.AttendanceSheets.FindAsync(id);
            if (attendanceSheet == null)
            {
                return NotFound();
            }

            _context.AttendanceSheets.Remove(attendanceSheet);
            await _context.SaveChangesAsync();

            return attendanceSheet;
        }

        private bool AttendanceSheetExists(int id)
        {
            return _context.AttendanceSheets.Any(e => e.Id == id);
        }


    }
}
