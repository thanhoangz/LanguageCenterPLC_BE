using LanguageCenterPLC.Application.ViewModels.Categories;
using System;
using System.Collections.Generic;
using System.Text;

namespace LanguageCenterPLC.Application.ViewModels.Studies
{
    public class EndingCoursePointViewModel
    {
        /// <summary>
        /// Ngày vào điểm
        /// </summary>
        public int Id { get; set; }
        public DateTime DateOnPoint { get; set; }

        /// <summary>
        /// Ngày tổ chức thi, kiểm tra
        /// </summary>

        public DateTime ExaminationDate { get; set; }



        public StatusViewModel Status { get; set; }


        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }

        public string Note { get; set; }

        /* Foreign Key */

        //[Required]
         public string LanguageClassId { get; set; }

        // [Required]
           public int LecturerId { get; set; }

        // [Required]
         public Guid AppUserId { get; set; }

        /*Reference Table*/
        //[ForeignKey("LanguageClassId")]
        public LanguageClassViewModel LanguageClass { get; set; }

        // [ForeignKey("LecturerId")]
        public LecturerViewModel Lecturer { get; set; }

        // [ForeignKey("AppUserId")]
        public AppUserViewModel AppUser { get; set; }

        /*List of References */
        public ICollection<EndingCoursePointDetailViewModel> EndingCoursePointDetails { set; get; }
    }
}
