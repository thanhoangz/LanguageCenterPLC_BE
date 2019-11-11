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

        // PUT: api/AttendanceSheets/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
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
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<AttendanceSheetViewModel>> PostAttendanceSheet(AttendanceSheetViewModel attendanceSheet)
        {
            attendanceSheet.WageOfLecturer = 0;
            attendanceSheet.WageOfTutor = 0;
            attendanceSheet.DateCreated = DateTime.Now;
            attendanceSheet.Status = Status.Active;
            var result = Mapper.Map<AttendanceSheetViewModel, AttendanceSheet>(attendanceSheet);
            _context.AttendanceSheets.Add(result);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAttendanceSheet", new { id = attendanceSheet.Id }, attendanceSheet);
        }

        // DELETE: api/AttendanceSheets/5
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



        [HttpPost]
        [Route("payroll-approval-lecturer")]
        public async Task<Object> PayrollApprovalLecturer(List<int> attendanceList)
        {
            try
            {
                await Task.Run(() =>
                {
                    List<SalaryPay> salaryPays = new List<SalaryPay>();
                    foreach (var item in attendanceList)
                    {
                        SalaryPay salaryPay = new SalaryPay();
                        var attendances = _context.AttendanceSheets.Find(item);
                        
                        //salaryPay.PersonnelId = timeSheet.PersonnelId;
                        //salaryPay.TotalBasicSalary = timeSheet.Salary;
                        //salaryPay.TotalAllowance = timeSheet.Allowance;
                        //salaryPay.TotalBonus = timeSheet.Bonus;
                        //salaryPay.TotalInsurancePremium = timeSheet.InsurancePremiums;

                        //salaryPay.TotalSalaryOfDay = timeSheet.SalaryOfDay;
                        //salaryPay.TotalWorkdays = timeSheet.TotalWorkday;
                        //salaryPay.TotalTheoreticalAmount = timeSheet.SalaryOfDay * Convert.ToDecimal(timeSheet.TotalWorkday);
                        //if (salaryPay.TotalWorkdays >= 25)
                        //{
                        //    salaryPay.TotalRealityAmount = salaryPay.TotalBasicSalary + salaryPay.TotalAllowance + salaryPay.TotalBonus - salaryPay.TotalInsurancePremium;
                        //}
                        //else
                        //{
                        //    salaryPay.TotalRealityAmount = salaryPay.TotalSalaryOfDay * Convert.ToDecimal(salaryPay.TotalWorkdays) + salaryPay.TotalAllowance + salaryPay.TotalBonus - salaryPay.TotalInsurancePremium;
                        //}

                        salaryPays.Add(salaryPay);
                    }

                    _context.SalaryPays.AddRange(salaryPays);
                    _context.SaveChanges();
                });
            }
            catch
            {

                throw new Exception(string.Format("Có lỗi xảy ra !"));
            }

            return Ok();
        }

    }
}
