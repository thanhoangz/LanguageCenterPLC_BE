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
    public class PeriodicPointDetailsController : ControllerBase
    {
        private readonly IPeriodicPointDetailService _periodicPointDetailService;

        public PeriodicPointDetailsController(IPeriodicPointDetailService periodicPointDetailService)
        {
            _periodicPointDetailService = periodicPointDetailService;
        }

        // GET: api/PeriodicPointDetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PeriodicPointDetailViewModel>>> GetPeriodicPointDetails()
        {
            return await Task.FromResult(_periodicPointDetailService.GetAll());
        }

        // GET: api/PeriodicPointDetails/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PeriodicPointDetailViewModel>> GetPeriodicPointDetail(int id)
        {
            var periodicPointDetail = _periodicPointDetailService.GetById(id);

            if (periodicPointDetail == null)
            {
                return NotFound("Không tìm thấy id = " + id);
            }

            return await Task.FromResult(periodicPointDetail);
        }

        // PUT: api/PeriodicPointDetails/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPeriodicPointDetail(int id, PeriodicPointDetailViewModel periodicPointDetail, string classId, Guid userId)
        {
            if (periodicPointDetail.Id != id)
            {
                throw new Exception(string.Format("Id và Id của giáo viên không giống nhau!"));
            }

            try
            {
                await Task.Run(() =>
                {
                    _periodicPointDetailService.Update(periodicPointDetail, classId , userId);
                    _periodicPointDetailService.SaveChanges();
                    return Ok("Cập nhập thành công!");
                });

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PeriodicPointDetailExists(id))
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

        // POST: api/PeriodicPointDetails
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<PeriodicPointDetailViewModel>> PostPeriodicPointDetail(PeriodicPointDetailViewModel periodicPointDetail)
        {
            if (periodicPointDetail != null)
            {
                try
                {
                    await Task.Run(() =>
                    {
                        _periodicPointDetailService.Add(periodicPointDetail);
                        _periodicPointDetailService.SaveChanges();
                        return Ok("Thêm giáo viên thành công!");
                    });

                }
                catch
                {

                    throw new Exception(string.Format("Lỗi khi thêm dữ liệu"));
                }

            }

            return CreatedAtAction("GetPeriodicPointDetails()", new { id = periodicPointDetail.Id }, periodicPointDetail);
        }

        [HttpPost("/api/PeriodicPointDetails/post-periodic-point-conditions")]
        public async Task<ActionResult<PeriodicPointDetailViewModel>> PostPeriodicPointDetailAll()
        {       
                try
                {
                    await Task.Run(() =>
                    {
                        _periodicPointDetailService.AddRange();
                        _periodicPointDetailService.SaveChanges();
                        return Ok("Thêm giáo viên thành công!");
                    });

                }
                catch
                {

                    throw new Exception(string.Format("Lỗi khi thêm dữ liệu"));
                }
            return Ok();
        }

        // DELETE: api/PeriodicPointDetails/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PeriodicPointDetailViewModel>> DeletePeriodicPointDetail(int id)
        {
            var periodicPointDetail = _periodicPointDetailService.GetById(id);
            if (periodicPointDetail == null)
            {
                return NotFound("Không tìm thấy Id = " + id);
            }

            try
            {
                await Task.Run(() =>
                {
                    _periodicPointDetailService.Delete(id);
                    _periodicPointDetailService.SaveChanges();
                });
            }
            catch
            {

                throw new Exception(string.Format("Có lỗi xảy ra không thể xóa!"));
            }

            return Ok();
        }

        // get all theo điều kiện
        [HttpPost("/api/PeriodicPointDetails/get-all-with-conditions")]
        public async Task<ActionResult<IEnumerable<PeriodicPointDetailViewModel>>> GetAllConditions(int periodicPointId)
        {
            return await Task.FromResult(_periodicPointDetailService.GetAllWithConditions(periodicPointId));
        }
        private bool PeriodicPointDetailExists(int id)
        {
            return _periodicPointDetailService.IsExists(id);
        }
    }
}
