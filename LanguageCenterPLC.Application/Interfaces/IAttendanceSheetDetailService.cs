using LanguageCenterPLC.Application.ViewModels.Timekeepings;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LanguageCenterPLC.Application.Interfaces
{
    public interface IAttendanceSheetDetailService
    {
        Task<bool> AddAsync(AttendanceSheetDetailViewModel attendanceSheetDetailVm);

        Task<bool> UpdateAsync(AttendanceSheetDetailViewModel attendanceSheetDetailVm);

        Task<bool> DeleteAsync(int id);

        Task<List<AttendanceSheetDetailViewModel>> GetAll();

        Task<AttendanceSheetDetailViewModel> GetById(int id);

        void SaveChanges();
    }
}
