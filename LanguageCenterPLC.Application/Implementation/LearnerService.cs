using AutoMapper;
using LanguageCenterPLC.Application.Interfaces;
using LanguageCenterPLC.Application.ViewModels.Finances;
using LanguageCenterPLC.Application.ViewModels.Studies;
using LanguageCenterPLC.Data.Entities;
using LanguageCenterPLC.Infrastructure.Enums;
using LanguageCenterPLC.Infrastructure.Interfaces;
using LanguageCenterPLC.Utilities.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LanguageCenterPLC.Application.Implementation
{
    public class LearnerService : ILearnerService
    {
        private readonly IRepository<Learner, string> _learnerRepository;
        private readonly IRepository<StudyProcess, int> _studyProcessRepository;
        private readonly IRepository<GuestType, int> _guestTypeRepository;
        private readonly IRepository<ReceiptDetail, int> _receiptDetailRepository;
        private readonly IRepository<Receipt, string> _receiptRepository;
        private readonly IUnitOfWork _unitOfWork;

        public LearnerService(IRepository<Learner, string> learnerRepository, IRepository<GuestType, int> guestTypeRepository, 
         IRepository<StudyProcess, int> studyProcessRepository, IRepository<ReceiptDetail, int> receiptDetailRepository, IRepository<Receipt, string> receiptRepository,
         IUnitOfWork unitOfWork)
        {
            _learnerRepository = learnerRepository;
            _studyProcessRepository = studyProcessRepository;
            _guestTypeRepository = guestTypeRepository;
            _receiptDetailRepository = receiptDetailRepository;
            _receiptRepository = receiptRepository;
            _unitOfWork = unitOfWork;
        }

        public bool Add(LearnerViewModel leanerVm)
        {
            try
            {
                var learner = Mapper.Map<LearnerViewModel, Learner>(leanerVm);
                #region sinh mã cardId tăng tự động
                learner.DateCreated = DateTime.Now;
                learner.Id = TextHelper.RandomString(10);
                string cardId = _learnerRepository.FindAll().OrderByDescending(x => x.DateCreated).First().CardId;
                learner.CardId = cardId.Substring(2);

                int newCardId = Convert.ToInt32(learner.CardId) + 1;

                cardId = newCardId.ToString();
                while (cardId.Length < 5)
                {
                    cardId = "0" + cardId;
                }
                learner.CardId = "HV" + cardId;
                #endregion
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
            var Learners = (from l in _learnerRepository.FindAll().Where(x => x.Status == Status.Active)
                           join sp in _studyProcessRepository.FindAll().Where(x => x.LanguageClassId == classId && x.Status == Status.Active)
                           on l.Id equals sp.LearnerId
                           select l).OrderBy(x => x.LastName).ToList();

            Learners = Learners.OrderBy(x => x.FirstName).ToList();
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
            outLeaners = outLeaners.OrderBy(x => x.LastName).ToList();
            var learnerViewModel = Mapper.Map<List<LearnerViewModel>>(outLeaners);
            return learnerViewModel;
        }

        // Da có lớp : học viên 1, QTHT != 1, 
        public List<LearnerViewModel> DaCoLop()
        {
            var inClassLearners = (from l in _learnerRepository.FindAll().Where(x => x.Status == Status.Active)
                                   join sp in _studyProcessRepository.FindAll().Where(x => x.Status == Status.Active)
                                   on l.Id equals sp.LearnerId
                                   select l).Distinct();


            inClassLearners = inClassLearners.OrderBy(x => x.LastName);
            var learnerViewModel = Mapper.Map<List<LearnerViewModel>>(inClassLearners);

            foreach (var item in learnerViewModel)
            {
                string name = _guestTypeRepository.FindById(item.GuestTypeId).Name;
                item.GuestTypeName = name;
            }
            return learnerViewModel;
        }


        // chwua có lớp : học viên 1, QTHT != 1, 
        public List<LearnerViewModel> ChuaCoLop()
        {
            var inClassLearners = from l in _learnerRepository.FindAll().Where(x => x.Status == Status.Active)
                             join sp in _studyProcessRepository.FindAll().Where(x => x.Status == Status.Active)
                             on l.Id equals sp.LearnerId
                             select l;

            var Learners = _learnerRepository.FindAll().Where(x => x.Status == Status.Active);
            var emptyForClassLeaners = new List<Learner>();
            foreach (var learner in Learners)
            {
                if (!inClassLearners.Contains(learner))
                {
                    emptyForClassLeaners.Add(learner);
                }
            }

            emptyForClassLeaners = emptyForClassLeaners.OrderBy(x => x.LastName).ToList();
            var learnerViewModel = Mapper.Map<List<LearnerViewModel>>(emptyForClassLeaners);
            foreach (var item in learnerViewModel)
            {
                string name = _guestTypeRepository.FindById(item.GuestTypeId).Name;
                item.GuestTypeName = name;
            }
            return learnerViewModel;
        }






        public List<LearnerViewModel> GetOutClassWithCondition(string classId, string keyword)
        {
            var inLearners = from l in _learnerRepository.FindAll().Where(x => x.Status == Status.Active)
                             join sp in _studyProcessRepository.FindAll().Where(x => x.LanguageClassId == classId)
                             on l.Id equals sp.LearnerId
                             select l;
            var fullLeaners = _learnerRepository.FindAll().Where(x => x.Status == Status.Active && (x.Id.Contains(keyword) || x.LastName.Contains(keyword) || x.FirstName.Contains(keyword)) );

            var outLeaners = new List<Learner>();

            foreach (var item in fullLeaners)
            {
                if (!inLearners.Contains(item))
                {
                    outLeaners.Add(item);
                }
            }
            outLeaners = outLeaners.OrderBy(x => x.LastName).ToList();
            var learnerViewModel = Mapper.Map<List<LearnerViewModel>>(outLeaners);
            return learnerViewModel;
        }

        public List<LearnerViewModel> GetAllWithConditions(string keyword, int status)
        {
            var query = _learnerRepository.FindAll();
            if (!string.IsNullOrEmpty(keyword))                 // tìm kiếm tên
            {
                query = query.Where(x => x.CardId.Contains(keyword) || x.LastName.Contains(keyword) || x.FirstName.Contains(keyword) || x.Phone.Contains(keyword) || x.Email.Contains(keyword));
            }
            Status _status = (Status)status;                      // tìm kiếm trạng thái
            if (_status == Status.Active || _status == Status.InActive || _status == Status.Pause)    // hoạt động or nghỉ  or hẹn đi học  // kp thì là tất cả
            {
                query = query.Where(x => x.Status == _status);
            }

            query = query.OrderBy(x => x.LastName);
            var learnerViewModel = Mapper.Map<List<LearnerViewModel>>(query);

            foreach (var item in learnerViewModel)
            {
                string name = _guestTypeRepository.FindById(item.GuestTypeId).Name;
                item.GuestTypeName = name;
            }

     

            return learnerViewModel;
        }

        public LearnerViewModel GetByCardId(string cardId = "")
        {
            var learner = _learnerRepository.FindAll().Where(x => x.CardId == cardId).Single();
           
            var learnerViewModel = Mapper.Map<LearnerViewModel>(learner);

            var item = learnerViewModel;
            var guestType = _guestTypeRepository.FindById(item.GuestTypeId);
            item.GuestTypeName = guestType.Name;

            return learnerViewModel;
        }

        public List<LearnerViewModel> GetFullLearningByClass(string classId)
        {
            var studies = _studyProcessRepository.FindAll().Where(x => x.LanguageClassId == classId && x.Status == Status.Active);
            var learners = new List<Learner>();
            foreach (var item in studies)
            {
                learners.Add(_learnerRepository.FindById(item.LearnerId));
            }

            return Mapper.Map<List<LearnerViewModel>>(learners);
        }

        public List<LearnerViewModel> GetAll()
        {
            List<Learner> Learners = _learnerRepository.FindAll().ToList();
            Learners = Learners.OrderBy(x => x.LastName).ToList();
            var learnerViewModel = Mapper.Map<List<LearnerViewModel>>(Learners);

            foreach (var item in learnerViewModel)
            {
                string name = _guestTypeRepository.FindById(item.GuestTypeId).Name;
                item.GuestTypeName = name;
            }

            return learnerViewModel;
        }

        public LearnerViewModel GetById(string id)
        {
            var learner = _learnerRepository.FindById(id);
            var learnerViewModel = Mapper.Map<LearnerViewModel>(learner);

            var item = learnerViewModel;
            var guestType = _guestTypeRepository.FindById(item.GuestTypeId);
            item.GuestTypeName = guestType.Name;

            return learnerViewModel;
        }

        // Bò
        public LearnerViewModel GetLearnerCardIdForReceipt(string cardId)
        {
            var learner = _learnerRepository.FindAll().Where(x=>x.CardId == cardId).SingleOrDefault();
            var learnerViewModel = Mapper.Map<LearnerViewModel>(learner);
            var study = _studyProcessRepository.FindAll();
            foreach (var a in study)
            {
                if (learnerViewModel.Id == a.LearnerId)
                {
                    var item = learnerViewModel;
                    var guestType = _guestTypeRepository.FindById(item.GuestTypeId);
                    item.GuestTypeName = guestType.Name;

                    return learnerViewModel;
                }
            }
            return null;
        }

        // kết thúc phần bò hạ code

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
