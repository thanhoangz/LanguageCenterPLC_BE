using LanguageCenterPLC.Application.ViewModels.Studies;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LanguageCenterPLC.Application.Interfaces
{
    public interface IEndingCoursePointService
    {
        Task<bool> AddAsync(EndingCoursePointViewModel endingCoursePointVm);

        Task<bool> UpdateAsync(EndingCoursePointViewModel endingCoursePointVm);

        Task<bool> DeleteAsync(int id);

        Task<List<EndingCoursePointViewModel>> GetAll();

        Task<EndingCoursePointViewModel> GetById(int id);

        void SaveChanges();
    }
}
