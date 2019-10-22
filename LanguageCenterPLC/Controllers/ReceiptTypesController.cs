using LanguageCenterPLC.Application.Interfaces;
using LanguageCenterPLC.Application.ViewModels.Categories;
using LanguageCenterPLC.Utilities.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LanguageCenterPLC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReceiptTypesController : ControllerBase
    {

        private readonly IReceiptTypeService _receiptTypeService;

        public ReceiptTypesController(IReceiptTypeService receiptTypeService)
        {
            _receiptTypeService = receiptTypeService;
        }

        // GET: api/ReceiptTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReceiptTypeViewModel>>> GetReceiptTypes()
        {
            return await Task.FromResult(_receiptTypeService.GetAll());
        }

        // GET: api/ReceiptTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ReceiptTypeViewModel>> GetReceiptType(int id)
        {
            var receiptType = _receiptTypeService.GetById(id);

            if (receiptType == null)
            {
                return NotFound("Không tìm thấy id = " + id);
            }

            return await Task.FromResult(receiptType);
        }

        // PUT: api/ReceiptTypes/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReceiptType(int id, ReceiptTypeViewModel receiptType)
        {
            if (receiptType.Id != id)
            {
                throw new Exception(string.Format("Id và Id của loại thu không giống nhau!"));
            }

            try
            {
                await Task.Run(() =>
                {
                    receiptType.DateModified = DateTime.Now;
                    _receiptTypeService.Update(receiptType);
                    _receiptTypeService.SaveChanges();
                    return Ok("Cập nhập thành công!");
                });

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReceiptTypeExists(id))
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

        [HttpPost("/api/ReceiptTypes/paging")]
        public async Task<ActionResult<PagedResult<ReceiptTypeViewModel>>> PagingCourse(string keyword = "", int pageSize = 10, int pageIndex = 0)
        {
            try
            {
                return await Task.FromResult(_receiptTypeService.GetAllPaging(keyword, pageSize, pageIndex));
            }
            catch
            {
                throw new Exception(string.Format("Lỗi xảy ra ở phân trang!"));
            }
        }

        [HttpPost("/api/ReceiptTypes/get-all-with-conditions")]
        public async Task<ActionResult<IEnumerable<ReceiptTypeViewModel>>> GetAllConditions(string keyword = "", int status = 1)
        {
            return await Task.FromResult(_receiptTypeService.GetAllWithConditions(keyword, status));
        }

        // POST: api/ReceiptTypes
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<ReceiptTypeViewModel>> PostReceiptType(ReceiptTypeViewModel receiptType)
        {
            if (receiptType != null)
            {
                try
                {
                    await Task.Run(() =>
                    {
                        receiptType.DateCreated = DateTime.Now;
                        _receiptTypeService.Add(receiptType);
                        _receiptTypeService.SaveChanges();
                        return Ok("Thêm loại thu thành công!");
                    });

                }
                catch
                {

                    throw new Exception(string.Format("Lỗi khi thêm dữ liệu"));
                }

            }

            return CreatedAtAction("GetReceiptType", new { id = receiptType.Id }, receiptType);
        }

        // DELETE: api/ReceiptTypes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteReceiptType(int id)
        {
            var receiptType = _receiptTypeService.GetById(id);
            if (receiptType == null)
            {
                return NotFound("Không tìm thấy Id = " + id);
            }

            try
            {
                await Task.Run(() =>
                {
                    _receiptTypeService.Delete(id);
                    _receiptTypeService.SaveChanges();
                });
            }
            catch
            {

                throw new Exception(string.Format("Có lỗi xảy ra không thể xóa!"));
            }

            return Ok();
        }

        private bool ReceiptTypeExists(int id)
        {
            return _receiptTypeService.IsExists(id);
        }
    }
}
