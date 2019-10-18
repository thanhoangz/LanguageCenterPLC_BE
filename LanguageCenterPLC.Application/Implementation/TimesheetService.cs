using AutoMapper;
using LanguageCenterPLC.Application.Interfaces;
using LanguageCenterPLC.Application.ViewModels.Timekeepings;
using LanguageCenterPLC.Data.Entities;
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
        public TimesheetService(IRepository<Timesheet, int> timesheetRepository,IRepository<Personnel,string> peronelRepository, IUnitOfWork unitOfWork)
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
                string name = _peronelRepository.FindById(item.PersonnelId).FirstName+' '+_peronelRepository.FindById(item.PersonnelId).LastName;
                item.PersonnelName = name;
            }
            return timesheetViewModels;
        }

        public List<TimesheetViewModel> GetAllWithConditions(int month , int year)
        {
            var timeSheets = _timesheetRepository.FindAll();
            if (month != 0 && year!=0)
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

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public bool Update(TimesheetViewModel timesheetVm)
        {
            try
            {
                //timesheetVm.TotalWorkday = timesheetVm.Day_1 + timesheetVm.Day_2 + timesheetVm.Day_3 + timesheetVm.Day_4 + timesheetVm.Day_5 + timesheetVm.Day_6 + timesheetVm.Day_7 + timesheetVm.Day_8 + timesheetVm.Day_9 + timesheetVm.Day_10 + timesheetVm.Day_11 + timesheetVm.Day_12 + timesheetVm.Day_13 + timesheetVm.Day_14 + timesheetVm.Day_15 + timesheetVm.Day_16 + timesheetVm.Day_17 + timesheetVm.Day_18 + timesheetVm.Day_19 + timesheetVm.Day_20 + timesheetVm.Day_21 + timesheetVm.Day_22 + timesheetVm.Day_23 + timesheetVm.Day_24 + timesheetVm.Day_25 + timesheetVm.Day_26 + timesheetVm.Day_27 + timesheetVm.Day_28 + timesheetVm.Day_29 + timesheetVm.Day_30 + timesheetVm.Day_31 ;
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
