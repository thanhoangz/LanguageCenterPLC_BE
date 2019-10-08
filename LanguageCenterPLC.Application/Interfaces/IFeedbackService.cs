using LanguageCenterPLC.Application.ViewModels.Studies;
using LanguageCenterPLC.Utilities.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LanguageCenterPLC.Application.Interfaces
{
    public interface IFeedbackService
    {
        Task<bool> AddAsync(FeedbackViewModel feedbackVm);

        Task<bool> UpdateAsync(FeedbackViewModel feedbackVm);

        Task<bool> DeleteAsync(int id);

        Task<List<FeedbackViewModel>> GetAll();

        PagedResult<FeedbackViewModel> GetAllPaging(string keyword, int page, int pageSize);

        Task<FeedbackViewModel> GetById(int id);

        void SaveChanges();
    }
}
