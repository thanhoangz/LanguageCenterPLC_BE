using LanguageCenterPLC.Application.ViewModels.Finances;
using System;

namespace LanguageCenterPLC.Application.ViewModels.Categories
{
    public class PermissionViewModel
    {
        public int Id { get; set; }

        public bool CanCreate { set; get; }

        public bool CanRead { set; get; }

        public bool CanUpdate { set; get; }

        public bool CanDelete { set; get; }

        public Guid AppUserId { get; set; }

        public string FunctionId { get; set; }

        public string UserName { get; set; }

        public string FunctionName { get; set; }

    }
}
