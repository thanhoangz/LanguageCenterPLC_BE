using AutoMapper;
using LanguageCenterPLC.Application.Interfaces;
using LanguageCenterPLC.Application.ViewModels.Studies;
using LanguageCenterPLC.Data.Entities;
using LanguageCenterPLC.Infrastructure.Enums;
using LanguageCenterPLC.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LanguageCenterPLC.Application.Implementation
{
    public class LearnerService : ILearnerService
    {
        private readonly IRepository<Learner, string> _learnerRepository;
        private readonly IRepository<StudyProcess, int> _studyProcessRepository;
        private readonly IUnitOfWork _unitOfWork;

        public LearnerService(IRepository<Learner, string> learnerRepository, IRepository<StudyProcess, int> studyProcessRepository,
         IUnitOfWork unitOfWork)
        {
            _learnerRepository = learnerRepository;
            _studyProcessRepository = studyProcessRepository;
            _unitOfWork = unitOfWork;
        }

        public bool Add(LearnerViewModel leanerVm)
        {
            try
            {
                var learner = Mapper.Map<LearnerViewModel, Learner>(leanerVm);

                _learnerRepository.Add(learner);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Delete(string id)
        {
            try
            {
                var learner = _learnerRepository.FindById(id);

                _learnerRepository.Remove(learner);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<LearnerViewModel> GetAllInClass(string classId)
        {
            var Learners = from l in _learnerRepository.FindAll().Where(x => x.Status == Status.Active)
                           join sp in _studyProcessRepository.FindAll().Where(x => x.LanguageClassId == classId && x.Status == Status.Active)
                           on l.Id equals sp.LearnerId
                           select l;
                           

            var learnerViewModel = Mapper.Map<List<LearnerViewModel>>(Learners);

            return learnerViewModel;
        }

        public List<LearnerViewModel> GetAllOutClass(string classId)
        {
            var inLearners = from l in _learnerRepository.FindAll().Where(x => x.Status == Status.Active)
                             join sp in _studyProcessRepository.FindAll().Where(x => x.LanguageClassId == classId)
                             on l.Id equals sp.LearnerId
                             select l;

            var fullLeaners = _learnerRepository.FindAll().Where(x => x.Status == Status.Active);

            var outLeaners = new List<Learner>();

            foreach (var item in fullLeaners)
            {
                if (!inLearners.Contains(item))
                {
                    outLeaners.Add(item);
                }
            }

            var learnerViewModel = Mapper.Map<List<LearnerViewModel>>(outLeaners);

            return learnerViewModel;
        }

        public List<LearnerViewModel> GetAll()
        {
            List<Learner> Learners = _learnerRepository.FindAll().ToList();
            var learnerViewModel = Mapper.Map<List<LearnerViewModel>>(Learners);
            return learnerViewModel;
        }

        public LearnerViewModel GetById(string id)
        {
            var learner = _learnerRepository.FindById(id);
            var learnerViewModel = Mapper.Map<LearnerViewModel>(learner);
            return learnerViewModel;
        }

        public bool IsExists(string id)
        {
            var learner = _learnerRepository.FindById(id);
            return (learner == null) ? false : true;
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public bool Update(LearnerViewModel leanerVm)
        {
            try
            {
                var learner = Mapper.Map<LearnerViewModel, Learner>(leanerVm);
                _learnerRepository.Update(learner);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
