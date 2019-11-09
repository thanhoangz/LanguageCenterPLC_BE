using LanguageCenterPLC.Application.ViewModels.Studies;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LanguageCenterPLC.Application.Interfaces
{
    public interface ILecturerService
    {
        bool Add(LecturerViewModel lecturerVm);

        bool Update(LecturerViewModel lecturerVm);

        bool Delete(int id);

        List<LecturerViewModel> GetAll();

         LecturerViewModel GetById(int id);
        bool IsExists(int id);
        List<LecturerViewModel> GetAllWithConditions(string keyword, string position, int status);
        LecturerViewModel GetByCardId(string keyword);

        public List<LecturerViewModel> GetAllTutors();
        void SaveChanges();
    }
}
