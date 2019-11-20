using AutoMapper;
using LanguageCenterPLC.Application.Interfaces;
using LanguageCenterPLC.Application.ViewModels.Finances;
using LanguageCenterPLC.Data.Entities;
using LanguageCenterPLC.Infrastructure.Enums;
using LanguageCenterPLC.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LanguageCenterPLC.Application.Implementation
{
    public class ReceiptDetailService : IReceiptDetailService
    {
        private readonly IRepository<ReceiptDetail, int> _receiptDetailRepository;
        private readonly IRepository<Receipt, string> _receiptRepository;
        private readonly IRepository<LanguageClass, string> _classRepository;
        private readonly IRepository<Learner, string> _learnerRepository; 
        private readonly IRepository<StudyProcess, int> _studyRepository;

        private readonly IUnitOfWork _unitOfWork;

        public ReceiptDetailService(IRepository<ReceiptDetail, int> receiptDetailRepository, IRepository<Receipt, string> receiptRepository,
            IRepository<LanguageClass, string> classRepository, IUnitOfWork unitOfWork, IRepository<Learner, string> learnerRepository,
            IRepository<StudyProcess, int> studyRepository)
        {
            _receiptDetailRepository = receiptDetailRepository;
            _receiptRepository = receiptRepository;
            _classRepository = classRepository;
            _learnerRepository = learnerRepository;
            _studyRepository = studyRepository;
            _unitOfWork = unitOfWork;
        }

        public bool Add(ReceiptDetailViewModel receiptDetailVm)
        {
            try
            {
                var receiptDetail = Mapper.Map<ReceiptDetailViewModel, ReceiptDetail>(receiptDetailVm);

                _receiptDetailRepository.Add(receiptDetail);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool AddList(List<ReceiptDetailViewModel> receiptDetailVm)
        {
            try
            {
                var receipt = _receiptRepository.FindAll().Where(x => x.Status == Status.Active).OrderByDescending(x => x.DateCreated).ToList();
                foreach (var item in receiptDetailVm)
                {
                    var receiptDetail = Mapper.Map<ReceiptDetailViewModel, ReceiptDetail>(item);
                    receiptDetail.ReceiptId = receipt[0].Id;
                    _receiptDetailRepository.Add(receiptDetail);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<ReceiptDetailViewModel> GetAll()
        {
            var receiptDetail = _receiptDetailRepository.FindAll().ToList();

            var receiptDetailViewModel = Mapper.Map<List<ReceiptDetailViewModel>>(receiptDetail);

            foreach (var item in receiptDetailViewModel)
            {
                string name = _classRepository.FindById(item.LanguageClassId).Name;
                item.LanguageClassName = name;
            }
            return receiptDetailViewModel;
        }

        public List<ReceiptDetailViewModel> GetAllWithConditions(string receiptsId)
        {
            var query = _receiptDetailRepository.FindAll();
       
            if (!string.IsNullOrEmpty(receiptsId))
            {
                query = query.Where(x => x.ReceiptId == receiptsId).OrderByDescending(x => x.DateCreated);
            }        
            var receiptDetailViewModel = Mapper.Map<List<ReceiptDetailViewModel>>(query);

            foreach (var item in receiptDetailViewModel)
            {
                string name = _classRepository.FindById(item.LanguageClassId).Name;
                item.LanguageClassName = name;
            }

            return receiptDetailViewModel;
        }

        public List<ReceiptDetailViewModel> GetDetailReceipt(string receiptId)
        {

            var receiptDetail = _receiptDetailRepository.FindAll().Where(x=>x.ReceiptId == receiptId && x.Status == Status.Active ).ToList();
            var receiptDetailViewModel = Mapper.Map<List<ReceiptDetailViewModel>>(receiptDetail);
            foreach (var item in receiptDetailViewModel)
            {
                string name = _classRepository.FindById(item.LanguageClassId).Name;
                item.LanguageClassName = name;
            }
            return receiptDetailViewModel;
        }

        public ReceiptDetailViewModel GetById(int id)
        {
            throw new NotImplementedException();
        }

        public bool IsExists(int id)
        {
            var receipt = _receiptDetailRepository.FindById(id);
            return (receipt == null) ? false : true;
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public bool Update(ReceiptDetailViewModel receiptDetailVm)
        {
            try
            {
                var receipt = Mapper.Map<ReceiptDetailViewModel, ReceiptDetail>(receiptDetailVm);
                _receiptDetailRepository.Update(receipt);
                return true;

            }
            catch
            {
                return false;
            }
        }

        // Bò đã hạ code ở đây
        public List<ReceiptDetailViewModel> GetDetailReceiptForReport(int month, int year)
        {

            var receiptDetail = _receiptDetailRepository.FindAll().Where(x => x.Month == month && x.Year == year && x.Status == Status.Active).ToList();
            var receiptDetailViewModel = Mapper.Map<List<ReceiptDetailViewModel>>(receiptDetail);
            foreach (var item in receiptDetailViewModel)
            {
                item.LanguageClassName = _classRepository.FindById(item.LanguageClassId).Name;
                string learnerId = _receiptRepository.FindById(item.ReceiptId).LearnerId;
                item.LearnerName = _learnerRepository.FindById(learnerId).FirstName + " " + _learnerRepository.FindById(learnerId).LastName;
                item.LearnerBirthday = _learnerRepository.FindById(learnerId).Birthday;
                item.CollectionDate = _receiptRepository.FindById(item.ReceiptId).CollectionDate;               
            }
            return receiptDetailViewModel;
        }
        public List<ReceiptDetailViewModel> GetLearNotPaidTuiTion(int month, int year, string classId)
        {
            var leanerInClass = _studyRepository.FindAll().Where(x => x.LanguageClassId == classId && x.Status == Status.Active).ToList();


            var receiptDetail = _receiptDetailRepository.FindAll().Where(x => x.Month == month && x.Year == year && x.Status == Status.Active).ToList();
            var receiptDetailViewModel = Mapper.Map<List<ReceiptDetailViewModel>>(receiptDetail);
            foreach (var item in receiptDetailViewModel)
            {
                item.LanguageClassName = _classRepository.FindById(item.LanguageClassId).Name;
                string learnerId = _receiptRepository.FindById(item.ReceiptId).LearnerId;
                item.LearnerName = _learnerRepository.FindById(learnerId).FirstName + " " + _learnerRepository.FindById(learnerId).LastName;
                item.LearnerBirthday = _learnerRepository.FindById(learnerId).Birthday;
                item.CollectionDate = _receiptRepository.FindById(item.ReceiptId).CollectionDate;
            }
            return receiptDetailViewModel;
        }
        //

    }
}
