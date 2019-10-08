using LanguageCenterPLC.Application.ViewModels.Finances;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LanguageCenterPLC.Application.Interfaces
{
    public interface IReceiptDetailService
    {
        Task<bool> AddAsync(ReceiptDetailViewModel personnelVm);

        Task<bool> UpdateAsync(ReceiptDetailViewModel personnelVm);

        Task<bool> DeleteAsync(int id);

        Task<List<ReceiptDetailViewModel>> GetAll();

        Task<ReceiptDetailViewModel> GetById(int id);

        void SaveChanges();
    }
}
