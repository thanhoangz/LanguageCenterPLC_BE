using LanguageCenterPLC.Application.ViewModels.Studies;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LanguageCenterPLC.Application.Interfaces
{
    public interface IStudyProcessService
    {
        Task<bool> AddAsync(StudyProcessViewModel studyProcessVm);

        Task<bool> UpdateAsync(StudyProcessViewModel studyProcessVm);

        Task<bool> DeleteAsync(int id);

        Task<List<StudyProcessViewModel>> GetAll();

        Task<StudyProcessViewModel> GetById(int id);

        void SaveChanges();
    }
}
