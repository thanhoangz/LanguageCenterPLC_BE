using LanguageCenterPLC.Application.ViewModels.Studies;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LanguageCenterPLC.Application.Interfaces
{
    public interface IReceiptService
    {
        Task<bool> AddAsync(ReceiptViewModel receiptVm);

        Task<bool> UpdateAsync(ReceiptViewModel receiptVm);

        Task<bool> DeleteAsync(int id);

        Task<List<ReceiptViewModel>> GetAll();

        Task<ReceiptViewModel> GetById(int id);

        void SaveChanges();
    }
}
