using AutoMapper;
using LanguageCenterPLC.Application.Interfaces;
using LanguageCenterPLC.Application.ViewModels.Timekeepings;
using LanguageCenterPLC.Data.Entities;
using LanguageCenterPLC.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LanguageCenterPLC.Application.Implementation
{
    public class TimesheetService : ITimesheetService
    {
        private readonly IRepository<Timesheet, int> _timesheetRepository;

        private readonly IUnitOfWork _unitOfWork;
        public TimesheetService(IRepository<Timesheet, int> timesheetRepository,IUnitOfWork unitOfWork)
        {
            _timesheetRepository = timesheetRepository;
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
            List<Timesheet> timeSheet = _timesheetRepository.FindAll().ToList();
            var timesheetViewModels = Mapper.Map<List<TimesheetViewModel>>(timeSheet);
            return timesheetViewModels;
        }

        public List<TimesheetViewModel> GetAllWithConditions(DateTime month, DateTime year)
        {
            throw new NotImplementedException();
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
