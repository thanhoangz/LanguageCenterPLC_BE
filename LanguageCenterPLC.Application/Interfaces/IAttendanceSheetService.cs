using LanguageCenterPLC.Application.ViewModels.Timekeepings;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LanguageCenterPLC.Application.Interfaces
{
    public interface IAttendanceSheetService
    {
        Task<bool> AddAsync(AttendanceSheetViewModel attendanceSheetVm);

        Task<bool> UpdateAsync(AttendanceSheetViewModel attendanceSheetVm);

        Task<bool> DeleteAsync(int id);

        Task<List<AttendanceSheetViewModel>> GetAll();

        Task<AttendanceSheetViewModel> GetById(int id);

        void SaveChanges();
    }
}
