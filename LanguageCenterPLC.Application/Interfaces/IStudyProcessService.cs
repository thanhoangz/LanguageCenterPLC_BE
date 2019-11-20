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

        bool DeleteByLearner(string languageClassId, string LearnerId);

        List<StudyProcessViewModel> GetAll();

        List<StudyProcessViewModel> GetAllWithConditions(string languageClassId, string LearnerId, int status);

        PagedResult<StudyProcessViewModel> GetAllPaging(string keyword, int status, int pageSize, int pageIndex);

        int GetStudyProByLearner(string languageClassId, string LearnerId);

        StudyProcessViewModel GetById(int id);

        List<StudyProcessViewModel> GetStudyProcessByClassId(string languageClassId, int status);

        void SaveChanges();

        bool UpdateStatus(int studyProcessId, Status status);

        bool IsExists(int id);
        StudyProcessViewModel GetByClassLearner(string classId, string learnerId);

        // phần này của thằng bò cấm động vào :v
        public List<StudyProcessViewModel> GetAllClassOfLearner(string learnerId);
        public List<StudyProcessViewModel> GetLearnerForReceipt();

        public List<StudyProcessViewModel> GetLearNotPaidTuiTion(int month, int year, string classId);

        // kết thúc phần của thằng Bò

        List<StudyProcessViewModel> GetAllInClass(string classId,int status);
    }
}
