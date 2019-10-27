﻿using LanguageCenterPLC.Application.ViewModels.Studies;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LanguageCenterPLC.Application.Interfaces
{
    public interface IPeriodicPointDetailService
    {
        bool Add(PeriodicPointDetailViewModel periodicPointDetailVm);

        bool Update(PeriodicPointDetailViewModel periodicPointDetailVm);

        bool Delete(int id);

        List<PeriodicPointDetailViewModel> GetAll();

        PeriodicPointDetailViewModel GetById(int id);

        List<PeriodicPointViewModel> GetAllWithConditions();

        bool IsExists(int id);
        void SaveChanges();
    }
}
