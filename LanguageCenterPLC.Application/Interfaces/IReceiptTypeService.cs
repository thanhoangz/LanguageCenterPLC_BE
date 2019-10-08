using LanguageCenterPLC.Application.ViewModels.Categories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LanguageCenterPLC.Application.Interfaces
{
    public interface IReceiptTypeService
    {
        Task<bool> AddAsync(ReceiptTypeViewModel receiptTypeVm);

        Task<bool> UpdateAsync(ReceiptTypeViewModel receiptTypeVm);

        Task<bool> DeleteAsync(int id);

        Task<List<ReceiptTypeViewModel>> GetAll();

        Task<ReceiptTypeViewModel> GetById(int id);

        void SaveChanges();
    }
}
