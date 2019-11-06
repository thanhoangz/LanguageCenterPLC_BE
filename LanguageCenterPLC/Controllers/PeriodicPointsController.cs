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
    public class PeriodicPointsController : ControllerBase
    {
        private readonly IPeriodicPointService _periodicPointService;

        public PeriodicPointsController(IPeriodicPointService periodicPointService)
        {
            _periodicPointService = periodicPointService;
        }

        // GET: api/PeriodicPoints
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PeriodicPointViewModel>>> GetPeriodicPoints()
        {
            return await Task.FromResult(_periodicPointService.GetAll());
        }

        // GET: api/PeriodicPoints/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PeriodicPointViewModel>> GetPeriodicPoint(int id)
        {
            var periodicPoint = _periodicPointService.GetById(id);

            if (periodicPoint == null)
            {
                return NotFound("Không tìm thấy id = " + id);
            }

            return await Task.FromResult(periodicPoint);
        }

        // PUT: api/PeriodicPoints/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPeriodicPoint(int id, PeriodicPointViewModel periodicPoint)
        {
            if (periodicPoint.Id != id)
            {
                throw new Exception(string.Format("Id và Id của giáo viên không giống nhau!"));
            }

            try
            {
                await Task.Run(() =>
                {
                    _periodicPointService.Update(periodicPoint);
                    _periodicPointService.SaveChanges();
                    return Ok("Cập nhập thành công!");
                });

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PeriodicPointExists(id))
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

        // POST: api/PeriodicPoints
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<PeriodicPointViewModel>> PostPeriodicPoint(PeriodicPointViewModel periodicPoint , Guid userId)
        {
            if (periodicPoint != null)
            {
                try
                {
                    await Task.Run(() =>
                    {
                        periodicPoint.DateCreated = DateTime.Now;
                        periodicPoint.DateOnPoint = DateTime.Now;

                        periodicPoint.AppUserId = userId;
                        _periodicPointService.Add(periodicPoint);
                        _periodicPointService.SaveChanges();
                        return Ok("Thêm giáo viên thành công!");
                    });

                }
                catch
                {

                    throw new Exception(string.Format("Lỗi khi thêm dữ liệu"));
                }

            }

            return CreatedAtAction("GetPeriodicPoints()", new { id = periodicPoint.Id }, periodicPoint);
        }


        [HttpPost]
        [Route("get-all-with-conditions")]
        public async Task<ActionResult<IEnumerable<PeriodicPointViewModel>>> GetAllConditions(string languageClassId)
        {
            return await Task.FromResult(_periodicPointService.GetAllWithConditions(languageClassId));
        }


        // DELETE: api/PeriodicPoints/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PeriodicPointViewModel>> DeletePeriodicPoint(int id)
        {
            var periodicPoint = _periodicPointService.GetById(id);
            if (periodicPoint == null)
            {
                return NotFound("Không tìm thấy Id = " + id);
            }

            try
            {
                await Task.Run(() =>
                {
                    _periodicPointService.Delete(id);
                    _periodicPointService.SaveChanges();
                });
            }
            catch
            {

                throw new Exception(string.Format("Có lỗi xảy ra không thể xóa!"));
            }

            return Ok();
        }

        private bool PeriodicPointExists(int id)
        {
            return _periodicPointService.IsExists(id);
        }
    }
}
