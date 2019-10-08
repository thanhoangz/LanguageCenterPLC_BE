using LanguageCenterPLC.Application.ViewModels.Categories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LanguageCenterPLC.Application.Interfaces
{
    public interface IPermissionService
    {
        Task<bool> AddAsync(PermissionViewModel permissionVm);

        Task<bool> UpdateAsync(PermissionViewModel permissionVm);

        Task<bool> DeleteAsync(int id);

        Task<List<PermissionViewModel>> GetAll();

        Task<PermissionViewModel> GetById(int id);

        void SaveChanges();
    }
}
