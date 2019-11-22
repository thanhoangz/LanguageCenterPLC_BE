using LanguageCenterPLC.Application.ViewModels.Studies;
using LanguageCenterPLC.Infrastructure.Enums;
using LanguageCenterPLC.Utilities.Dtos;
using System;
using System.Collections.Generic;

namespace LanguageCenterPLC.Application.Interfaces
{
    public interface IPaySlipService
    {
        bool Add(PaySlipViewModel payslipVm);

        bool Update(PaySlipViewModel payslipVm);

        bool Delete(string id);

        List<PaySlipViewModel> GetAll();

        List<PaySlipViewModel> GetAllWithConditions(DateTime? startDate, DateTime? endDate,string keyword, int phieuchi, int status);
        List<PaySlipViewModel> GetAllWithConditions_report(int month, int year);

        PagedResult<PaySlipViewModel> GetAllPaging(string keyword, int status, int pageSize, int pageIndex);

        PaySlipViewModel GetById(string id);

        bool UpdateStatus(string payslipId, Status status);

        bool IsExists(string id);

        void SaveChanges();
    }
}
