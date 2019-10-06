using LanguageCenterPLC.Application.ViewModels.Finances;
using System;
using System.Collections.Generic;
using System.Text;

namespace LanguageCenterPLC.Application.ViewModels.Categories
{
    public class PermissionViewModel
    {
        public int Id { get; set; }
        
        public bool CanCreate { set; get; }

        public bool CanRead { set; get; }

        public bool CanUpdate { set; get; }

        public bool CanDelete { set; get; }

        public Guid AppRoleId { get; set; }

        public string FunctionId { get; set; }

        public AppRoleViewModel AppRole { get; set; }

        public  FunctionViewModel Function { get; set; }
    }
}
