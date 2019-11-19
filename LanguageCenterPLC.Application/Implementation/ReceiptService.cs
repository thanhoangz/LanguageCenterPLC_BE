using AutoMapper;
using LanguageCenterPLC.Application.Interfaces;
using LanguageCenterPLC.Application.ViewModels.Studies;
using LanguageCenterPLC.Data.EF;
using LanguageCenterPLC.Data.Entities;
using LanguageCenterPLC.Infrastructure.Enums;
using LanguageCenterPLC.Infrastructure.Interfaces;
using LanguageCenterPLC.Utilities.Dtos;
using LanguageCenterPLC.Utilities.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LanguageCenterPLC.Application.Implementation
{
    public class ReceiptService : IReceiptService
    {
        private readonly IRepository<Receipt, string> _receiptRepository;
        private readonly IRepository<ReceiptType, int> _receiptTypeRepository;
        private readonly IRepository<LanguageClass, string> _languageClassRepository;
        private readonly IRepository<Learner, string> _learnerRepository;
        private readonly IRepository<StudyProcess, int> _studyProcessRepository;
        private readonly IRepository<Personnel, string> _personnelRepository;
        private readonly IRepository<ReceiptDetail, int> _receiptDetailRepository;

        private readonly IUnitOfWork _unitOfWork;
        private readonly AppDbContext _context;

        public ReceiptService(IRepository<Receipt, string> receiptRepository, IRepository<ReceiptType, int> receiptTypeRepository, IUnitOfWork unitOfWork,
             IRepository<LanguageClass, string> languageClassRepository, IRepository<Learner, string> learnerRepository,
             IRepository<StudyProcess, int> studyProcessRepository, AppDbContext context, IRepository<Personnel, string> personnelRepository,
             IRepository<ReceiptDetail, int> receiptDetailRepository)
        {
            _receiptRepository = receiptRepository;
            _receiptTypeRepository = receiptTypeRepository;
            _languageClassRepository = languageClassRepository;
            _unitOfWork = unitOfWork;
            _learnerRepository = learnerRepository;
            _personnelRepository = personnelRepository;
            _studyProcessRepository = studyProcessRepository;
            _receiptDetailRepository = receiptDetailRepository;
            _context = context;
            

        }

        public bool Add(ReceiptViewModel receiptVm)
        {
            try
            {
                receiptVm.ForReason = receiptVm.NameOfPaymentApplicant;
                var receipt = Mapper.Map<ReceiptViewModel, Receipt>(receiptVm);
                receipt.Id = TextHelper.RandomNumber(10);
                _receiptRepository.Add(receipt);

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
                var receipt = _receiptRepository.FindById(id);

                _receiptRepository.Remove(receipt);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<ReceiptViewModel> GetAll()
        {

            var receipt = _receiptRepository.FindAll().OrderByDescending(x=>x.DateCreated).ToList();

            var receiptViewModel = Mapper.Map<List<ReceiptViewModel>>(receipt);

            foreach (var item in receiptViewModel)
            {           
                item.ReceiptTypeName = _receiptTypeRepository.FindById(item.ReceiptTypeId).Name;
                item.LearnerName = _learnerRepository.FindById(item.LearnerId).FirstName + " " + _learnerRepository.FindById(item.LearnerId).LastName;
                item.LearnerCardId = _learnerRepository.FindById(item.LearnerId).CardId;
                item.LearnerAdress = _learnerRepository.FindById(item.LearnerId).Address;
                item.PersonnelName = _personnelRepository.FindById(item.PersonnelId).FirstName + " " + _personnelRepository.FindById(item.PersonnelId).LastName;
                item.LearnerPhone = _learnerRepository.FindById(item.LearnerId).Phone;

            }
            return receiptViewModel;
        }

        public PagedResult<ReceiptViewModel> GetAllPaging(string keyword, int status, int pageSize, int pageIndex)
        {
            var query = _receiptRepository.FindAll();
            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(x => x.Id.Contains(keyword));
            }

            Status _status = (Status)status;

            query = query.Where(x => x.Status == _status);

            var totalRow = query.Count();
            var data = query.OrderByDescending(x => x.Id)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize);
            var resultPaging = Mapper.Map<List<ReceiptViewModel>>(data);

            return new PagedResult<ReceiptViewModel>()
            {
                CurrentPage = pageIndex,
                PageSize = pageSize,
                Results = resultPaging,
                RowCount = totalRow
            };
        }

        public List<ReceiptViewModel> GetAllWithConditions(DateTime? startDate, DateTime? endDate, string id, string learnerId, int loaiphieuthu, int status)
        {
            var query = _receiptRepository.FindAll();

            if (!string.IsNullOrEmpty(id))
            {
                query = query.Where(x => x.Id.Contains(id)).OrderByDescending(x => x.DateCreated);
            }

            if (!string.IsNullOrEmpty(learnerId))
            {
                query = query.Where(x => x.LearnerId == learnerId).OrderByDescending(x => x.DateCreated);
            }


            Status _status = (Status)status;

            if (_status == Status.Active || _status == Status.InActive || _status == Status.Pause)
            {
                query = query.Where(x => x.Status == _status).OrderByDescending(x => x.DateCreated);
            }

            if (startDate != null && endDate != null)
            {
                query = query.Where(x => x.CollectionDate >= startDate && x.CollectionDate <= endDate);
            }
            if (loaiphieuthu != -1)
            {
                query = query.Where(x => x.ReceiptTypeId == loaiphieuthu).OrderByDescending(x => x.DateCreated);
            }

            var receiptViewModel = Mapper.Map<List<ReceiptViewModel>>(query);

            foreach (var item in receiptViewModel)
            {
                string name = _receiptTypeRepository.FindById(item.ReceiptTypeId).Name;
                item.ReceiptTypeName = name;
            }

            return receiptViewModel;
        }

        public ReceiptViewModel GetById(string id)
        {
            var receipt = _receiptRepository.FindById(id);
            var receiptViewModel = Mapper.Map<ReceiptViewModel>(receipt);

            var item = receiptViewModel;
            var receiptype = _receiptTypeRepository.FindById(item.ReceiptTypeId);
            item.ReceiptTypeName = receiptype.Name;

            return receiptViewModel;
        }

        public bool IsExists(string id)
        {
            var receipt = _receiptRepository.FindById(id);
            return (receipt == null) ? false : true;
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public bool Update(ReceiptViewModel receiptVm)
        {
            try
            {
                var receipt = Mapper.Map<ReceiptViewModel, Receipt>(receiptVm);
                _receiptRepository.Update(receipt);
                return true;

            }
            catch
            {
                return false;
            }
        }

        // Bò
        public bool UpdateStatusReceiptAnđetail(ReceiptViewModel receiptVm)
        {
            try
            {
                var receipt = Mapper.Map<ReceiptViewModel, Receipt>(receiptVm);
                receipt.ForReason = receipt.NameOfPaymentApplicant;
                _receiptRepository.Update(receipt);
                var detail = _receiptDetailRepository.FindAll(x => x.ReceiptId == receipt.Id).ToList();
                foreach (var item in detail)
                {
                    item.Status = receipt.Status;
                    _receiptDetailRepository.Update(item);
                }
                return true;

            }
            catch
            {
                return false;
            }
        }

        public List<ReceiptViewModel> SearchReceipt(string keyWord = "")
        {
            
            var receipts = (from receipt in _receiptRepository.FindAll()
                            join learner in _learnerRepository.FindAll() on receipt.LearnerId equals learner.Id 
                            where receipt.Id == keyWord || learner.FirstName.Contains(keyWord) || learner.LastName.Contains(keyWord)
                            || (learner.FirstName +" "+ learner.LastName).Contains(keyWord) select receipt).ToList();
            var receiptViewModel = Mapper.Map<List<ReceiptViewModel>>(receipts);
            foreach (var item in receiptViewModel)
            {
                item.ReceiptTypeName = _receiptTypeRepository.FindById(item.ReceiptTypeId).Name;
                item.LearnerName = _learnerRepository.FindById(item.LearnerId).FirstName + " " + _learnerRepository.FindById(item.LearnerId).LastName;
                item.LearnerCardId = _learnerRepository.FindById(item.LearnerId).CardId;
                item.LearnerAdress = _learnerRepository.FindById(item.LearnerId).Address;
                item.PersonnelName = _personnelRepository.FindById(item.PersonnelId).FirstName + " " + _personnelRepository.FindById(item.PersonnelId).LastName;
                item.LearnerPhone = _learnerRepository.FindById(item.LearnerId).Phone;
            }
            return receiptViewModel;

            
        }

    }
}
