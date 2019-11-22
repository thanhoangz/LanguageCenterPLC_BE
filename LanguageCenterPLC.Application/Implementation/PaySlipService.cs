using AutoMapper;
using LanguageCenterPLC.Application.Interfaces;
using LanguageCenterPLC.Application.ViewModels.Studies;
using LanguageCenterPLC.Application.ViewModels.Timekeepings;
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
    public class PaySlipService : IPaySlipService
    {
        private readonly IRepository<PaySlip, string> _payslipRepository;
        private readonly IRepository<PaySlipType, int> _paysliptypeRepository;
        private readonly IRepository<Timesheet, int> _timesheetRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly AppDbContext _context;
        public PaySlipService(IRepository<PaySlip, string> payslipRepository, IRepository<PaySlipType, int> paySlipTypeRepository,
            IRepository<Timesheet, int> timesheetRepository, IUnitOfWork unitOfWork,
            AppDbContext context
            )
        {
            _payslipRepository = payslipRepository;
            _paysliptypeRepository = paySlipTypeRepository;
            _timesheetRepository = timesheetRepository;
            _unitOfWork = unitOfWork;
            _context = context;
        }
        
        public TimesheetViewModel GetWithConditions(string personnelId, int month, int year)
        {
            var timeSheets = _timesheetRepository.FindAll().Where(x => x.PersonnelId == personnelId && x.Month == month && x.Year == year).SingleOrDefault();

            var timesheetViewModels = Mapper.Map<TimesheetViewModel>(timeSheets);

            return timesheetViewModels;
        }

        public bool Add(PaySlipViewModel payslipVm)
        {
            try
            {
                var payslip = Mapper.Map<PaySlipViewModel, PaySlip>(payslipVm);

                payslip.Id = TextHelper.RandomNumber(10);

                _payslipRepository.Add(payslip);
                SaveChanges();

            
                if(payslip.PaySlipTypeId == 1)  // =1 là loại chi tạm ứng      
                {
                    var Tamung = payslip.Total; // tiền tạm ứng
                    
                    if ( !string.IsNullOrEmpty(payslip.ReceivePersonnelId)) // mã nv khác rỗng
                    {
                        var timeSheet = _context.Timesheets.Where(x => x.Month == payslip.Date.Month && x.Year == payslip.Date.Year && x.PersonnelId == payslip.ReceivePersonnelId).SingleOrDefault();
                        timeSheet.AdvancePayment += Tamung; // update tạm ứng trong chấm công
                        timeSheet.TotalActualSalary = timeSheet.SalaryOfDay * Convert.ToDecimal(timeSheet.TotalWorkday)
                    + timeSheet.Allowance + timeSheet.Bonus - timeSheet.AdvancePayment - timeSheet.InsurancePremiums;
                        _context.Timesheets.Update(timeSheet);
                        _context.SaveChanges();
                    }                
                }

                

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

       

        public List<PaySlipViewModel> GetAllWithConditions(DateTime? startDate, DateTime? endDate, string keyword,int phieuchi, int status)
        {
            var query = _payslipRepository.FindAll();

            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(x => x.Id == keyword);
            }

            Status _status = (Status)status;

            if (_status == Status.Active || _status == Status.InActive || _status == Status.Pause)
            {
                query = query.Where(x => x.Status == _status).OrderByDescending(x => x.DateCreated);
            }

            if (startDate != null && endDate !=null )
            {
                query = query.Where(x => x.Date >= startDate && x.Date <= endDate);
            }
            if (phieuchi !=-1)
            {
                query = query.Where(x => x.PaySlipTypeId == phieuchi);
            }
               
            var payslipViewModel = Mapper.Map<List<PaySlipViewModel>>(query);

            return payslipViewModel;
        }

        public List<PaySlipViewModel> GetAllWithConditions_report(int month, int year)
        {
            var query = _payslipRepository.FindAll().Where(x => x.Status == Status.Active).OrderBy(x => x.Date).ToList();
            query = query.Where(x => x.Date.Year == year && x.Date.Month == month).ToList();
            var payslipViewModel = Mapper.Map<List<PaySlipViewModel>>(query);
            foreach (var item in payslipViewModel)
            {
                string name = _paysliptypeRepository.FindById(item.PaySlipTypeId).Name;
                item.PaySlipTypeName = name;
            }

            return payslipViewModel;
        }


    }
}

