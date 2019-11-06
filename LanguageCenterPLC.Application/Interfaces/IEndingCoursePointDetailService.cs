using LanguageCenterPLC.Application.ViewModels.Studies;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LanguageCenterPLC.Application.Interfaces
{
    public interface IEndingCoursePointDetailService
    {

        bool Add(EndingCoursePointDetailViewModel endingCoursePointDetailVm);

        bool Update(EndingCoursePointDetailViewModel endingCoursePointDetailVm);

        bool Delete(int id);

        List<EndingCoursePointDetailViewModel> GetAll();

        EndingCoursePointDetailViewModel GetById(int id);
        List<EndingCoursePointDetailViewModel> GetAllWithConditions(int endingPointId);
        bool AddRange();
        bool IsExists(int id);
        void SaveChanges();
    }
}
