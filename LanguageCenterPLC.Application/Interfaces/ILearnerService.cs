using LanguageCenterPLC.Application.ViewModels.Studies;
using System.Collections.Generic;

namespace LanguageCenterPLC.Application.Interfaces
{
    public interface ILearnerService
    {
        bool Add(LearnerViewModel leanerVm);

        bool Update(LearnerViewModel leanerVm);

        bool Delete(string id);

        List<LearnerViewModel> GetAll();

        LearnerViewModel GetById(string id);

        void SaveChanges();

        bool IsExists(string id);

        public List<LearnerViewModel> GetAllInClass(string classId);
        public List<LearnerViewModel> GetAllOutClass(string classId);
        public List<LearnerViewModel> GetOutClassWithCondition(string classId, string keyword);
    }
}
