using LanguageCenterPLC.Application.ViewModels.Categories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LanguageCenterPLC.Application.Interfaces
{
    public interface IPersonnelService
    {
        Task<bool> AddAsync(PersonnelViewModel personnelVm);

        Task<bool> UpdateAsync(PersonnelViewModel personnelVm);

        Task<bool> DeleteAsync(int id);

        Task<List<PersonnelViewModel>> GetAll();

        Task<PersonnelViewModel> GetById(int id);

        void SaveChanges();
    }
}
