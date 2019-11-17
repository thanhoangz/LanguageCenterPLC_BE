using LanguageCenterPLC.Application.ViewModels.Categories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LanguageCenterPLC.Application.Interfaces
{
    public interface IPersonnelService
    {
        bool Add(PersonnelViewModel personnelVm);

        bool Update(PersonnelViewModel personnelVm);

        bool Delete(string id);


        List<PersonnelViewModel> GetAll();
        List<PersonnelViewModel> GetAllWithConditions(string keyword, string position, int status);
        PersonnelViewModel GetByCardId(string keyword);

        PersonnelViewModel GetById(string id);
        bool IsExists(string id);


        void SaveChanges();
    }
}
