using LanguageCenterPLC.Application.ViewModels.Categories;
using LanguageCenterPLC.Application.ViewModels.Finances;

namespace LanguageCenterPLC.Application.Interfaces
{
    public interface ICommonService
    {
        FooterViewModel GetFooter();
       
        SystemConfigViewModel GetSystemConfig(string code);
    }
}
