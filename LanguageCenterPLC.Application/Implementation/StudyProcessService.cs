using AutoMapper;
using LanguageCenterPLC.Application.Interfaces;
using LanguageCenterPLC.Application.ViewModels.Studies;
using LanguageCenterPLC.Data.Entities;
using LanguageCenterPLC.Infrastructure.Enums;
using LanguageCenterPLC.Infrastructure.Interfaces;
using LanguageCenterPLC.Utilities.Dtos;
using System.Collections.Generic;
using System;
using System.Linq;

namespace LanguageCenterPLC.Application.Implementation
{
    public class StudyProcessService : IStudyProcessService
    {
        private readonly IRepository<StudyProcess, int> _studyProcessRepository;
        private readonly IRepository<LanguageClass, string> _languageClassRepository;
        private readonly IRepository<Learner, string> _learnerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public StudyProcessService(IRepository<StudyProcess, int> studyProcessRepository, IRepository<LanguageClass, string> languageClassRepository, IRepository<Learner, string> learnerRepository, IUnitOfWork unitOfWork)
        {
            _studyProcessRepository = studyProcessRepository;
            _languageClassRepository = languageClassRepository;
            _learnerRepository = learnerRepository;
            _unitOfWork = unitOfWork;
        }


        public bool Add(StudyProcessViewModel studyProcessVm)
        {
            try
            {
                var studyProcess = Mapper.Map<StudyProcessViewModel, StudyProcess>(studyProcessVm);

                _studyProcessRepository.Add(studyProcess);

                return true;
            }
            catch
            {
                return false;
            }
        }

        //public bool Add(StudyProcessViewModel studyProcessVm)
        //{
        //    throw new NotImplementedException();
        //}

        public bool Delete(int id)
        {
            try
            {
                var studyProcess = _studyProcessRepository.FindById(id);

                _studyProcessRepository.Remove(studyProcess);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteByLearner(string languageClassId, string LearnerId)
        {
            try
            {
                var id = GetStudyProByLearner(languageClassId, LearnerId);
                var studyProcess = _studyProcessRepository.FindById(id);
                _studyProcessRepository.Remove(studyProcess);

                return true;
            }
            catch
            {
                return false;
            }
        }


        public List<StudyProcessViewModel> GetAll()
        {
            var languageClass = _languageClassRepository.FindAll().ToList();

            var studyProcess = _studyProcessRepository.FindAll().ToList();

            var studyProcessViewModel = Mapper.Map<List<StudyProcessViewModel>>(studyProcess);

            foreach (var item in studyProcessViewModel)
            {
                string name = _languageClassRepository.FindById(item.LanguageClassId).Name;
                item.LanguageClassName = name;
            }

            return studyProcessViewModel;
        }

        public PagedResult<StudyProcessViewModel> GetAllPaging(string keyword, int status, int pageSize, int pageIndex)
        {
            var query = _studyProcessRepository.FindAll();
            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(x => x.LanguageClassId.Contains(keyword));
            }

            Status _status = (Status)status;

            query = query.Where(x => x.Status == _status);

            var totalRow = query.Count();
            var data = query.OrderByDescending(x => x.LanguageClassId)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize);
            var resultPaging = Mapper.Map<List<StudyProcessViewModel>>(data);

            return new PagedResult<StudyProcessViewModel>()
            {
                CurrentPage = pageIndex,
                PageSize = pageSize,
                Results = resultPaging,
                RowCount = totalRow
            };
        }

        public List<StudyProcessViewModel> GetAllWithConditions(string languageClassId, string LearnerId, int status)
        {
            var query = _studyProcessRepository.FindAll();

            if (!string.IsNullOrEmpty(languageClassId))
            {
                query = query.Where(x => x.LanguageClassId.Contains(languageClassId));
            }

            if (!string.IsNullOrEmpty(LearnerId))
            {
                query = query.Where(x => x.LearnerId.Contains(LearnerId));
            }

            Status _status = (Status)status;
            if (_status == Status.Active || _status == Status.InActive || _status == Status.Pause)
            {
                query = query.Where(x => x.Status == _status).OrderBy(x => x.DateCreated);
            }

            var studyProcessViewModel = Mapper.Map<List<StudyProcessViewModel>>(query);

            return studyProcessViewModel;
        }

        public int GetStudyProByLearner(string languageClassId, string LearnerId)
        {
            var query = _studyProcessRepository.FindAll();

            if (!string.IsNullOrEmpty(languageClassId))
            {
                query = query.Where(x => x.LanguageClassId == languageClassId);
            }

            if (!string.IsNullOrEmpty(LearnerId))
            {
                query = query.Where(x => x.LearnerId == LearnerId);
            }

            var studyProcessViewModel = Mapper.Map<StudyProcessViewModel>(query.FirstOrDefault());

            return studyProcessViewModel.Id;
        }

        public StudyProcessViewModel GetByClassLearner(string classId, string learnerId)
        {
            var query = _studyProcessRepository.FindAll();

            if (!string.IsNullOrEmpty(classId))
            {
                query = query.Where(x => x.LanguageClassId == classId);
            }

            if (!string.IsNullOrEmpty(learnerId))
            {
                query = query.Where(x => x.LearnerId == learnerId);
            }

            var studyProcessViewModel = Mapper.Map<StudyProcessViewModel>(query.FirstOrDefault());

            return studyProcessViewModel;
        }

        public StudyProcessViewModel GetById(int id)
        {
            var studyProcess = _studyProcessRepository.FindById(id);
            var studyProcessViewModel = Mapper.Map<StudyProcessViewModel>(studyProcess);
            //////// tìm kiếm thêm học viên
            var item = studyProcessViewModel;

            var learner = _learnerRepository.FindById(item.LearnerId);
            var learnerViewModel = new LearnerViewModel();
            learnerViewModel.Id = learner.Id;
            learnerViewModel.FirstName = learner.FirstName;
            learnerViewModel.LastName = learner.LastName;
            learnerViewModel.Sex = learner.Sex;
            learnerViewModel.Birthday = learner.Birthday;
            learnerViewModel.Address = learner.Address;
            item.Learner = learnerViewModel;


            return studyProcessViewModel;
        }

        public List<StudyProcessViewModel> GetStudyProcessByClassId(string languageClassId, int status)
        {
            var query = _studyProcessRepository.FindAll();

            if (!string.IsNullOrEmpty(languageClassId))
            {
                query = query.Where(x => x.LanguageClassId.Contains(languageClassId));
            }

            Status _status = (Status)status;
            if (_status == Status.Active || _status == Status.InActive || _status == Status.Pause)
            {
                query = query.Where(x => x.Status == _status).OrderBy(x => x.DateCreated);
            }

            var studyProcessViewModel = Mapper.Map<List<StudyProcessViewModel>>(query);

            return studyProcessViewModel;
        }

        public bool IsExists(int id)
        {
            var studyProcess = _studyProcessRepository.FindById(id);
            return (studyProcess == null) ? false : true;
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public bool Update(StudyProcessViewModel studyProcessVm)
        {
            try
            {
                var studyProcess = Mapper.Map<StudyProcessViewModel, StudyProcess>(studyProcessVm);

                _studyProcessRepository.Update(studyProcess);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool UpdateStatus(int studyProcessId, Status status)
        {
            try
            {
                var studyProcess = _studyProcessRepository.FindById(studyProcessId);
                studyProcess.Status = status;
                return true;
            }
            catch
            {
                return false;
            }
        }


        public List<StudyProcessViewModel> GetAllInClass(string classId, int status)
        {
            var studyprocess = _studyProcessRepository.FindAll().Where(x => x.LanguageClassId == classId);

            Status _status = (Status)status;    //
            if (_status == Status.Active || _status == Status.InActive || _status == Status.Pause)
            {
                studyprocess = studyprocess.Where(x => x.Status == _status).OrderBy(x => x.Learner.LastName);
            }

            var studyprocessViewModel = Mapper.Map<List<StudyProcessViewModel>>(studyprocess);

            foreach (var item in studyprocessViewModel)
            {
                var learner = _learnerRepository.FindById(item.LearnerId);
                var learnerViewModel = new LearnerViewModel();
                learnerViewModel.Id = learner.Id;
                learnerViewModel.CardId = learner.CardId;
                learnerViewModel.FirstName = learner.FirstName;
                learnerViewModel.LastName = learner.LastName;
                learnerViewModel.Sex = learner.Sex;
                learnerViewModel.Birthday = learner.Birthday;
                learnerViewModel.Address = learner.Address;
                item.Learner = learnerViewModel;

            }
            return studyprocessViewModel;
        }
    }
}
