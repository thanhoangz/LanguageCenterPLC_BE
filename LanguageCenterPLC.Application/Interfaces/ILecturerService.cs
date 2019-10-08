using LanguageCenterPLC.Application.ViewModels.Studies;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LanguageCenterPLC.Application.Interfaces
{
    public interface ILecturerService
    {
        Task<bool> AddAsync(LecturerViewModel lecturerVm);

        Task<bool> UpdateAsync(LecturerViewModel lecturerVm);

        Task<bool> DeleteAsync(int id);

        Task<List<LecturerViewModel>> GetAll();

         Task<LecturerViewModel> GetById(int id);

        void SaveChanges();
    }
}
