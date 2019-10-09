using LanguageCenterPLC.Application.ViewModels;
using LanguageCenterPLC.Infrastructure.Enums;
using LanguageCenterPLC.Utilities.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LanguageCenterPLC.Application.Interfaces
{
    public interface ICourseService
    {
        Task<bool> AddSync(CourseViewModel courseVm);

        Task<bool> UpdateSync(CourseViewModel courseVm);
        Task<bool> Delete(int courseId);

        PagedResult<CourseViewModel> GetAllPaging(string keyword, int status,
            int pageIndex, int pageSize);

        Task<CourseViewModel> GetDetail(int courseId);
        Task<List<CourseViewModel>> GetAll();
        Task<bool> UpdateStatusSync(int courseId, Status status);

        void SaveChanges();
    }
}
