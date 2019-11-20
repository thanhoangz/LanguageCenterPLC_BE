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
using LanguageCenterPLC.Application.ViewModels.Finances;

namespace LanguageCenterPLC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReceiptDetailsController : ControllerBase
    {
        private readonly IReceiptDetailService _receiptDetailService;

        public ReceiptDetailsController(IReceiptDetailService receiptDetailService)
        {
            _receiptDetailService = receiptDetailService;
        }

        // GET: api/ReceiptDetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReceiptDetailViewModel>>> GetReceiptDetails()
        {
            return await Task.FromResult(_receiptDetailService.GetAll());
        }
        ///////////////////////////////////////////////////////////////////////
        [HttpPost("/api/ReceiptDetails/get-all-with-conditions")]
        public async Task<ActionResult<IEnumerable<ReceiptDetailViewModel>>> GetAllConditions(string receiptId = "")
        {
            return await Task.FromResult(_receiptDetailService.GetAllWithConditions( receiptId));
        }

        ///////////////////////////////////////////////////////////////////////
        [HttpPost("/api/ReceiptDetails/get-receipt-detail-for-receipt")]
        public async Task<ActionResult<IEnumerable<ReceiptDetailViewModel>>> GetDetailReceiptForReceipt(string receiptId = "")
        {
            return await Task.FromResult(_receiptDetailService.GetDetailReceipt(receiptId));
        }


        [HttpPost("/api/ReceiptDetails/get-receipt-detail-for-report")]
        public async Task<ActionResult<IEnumerable<ReceiptDetailViewModel>>> GetAllReceiptDetailForReport(int month, int year)
        {
            return await Task.FromResult(_receiptDetailService.GetDetailReceiptForReport(month,year));
        }
        // PUT: api/ReceiptDetails/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReceiptDetail(int id, ReceiptDetailViewModel receiptDetail)
        {
            if (receiptDetail.Id != id)
            {
                throw new Exception(string.Format("Id và Id của phiếu thu không giống nhau!"));
            }

            try
            {
                await Task.Run(() =>
                {
                    receiptDetail.DateModified = DateTime.Now;
                    _receiptDetailService.Update(receiptDetail);
                    _receiptDetailService.SaveChanges();
                    return Ok();
                });

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReceiptDetailExists(id))
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

        // POST: api/ReceiptDetails
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<ReceiptDetailViewModel>> PostReceiptDetail(ReceiptDetailViewModel receiptDetail)
        {
            if (receiptDetail != null)
            {
                try
                {
                    await Task.Run(() =>
                    {
                        receiptDetail.DateCreated = DateTime.Now;
                        _receiptDetailService.Add(receiptDetail);
                        _receiptDetailService.SaveChanges();
                        return Ok("Thêm phiếu chi thành công!");
                    });
                }
                catch
                {
                    throw new Exception(string.Format("Lỗi khi thêm dữ liệu"));
                }
            }
            return CreatedAtAction("PostReceipt", new { id = receiptDetail.Id }, receiptDetail);
        }

        [HttpPost("/api/ReceiptDetails/add-list-receiptDetail")]
        public async Task<ActionResult<ReceiptDetailViewModel>> AddListReceiptDetail(List<ReceiptDetailViewModel> receiptDetail)
        {
            try
            {
                await Task.Run(() =>
                {
                    foreach (var item in receiptDetail)
                    {
                        item.DateCreated = DateTime.Now;
                    }                 
                    _receiptDetailService.AddList(receiptDetail);
                    _receiptDetailService.SaveChanges();
                    return Ok("Thêm giáo viên thành công!");
                });

            }
            catch
            {

                throw new Exception(string.Format("Lỗi khi thêm dữ liệu"));
            }
            return Ok();
        }


        private bool ReceiptDetailExists(int id)
        {
            return _receiptDetailService.IsExists(id);
        }


    }
}
