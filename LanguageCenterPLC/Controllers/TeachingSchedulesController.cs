using AutoMapper;
using LanguageCenterPLC.Application.ViewModels.Categories;
using LanguageCenterPLC.Application.ViewModels.Studies;
using LanguageCenterPLC.Data.EF;
using LanguageCenterPLC.Data.Entities;
using LanguageCenterPLC.Infrastructure.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LanguageCenterPLC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeachingSchedulesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TeachingSchedulesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/TeachingSchedules
        [HttpGet]
        public async Task<List<TeachingScheduleViewModel>> GetTeachingSchedules()
        {
            var schedules = _context.TeachingSchedules.Where(x => x.Status == Status.Active);
            var schedulesViewModel = Mapper.Map<List<TeachingScheduleViewModel>>(schedules);
            foreach (var item in schedulesViewModel)
            {
                var lecturer = _context.Lecturers.Where(x => x.Id == item.LecturerId).Single();
                item.Lecturer = Mapper.Map<LecturerViewModel>(lecturer);
                var classroom = _context.Classrooms.Where(x => x.Id == item.ClassroomId).Single();
                item.ClassRoom = Mapper.Map<ClassroomViewModel>(classroom);
                var languageClass = _context.LanguageClasses.Where(x => x.Id == item.LanguageClassId).Single();
                item.LanguageClass = Mapper.Map<LanguageClassViewModel>(languageClass);
            }

            return await Task.FromResult(schedulesViewModel);
        }

        // GET: api/TeachingSchedules/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TeachingSchedule>> GetTeachingSchedule(int id)
        {
            var teachingSchedule = await _context.TeachingSchedules.FindAsync(id);

            if (teachingSchedule == null)
            {
                return NotFound();
            }

            return teachingSchedule;
        }

        // PUT: api/TeachingSchedules/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTeachingSchedule(int id, TeachingSchedule teachingSchedule)
        {
            if (id != teachingSchedule.Id)
            {
                return BadRequest();
            }

            _context.Entry(teachingSchedule).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TeachingScheduleExists(id))
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

        // POST: api/TeachingSchedules
        [HttpPost]
        public async Task<ActionResult<TeachingSchedule>> PostTeachingSchedule(TeachingSchedule teachingSchedule)
        {
            _context.TeachingSchedules.Add(teachingSchedule);
            /*Tạo các buổi học gắn vào lịch giảng*/
            string[] day = teachingSchedule.Note.Split(',');



            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTeachingSchedule", new { id = teachingSchedule.Id }, teachingSchedule);
        }

        // DELETE: api/TeachingSchedules/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TeachingSchedule>> DeleteTeachingSchedule(int id)
        {
            var teachingSchedule = await _context.TeachingSchedules.FindAsync(id);
            if (teachingSchedule == null)
            {
                return NotFound();
            }

            _context.TeachingSchedules.Remove(teachingSchedule);
            await _context.SaveChangesAsync();

            return teachingSchedule;
        }

        private bool TeachingScheduleExists(int id)
        {
            return _context.TeachingSchedules.Any(e => e.Id == id);
        }
    }
}
