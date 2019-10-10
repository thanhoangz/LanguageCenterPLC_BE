using LanguageCenterPLC.Application.ViewModels;
using LanguageCenterPLC.Infrastructure.Enums;
using LanguageCenterPLC.Utilities.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LanguageCenterPLC.Application.Interfaces
{
    public interface ICourseService
    {
        bool Add(CourseViewModel courseVm);

        bool Update(CourseViewModel courseVm);

        bool Delete(int courseId);

        List<CourseViewModel> GetAll();

        PagedResult<CourseViewModel> GetAllPaging(string keyword, int status,
           int pageSize, int pageIndex);

        CourseViewModel GetById(int courseId);

        bool UpdateStatus(int courseId, Status status);

        bool IsExists(int id);

        void SaveChanges();
    }
}
