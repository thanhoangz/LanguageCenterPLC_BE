using System;
using System.Collections.Generic;
using System.Text;

namespace LanguageCenterPLC.Application.ViewModels.Finances
{
    public class LogWorkViewModel
    {
        public long Id { get; set; }
        public string Content { get; set; }
        public DateTime DateCreated { set; get; }
    }
}
