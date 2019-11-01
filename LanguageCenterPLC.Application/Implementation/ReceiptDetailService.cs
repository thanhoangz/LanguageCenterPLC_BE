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
        private readonly IUnitOfWork _unitOfWork;

        public ReceiptDetailService(IRepository<ReceiptDetail, int> receiptDetailRepository, IRepository<Receipt, string> receiptRepository, IRepository<LanguageClass, string> classRepository, IUnitOfWork unitOfWork)
        {
            _receiptDetailRepository = receiptDetailRepository;
            _receiptRepository = receiptRepository;
            _classRepository = classRepository;
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

        public bool UpdateStatus(int id, Status status)
        {
            try
            {
                var receipt = _receiptDetailRepository.FindById(id);
                receipt.Status = status;
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
