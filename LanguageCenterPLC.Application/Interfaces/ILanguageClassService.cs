using LanguageCenterPLC.Application.ViewModels.Categories;
using System;
using System.Collections.Generic;

namespace LanguageCenterPLC.Application.Interfaces
{
    public interface ILanguageClassService
    {
        bool Add(LanguageClassViewModel languageClassVm);

        bool Update(LanguageClassViewModel languageClassVm);

        bool Delete(string id);

        List<LanguageClassViewModel> GetAll();

        LanguageClassViewModel GetById(string id);

        void SaveChanges();

        public List<LanguageClassViewModel> GetAllWithConditions(DateTime start, string keyword = "", int status = 1);

        bool IsExists(string id);
    }
}
