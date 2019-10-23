using LanguageCenterPLC.Application.ViewModels.Categories;
using LanguageCenterPLC.Utilities.Dtos;
using System.Collections.Generic;

namespace LanguageCenterPLC.Application.Interfaces
{
    public interface IUserService
    {
        bool Add(AppUserViewModel userVm);

        bool Update(AppUserViewModel userVm);

        bool Delete(string id);

        List<AppUserViewModel> GetAll();

        AppUserViewModel GetById(string id);

        void SaveChanges();
        bool IsExists(string id);
    }
}
