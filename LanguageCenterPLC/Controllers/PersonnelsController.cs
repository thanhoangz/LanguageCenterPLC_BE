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
using AutoMapper;

namespace LanguageCenterPLC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonnelsController : ControllerBase
    {
        private readonly IPersonnelService _personnelService;
        private readonly AppDbContext _context;
        public PersonnelsController(IPersonnelService personnelService, AppDbContext context)
        {
            _personnelService = personnelService;
            _context = context;
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

        // GET: api/Personnels/get-by-cardid
        [HttpGet("/api/Personnels/get-by-cardid/{keyword}")]
        public async Task<ActionResult<PersonnelViewModel>> GetByCardId(string keyword)
        {
            return await Task.FromResult(_personnelService.GetByCardId(keyword));
        }

        // PUT: api/Personnels/5
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
        public async Task<ActionResult<IEnumerable<PersonnelViewModel>>> GetAllConditions(string keyword = "", string position = "", int status = 2)
        {
            return await Task.FromResult(_personnelService.GetAllWithConditions(keyword, position, status));
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


        [HttpGet]
        [Route("paied-roll-personnels")]
        public async Task<ActionResult<IEnumerable<PersonnelViewModel>>> PaiedPersonnels(int month, int year)
        {
            var salaryPaies = _context.SalaryPays.Where(x => x.Month == month && x.Year == year).ToList();
            if (salaryPaies.Count != 0)
            {
                var personnels = new List<Personnel>();
                foreach (var item in salaryPaies)
                {
                    if (item.PersonnelId != null)
                    {
                        Personnel personnel = _context.Personnels.Find(item.PersonnelId);
                        if (personnel != null)
                            personnels.Add(personnel);
                    }
                }
                var personnelsViewModel = Mapper.Map<List<PersonnelViewModel>>(personnels);

                return await Task.FromResult(personnelsViewModel);
            }
            else
            {
                List<PersonnelViewModel> empty = new List<PersonnelViewModel>();
                return await Task.FromResult(empty);
            }
        }

        [HttpGet]
        [Route("not-paied-roll-personnels")]
        public async Task<ActionResult<IEnumerable<PersonnelViewModel>>> NotPaiedPersonnels(int month, int year)
        {
            var salaryPaies = _context.SalaryPays.Where(x => x.Month == month && x.Year == year).ToList();
            if (salaryPaies.Count != 0)
            {
                var personnels = new List<Personnel>();
                foreach (var item in salaryPaies)
                {
                    if (item.PersonnelId != null)
                    {
                        Personnel personnel = _context.Personnels.Find(item.PersonnelId);
                        if (personnel != null)
                            personnels.Add(personnel);
                    }
                }

                var results = _context.Personnels.ToList();
                foreach (var item in personnels)
                {
                    results.Remove(item);
                }

                var personnelsViewModel = Mapper.Map<List<PersonnelViewModel>>(results);

                return await Task.FromResult(personnelsViewModel);
            }
            else
            {
                List<PersonnelViewModel> empty = new List<PersonnelViewModel>();
                return await Task.FromResult(empty);
            }
        }


    }
}
