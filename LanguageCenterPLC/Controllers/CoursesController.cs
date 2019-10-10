﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LanguageCenterPLC.Data.EF;
using LanguageCenterPLC.Data.Entities;
using LanguageCenterPLC.Infrastructure.Enums;
using LanguageCenterPLC.Application.Implementation;
using LanguageCenterPLC.Application.ViewModels;
using LanguageCenterPLC.Application.Interfaces;
using LanguageCenterPLC.Utilities.Dtos;

namespace LanguageCenterPLC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        //private readonly AppDbContext _context;

        private readonly ICourseService _courseService;

        public CoursesController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        // GET: api/Courses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CourseViewModel>>> GetCourses()
        {
            return await Task.FromResult(_courseService.GetAll());
        }

        // GET: api/Courses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CourseViewModel>> GetCourse(int id)
        {
            var course = _courseService.GetById(id);

            if (course == null)
            {
                return NotFound("Không tìm thấy khóa học có id = " + id);
            }

            return await Task.FromResult(course);
        }

        // PUT: api/Courses/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCourse(int id, CourseViewModel course)
        {
            if (course.Id != id)
            {
                throw new Exception(string.Format("Id và Id của khóa học không giống nhau!"));
            }

            try
            {
                await Task.Run(() =>
                {
                    course.DateModified = DateTime.Now;
                    _courseService.Update(course);
                    _courseService.SaveChanges();
                    return Ok("Cập nhập khóa học thành công!");
                });
                
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CourseExists(id))
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

        // POST: api/Courses
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<CourseViewModel>> PostCourse(CourseViewModel course)
        {
            if (course != null)
            {
                try
                {
                    await Task.Run(() =>
                    {
                        course.DateCreated = DateTime.Now;
                        course.DateModified = DateTime.Now;
                        _courseService.Add(course);
                        _courseService.SaveChanges();
                        return Ok("thêm khóa học thành công!");
                    });

                }
                catch
                {

                    throw new Exception(string.Format("Lỗi khi thêm dữ liệu"));
                }

            }

            return CreatedAtAction("GetCourse", new { id = course.Id }, course);
        }

        [HttpPost("/api/Course/paging")]
        public async Task<ActionResult<PagedResult<CourseViewModel>>> PagingCourse(string keyword = "", int status = 0, int pageSize = 10, int pageIndex = 0)
        {
            try
            {
                return await Task.FromResult(_courseService.GetAllPaging(keyword, status, pageSize, pageIndex));
            }
            catch
            {
                throw new Exception(string.Format("Lỗi xảy ra ở phân trang!"));
            }


        }

        // DELETE: api/Courses/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<CourseViewModel>> DeleteCourse(int id)
        {

            var course = _courseService.GetById(id);
            if (course == null)
            {
                return NotFound("Không tìm thấy khóa học có Id = " + id);
            }

            try
            {
                await Task.Run(() =>
                {
                    _courseService.Delete(id);
                    _courseService.SaveChanges();
                });
            }
            catch
            {

                throw new Exception(string.Format("Có lỗi xảy ra không thể xóa!"));
            }

            return course;
        }

        private bool CourseExists(int id)
        {
            return _courseService.IsExists(id);
        }
    }
}
