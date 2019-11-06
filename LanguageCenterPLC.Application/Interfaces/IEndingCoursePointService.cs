using LanguageCenterPLC.Application.ViewModels.Studies;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LanguageCenterPLC.Application.Interfaces
{
    public interface IEndingCoursePointService
    {
        bool Add(EndingCoursePointViewModel endingCoursePointVm);

        bool Update(EndingCoursePointViewModel endingCoursePointVm);

        bool Delete(int id);

        List<EndingCoursePointViewModel> GetAll();

        EndingCoursePointViewModel GetById(int id);
        EndingCoursePointViewModel GetAllWithConditions(string languageClassId);

        bool IsExists(int id);
        void SaveChanges();
    }
}
