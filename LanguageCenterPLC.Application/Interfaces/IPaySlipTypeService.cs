using LanguageCenterPLC.Application.ViewModels.Categories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LanguageCenterPLC.Application.Interfaces
{
    public interface IPaySlipTypeService
    {
        Task<bool> AddAsync(PaySlipTypeViewModel payslipTypeVm);

        Task<bool> UpdateAsync(PaySlipTypeViewModel payslipTypeVm);

        Task<bool> DeleteAsync(int id);

        Task<List<PaySlipTypeViewModel>> GetAll();

        Task<PaySlipTypeViewModel> GetById(int id);

        void SaveChanges();
    }
}
