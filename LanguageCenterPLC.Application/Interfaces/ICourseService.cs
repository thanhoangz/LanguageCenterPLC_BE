using LanguageCenterPLC.Application.ViewModels;
using LanguageCenterPLC.Infrastructure.Enums;
using LanguageCenterPLC.Utilities.Dtos;

namespace LanguageCenterPLC.Application.Interfaces
{
    public interface ICourseService
    {
        void Create(CourseViewModel courseVm);

        void Update(CourseViewModel courseVm);

        PagedResult<CourseViewModel> GetAllPaging(string keyword, int status,
            int pageIndex, int pageSize);

        CourseViewModel GetDetail(int courseId);

        void Delete(int courseId);

        void UpdateStatus(int courseId, Status status);

        void Save();
    }
}
