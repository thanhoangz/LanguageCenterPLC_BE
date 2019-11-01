﻿using LanguageCenterPLC.Application.ViewModels.Finances;
using LanguageCenterPLC.Infrastructure.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LanguageCenterPLC.Application.Interfaces
{
    public interface IReceiptDetailService
    {
        bool Add(ReceiptDetailViewModel receiptDetailVm);

        bool Update(ReceiptDetailViewModel receiptDetailVm);

       bool Delete(int id);

        List<ReceiptDetailViewModel> GetAll();
        List<ReceiptDetailViewModel> GetAllWithConditions(string receiptsId);

        ReceiptDetailViewModel GetById(int id);

        void SaveChanges();

        bool UpdateStatus(int id, Status status);

        bool IsExists(int id);
    }
}
