using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using LanguageCenterPLC.Application.Interfaces;
using LanguageCenterPLC.Application.ViewModels.Studies;
using LanguageCenterPLC.Data.Entities;
using LanguageCenterPLC.Infrastructure.Interfaces;

namespace LanguageCenterPLC.Application.Implementation
{
    public class LearnerService : ILearnerService
    {
        private readonly IRepository<Learner, string> _learnerRepository;

        private readonly IUnitOfWork _unitOfWork;

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
