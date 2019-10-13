﻿using LanguageCenterPLC.Application.ViewModels.Categories;
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

        LanguageClassViewModel GetById(string id);

        void SaveChanges();

        public List<LanguageClassViewModel> GetAllWithConditions(string keyword, int status, DateTime start,DateTime end);
        bool IsExists(string id);
    }
}
