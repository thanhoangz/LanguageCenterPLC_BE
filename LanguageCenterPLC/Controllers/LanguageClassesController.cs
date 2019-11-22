using AutoMapper;
using LanguageCenterPLC.Application.Interfaces;
using LanguageCenterPLC.Application.ViewModels.Categories;
using LanguageCenterPLC.Data.EF;
using LanguageCenterPLC.Data.Entities;
using LanguageCenterPLC.Infrastructure.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace LanguageCenterPLC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LanguageClassesController : ControllerBase
    {
        ILanguageClassService _languageClassService;
        private readonly AppDbContext _context;
        public LanguageClassesController(ILanguageClassService languageClassService, AppDbContext context)
        {
            _languageClassService = languageClassService;
            _context = context;
        }

        // GET: api/LanguageClasses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LanguageClassViewModel>>> GetLanguageClasses()
        {
            return await Task.FromResult(_languageClassService.GetAll());
        }


        [HttpGet("/api/LanguageClasses/get-class-action")]
        public async Task<ActionResult<IEnumerable<LanguageClassViewModel>>> GetLopHoatDong()
        {
            return await Task.FromResult(_languageClassService.LopHoatDong());
        }

        [HttpGet("/api/LanguageClasses/get-class-studied-with-learnerid/{learnerId}")]
        public async Task<ActionResult<IEnumerable<LanguageClassViewModel>>> GetClass_Learner_Studied(string learnerId = "")
        {
            return await Task.FromResult(_languageClassService.GetClass_Learner_Studied(learnerId));
        }


        [HttpPost]
        [Route("getallbycourse/{courseId}")]
        public async Task<ActionResult<IEnumerable<LanguageClassViewModel>>> GetLanguageClassesByCourse(int courseId)
        {
            return await Task.FromResult(_languageClassService.GetLanguageClassesByCourse(courseId));
        }

        [HttpPost]
        [Route("getall-status-bycourse/{courseId}")]
        public async Task<ActionResult<IEnumerable<LanguageClassViewModel>>> GetAllClassByCourseId(int courseId)
        {
            return await Task.FromResult(_languageClassService.GetAllClassByCourseId(courseId));
        }


        [HttpPost("/api/LanguageClasses/get-class-chuyen-lop")]
        public async Task<ActionResult<IEnumerable<LanguageClassViewModel>>> LopDeChuyen(string classId, int courseId)
        {
            return await Task.FromResult(_languageClassService.LopDeChuyen(classId, courseId));
        }


        //GET: api/LanguageClasses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LanguageClassViewModel>> GetLanguageClass(string id)
        {
            var languageClass = _languageClassService.GetById(id);

            if (languageClass == null)
            {
                return NotFound("Không tìm thấy lớp học có id = " + id);
            }

            return await Task.FromResult(languageClass);
        }

        // PUT: api/LanguageClasses/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLanguageClass(string id, LanguageClassViewModel languageClassViewModel)
        {
            if (languageClassViewModel.Id != id)
            {
                throw new Exception(string.Format("Id và Id của lớp học không giống nhau!"));
            }

            try
            {
                await Task.Run(() =>
                {
                    _languageClassService.Update(languageClassViewModel);
                    _languageClassService.SaveChanges();
                    return Ok();
                });

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
        [HttpPost]
        public async Task<ActionResult<LanguageClassViewModel>> PostLanguageClass(LanguageClassViewModel languageClass)
        {
            if (languageClass != null)
            {
                try
                {
                    await Task.Run(() =>
                    {
                        _languageClassService.Add(languageClass);
                        _languageClassService.SaveChanges();
                        return Ok("thêm lớp học thành công!");
                    });

                }
                catch
                {

                    throw new Exception(string.Format("Lỗi khi thêm dữ liệu"));
                }

            }

            return CreatedAtAction("GetCourse", new { id = languageClass.Id }, languageClass);
        }


        [HttpPost("/api/LanguageClasses/get-all-with-conditions")]
        public async Task<ActionResult<IEnumerable<LanguageClassViewModel>>> GetAllConditions(DateTime? start, DateTime? end, string keyword = "", int courseKeyword = -1, int status = 1)
        {
            return await Task.FromResult(_languageClassService.GetAllWithConditions(start, end, keyword, courseKeyword, status));
        }

        // DELETE: api/LanguageClasses/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<LanguageClassViewModel>> DeleteLanguageClass(string id)
        {
            var languageClass = _languageClassService.GetById(id);
            if (languageClass == null)
            {
                return NotFound("Không tìm thấy khóa học có Id = " + id);
            }

            try
            {
                await Task.Run(() =>
                {
                    _languageClassService.Delete(id);
                    _languageClassService.SaveChanges();
                });
            }
            catch
            {

                throw new Exception(string.Format("Có lỗi xảy ra không thể xóa!"));
            }

            return Ok();
        }

        private bool LanguageClassExists(string id)
        {
            return _languageClassService.IsExists(id);
        }

        [HttpGet]
        [Route("get-all-class-schedule/{courseId}")]
        public async Task<ActionResult<IEnumerable<LanguageClassViewModel>>> GetAllClassForSchedule(int courseId)
        {
            var scheduleForClass = _context.TeachingSchedules.Where(x => x.Status == Status.Pause);
            List<LanguageClass> classes = new List<LanguageClass>();
            foreach (var schedule in scheduleForClass)
            {
                classes.Add(_context.LanguageClasses.Find(schedule.LanguageClassId));
            }
            var result = classes.Where(x => x.CourseId == courseId).ToList();

            var resultVm = Mapper.Map<List<LanguageClassViewModel>>(result);
            return await Task.FromResult(resultVm);
        }

        [HttpGet]
        [Route("get-info-class/{classId}")]
        public async Task<Object> GetInfoOfClass(string classId)
        {
            var inforClass = _context.LanguageClasses.Find(classId);
            var schedule = _context.TeachingSchedules.Where(x => x.LanguageClassId == classId).SingleOrDefault();
            string scheduleStatus = "Chưa xếp lịch";
            string nameOfLecturer = "Chưa xếp giảng viên";
            if (schedule != null)
            {
                if (schedule.Status == Status.Active)
                {
                    scheduleStatus = "Đã xếp lịch";
                }
                if (schedule.Status == Status.InActive)
                {
                    scheduleStatus = "Khóa";
                }
                if (schedule.Status == Status.Pause)
                {
                    scheduleStatus = "Chờ xếp lịch";
                }

                var lecturer = _context.Lecturers.Where(x => x.Id == schedule.LecturerId).SingleOrDefault();
                if (lecturer != null)
                {
                    nameOfLecturer = lecturer.FirstName.Trim() + " " + lecturer.LastName.Trim();
                }
            }

          

            var number = (from l in _context.Learners
                          join st in _context.StudyProcesses on l.Id equals st.LearnerId
                          join c in _context.LanguageClasses on st.LanguageClassId equals c.Id
                          where st.LanguageClassId == classId && st.Status == Status.Active
                          select st).ToList().Count;

            var result = new
            {
                inforClass.Name,
                inforClass.Status,
                ScheduleStatus = scheduleStatus,
                NameOfLecturer = nameOfLecturer,
                NumberOfPeople = number,
                Time = inforClass.StartDay.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) + " - " + inforClass.EndDay.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)
            };

            return await Task.FromResult(result);
        }




        [HttpGet]
        [Route("get-same-class/{classId}")]
        public async Task<List<LanguageClassViewModel>> GetClassBySameCourse(string classId)
        {
            var languageClass = _context.LanguageClasses.Find(classId);
            List<LanguageClass> result = _context.LanguageClasses.Where(x => x.CourseId == languageClass.CourseId &&  x.Id != classId).ToList();

            var resultVm = Mapper.Map<List<LanguageClassViewModel>>(result);



            return await Task.FromResult(resultVm);
        }

    }
}
