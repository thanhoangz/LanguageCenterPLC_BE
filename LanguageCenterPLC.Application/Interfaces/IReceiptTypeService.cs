using LanguageCenterPLC.Application.ViewModels.Categories;
using LanguageCenterPLC.Utilities.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LanguageCenterPLC.Application.Interfaces
{
    public interface IReceiptTypeService
    {
        bool Add(ReceiptTypeViewModel receiptTypeViewModel );

        bool Update(ReceiptTypeViewModel receiptTypeViewModel);

        bool Delete(int receiptTypeId);

        List<ReceiptTypeViewModel> GetAll();

        PagedResult<ReceiptTypeViewModel> GetAllPaging(string keyword, int status,
           int pageSize, int pageIndex);

        ReceiptTypeViewModel GetById(int receiptTypeId);   

        bool IsExists(int id);

        void SaveChanges();
        //Task<bool> AddAsync(ReceiptTypeViewModel receiptTypeVm);

        //Task<bool> UpdateAsync(ReceiptTypeViewModel receiptTypeVm);

        //Task<bool> DeleteAsync(int id);

        //Task<List<ReceiptTypeViewModel>> GetAll();

        //Task<ReceiptTypeViewModel> GetById(int id);

        //void SaveChanges();
    }
}
