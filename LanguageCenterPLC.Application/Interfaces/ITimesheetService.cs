using LanguageCenterPLC.Application.ViewModels.Timekeepings;
using LanguageCenterPLC.Utilities.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LanguageCenterPLC.Application.Interfaces
{
    public interface ITimesheetService
    {
        Task<bool> AddAsync(TimesheetViewModel timesheetVm);

        Task<bool> UpdateAsync(TimesheetViewModel timesheetVm);

        Task<bool> DeleteAsync(int id);

        Task<List<TimesheetViewModel>> GetAllAsync();

        PagedResult<TimesheetViewModel> GetAllPagingAsync(string keyword, int page, int pageSize);

        Task<TimesheetViewModel> GetById(int id);

        void SaveChanges();
    }
}
