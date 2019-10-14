using LanguageCenterPLC.Application.ViewModels.Categories;
using LanguageCenterPLC.Utilities.Dtos;
using System;
using System.Collections.Generic;

namespace LanguageCenterPLC.Application.Interfaces
{
    public interface IReceiptTypeService
    {
        bool Add(ReceiptTypeViewModel receiptTypeViewModel);

        bool Update(ReceiptTypeViewModel receiptTypeViewModel);

        bool Delete(int receiptTypeId);

        List<ReceiptTypeViewModel> GetAll();

        PagedResult<ReceiptTypeViewModel> GetAllPaging(string keyword,
           int pageSize, int pageIndex);
        List<ReceiptTypeViewModel> GetAllWithConditions(string keyword, int status);

        ReceiptTypeViewModel GetById(int receiptTypeId);

        bool IsExists(int id);

        void SaveChanges();
    }
}
