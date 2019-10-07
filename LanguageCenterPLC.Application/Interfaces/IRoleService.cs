using LanguageCenterPLC.Application.ViewModels.Categories;
using LanguageCenterPLC.Utilities.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LanguageCenterPLC.Application.Interfaces
{
    public interface IRoleService
    {
        Task<bool> AddAsync(AppRoleViewModel userVm);

        Task DeleteAsync(Guid id);

        Task<List<AppRoleViewModel>> GetAllAsync();

        PagedResult<AppRoleViewModel> GetAllPagingAsync(string keyword, int page, int pageSize);

        Task<AppRoleViewModel> GetById(Guid id);


        Task UpdateAsync(AppRoleViewModel userVm);

        List<PermissionViewModel> GetListFunctionWithRole(Guid roleId);

        void SavePermission(List<PermissionViewModel> permissions, Guid roleId);

        Task<bool> CheckPermission(string functionId, string action, string[] roles);
    }
}
