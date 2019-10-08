using LanguageCenterPLC.Application.ViewModels.Finances;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LanguageCenterPLC.Application.Interfaces
{
    public interface ILogWorkService
    {
        Task<bool> AddAsync(LogWorkViewModel logWorkVm);

        Task<bool> UpdateAsync(LogWorkViewModel logWorkVm);

        Task<bool> DeleteAsync(int id);

        Task< List<LogWorkViewModel>> GetAll();

        Task<LogWorkViewModel> GetById(int id);

        void SaveChanges();
    }
}
