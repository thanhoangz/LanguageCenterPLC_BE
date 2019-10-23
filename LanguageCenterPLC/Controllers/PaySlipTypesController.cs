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
    public class PaySlipTypesController : ControllerBase
    {
        private readonly IPaySlipTypeService _paysliptypeService;

        public PaySlipTypesController(IPaySlipTypeService paySlipTypeService)
        {
            _paysliptypeService = paySlipTypeService;
        }


        // GET: api/PaySlipTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PaySlipTypeViewModel>>> GetPaySlipTypes()
        {
            return await Task.FromResult(_paysliptypeService.GetAll());
        }



        // GET: api/PaySlipTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PaySlipTypeViewModel>> GetPaySlipType(int id)
        {
            var paysliptype = _paysliptypeService.GetById(id);

            if (paysliptype == null)
            {
                return NotFound("Không tìm thấy khóa học có id = " + id);
            }

            return await Task.FromResult(paysliptype);
        }

        // PUT: api/PaySlipTypes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPaySlipType(int id, PaySlipTypeViewModel paySlipType)
        {
            if (paySlipType.Id != id)
            {
                throw new Exception(string.Format("Id và Id của loại phiếu chi không giống nhau!"));
            }

            try
            {
                await Task.Run(() =>
                {
                    paySlipType.DateModified = DateTime.Now;
                    _paysliptypeService.Update(paySlipType);
                    _paysliptypeService.SaveChanges();
                    return Ok();
                });

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PaySlipTypeExists(id))
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

        // POST: api/PaySlipTypes
        [HttpPost]
        public async Task<ActionResult<PaySlipTypeViewModel>> PostPaySlipType(PaySlipTypeViewModel paySlipType)
        {
            if (paySlipType != null)
            {
                try
                {
                    await Task.Run(() =>
                    {
                        paySlipType.DateCreated = DateTime.Now;
                        _paysliptypeService.Add(paySlipType);
                        _paysliptypeService.SaveChanges();
                        return Ok("thêm loại chi thành công!");
                    });

                }
                catch
                {

                    throw new Exception(string.Format("Lỗi khi thêm dữ liệu"));
                }

            }

            return CreatedAtAction("GetPaySlipType", new { id = paySlipType.Id }, paySlipType);
        }

        [HttpPost("/api/PaySlipType/paging")]
        public async Task<ActionResult<PagedResult<PaySlipTypeViewModel>>> PagingCourse(string keyword = "", int status = 0, int pageSize = 10, int pageIndex = 0)
        {
            try
            {
                return await Task.FromResult(_paysliptypeService.GetAllPaging(keyword, status, pageSize, pageIndex));
            }
            catch
            {
                throw new Exception(string.Format("Lỗi xảy ra ở phân trang!"));
            }


        }


        [HttpPost("/api/PaySlipTypes/get-all-with-conditions")]
        public async Task<ActionResult<IEnumerable<PaySlipTypeViewModel>>> GetAllConditions(string keyword = "", int status = 1)
        {
            return await Task.FromResult(_paysliptypeService.GetAllWithConditions(keyword, status));
        }



        // DELETE: api/PaySlipTypes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePaySlipType(int id)
        {
            var paySlipType = _paysliptypeService.GetById(id);
            if (paySlipType == null)
            {
                return NotFound("Không tìm thấy loại chi có Id = " + id);
            }

            try
            {
                await Task.Run(() =>
                {
                    _paysliptypeService.Delete(id);
                    _paysliptypeService.SaveChanges();
                });
            }
            catch
            {

                throw new Exception(string.Format("Có lỗi xảy ra không thể xóa!"));
            }

            return Ok();
        }

        private bool PaySlipTypeExists(int id)
        {
            return _paysliptypeService.IsExists(id);
        }
    }
}
