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

namespace LanguageCenterPLC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EndingCoursePointsController : ControllerBase
    {
        private readonly IEndingCoursePointService _endingCoursePointService;

        public EndingCoursePointsController(IEndingCoursePointService endingCoursePointService)
        {
            _endingCoursePointService = endingCoursePointService;
        }

        // GET: api/EndingCoursePoints
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EndingCoursePointViewModel>>> GetEndingCoursePoints()
        {
            return await Task.FromResult(_endingCoursePointService.GetAll());
        }

        // GET: api/EndingCoursePoints/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EndingCoursePointViewModel>> GetEndingCoursePoint(int id)
        {
            var endingCoursePoint = _endingCoursePointService.GetById(id);

            if (endingCoursePoint == null)
            {
                return NotFound("Không tìm thấy id = " + id);
            }

            return await Task.FromResult(endingCoursePoint);
        }

        // PUT: api/EndingCoursePoints/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEndingCoursePoint(int id, EndingCoursePointViewModel endingCoursePoint)
        {
            if (endingCoursePoint.Id != id)
            {
                throw new Exception(string.Format("Id và Id của giáo viên không giống nhau!"));
            }

            try
            {
                await Task.Run(() =>
                {
                    _endingCoursePointService.Update(endingCoursePoint);
                    _endingCoursePointService.SaveChanges();
                    return Ok("Cập nhập thành công!");
                });

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EndingCoursePointExists(id))
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

        // POST: api/EndingCoursePoints
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<EndingCoursePointViewModel>> PostEndingCoursePoint(EndingCoursePointViewModel endingCoursePoint, Guid userId)
        {
            if (endingCoursePoint != null)
            {
                try
                {
                    await Task.Run(() =>
                    {
                        endingCoursePoint.DateModified = DateTime.Now;
                        endingCoursePoint.DateOnPoint = DateTime.Now;
                        endingCoursePoint.AppUserId = userId;
                        _endingCoursePointService.Add(endingCoursePoint);
                        _endingCoursePointService.SaveChanges();
                        return Ok("Thêm giáo viên thành công!");
                    });

                }
                catch
                {

                    throw new Exception(string.Format("Lỗi khi thêm dữ liệu"));
                }

            }

            return CreatedAtAction("GetEndingCoursePoints()", new { id = endingCoursePoint.Id }, endingCoursePoint);
        }

        [HttpPost]
        [Route("get-all-with-conditions")]
        public async Task<ActionResult<EndingCoursePointViewModel>> GetAllConditions(string languageClassId)
        {
            return await Task.FromResult(_endingCoursePointService.GetAllWithConditions(languageClassId));
        }
        // DELETE: api/EndingCoursePoints/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<EndingCoursePointViewModel>> DeleteEndingCoursePoint(int id)
        {
            var endingCoursePoint = _endingCoursePointService.GetById(id);
            if (endingCoursePoint == null)
            {
                return NotFound("Không tìm thấy Id = " + id);
            }

            try
            {
                await Task.Run(() =>
                {
                    _endingCoursePointService.Delete(id);
                    _endingCoursePointService.SaveChanges();
                });
            }
            catch
            {

                throw new Exception(string.Format("Có lỗi xảy ra không thể xóa!"));
            }

            return Ok();
        }

        private bool EndingCoursePointExists(int id)
        {
            return _endingCoursePointService.IsExists(id);
        }
    }
}
