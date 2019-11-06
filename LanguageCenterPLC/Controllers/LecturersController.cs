﻿using LanguageCenterPLC.Application.Interfaces;
using LanguageCenterPLC.Application.ViewModels.Studies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LanguageCenterPLC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LecturersController : ControllerBase
    {
        private readonly ILecturerService _lecturerService;

        public LecturersController(ILecturerService lecturerService)
        {
            _lecturerService = lecturerService;
        }

        // GET: api/Lecturers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LecturerViewModel>>> GetLecturers()
        {
            return await Task.FromResult(_lecturerService.GetAll());

        }

        // GET: api/Lecturers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LecturerViewModel>> GetLecturer(int id)
        {
            var lecturers = _lecturerService.GetById(id);

            if (lecturers == null)
            {
                return NotFound("Không tìm thấy id = " + id);
            }

            return await Task.FromResult(lecturers);
        }

        // PUT: api/Lecturers/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLecturer(int id, LecturerViewModel lecturer)
        {
            if (lecturer.Id != id)
            {
                throw new Exception(string.Format("Id và Id của giáo viên không giống nhau!"));
            }

            try
            {
                await Task.Run(() =>
                {
                    _lecturerService.Update(lecturer);
                    _lecturerService.SaveChanges();
                    return Ok("Cập nhập thành công!");
                });

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LecturerExists(id))
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

        // POST: api/Lecturers
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<LecturerViewModel>> PostLecturer(LecturerViewModel lecturer)
        {
            if (lecturer != null)
            {
                try
                {
                    await Task.Run(() =>
                    {
                        _lecturerService.Add(lecturer);
                        _lecturerService.SaveChanges();
                        return Ok("Thêm giáo viên thành công!");
                    });

                }
                catch
                {

                    throw new Exception(string.Format("Lỗi khi thêm dữ liệu"));
                }

            }

            return CreatedAtAction("GetLecturer()", new { id = lecturer.Id }, lecturer);
        }



        [HttpPost]
        [Route("get-all-with-conditions")]
        public async Task<ActionResult<IEnumerable<LecturerViewModel>>> GetAllConditions(string keyword = "", string position = "", int status = -1)
        {
            return await Task.FromResult(_lecturerService.GetAllWithConditions(keyword, position, status));
        }

        // DELETE: api/Lecturers/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<LecturerViewModel>> DeleteLecturer(int id)
        {
            var lecturer = _lecturerService.GetById(id);
            if (lecturer == null)
            {
                return NotFound("Không tìm thấy Id = " + id);
            }

            try
            {
                await Task.Run(() =>
                {
                    _lecturerService.Delete(id);
                    _lecturerService.SaveChanges();
                });
            }
            catch
            {

                throw new Exception(string.Format("Có lỗi xảy ra không thể xóa!"));
            }

            return Ok();
        }

        private bool LecturerExists(int id)
        {
            return _lecturerService.IsExists(id);
        }
    }
}
