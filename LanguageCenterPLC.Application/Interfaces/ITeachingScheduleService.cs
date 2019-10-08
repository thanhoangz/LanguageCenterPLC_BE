using LanguageCenterPLC.Application.ViewModels.Studies;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LanguageCenterPLC.Application.Interfaces
{
    public interface ITeachingScheduleService
    {
        Task<bool> AddAsync(TeachingScheduleViewModel teachingScheduleVm);

        Task<bool> UpdateAsync(TeachingScheduleViewModel teachingScheduleVm);

        Task<bool> DeleteAsync(int id);

        Task<List<TeachingScheduleViewModel>>  GetAll();

        Task<TeachingScheduleViewModel> GetById(int id);

        void SaveChanges();
    }
}
