using LanguageCenterPLC.Application.ViewModels.Timekeepings;
using System;
using System.Collections.Generic;

namespace LanguageCenterPLC.Application.Interfaces
{
    public interface ITimesheetService
    {
        bool Add(TimesheetViewModel timesheetVm);

        bool Update(TimesheetViewModel timesheetVm);
        bool UpdateTamUng(TimesheetViewModel timesheetVm, decimal tientamung);

        bool Delete(int id);

        List<TimesheetViewModel> GetAll();

        List<TimesheetViewModel> GetAllWithConditions(int month, int year);
        TimesheetViewModel GetWithCondition(string personnelId, int month, int year);

        TimesheetViewModel GetById(int id);
        bool IsExists(int id);
        bool IsExistsTimeSheetCondition(int month, int year, string personnelId);

        bool AddRange(int month, int year, Guid userId);
        
        void SaveChanges();
    }
}
