using AutoMapper;
using LanguageCenterPLC.Application.Interfaces;
using LanguageCenterPLC.Application.ViewModels.Categories;
using LanguageCenterPLC.Data.Entities;
using LanguageCenterPLC.Infrastructure.Enums;
using LanguageCenterPLC.Infrastructure.Interfaces;
using LanguageCenterPLC.Utilities.Dtos;
using System.Collections.Generic;
using System.Linq;

namespace LanguageCenterPLC.Application.Implementation
{
    public class ReceiptTypeService : IReceiptTypeService
    {
        private readonly IRepository<ReceiptType, int> _receiptTypeRepository;

        private readonly IUnitOfWork _unitOfWork;
        public ReceiptTypeService(IRepository<ReceiptType, int> receiptTypeRepository,
          IUnitOfWork unitOfWork)
        {
            _receiptTypeRepository = receiptTypeRepository;
            _unitOfWork = unitOfWork;
        }
        public bool Add(ReceiptTypeViewModel receiptTypeViewModel)
        {
            try
            {
                var receiptType = Mapper.Map<ReceiptTypeViewModel, ReceiptType>(receiptTypeViewModel);

                _receiptTypeRepository.Add(receiptType);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Delete(int receiptTypeId)
        {
            try
            {
                var receiptType = _receiptTypeRepository.FindById(receiptTypeId);

                _receiptTypeRepository.Remove(receiptType);

                return true;
            }
            catch
            {
                return false;
            }
        }


        public List<ReceiptTypeViewModel> GetAll()
        {
            List<ReceiptType> receiptTypes = _receiptTypeRepository.FindAll().ToList();
            var receiptTypeViewModel = Mapper.Map<List<ReceiptTypeViewModel>>(receiptTypes);
            return receiptTypeViewModel;
        }


        public PagedResult<ReceiptTypeViewModel> GetAllPaging(string keyword, int pageSize, int pageIndex)
        {
            var query = _receiptTypeRepository.FindAll();

            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(x => x.Name.Contains(keyword));
            }
          
            var totalRow = query.Count();

            var data = query.OrderBy(x => x.Name)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize);

            var resultPaging = Mapper.Map<List<ReceiptTypeViewModel>>(data);

            return new PagedResult<ReceiptTypeViewModel>()
            {
                CurrentPage = pageIndex,
                PageSize = pageSize,
                Results = resultPaging,
                RowCount = totalRow
            };
        }

        public List<ReceiptTypeViewModel> GetAllWithConditions(string keyword, int status)
        {
            var query = _receiptTypeRepository.FindAll();

            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(x => x.Name.Contains(keyword));
            }

            Status _status = (Status)status;
            if (_status == Status.Active || _status == Status.InActive)
            {
                query = query.Where(x => x.Status == _status).OrderBy(x => x.Name);
            }
            var receiptTypeViewModel = Mapper.Map<List<ReceiptTypeViewModel>>(query);

            return receiptTypeViewModel;
        }


        public ReceiptTypeViewModel GetById(int receiptTypeId)
        {
            var receiptType = _receiptTypeRepository.FindById(receiptTypeId);
            var receiptTypeViewModel = Mapper.Map<ReceiptTypeViewModel>(receiptType);
            return receiptTypeViewModel;
        }

        public bool IsExists(int id)
        {
            var receiptType = _receiptTypeRepository.FindById(id);
            return (receiptType == null) ? false : true;
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public bool Update(ReceiptTypeViewModel receiptTypeViewModel)
        {
            try
            {
                var receiptType = Mapper.Map<ReceiptTypeViewModel, ReceiptType>(receiptTypeViewModel);
                _receiptTypeRepository.Update(receiptType);
                return true;

            }
            catch
            {
                return false;
            }
        }
    }
}
