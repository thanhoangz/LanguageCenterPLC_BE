using LanguageCenterPLC.Application.ViewModels.Studies;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LanguageCenterPLC.Application.Interfaces
{
    public interface IPeriodicPointDetailService
    {
        bool Add(PeriodicPointDetailViewModel periodicPointDetailVm);

        bool Update(PeriodicPointDetailViewModel periodicPointDetailVm,string classID, Guid userId);

        bool Delete(int id);

        List<PeriodicPointDetailViewModel> GetAll();

        PeriodicPointDetailViewModel GetById(int id);

        List<PeriodicPointDetailViewModel> GetAllWithConditions(int periodicPointId);
        bool AddRange();

        bool IsExists(int id);
        void SaveChanges();
    }
}
