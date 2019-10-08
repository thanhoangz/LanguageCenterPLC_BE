using LanguageCenterPLC.Application.ViewModels.Categories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LanguageCenterPLC.Application.Interfaces
{
    public interface IGuestTypeService
    {
        Task<bool> AddAsync(GuestTypeViewModel guestTypeVm);

        Task<bool> UpdateAsync(GuestTypeViewModel guestTypeVm);

        Task<bool> DeleteAsync(int id);

        Task<List<GuestTypeViewModel>> GetAll();

        Task<GuestTypeViewModel> GetById(int id);

        void SaveChanges();
    }
}
