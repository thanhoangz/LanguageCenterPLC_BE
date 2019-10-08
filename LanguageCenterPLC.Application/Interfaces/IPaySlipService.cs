using LanguageCenterPLC.Application.ViewModels.Studies;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LanguageCenterPLC.Application.Interfaces
{
    public interface IPaySlipService
    {
        Task<bool> AddAsync(PaySlipViewModel payslipVm);

        Task<bool> UpdateAsync(PaySlipViewModel payslipVm);

        Task<bool> DeleteAsync(int id);

        Task<List<PaySlipViewModel>> GetAll();

        Task<PaySlipViewModel> GetById(int id);

        void SaveChanges();
    }
}
