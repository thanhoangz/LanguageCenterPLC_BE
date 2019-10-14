using AutoMapper;
using LanguageCenterPLC.Application.Interfaces;
using LanguageCenterPLC.Application.ViewModels.Studies;
using LanguageCenterPLC.Data.Entities;
using LanguageCenterPLC.Infrastructure.Enums;
using LanguageCenterPLC.Infrastructure.Interfaces;
using LanguageCenterPLC.Utilities.Dtos;
using System.Collections.Generic;
using System.Linq;

namespace LanguageCenterPLC.Application.Implementation
{
    public class PaySlipService : IPaySlipService
    {
        private readonly IRepository<PaySlip, string> _payslipRepository;
        private readonly IRepository<PaySlipType, int> _paysliptypeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PaySlipService(IRepository<PaySlip, string> payslipRepository, IRepository<PaySlipType, int> paySlipTypeRepository, IUnitOfWork unitOfWork)
        {
            _payslipRepository = payslipRepository;
            _paysliptypeRepository = paySlipTypeRepository;
            _unitOfWork = unitOfWork;
        }

        public bool Add(PaySlipViewModel payslipVm)
        {
            try
            {
                var payslip = Mapper.Map<PaySlipViewModel, PaySlip>(payslipVm);

                _payslipRepository.Add(payslip);

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
                var payslip = _payslipRepository.FindById(id);

                _payslipRepository.Remove(payslip);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<PaySlipViewModel> GetAll()
        {
            var paysliptypes = _paysliptypeRepository.FindAll().ToList();

            var payslips = _payslipRepository.FindAll().ToList();

            var payslipViewModel = Mapper.Map<List<PaySlipViewModel>>(payslips);

            foreach (var item in payslipViewModel)
            {
                string name = _paysliptypeRepository.FindById(item.PaySlipTypeId).Name;
                item.PaySlipTypeName = name;
            }

            return payslipViewModel;

        }

        public List<PaySlipViewModel> GetAllWithConditions(string keyword, int status)
        {
            var query = _payslipRepository.FindAll();

            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(x => x.Id.Contains(keyword));
            }

            Status _status = (Status)status;

            if (_status == Status.Active || _status == Status.InActive)
            {
                query = query.Where(x => x.Status == _status).OrderBy(x => x.DateCreated);
            }

            var payslipViewModel = Mapper.Map<List<PaySlipViewModel>>(query);

            return payslipViewModel;
        }

        public PagedResult<PaySlipViewModel> GetAllPaging(string keyword, int status, int pageSize, int pageIndex)
        {
            var query = _payslipRepository.FindAll();
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
            var resultPaging = Mapper.Map<List<PaySlipViewModel>>(data);

            return new PagedResult<PaySlipViewModel>()
            {
                CurrentPage = pageIndex,
                PageSize = pageSize,
                Results = resultPaging,
                RowCount = totalRow
            };
        }

        

        public PaySlipViewModel GetById(string id)
        {
            var payslip = _payslipRepository.FindById(id);
            var payslipViewModel = Mapper.Map<PaySlipViewModel>(payslip);
            return payslipViewModel;
        }

        public bool IsExists(string id)
        {
            var course = _payslipRepository.FindById(id);
            return (course == null) ? false : true;
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public bool Update(PaySlipViewModel payslipVm)
        {
            try
            {
                var payslip = Mapper.Map<PaySlipViewModel, PaySlip>(payslipVm);
                _payslipRepository.Update(payslip);
                return true;

            }
            catch
            {
                return false;
            }
        }

        public bool UpdateStatus(string payslipId, Status status)
        {
            try
            {
                var payslip = _payslipRepository.FindById(payslipId);
                payslip.Status = status;
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}

