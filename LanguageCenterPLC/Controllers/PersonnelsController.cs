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
using LanguageCenterPLC.Application.ViewModels.Categories;

namespace LanguageCenterPLC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonnelsController : ControllerBase
    {
        private readonly IPersonnelService _personnelService;

        public PersonnelsController(IPersonnelService personnelService)
        {
            _personnelService = personnelService;
        }

        // GET: api/Personnels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PersonnelViewModel>>> GetPersonnels()
        {
            return await Task.FromResult(_personnelService.GetAll());
        }

        // GET: api/Personnels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PersonnelViewModel>> GetPersonnel(string id)
        {
            var personnel = _personnelService.GetById(id);

            if (personnel == null)
            {
                return NotFound("Không tìm thấy id = " + id);
            }

            return await Task.FromResult(personnel);
        }

        // PUT: api/Personnels/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPersonnel(string id, PersonnelViewModel personnel)
        {
            if (personnel.Id != id)
            {
                throw new Exception(string.Format("Id và Id của lớp học không giống nhau!"));
            }

            try
            {
                await Task.Run(() =>
                {
                    personnel.DateModified = DateTime.Now;
                    _personnelService.Update(personnel);
                    _personnelService.SaveChanges();
                    return Ok("Cập nhập thành công!");
                });

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonnelExists(id))
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

        // POST: api/Personnels
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<PersonnelViewModel>> PostPersonnel(PersonnelViewModel personnel)
        {
            if (personnel != null)
            {
                try
                {
                    await Task.Run(() =>
                    {
                        _personnelService.Add(personnel);
                        _personnelService.SaveChanges();
                        return Ok("Thêm nhân viên thành công!");
                    });

                }
                catch
                {

                    throw new Exception(string.Format("Lỗi khi thêm dữ liệu"));
                }
            }

            return CreatedAtAction("GetPersonnel", new { id = personnel.Id }, personnel);
        }


        [HttpPost("/api/Personnels/get-all-with-conditions/")]
        public async Task<ActionResult<IEnumerable<PersonnelViewModel>>> GetAllConditions(string keyword = "", int status = 2)
        {
            return await Task.FromResult(_personnelService.GetAllWithConditions(keyword, status));
        }
        // DELETE: api/Personnels/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PersonnelViewModel>> DeletePersonnel(string id)
        {
            var personnel = _personnelService.GetById(id);
            if (personnel == null)
            {
                return NotFound("Không tìm thấy Id = " + id);
            }

            try
            {
                await Task.Run(() =>
                {
                    _personnelService.Delete(id);
                    _personnelService.SaveChanges();
                });
            }
            catch
            {

                throw new Exception(string.Format("Có lỗi xảy ra không thể xóa!"));
            }

            return Ok();
        }

        private bool PersonnelExists(string id)
        {
            return _personnelService.IsExists(id);
        }
    }
}
