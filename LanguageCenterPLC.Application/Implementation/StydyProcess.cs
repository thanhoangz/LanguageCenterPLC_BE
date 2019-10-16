using LanguageCenterPLC.Application.Interfaces;
using LanguageCenterPLC.Application.ViewModels.Studies;
using LanguageCenterPLC.Infrastructure.Enums;
using LanguageCenterPLC.Utilities.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace LanguageCenterPLC.Application.Implementation
{
    public class StydyProcess : IStudyProcessService
    {
        public bool Add(StudyProcessViewModel studyProcessVm)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<StudyProcessViewModel> GetAll()
        {
            throw new NotImplementedException();
        }

        public PagedResult<StudyProcessViewModel> GetAllPaging(string keyword, int status, int pageSize, int pageIndex)
        {
            throw new NotImplementedException();
        }

        public List<StudyProcessViewModel> GetAllWithConditions(string languageClassId, string LearnerId, int status)
        {
            throw new NotImplementedException();
        }

        public StudyProcessViewModel GetById(int id)
        {
            throw new NotImplementedException();
        }

        public bool IsExists(string id)
        {
            throw new NotImplementedException();
        }

        public bool IsExists(int id)
        {
            throw new NotImplementedException();
        }

        public void SaveChanges()
        {
            throw new NotImplementedException();
        }

        public bool Update(StudyProcessViewModel studyProcessVm)
        {
            throw new NotImplementedException();
        }

        public bool UpdateStatus(int studyProcessId, Status status)
        {
            throw new NotImplementedException();
        }
    }
}
