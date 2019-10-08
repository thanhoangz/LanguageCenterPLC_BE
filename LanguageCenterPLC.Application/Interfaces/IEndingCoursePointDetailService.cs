using LanguageCenterPLC.Application.ViewModels.Studies;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LanguageCenterPLC.Application.Interfaces
{
    public interface IEndingCoursePointDetailService
    {

        Task<bool> AddAsync(EndingCoursePointDetailViewModel endingCoursePointDetailVm);

        Task<bool> UpdateAsync(EndingCoursePointDetailViewModel endingCoursePointDetailVm);

        Task<bool> DeleteAsync(int id);

        Task<List<EndingCoursePointDetailViewModel>> GetAll();

        Task<EndingCoursePointDetailViewModel> GetById(int id);

        void SaveChanges();
    }
}
