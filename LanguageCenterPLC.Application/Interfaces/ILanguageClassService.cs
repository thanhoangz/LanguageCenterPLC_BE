using LanguageCenterPLC.Application.ViewModels.Categories;
using LanguageCenterPLC.Data.Entities;
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
        List<LanguageClassViewModel> LopDeChuyen(string classId, int courseId);

        LanguageClassViewModel GetById(string id);

        void SaveChanges();

        public List<LanguageClassViewModel> GetAllWithConditions(DateTime? start, DateTime? end, string keyword = "", int courseKeyword = -1, int status = 1);


        List<LanguageClassViewModel> GetLanguageClassesByCourse(int courseId);
        bool IsExists(string id);
    }
}
