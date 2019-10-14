using LanguageCenterPLC.Application.ViewModels.Categories;
using System.Collections.Generic;

namespace LanguageCenterPLC.Application.Interfaces
{
    public interface IGuestTypeService
    {
        bool Add(GuestTypeViewModel guestTypeVm);

        bool Update(GuestTypeViewModel guestTypeVm);

        bool Delete(int id);

        List<GuestTypeViewModel> GetAll();

        List<GuestTypeViewModel> GetAllWithConditions(string keyword, int status);

        bool IsExists(int id);

        GuestTypeViewModel GetById(int id);

        void SaveChanges();
    }
}
