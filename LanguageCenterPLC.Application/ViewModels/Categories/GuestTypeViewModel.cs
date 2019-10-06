using LanguageCenterPLC.Application.ViewModels.Studies;
using LanguageCenterPLC.Infrastructure.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace LanguageCenterPLC.Application.ViewModels.Categories
{
    public class GuestTypeViewModel
    {
       
        public string Name { get; set; }

       
        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }

        public Status Status { get; set; }

        public string Note { get; set; }

        /* Foreign Key */
        /*Reference Table*/
        /*List of References */
        public ICollection<LearnerViewModel> Learners { set; get; }
    }
}
