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
        List<PersonnelViewModel> GetAllWithConditions(DateTime briday, string keyword, int status, int sex);

        PersonnelViewModel GetById(string id);
        bool IsExists(string id);

        void SaveChanges();
    }
}
