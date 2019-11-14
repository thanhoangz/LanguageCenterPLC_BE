using LanguageCenterPLC.Application.ViewModels.Studies;
using LanguageCenterPLC.Infrastructure.Enums;
using LanguageCenterPLC.Utilities.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LanguageCenterPLC.Application.Interfaces
{
    public interface IReceiptService
    {
       bool Add(ReceiptViewModel receiptVm);

        bool Update(ReceiptViewModel receiptVm);

        bool Delete(string id);

       List<ReceiptViewModel> GetAll();

        List<ReceiptViewModel> GetAllWithConditions(DateTime? startDate, DateTime? endDate, string id, string learnerId, int loaiphieuthu, int status);

        PagedResult<ReceiptViewModel> GetAllPaging(string keyword, int status, int pageSize, int pageIndex);

        ReceiptViewModel GetById(string id);

        // bò
        public bool UpdateStatusReceiptAnđetail(ReceiptViewModel receiptVm);
        public List<ReceiptViewModel> SearchReceipt(string keyWord);
        bool IsExists(string id);

        void SaveChanges();
    }
}
