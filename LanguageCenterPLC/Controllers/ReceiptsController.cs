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
    public class ReceiptsController : ControllerBase
    {
        private readonly IReceiptService _receiptService;

        public ReceiptsController(IReceiptService receiptService)
        {
            _receiptService = receiptService;
        }

        // GET: api/Receipts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReceiptViewModel>>> GetReceipts()
        {
            return await Task.FromResult(_receiptService.GetAll());
        }


        /*
        [HttpGet]
        [Route("asdasd")]
        public async Task<Object> GetReceiptDetails()
        {
            var  phieuthu = _receiptService.GetAll();
            var x = new {
                id = 2,
                name = "asdasdasd",
                DanhSachPhieu = phieuthu
            };

            return x;
        }
        */

        // GET: api/Receipts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ReceiptViewModel>> GetReceipt(string id)
        {
            var receipt = _receiptService.GetById(id);

            if (receipt == null)
            {
                return NotFound("Không tìm thấy phiếu thu có id = " + id);
            }

            return await Task.FromResult(receipt);
        }

        // PUT: api/Receipts/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReceipt(string id, ReceiptViewModel receipt)
        {
            if (string.Compare(receipt.Id, id) != 0)
            {
                throw new Exception(string.Format("Id và Id của phiếu thu không giống nhau!"));
            }

            try
            {
                await Task.Run(() =>
                {
                    receipt.DateModified = DateTime.Now;
                    _receiptService.UpdateStatusReceiptAnđetail(receipt);
                    _receiptService.SaveChanges();
                    return Ok();
                });

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReceiptExists(id))
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

        // POST: api/Receipts
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<ReceiptViewModel>> PostReceipt(ReceiptViewModel receipt, Guid userId)
        {
            if (receipt != null)
            {
                try
                {
                    await Task.Run(() =>
                    {
                        receipt.DateCreated = DateTime.Now;
                        receipt.AppUserId = userId;
                        _receiptService.Add(receipt);
                        _receiptService.SaveChanges();
                        return Ok("Thêm phiếu thu thành công!");
                    });
                }
                catch
                {
                    throw new Exception(string.Format("Lỗi khi thêm dữ liệu"));
                }
            }
            return CreatedAtAction("GetReceipt()", new { id = receipt.Id }, receipt);
        }

        [HttpPost("/api/Receipts/get-all-with-conditions")]
        public async Task<ActionResult<IEnumerable<ReceiptViewModel>>> GetAllConditions(DateTime? startDate, DateTime? endDate, string id = "", string learnerId ="", int loaiphieuthu = -1, int status = 1)
        {
            return await Task.FromResult(_receiptService.GetAllWithConditions(startDate, endDate, id, learnerId, loaiphieuthu, status));
        }

        [HttpPost("/api/Receipts/paging")]
        public async Task<ActionResult<PagedResult<ReceiptViewModel>>> PagingPaySlip(string keyword = "", int status = 0, int pageSize = 10, int pageIndex = 0)
        {
            try
            {
                return await Task.FromResult(_receiptService.GetAllPaging(keyword, status, pageSize, pageIndex));
            }
            catch
            {
                throw new Exception(string.Format("Lỗi xảy ra ở phân trang!"));
            }
        }

        // bò
        [HttpPost("/api/Receipts/search-receipt")]
        public async Task<ActionResult<IEnumerable<ReceiptViewModel>>> SearchReceipt(string keyword)
        {
            return await Task.FromResult(_receiptService.SearchReceipt(keyword));
        }

        // DELETE: api/Receipts/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ReceiptViewModel>> DeleteReceipt(string id)
        {
            var receipt = _receiptService.GetById(id);
            if (receipt == null)
            {
                return NotFound("Không tìm thấy phiếu thu có Id = " + id);
            }

            try
            {
                await Task.Run(() =>
                {
                    _receiptService.Delete(id);
                    _receiptService.SaveChanges();
                });
            }
            catch
            {

                throw new Exception(string.Format("Có lỗi xảy ra không thể xóa!"));
            }

            return Ok();
        }

        private bool ReceiptExists(string id)
        {
            return _receiptService.IsExists(id);
        }
    }
}
