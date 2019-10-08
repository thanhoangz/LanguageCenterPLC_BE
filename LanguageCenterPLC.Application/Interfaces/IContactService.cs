using LanguageCenterPLC.Application.ViewModels.Studies;
using LanguageCenterPLC.Utilities.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LanguageCenterPLC.Application.Interfaces
{
    public interface IContactService
    {
        Task<bool> AddAsync(ContactViewModel contactVm);

        Task<bool> UpdateAsync(ContactViewModel contactVm);

        Task<bool> DeleteAsync(string id);

        Task<List<ContactViewModel>> GetAll();

        PagedResult<ContactViewModel> GetAllPaging(string keyword, int page, int pageSize);

        Task<ContactViewModel> GetById(string id);

        void SaveChanges();
    }
}
