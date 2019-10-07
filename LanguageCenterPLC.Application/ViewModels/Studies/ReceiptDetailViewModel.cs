using LanguageCenterPLC.Application.ViewModels.Categories;
using LanguageCenterPLC.Application.ViewModels.Studies;
using LanguageCenterPLC.Infrastructure.Enums;
using System;
using System.Collections.Generic;

namespace LanguageCenterPLC.Application.ViewModels.Finances
{
    public class ReceiptDetailViewModel
    {
        public int Id { get; set; }

        public DateTime DateOnPoint { get; set; }

        public DateTime ExaminationDate { get; set; }

        public int Week { get; set; }

        public Status Status { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }

        public string Note { get; set; }

        public string LanguageClassId { get; set; }

        public int LecturerId { get; set; }

        public Guid AppUserId { get; set; }

        public  LanguageClassViewModel LanguageClass { get; set; }

        public  LecturerViewModel Lecturer { get; set; }

        public  AppUserViewModel AppUser { get; set; }

        public ICollection<PeriodicPointDetailViewModel> PeriodicPointDetails { set; get; }
    }
}
