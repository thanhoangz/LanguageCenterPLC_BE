using LanguageCenterPLC.Application.ViewModels.Categories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LanguageCenterPLC.Application.Interfaces
{
    public interface ISystemConfigService
    {
        Task<bool> AddAsync(SystemConfigViewModel systemConfigVm);

        Task<bool> UpdateAsync(SystemConfigViewModel systemConfigVm);

        Task<bool> DeleteAsync(int id);

        Task<List<SystemConfigViewModel>> GetAll();

        Task<SystemConfigViewModel> GetById(int id);

        void SaveChanges();
    }
}
