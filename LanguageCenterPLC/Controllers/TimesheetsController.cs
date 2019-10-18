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
using LanguageCenterPLC.Application.ViewModels.Timekeepings;

namespace LanguageCenterPLC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimesheetsController : ControllerBase
    {
        private readonly ITimesheetService _timesheetService;

        public TimesheetsController(ITimesheetService timesheetService)
        {
            _timesheetService = timesheetService;
        }

        // GET: api/Timesheets
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TimesheetViewModel>>> GetTimesheets()
        {
            return await Task.FromResult(_timesheetService.GetAll());
        }

        // GET: api/Timesheets/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TimesheetViewModel>> GetTimesheet(int id)
        {
            var timeSheet = _timesheetService.GetById(id);

            if (timeSheet == null)
            {
                return NotFound("Không tìm thấy id = " + id);
            }

            return await Task.FromResult(timeSheet);
        }

        // PUT: api/Timesheets/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTimesheet(int id, TimesheetViewModel timesheet)
        {

            if (timesheet.Id != id)
            {
                throw new Exception(string.Format("Id không giống nhau!"));
            }

            try
            {
                await Task.Run(() =>
                {
                    timesheet.DateModified = DateTime.Now;
                    _timesheetService.Update(timesheet);
                    _timesheetService.SaveChanges();
                    return Ok("Cập nhập thành công!");
                });

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TimesheetExists(id))
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

        // POST: api/Timesheets
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<TimesheetViewModel>> PostTimesheet(TimesheetViewModel timesheet)
        {
            if (timesheet != null)
            {
                try
                {
                    await Task.Run(() =>
                    {
                        timesheet.DateCreated = DateTime.Now;
                        timesheet.DateModified = DateTime.Now;
                        _timesheetService.Add(timesheet);
                        _timesheetService.SaveChanges();
                        return Ok("Thêm thành công!");
                    });

                }
                catch
                {

                    throw new Exception(string.Format("Lỗi khi thêm dữ liệu"));
                }

            }
            
            return CreatedAtAction("GetLecturer()", new { id = timesheet.Id }, timesheet);
        }

        [HttpPost("/api/Timesheets/get-all-with-conditions")]
        public async Task<ActionResult<IEnumerable<TimesheetViewModel>>> GetAllConditions(int month,int year)
        {
            return await Task.FromResult(_timesheetService.GetAllWithConditions(month,year));
        }

        // DELETE: api/Timesheets/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TimesheetViewModel>> DeleteTimesheet(int id)
        {
            var timesheet = _timesheetService.GetById(id);
            if (timesheet == null)
            {
                return NotFound("Không tìm thấy Id = " + id);
            }

            try
            {
                await Task.Run(() =>
                {
                    _timesheetService.Delete(id);
                    _timesheetService.SaveChanges();
                });
            }
            catch
            {

                throw new Exception(string.Format("Có lỗi xảy ra không thể xóa!"));
            }

            return Ok();
        }

        private bool TimesheetExists(int id)
        {
            return _timesheetService.IsExists(id);
        }
    }
}
