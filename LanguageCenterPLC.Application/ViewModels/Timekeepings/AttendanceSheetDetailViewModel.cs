using LanguageCenterPLC.Application.ViewModels.Categories;
using LanguageCenterPLC.Application.ViewModels.Studies;
using LanguageCenterPLC.Infrastructure.Enums;
using System;

namespace LanguageCenterPLC.Application.ViewModels.Timekeepings
{
    public class AttendanceSheetDetailViewModel
    {
        public int Id { get; set; }

        public Status Status { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }

        public string Note { get; set; }

        public string LearnerId { get; set; }

        public string LanguageClassId { get; set; }

        public int AttendanceSheetId { get; set; }

        public  LearnerViewModel Learner { get; set; }

        public  LanguageClassViewModel LanguageClass { get; set; }

        public virtual AttendanceSheetViewModel AttendanceSheet { get; set; }
    }
}
