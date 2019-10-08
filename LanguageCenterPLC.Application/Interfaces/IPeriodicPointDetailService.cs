using LanguageCenterPLC.Application.ViewModels.Studies;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LanguageCenterPLC.Application.Interfaces
{
    public interface IPeriodicPointDetailService
    {
        Task<bool> AddAsync(PeriodicPointDetailViewModel periodicPointDetailVm);

        Task<bool> UpdateAsync(PeriodicPointDetailViewModel periodicPointDetailVm);

        Task<bool> DeleteAsync(int id);

        Task<List<PeriodicPointDetailViewModel>> GetAll();

        Task<PeriodicPointDetailViewModel> GetById(int id);

        void SaveChanges();
    }
}
