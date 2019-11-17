using LanguageCenterPLC.Application.ViewModels.Categories;
using System;
using System.Collections.Generic;

namespace LanguageCenterPLC.Application.Interfaces
{
    public interface IPermissionService
    {
        bool Add(PermissionViewModel permissionVm);

        bool Update(PermissionViewModel permissionVm);

        bool Delete(int id);

        List<PermissionViewModel> GetAll();

        List<PermissionViewModel> GetAllByUser(Guid userId);

        PermissionViewModel GetById(int id);
        List<PermissionViewModel> GetAllByBo(Guid userId);
        bool AddRangPermission();
        void SaveChanges();
        bool IsExists(int id);


    }
}
