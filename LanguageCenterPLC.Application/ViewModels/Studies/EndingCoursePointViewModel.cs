using LanguageCenterPLC.Application.ViewModels.Categories;
using LanguageCenterPLC.Infrastructure.Enums;
using System;
using System.Collections.Generic;

namespace LanguageCenterPLC.Application.ViewModels.Studies
{
    public class EndingCoursePointViewModel
    {
        public int Id { get; set; }

        public DateTime DateOnPoint { get; set; }

        public DateTime ExaminationDate { get; set; }

        public Status Status { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime? DateModified { get; set; }

        public string Note { get; set; }

        public string LanguageClassId { get; set; }

        public int LecturerId { get; set; }

        public string LanguageClassName { get; set; }

        public string LecturerName { get; set; }

        public Guid AppUserId { get; set; }

        public bool isLocked { get; set; }


        public LanguageClassViewModel LanguageClass { get; set; }

        public LecturerViewModel Lecturer { get; set; }

        public AppUserViewModel AppUser { get; set; }

        public ICollection<EndingCoursePointDetailViewModel> EndingCoursePointDetails { set; get; }
    }
}
