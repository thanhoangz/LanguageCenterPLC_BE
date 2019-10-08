using LanguageCenterPLC.Application.ViewModels.Studies;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LanguageCenterPLC.Application.Interfaces
{
    public interface IPeriodicPointService
    {
        Task<bool> AddAsync(PeriodicPointViewModel periodicPointVm);

        Task<bool> UpdateAsync(PeriodicPointViewModel periodicPointVm);

        Task<bool> DeleteAsync(int id);

        Task<List<PeriodicPointViewModel>> GetAll();

        Task<PeriodicPointViewModel> GetById(int id);

        void SaveChanges();
    }
}
