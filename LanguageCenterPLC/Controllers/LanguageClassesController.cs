using LanguageCenterPLC.Application.Interfaces;
using LanguageCenterPLC.Application.ViewModels.Categories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LanguageCenterPLC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LanguageClassesController : ControllerBase
    {
        ILanguageClassService _languageClassService;

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
            var languageClass = _languageClassService.GetById(id);

            if (languageClass == null)
            {
                return NotFound("Không tìm thấy lớp học có id = " + id);
            }

            return await Task.FromResult(languageClass);
        }

        // PUT: api/LanguageClasses/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLanguageClass(string id, LanguageClassViewModel languageClassViewModel)
        {
            if (languageClassViewModel.Id != id)
            {
                throw new Exception(string.Format("Id và Id của lớp học không giống nhau!"));
            }

            try
            {
                await Task.Run(() =>
                {
                    languageClassViewModel.DateModified = DateTime.Now;
                    _languageClassService.Update(languageClassViewModel);
                    _languageClassService.SaveChanges();
                    return Ok();
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
                        return Ok("thêm lớp học thành công!");
                    });

                }
                catch
                {

                    throw new Exception(string.Format("Lỗi khi thêm dữ liệu"));
                }

            }

            return CreatedAtAction("GetCourse", new { id = languageClass.Id }, languageClass);
        }


        [HttpPost("/api/LanguageClasses/get-all-with-conditions")]
        public async Task<ActionResult<IEnumerable<LanguageClassViewModel>>> GetAllConditions(DateTime? dateTime,string keyword = "", int status = 1)
        {
            return await Task.FromResult(_languageClassService.GetAllWithConditions(dateTime, keyword, status));
        }

        // DELETE: api/LanguageClasses/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<LanguageClassViewModel>> DeleteLanguageClass(string id)
        {
            var languageClass = _languageClassService.GetById(id);
            if (languageClass == null)
            {
                return NotFound("Không tìm thấy khóa học có Id = " + id);
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
