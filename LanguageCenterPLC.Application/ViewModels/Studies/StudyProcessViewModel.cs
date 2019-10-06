using LanguageCenterPLC.Application.ViewModels.Categories;
using LanguageCenterPLC.Infrastructure.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace LanguageCenterPLC.Application.ViewModels.Studies
{
    public class StudyProcessViewModel
    {
        public int Id { get; set; }
        public DateTime OutDate { get; set; }

        public DateTime InDate { get; set; }

        public Status Status { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }

        public string Note { get; set; }


        /* Foreign Key */

        public string LanguageClassId { get; set; }

        public string LearnerId { get; set; }

        /*Reference Table*/

        public  LearnerViewModel Learner { get; set; }


        public  LanguageClassViewModel LanguageClass { get; set; }
    }
}
