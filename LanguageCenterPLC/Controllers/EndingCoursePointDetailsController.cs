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
    public class EndingCoursePointDetailsController : ControllerBase
    {
        private readonly IEndingCoursePointDetailService _endingCoursePointDetailService;
        public EndingCoursePointDetailsController(IEndingCoursePointDetailService endingCoursePointDetailService)
        {
            _endingCoursePointDetailService = endingCoursePointDetailService;
        }

        // GET: api/EndingCoursePointDetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EndingCoursePointDetailViewModel>>> GetEndingCoursePointDetails()
        {
            return await Task.FromResult(_endingCoursePointDetailService.GetAll());
        }

        // GET: api/EndingCoursePointDetails/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EndingCoursePointDetailViewModel>> GetEndingCoursePointDetail(int id)
        {
            var endingCoursePointDetail = _endingCoursePointDetailService.GetById(id);

            if (endingCoursePointDetail == null)
            {
                return NotFound("Không tìm thấy id = " + id);
            }

            return await Task.FromResult(endingCoursePointDetail);
        }

        // PUT: api/EndingCoursePointDetails/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEndingCoursePointDetail(int id, EndingCoursePointDetailViewModel endingCoursePointDetail, Guid userId)
        {
            if (endingCoursePointDetail.Id != id)
            {
                throw new Exception(string.Format("Id và Id của giáo viên không giống nhau!"));
            }

            try
            {
                await Task.Run(() =>
                {
                    _endingCoursePointDetailService.Update(endingCoursePointDetail,userId);
                    _endingCoursePointDetailService.SaveChanges();
                    return Ok("Cập nhập thành công!");
                });

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EndingCoursePointDetailExists(id))
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

        // POST: api/EndingCoursePointDetails
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<EndingCoursePointDetailViewModel>> PostEndingCoursePointDetail(EndingCoursePointDetailViewModel endingCoursePointDetail)
        {
            if (endingCoursePointDetail != null)
            {
                try
                {
                    await Task.Run(() =>
                    {
                        _endingCoursePointDetailService.Add(endingCoursePointDetail);
                        _endingCoursePointDetailService.SaveChanges();
                        return Ok("Thêm giáo viên thành công!");
                    });

                }
                catch
                {

                    throw new Exception(string.Format("Lỗi khi thêm dữ liệu"));
                }

            }

            return CreatedAtAction("GetEndingCoursePointDetails()", new { id = endingCoursePointDetail.Id }, endingCoursePointDetail);
        }
        /// <summary>
        ///  Add all leaner
        /// </summary>
        /// <returns></returns>
        [HttpPost("/api/EndingCoursePointDetails/post-ending-point-conditions")]
        public async Task<ActionResult<EndingCoursePointDetailViewModel>> PostEndingPointDetailAll()
        {
            try
            {
                await Task.Run(() =>
                {
                    _endingCoursePointDetailService.AddRange();
                    _endingCoursePointDetailService.SaveChanges();
                    return Ok("Thêm giáo viên thành công!");
                });

            }
            catch
            {

                throw new Exception(string.Format("Lỗi khi thêm dữ liệu"));
            }
            return Ok();
        }


        [HttpPost]
        [Route("get-all-with-conditions")]
        public async Task<ActionResult<IEnumerable<EndingCoursePointDetailViewModel>>> GetAllConditions( int endingPointId)
        {
            return await Task.FromResult(_endingCoursePointDetailService.GetAllWithConditions(endingPointId));
        }


        // DELETE: api/EndingCoursePointDetails/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<EndingCoursePointDetailViewModel>> DeleteEndingCoursePointDetail(int id)
        {
            var endingCoursePointDetail = _endingCoursePointDetailService.GetById(id);
            if (endingCoursePointDetail == null)
            {
                return NotFound("Không tìm thấy Id = " + id);
            }

            try
            {
                await Task.Run(() =>
                {
                    _endingCoursePointDetailService.Delete(id);
                    _endingCoursePointDetailService.SaveChanges();
                });
            }
            catch
            {

                throw new Exception(string.Format("Có lỗi xảy ra không thể xóa!"));
            }

            return Ok();
        }

        private bool EndingCoursePointDetailExists(int id)
        {
            return _endingCoursePointDetailService.IsExists(id);
        }
    }
}
