using LanguageCenterPLC.Application.ViewModels.Studies;
using LanguageCenterPLC.Infrastructure.Enums;
using LanguageCenterPLC.Utilities.Dtos;
using System.Collections.Generic;

namespace LanguageCenterPLC.Application.Interfaces
{
    public interface IPaySlipService
    {
        bool Add(PaySlipViewModel payslipVm);

        bool Update(PaySlipViewModel payslipVm);

        bool Delete(string id);

        List<PaySlipViewModel> GetAll();

        List<PaySlipViewModel> GetAllWithConditions(string keyword, int status);

        PagedResult<PaySlipViewModel> GetAllPaging(string keyword, int status,
           int pageSize, int pageIndex);

        PaySlipViewModel GetById(string id);

        bool UpdateStatus(string payslipId, Status status);

        bool IsExists(string id);

        void SaveChanges();
    }
}
