using LanguageCenterPLC.Application.ViewModels.Categories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LanguageCenterPLC.Application.Interfaces
{
    public interface ILanguageClassService
    {
        Task<bool> AddAsync(LanguageClassViewModel languageClassVm);

        Task<bool> UpdateAsync(LanguageClassViewModel languageClassVm);

        Task<bool> DeleteAsync(int id);

        Task< List<LanguageClassViewModel>> GetAll();

        Task<LanguageClassViewModel> GetById(int id);

        void SaveChanges();
    }
}
