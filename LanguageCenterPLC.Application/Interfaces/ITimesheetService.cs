using LanguageCenterPLC.Application.ViewModels.Timekeepings;
using System.Collections.Generic;

namespace LanguageCenterPLC.Application.Interfaces
{
    public interface ITimesheetService
    {
        bool Add(TimesheetViewModel timesheetVm);

        bool Update(TimesheetViewModel timesheetVm);

        bool Delete(int id);

        List<TimesheetViewModel> GetAll();

        List<TimesheetViewModel> GetAllWithConditions(int month, int year);

        TimesheetViewModel GetById(int id);
        bool IsExists(int id);
        bool IsExistsTimeSheetCondition(int month, int year, string personnelId);

        bool AddRange(int month, int year);
        
        void SaveChanges();
    }
}
