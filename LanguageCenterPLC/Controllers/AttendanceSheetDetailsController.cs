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
    public class AttendanceSheetDetailsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AttendanceSheetDetailsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/AttendanceSheetDetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AttendanceSheetDetail>>> GetAttendanceSheetDetails()
        {
            return await _context.AttendanceSheetDetails.ToListAsync();
        }

        // GET: api/AttendanceSheetDetails/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AttendanceSheetDetail>> GetAttendanceSheetDetail(int id)
        {
            var attendanceSheetDetail = await _context.AttendanceSheetDetails.FindAsync(id);

            if (attendanceSheetDetail == null)
            {
                return NotFound();
            }

            return attendanceSheetDetail;
        }


        [HttpGet]
        [Route("get-details-by-date-attendance")]
        public async Task<ActionResult<IEnumerable<AttendanceSheetDetail>>> GetAttendanceSheetDetailByAtten(int attendanceId)
        {
            var attendanceSheetDetail = _context.AttendanceSheetDetails.Where(x => x.AttendanceSheetId == attendanceId);
            if (attendanceSheetDetail == null)
            {
                return NotFound();
            }

            return await Task.FromResult(attendanceSheetDetail.ToList());
        }


        // PUT: api/AttendanceSheetDetails/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAttendanceSheetDetail(int id, AttendanceSheetDetail attendanceSheetDetail)
        {
            if (id != attendanceSheetDetail.Id)
            {
                return BadRequest();
            }

            _context.Entry(attendanceSheetDetail).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AttendanceSheetDetailExists(id))
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

        // POST: api/AttendanceSheetDetails
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<AttendanceSheetDetail>> PostAttendanceSheetDetail(AttendanceSheetDetail attendanceSheetDetail)
        {
            _context.AttendanceSheetDetails.Add(attendanceSheetDetail);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAttendanceSheetDetail", new { id = attendanceSheetDetail.Id }, attendanceSheetDetail);
        }

        [HttpPost]
        [Route("attendance-list")]
        public async Task<ActionResult<List<AttendanceSheetDetail>>> PostAttendanceSheetDetailList(List<AttendanceSheetDetail> attendanceSheetDetails)
        {
            if (attendanceSheetDetails.Count != 0)
            {
                foreach (var item in attendanceSheetDetails)
                {
                    item.Status = Status.Active;
                    item.DateCreated = DateTime.Now;
                }
                var checkList = _context.AttendanceSheetDetails.Where(x => x.AttendanceSheetId == attendanceSheetDetails[0].AttendanceSheetId).ToList();

                if (checkList.Count != 0)
                {

                    foreach (var attendance in attendanceSheetDetails)
                    {
                        if (!IsExists(checkList, attendance))
                        {
                            _context.AttendanceSheetDetails.Add(attendance);
                        }
                    }

                }
                else
                {
                    _context.AttendanceSheetDetails.AddRange(attendanceSheetDetails);
                }

            }



            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAttendanceSheetDetails", attendanceSheetDetails);
        }



        [HttpPost]
        [Route("delete-attendance-list")]
        public async Task<ActionResult<List<AttendanceSheetDetail>>> DeleteAttendanceSheetDetailList(List<AttendanceSheetDetail> attendanceSheetDetails)
        {
            if (attendanceSheetDetails.Count != 0)
            {
                var checkList = _context.AttendanceSheetDetails.Where(x => x.AttendanceSheetId == attendanceSheetDetails[0].AttendanceSheetId).ToList();

                if (checkList.Count != 0)
                {

                    foreach (var attendance in attendanceSheetDetails)
                    {

                        var deteleAttendance = checkList.Where(x => x.LearnerId == attendance.LearnerId).SingleOrDefault();

                        if (deteleAttendance != null)
                            _context.AttendanceSheetDetails.Remove(deteleAttendance);

                    }

                }

            }



            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAttendanceSheetDetails", attendanceSheetDetails);
        }

        private bool IsExists(List<AttendanceSheetDetail> InDetails, AttendanceSheetDetail test)
        {
            foreach (var item in InDetails)
            {
                if (item.LearnerId == test.LearnerId)
                    return true;
            }
            return false;
        }

        // DELETE: api/AttendanceSheetDetails/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<AttendanceSheetDetail>> DeleteAttendanceSheetDetail(int id)
        {
            var attendanceSheetDetail = await _context.AttendanceSheetDetails.FindAsync(id);
            if (attendanceSheetDetail == null)
            {
                return NotFound();
            }

            _context.AttendanceSheetDetails.Remove(attendanceSheetDetail);
            await _context.SaveChangesAsync();

            return attendanceSheetDetail;
        }

        private bool AttendanceSheetDetailExists(int id)
        {
            return _context.AttendanceSheetDetails.Any(e => e.Id == id);
        }
    }
}
