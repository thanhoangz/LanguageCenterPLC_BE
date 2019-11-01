using LanguageCenterPLC.Application.ViewModels.Studies;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LanguageCenterPLC.Application.Interfaces
{
    public interface IPeriodicPointService
    {
        bool Add(PeriodicPointViewModel periodicPointVm);

        bool Update(PeriodicPointViewModel periodicPointVm);

        bool Delete(int id);

        List<PeriodicPointViewModel> GetAll();

        PeriodicPointViewModel GetById(int id);
        List<PeriodicPointViewModel> GetAllWithConditions( string languageClassId);


        bool IsExists(int id);
        void SaveChanges();
    }
}
