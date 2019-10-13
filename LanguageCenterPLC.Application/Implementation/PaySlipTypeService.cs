using AutoMapper;
using LanguageCenterPLC.Application.Interfaces;
using LanguageCenterPLC.Application.ViewModels.Categories;
using LanguageCenterPLC.Data.Entities;
using LanguageCenterPLC.Infrastructure.Enums;
using LanguageCenterPLC.Infrastructure.Interfaces;
using LanguageCenterPLC.Utilities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LanguageCenterPLC.Application.Implementation
{
    public class PaySlipTypeService : IPaySlipTypeService
    {

        private readonly IRepository<PaySlipType, int> _paysliptypeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PaySlipTypeService(IRepository<PaySlipType, int> paysliptypeRepository,
           IUnitOfWork unitOfWork)
        {
            _paysliptypeRepository = paysliptypeRepository;
            _unitOfWork = unitOfWork;
        }


        public bool Add(PaySlipTypeViewModel payslipTypeVm)
        {
            try
            {
                var paysliptype = Mapper.Map<PaySlipTypeViewModel, PaySlipType>(payslipTypeVm);

                _paysliptypeRepository.Add(paysliptype);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                _paysliptypeRepository.Remove(id);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<PaySlipTypeViewModel> GetAll()
        {
            List<PaySlipType> paysliptype = _paysliptypeRepository.FindAll().ToList();
            var paysliptypeViewModel = Mapper.Map<List<PaySlipTypeViewModel>>(paysliptype);
            return paysliptypeViewModel;
        }

        public PagedResult<PaySlipTypeViewModel> GetAllPaging(string keyword, int status, int pageSize, int pageIndex)
        {
            var query = _paysliptypeRepository.FindAll();

            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(x => x.Name.Contains(keyword));
            }

            Status _status = (Status)status;

            query = query.Where(x => x.Status == _status);

            var totalRow = query.Count();

            var data = query.OrderByDescending(x => x.Name)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize);

            var resultPaging = Mapper.Map<List<PaySlipTypeViewModel>>(data);

            return new PagedResult<PaySlipTypeViewModel>()
            {
                CurrentPage = pageIndex,
                PageSize = pageSize,
                Results = resultPaging,
                RowCount = totalRow
            };
        }

        public List<PaySlipTypeViewModel> GetAllWithConditions(string keyword, int status)
        {
            var query = _paysliptypeRepository.FindAll();

            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(x => x.Name.Contains(keyword));
            }

            Status _status = (Status)status;

            if (_status == Status.Active || _status == Status.InActive)
            {
                query = query.Where(x => x.Status == _status).OrderBy(x => x.Name);
            }

            var paySliptypeViewModel = Mapper.Map<List<PaySlipTypeViewModel>>(query);

            return paySliptypeViewModel;
        }

        public PaySlipTypeViewModel GetById(int id)
        {
            var paysliptype = _paysliptypeRepository.FindById(id);
            var paysliptypeViewModel = Mapper.Map<PaySlipTypeViewModel>(paysliptype);
            return paysliptypeViewModel;
        }

        public bool IsExists(int id)
        {
            var paysliptype = _paysliptypeRepository.FindById(id);
            return (paysliptype == null) ? false : true;
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public bool Update(PaySlipTypeViewModel payslipTypeVm)
        {
            try
            {
                var paysliptype = Mapper.Map<PaySlipTypeViewModel, PaySlipType>(payslipTypeVm);

                _paysliptypeRepository.Update(paysliptype);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool UpdateStatus(int paysliptypeId, Status status)
        {
            try
            {
                var paysliptype = _paysliptypeRepository.FindById(paysliptypeId);
                paysliptype.Status = status;
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
