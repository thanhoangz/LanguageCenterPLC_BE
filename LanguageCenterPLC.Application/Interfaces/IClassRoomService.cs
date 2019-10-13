using LanguageCenterPLC.Application.ViewModels.Studies;
using LanguageCenterPLC.Utilities.Dtos;
using System.Collections.Generic;

namespace LanguageCenterPLC.Application.Interfaces
{
    public interface IClassroomService
    {

        bool Add(ClassroomViewModel classroomVm);

        bool Update(ClassroomViewModel classroomVm);

        bool Delete(int classroomId);

        List<ClassroomViewModel> GetAll();

        ClassroomViewModel GetById(int classroomId);
        PagedResult<ClassroomViewModel> GetAllPaging(string keyword,
          int pageSize, int pageIndex);
        List<ClassroomViewModel> GetAllWithConditions(string keyword, int status);

        bool IsExists(int classroomId);

        void SaveChanges();
    }
}
