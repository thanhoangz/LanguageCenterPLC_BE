using LanguageCenterPLC.Application.Interfaces;
using LanguageCenterPLC.Application.ViewModels.Studies;
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
    public class PaySlipsController : ControllerBase
    {
        private readonly IPaySlipService _payslipService;

        public PaySlipsController(IPaySlipService payslipService)
        {
            _payslipService = payslipService;
        }

        // GET: api/PaySlips
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PaySlipViewModel>>> GetPaySlips()
        {
            return await Task.FromResult(_payslipService.GetAll());
        }



        // GET: api/PaySlips/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PaySlipViewModel>> GetPaySlip(string id)
        {
            var payslip = _payslipService.GetById(id);

            if (payslip == null)
            {
                return NotFound("Không tìm thấy phiếu chi có id = " + id);
            }

            return await Task.FromResult(payslip);
        }

        // PUT: api/PaySlips/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPaySlip(string id, PaySlipViewModel paySlip)
        {
            if (string.Compare(paySlip.Id, id) != 0)   
            {
                throw new Exception(string.Format("Id và Id của phiếu chi không giống nhau!"));
            }

            try
            {
                await Task.Run(() =>
                {
                    paySlip.DateModified = DateTime.Now;
                    _payslipService.Update(paySlip);
                    _payslipService.SaveChanges();
                    return Ok();
                });

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PaySlipExists(id))          
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

        // POST: api/PaySlips
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<PaySlipViewModel>> PostPaySlip(PaySlipViewModel paySlip)
        {
            if (paySlip != null)
            {
                try
                {
                    await Task.Run(() =>
                    {
                        paySlip.DateCreated = DateTime.Now;
                        _payslipService.Add(paySlip);
                        _payslipService.SaveChanges();
                        return Ok("Thêm phiếu chi thành công!");
                    });

                }
                catch
                {

                    throw new Exception(string.Format("Lỗi khi thêm dữ liệu"));
                }

            }

            return CreatedAtAction("GetPaySlip", new { id = paySlip.Id }, paySlip);
        }

        [HttpPost("/api/PaySlips/paging")]
        public async Task<ActionResult<PagedResult<PaySlipViewModel>>> PagingPaySlip(string keyword = "", int status = 0, int pageSize = 10, int pageIndex = 0)
        {
            try
            {
                return await Task.FromResult(_payslipService.GetAllPaging(keyword, status, pageSize, pageIndex));
            }
            catch
            {
                throw new Exception(string.Format("Lỗi xảy ra ở phân trang!"));
            }
        }

        [HttpPost("/api/PaySlips/get-all-with-conditions")]
        public async Task<ActionResult<IEnumerable<PaySlipViewModel>>> GetAllConditions(DateTime? startDate, DateTime? endDate, string keyword = "", int phieuchi = -1, int status = 1)
        {
            return await Task.FromResult(_payslipService.GetAllWithConditions(startDate, endDate, keyword, phieuchi, status));
        }

        [HttpPost("/api/PaySlips/get-all-with-conditions-report")]
        public async Task<ActionResult<IEnumerable<PaySlipViewModel>>> GetAllWithConditions_report(int month, int year)
        {
            return await Task.FromResult(_payslipService.GetAllWithConditions_report(month,year));
        }

        // DELETE: api/PaySlips/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePaySlip(string id)
        {
            var paySlip = _payslipService.GetById(id);
            if (paySlip == null)
            {
                return NotFound("Không tìm thấy phiếu chi có Id = " + id);
            }

            try
            {
                await Task.Run(() =>
                {
                    _payslipService.Delete(id);
                    _payslipService.SaveChanges();
                });
            }
            catch
            {

                throw new Exception(string.Format("Có lỗi xảy ra không thể xóa!"));
            }

            return Ok();
        }

        private bool PaySlipExists(string id)
        {
            return _payslipService.IsExists(id);
        }
    }
}
