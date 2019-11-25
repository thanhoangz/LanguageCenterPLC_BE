using AutoMapper;
using LanguageCenterPLC.Application.Interfaces;
using LanguageCenterPLC.Application.ViewModels.Timekeepings;
using LanguageCenterPLC.Data.Entities;
using LanguageCenterPLC.Infrastructure.Enums;
using LanguageCenterPLC.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LanguageCenterPLC.Application.Implementation
{
    public class TimesheetService : ITimesheetService
    {
        private readonly IRepository<Timesheet, int> _timesheetRepository;
        private readonly IRepository<Personnel, string> _peronelRepository;

        private readonly IUnitOfWork _unitOfWork;
        public TimesheetService(IRepository<Timesheet, int> timesheetRepository, IRepository<Personnel, string> peronelRepository, IUnitOfWork unitOfWork)
        {
            _timesheetRepository = timesheetRepository;
            _peronelRepository = peronelRepository;
            _unitOfWork = unitOfWork;
        }
        public bool Add(TimesheetViewModel timesheetVm)
        {
            try
            {
                var timeSheet = Mapper.Map<TimesheetViewModel, Timesheet>(timesheetVm);

                _timesheetRepository.Add(timeSheet);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool AddRange(int month, int year, Guid userId)
        {

            try
            {
                var personnelList = _peronelRepository.FindAll();
                var personnelList1 = personnelList.Where(x => x.Status == Status.Active).ToList();

                foreach (var personnel in personnelList1)
                {
                    if (!IsExistsTimeSheetCondition(month, year, personnel.Id))
                    {
                        Timesheet timeSheet = new Timesheet
                        {
                            Month = month,
                            Year = year,
                            Day_1 = 0,
                            Day_2 = 0,
                            Day_3 = 0,
                            Day_4 = 0,
                            Day_5 = 0,
                            Day_6 = 0,
                            Day_7 = 0,
                            Day_8 = 0,
                            Day_9 = 0,
                            Day_10 = 0,
                            Day_11 = 0,
                            Day_12 = 0,
                            Day_13 = 0,
                            Day_14 = 0,
                            Day_15 = 0,
                            Day_16 = 0,
                            Day_17 = 0,
                            Day_18 = 0,
                            Day_19 = 0,
                            Day_20 = 0,
                            Day_21 = 0,
                            Day_22 = 0,
                            Day_23 = 0,
                            Day_24 = 0,
                            Day_25 = 0,
                            Day_26 = 0,
                            Day_27 = 0,
                            Day_28 = 0,
                            Day_29 = 0,
                            Day_30 = 0,
                            Day_31 = 0,

                            Salary = personnel.BasicSalary
                        };
                        timeSheet.Salary = personnel.BasicSalary;
                        timeSheet.Allowance = personnel.Allowance;
                        timeSheet.Bonus = personnel.Bonus;
                        timeSheet.InsurancePremiums = personnel.InsurancePremium;
                        timeSheet.AdvancePayment = 0;
                        timeSheet.TotalActualSalary = 0;
                        timeSheet.TotalSalary = 0;
                        timeSheet.Status = (Status)1;
                        timeSheet.DateCreated = DateTime.Now;
                        timeSheet.DateModified = DateTime.Now;
                        timeSheet.AppUserId = userId; // need fix
                        timeSheet.PersonnelId = personnel.Id;
                        timeSheet.SalaryOfDay = personnel.SalaryOfDay;
                        timeSheet.isLoocked = false;
                        _timesheetRepository.Add(timeSheet);
                        _unitOfWork.Commit();
                    }
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
            try
            {
                var timeSheet = _timesheetRepository.FindById(id);

                _timesheetRepository.Remove(timeSheet);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<TimesheetViewModel> GetAll()
        {
            var timeSheet = _timesheetRepository.FindAll().ToList();

            var timesheetViewModels = Mapper.Map<List<TimesheetViewModel>>(timeSheet);

            foreach (var item in timesheetViewModels)
            {
                string name = _peronelRepository.FindById(item.PersonnelId).FirstName + ' ' + _peronelRepository.FindById(item.PersonnelId).LastName;
                item.PersonnelName = name;
            }
            return timesheetViewModels;
        }

        public List<TimesheetViewModel> GetAllWithConditions(int month, int year)
        {
            var timeSheets = _timesheetRepository.FindAll();
            if (month != 0 && year != 0)
            {
                timeSheets = timeSheets.Where(x => x.Month == month && x.Year == year);
            }
            var timesheetViewModels = Mapper.Map<List<TimesheetViewModel>>(timeSheets);

            foreach (var item in timesheetViewModels)
            {
                string name = _peronelRepository.FindById(item.PersonnelId).FirstName + ' ' + _peronelRepository.FindById(item.PersonnelId).LastName;
                item.PersonnelName = name;
            }
            return timesheetViewModels;
        }

        public TimesheetViewModel GetWithCondition(string personnelId, int month, int year)
        {
            var timeSheets = _timesheetRepository.FindAll().Where(x => x.PersonnelId == personnelId &&  x.Month == month && x.Year == year).SingleOrDefault();
            
            var timesheetViewModels = Mapper.Map<TimesheetViewModel>(timeSheets);

            return timesheetViewModels;
        }

        public TimesheetViewModel GetById(int id)
        {
            var timeSheet = _timesheetRepository.FindById(id);
            var timesheetViewModels = Mapper.Map<TimesheetViewModel>(timeSheet);
            return timesheetViewModels;
        }

        public bool IsExists(int id)
        {
            var timeSheet = _timesheetRepository.FindById(id);
            return (timeSheet == null) ? false : true;
        }
        public bool IsExistsTimeSheetCondition(int month, int year, string personnelId)
        {
            var timeSheet = _timesheetRepository.FindAll();
            var queryTimeSheet = timeSheet.Where(x => x.Month == month && x.Year == year && x.PersonnelId == personnelId).ToList();
            return (queryTimeSheet.Count == 0) ? false : true;
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public bool Update(TimesheetViewModel timesheetVm)
        {
          
                timesheetVm.TotalWorkday = timesheetVm.Day_1 + timesheetVm.Day_2
                    + timesheetVm.Day_3 + timesheetVm.Day_4 + timesheetVm.Day_5 + timesheetVm.Day_6
                    + timesheetVm.Day_7 + timesheetVm.Day_8 + timesheetVm.Day_9 + timesheetVm.Day_10
                    + timesheetVm.Day_11 + timesheetVm.Day_12 + timesheetVm.Day_13 + timesheetVm.Day_14 + timesheetVm.Day_15
                    + timesheetVm.Day_16 + timesheetVm.Day_17 + timesheetVm.Day_18 + timesheetVm.Day_19 + timesheetVm.Day_20
                    + timesheetVm.Day_21 + timesheetVm.Day_22 + timesheetVm.Day_23 + timesheetVm.Day_24 + timesheetVm.Day_25 + timesheetVm.Day_26
                    + timesheetVm.Day_27 + timesheetVm.Day_28 + timesheetVm.Day_29 + timesheetVm.Day_30 + timesheetVm.Day_31;

                timesheetVm.TotalSalary = timesheetVm.SalaryOfDay * Convert.ToDecimal(timesheetVm.TotalWorkday)
                    + timesheetVm.Allowance + timesheetVm.Bonus  - timesheetVm.InsurancePremiums;

                timesheetVm.TotalActualSalary = timesheetVm.SalaryOfDay * Convert.ToDecimal(timesheetVm.TotalWorkday)
                    +timesheetVm.Allowance + timesheetVm.Bonus - timesheetVm.AdvancePayment - timesheetVm.InsurancePremiums;

                var timeSheet = Mapper.Map<TimesheetViewModel, Timesheet>(timesheetVm);
                _timesheetRepository.Update(timeSheet);
                return true;
          
        }

        public bool UpdateTamUng(TimesheetViewModel timesheetVm, decimal tientamung)
        {
            try
            {
                timesheetVm.AdvancePayment = timesheetVm.AdvancePayment+ tientamung;
                timesheetVm.TotalActualSalary = timesheetVm.SalaryOfDay * Convert.ToDecimal(timesheetVm.TotalWorkday)
                    + timesheetVm.Allowance + timesheetVm.Bonus - timesheetVm.AdvancePayment - timesheetVm.InsurancePremiums;

                var timeSheet = Mapper.Map<TimesheetViewModel, Timesheet>(timesheetVm);
                _timesheetRepository.Update(timeSheet);
                return true;
            }
            catch
            {
                return false;
            }
        }




    }
}
