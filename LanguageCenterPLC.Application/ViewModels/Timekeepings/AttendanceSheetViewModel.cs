using LanguageCenterPLC.Application.ViewModels.Categories;
using LanguageCenterPLC.Application.ViewModels.Studies;
using LanguageCenterPLC.Infrastructure.Enums;
using System;
using System.Collections.Generic;

namespace LanguageCenterPLC.Application.ViewModels.Timekeepings
{
    public class AttendanceSheetViewModel
    {
        public int Id { get; set; }

        public decimal WageOfLecturer { get; set; }

        public decimal WageOfTutor { get; set; }

        public DateTime Date { get; set; }


        public Status Status { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }

        public string Note { get; set; }

        public string LanguageClassId { get; set; }

        public int? LecturerId { get; set; }

        public int? TutorId { get; set; }

        public Guid AppUserId { get; set; }

        public LanguageClassViewModel LanguageClass { get; set; }

        public LecturerViewModel Lecturer { get; set; }

        public LecturerViewModel Tutor { get; set; }

        public AppUserViewModel AppUser { get; set; }

        public ICollection<AttendanceSheetDetailViewModel> AttendanceSheetDetails { set; get; }
    }
}
