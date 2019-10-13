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
    public class LanguageClassesController : ControllerBase
    {
        private readonly ILanguageClassService _languageClassService;

        public LanguageClassesController(ILanguageClassService languageClassService)
        {
            _languageClassService = languageClassService;
        }

        // GET: api/LanguageClasses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LanguageClassViewModel>>> GetLanguageClasses()
        {
            return await Task.FromResult(_languageClassService.GetAll());
        }

        //GET: api/LanguageClasses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LanguageClassViewModel>> GetLanguageClass(string id)
        {
            var receiptType = _languageClassService.GetById(id);

            if (receiptType == null)
            {
                return NotFound("Không tìm thấy id = " + id);
            }

            return await Task.FromResult(receiptType);
        }

        // PUT: api/LanguageClasses/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLanguageClass(string id, LanguageClassViewModel languageClass)
        {
            if (languageClass.Id != id)
            {
                throw new Exception(string.Format("Id và Id của loại thu không giống nhau!"));
            }

            try
            {
                await Task.Run(() =>
                {
                    languageClass.DateModified = DateTime.Now;
                    _languageClassService.Update(languageClass);
                    _languageClassService.SaveChanges();
                    return Ok("Cập nhập thành công!");
                });

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LanguageClassExists(id))
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

        // POST: api/LanguageClasses
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<LanguageClassViewModel>> PostLanguageClass(LanguageClassViewModel languageClass)
        {
            if (languageClass != null)
            {
                try
                {
                    await Task.Run(() =>
                    {
                        languageClass.DateCreated = DateTime.Now;
                        languageClass.DateModified = DateTime.Now;
                        _languageClassService.Add(languageClass);
                        _languageClassService.SaveChanges();
                        return Ok("Thêm lớp học thành công!");
                    });

                }
                catch
                {

                    throw new Exception(string.Format("Lỗi khi thêm dữ liệu"));
                }

            }

            return CreatedAtAction("GetLanguageClass", new { id = languageClass.Id }, languageClass);
        }

        // DELETE: api/LanguageClasses/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<LanguageClassViewModel>> DeleteLanguageClass(string id)
        {
            var receiptType = _languageClassService.GetById(id);
            if (receiptType == null)
            {
                return NotFound("Không tìm thấy Id = " + id);
            }

            try
            {
                await Task.Run(() =>
                {
                    _languageClassService.Delete(id);
                    _languageClassService.SaveChanges();
                });
            }
            catch
            {

                throw new Exception(string.Format("Có lỗi xảy ra không thể xóa!"));
            }

            return Ok();
        }

        private bool LanguageClassExists(string id)
        {
            return _languageClassService.IsExists(id);

        }
    }
}
