using LanguageCenterPLC.Application.ViewModels.Categories;
using LanguageCenterPLC.Infrastructure.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace LanguageCenterPLC.Application.ViewModels.Studies
{
    public class PeriodicPointViewModel
    {
        public int Id { get; set; }
        /// <summary>
        /// Ngày vào điểm
        /// </summary>
        
        public DateTime DateOnPoint { get; set; }

        /// <summary>
        /// Ngày tổ chức thi, kiểm tra
        /// </summary>
       
        public DateTime ExaminationDate { get; set; }

        public int Week { get; set; }

        public Status Status { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }

        public string Note { get; set; }



        public string LanguageClassId { get; set; }

        public int LecturerId { get; set; }

        public Guid AppUserId { get; set; }

        /*Reference Table*/
        public  LanguageClassViewModel LanguageClass { get; set; }

        public  LecturerViewModel Lecturer { get; set; }

        public  AppUserViewModel AppUser { get; set; }

        /*List of References */
        public ICollection<PeriodicPointDetailViewModel> PeriodicPointDetails { set; get; }
    }
}
