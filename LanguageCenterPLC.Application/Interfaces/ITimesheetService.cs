using LanguageCenterPLC.Application.ViewModels.Timekeepings;
using LanguageCenterPLC.Utilities.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LanguageCenterPLC.Application.Interfaces
{
    public interface ITimesheetService
    {
        bool Add(TimesheetViewModel timesheetVm);

        bool Update(TimesheetViewModel timesheetVm);

        bool Delete(int id);

        List<TimesheetViewModel> GetAll();

        List<TimesheetViewModel> GetAllWithConditions(DateTime month, DateTime year);

        TimesheetViewModel GetById(int id);
        bool IsExists(int id);


        void SaveChanges();
    }
}
