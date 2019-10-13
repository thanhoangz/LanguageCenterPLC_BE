using LanguageCenterPLC.Application.ViewModels.Categories;
using LanguageCenterPLC.Infrastructure.Enums;
using LanguageCenterPLC.Utilities.Dtos;
using System.Collections.Generic;

namespace LanguageCenterPLC.Application.Interfaces
{
    public interface IPaySlipTypeService
    {
        bool Add(PaySlipTypeViewModel payslipTypeVm);

        bool Update(PaySlipTypeViewModel payslipTypeVm);

        bool Delete(int id);


        List<PaySlipTypeViewModel> GetAll();

        List<PaySlipTypeViewModel> GetAllWithConditions(string keyword, int status);

        PagedResult<PaySlipTypeViewModel> GetAllPaging(string keyword, int status,
           int pageSize, int pageIndex);

        PaySlipTypeViewModel GetById(int id);

        bool UpdateStatus(int paysliptypeId, Status status);

        bool IsExists(int id);

        void SaveChanges();

    }
}
