using LanguageCenterPLC.Application.Interfaces;
using LanguageCenterPLC.Application.ViewModels.Categories;
using LanguageCenterPLC.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LanguageCenterPLC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GuestTypesController : ControllerBase
    {
        private readonly IGuestTypeService _guestTypeService;

        public GuestTypesController(IGuestTypeService guestTypeService)
        {
            _guestTypeService = guestTypeService;
        }

        // GET: api/GuestTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GuestTypeViewModel>>> GetGuestTypes()
        {
            return await Task.FromResult(_guestTypeService.GetAll());
        }

        // GET: api/GuestTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GuestTypeViewModel>> GetGuestType(int id)
        {
            var guestType = _guestTypeService.GetById(id);

            if (guestType == null)
            {
                return NotFound("Không tìm thấy khóa học có id = " + id);
            }

            return await Task.FromResult(guestType);
        }

        // PUT: api/GuestTypes/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGuestType(int id, GuestTypeViewModel guestType)
        {
            if (guestType.Id != id)
            {
                throw new Exception(string.Format("Id và Id của khóa học không giống nhau!"));
            }

            try
            {
                await Task.Run(() =>
                {
                    guestType.DateModified = DateTime.Now;
                    _guestTypeService.Update(guestType);
                    _guestTypeService.SaveChanges();
                    return Ok();
                });

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GuestTypeExists(id))
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

        // POST: api/GuestTypes
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<GuestTypeViewModel>> PostGuestType(GuestTypeViewModel guestType)
        {
            if (guestType != null)
            {
                try
                {
                    await Task.Run(() =>
                    {
                        guestType.DateCreated = DateTime.Now;
                        _guestTypeService.Add(guestType);
                        _guestTypeService.SaveChanges();
                        return Ok("thêm khóa học thành công!");
                    });

                }
                catch
                {

                    throw new Exception(string.Format("Lỗi khi thêm dữ liệu"));
                }

            }

            return CreatedAtAction("GetGuestType", new { id = guestType.Id }, guestType);
        }

        [HttpPost("/api/GuestTypes/get-all-with-conditions")]
        public async Task<ActionResult<IEnumerable<GuestTypeViewModel>>> GetAllConditions(string keyword = "", int status = 1)
        {
            return await Task.FromResult(_guestTypeService.GetAllWithConditions(keyword, status));
        }


        // DELETE: api/GuestTypes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<GuestType>> DeleteGuestType(int id)
        {
            var guestType = _guestTypeService.GetById(id);
            if (guestType == null)
            {
                return NotFound("Không tìm thấy khóa học có Id = " + id);
            }

            try
            {
                await Task.Run(() =>
                {
                    _guestTypeService.Delete(id);
                    _guestTypeService.SaveChanges();
                });
            }
            catch
            {

                throw new Exception(string.Format("Có lỗi xảy ra không thể xóa!"));
            }

            return Ok();
        }

        private bool GuestTypeExists(int id)
        {
            return _guestTypeService.IsExists(id);
        }
    }
}
