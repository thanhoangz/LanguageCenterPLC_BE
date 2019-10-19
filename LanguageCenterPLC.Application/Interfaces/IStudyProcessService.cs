using LanguageCenterPLC.Application.ViewModels.Studies;
using LanguageCenterPLC.Infrastructure.Enums;
using LanguageCenterPLC.Utilities.Dtos;
using System.Collections.Generic;

namespace LanguageCenterPLC.Application.Interfaces
{
    public interface IStudyProcessService
    {
        bool Add(StudyProcessViewModel studyProcessVm);

        bool Update(StudyProcessViewModel studyProcessVm);

        bool Delete(int id);

        List<StudyProcessViewModel> GetAll();

        List<StudyProcessViewModel> GetAllWithConditions(string languageClassId, string LearnerId, int status);

        PagedResult<StudyProcessViewModel> GetAllPaging(string keyword, int status, int pageSize, int pageIndex);

        StudyProcessViewModel GetById(int id);

        List<StudyProcessViewModel> GetStudyProcessByClassId(string languageClassId, int status);

        void SaveChanges();

        bool UpdateStatus(int studyProcessId, Status status);

        bool IsExists(int id);
    }
}
