using LanguageCenterPLC.Infrastructure.Enums;
using System.Collections.Generic;

namespace LanguageCenterPLC.Application.ViewModels.Finances
{
    public class FunctionViewModel
    {
        
        public string Id { get; set; }

        public string Name { set; get; }

        public string URL { set; get; }

        public string ParentId { set; get; }

        public string IconCss { get; set; }

        public int SortOrder { set; get; }

        public List<FunctionViewModel> ChildFunctionViewModels { get; set; }

        public Status Status { set; get; }
    }
}
