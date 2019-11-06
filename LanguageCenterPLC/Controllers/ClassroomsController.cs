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
using LanguageCenterPLC.Application.ViewModels.Studies;
using LanguageCenterPLC.Utilities.Dtos;

namespace LanguageCenterPLC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassroomsController : ControllerBase
    {
        private readonly IClassroomService _classroomService;
        public ClassroomsController(IClassroomService classroomService)
        {
            _classroomService = classroomService;
        }

        // GET: api/Classrooms
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClassroomViewModel>>> GetClassrooms()
        {
            return await Task.FromResult(_classroomService.GetAll());
        }

        // GET: api/Classrooms/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ClassroomViewModel>> GetClassroom(int id)
        {
            var classRoom = _classroomService.GetById(id);

            if (classRoom == null)
            {
                return NotFound("Không tìm thấy id = " + id);
            }

            return await Task.FromResult(classRoom);
        }

        // PUT: api/Classrooms/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClassroom(int id, ClassroomViewModel classroom)
        {
            if (classroom.Id != id)
            {
                throw new Exception(string.Format("Id và Id của lớp học không giống nhau!"));
            }

            try
            {
                await Task.Run(() =>
                {
                    classroom.DateModified = DateTime.Now;
                    _classroomService.Update(classroom);
                    _classroomService.SaveChanges();
                    return Ok("Cập nhập thành công!");
                });

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClassroomExists(id))
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

        // POST: api/Classrooms
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<ClassroomViewModel>> PostClassroom(ClassroomViewModel classroom)
        {
            if(classroom != null)
            {
                try
                {
                    await Task.Run(() =>
                    {
                        classroom.DateCreated = DateTime.Now;
                        _classroomService.Add(classroom);
                        _classroomService.SaveChanges();
                        return Ok("Thêm phòng học thành công!");
                    });

                }
                catch
                {

                    throw new Exception(string.Format("Lỗi khi thêm dữ liệu"));
                }

            }

            return CreatedAtAction("GetReceiptType", new { id = classroom.Id }, classroom);
        }
        [HttpPost("/api/Classrooms/paging")]
        public async Task<ActionResult<PagedResult<ClassroomViewModel>>> PagingCourse(string keyword = "", int pageSize = 10, int pageIndex = 0)
        {
            try
            {
                return await Task.FromResult(_classroomService.GetAllPaging(keyword, pageSize, pageIndex));
            }
            catch
            {
                throw new Exception(string.Format("Lỗi xảy ra ở phân trang!"));
            }
        }

        [HttpPost("/api/Classrooms/get-all-with-conditions")]
        public async Task<ActionResult<IEnumerable<ClassroomViewModel>>> GetAllConditions(string keyword = "", int status = -1)
        {
            return await Task.FromResult(_classroomService.GetAllWithConditions(keyword, status));
        }

        // DELETE: api/Classrooms/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ClassroomViewModel>> DeleteClassroom(int id)
        {
            var classRoom = _classroomService.GetById(id);
            if (classRoom == null)
            {
                return NotFound("Không tìm thấy Id = " + id);
            }

            try
            {
                await Task.Run(() =>
                {
                    _classroomService.Delete(id);
                    _classroomService.SaveChanges();
                });
            }
            catch
            {

                throw new Exception(string.Format("Có lỗi xảy ra không thể xóa!"));
            }

            return Ok();
        }

        private bool ClassroomExists(int id)
        {
            return _classroomService.IsExists(id);
        }
    }
}
