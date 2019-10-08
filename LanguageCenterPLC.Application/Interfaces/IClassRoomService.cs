using LanguageCenterPLC.Application.ViewModels.Studies;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LanguageCenterPLC.Application.Interfaces
{
    public interface IClassroomService
    {
        Task<bool> AddAsync(ClassroomViewModel classroomVm);

        Task<bool> UpdateAsync(ClassroomViewModel classroomVm);

        Task<bool> DeleteAsync(int id);

        Task<List<ClassroomViewModel>> GetAll();

        Task<ClassroomViewModel> GetById(int id);

        void SaveChanges();
    }
}
