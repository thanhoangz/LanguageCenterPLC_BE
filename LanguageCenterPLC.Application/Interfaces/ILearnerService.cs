using LanguageCenterPLC.Application.ViewModels.Studies;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LanguageCenterPLC.Application.Interfaces
{
    public interface ILearnerService
    {
        Task<bool> AddAsync(LearnerViewModel leanerVm);

        Task<bool> UpdateAsync(LearnerViewModel leanerVm);

        Task<bool> DeleteAsync(int id);

        Task<List<LearnerViewModel>> GetAll();

         Task<LearnerViewModel> GetById(int id);

        void SaveChanges();
    }
}
