using LanguageCenterPLC.Application.ViewModels.Categories;
using LanguageCenterPLC.Application.ViewModels.Studies;
using System;
using System.Collections.Generic;

namespace LanguageCenterPLC.Application.ViewModels.Timekeepings
{
    public class AttendanceSheetViewModel
    {
        public int Id { get; set; }
        /// <summary>
        /// Tiền công giảng dạy với giáo viên
        /// </summary>
        public decimal WageOfLecturer { get; set; }

        /// <summary>
        /// Tiền công giảng dạy với trợ giảng
        /// </summary>
        public decimal WageOfTutor { get; set; }


        /// <summary>
        /// Ngày điểm danh
        /// </summary>
        public DateTime Date { get; set; }


        public StatusViewModel Status { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }

        public string Note { get; set; }

        /* Foreign Key */

        //[Required]
        public string LanguageClassId { get; set; }

        // [Required]
        // [ForeignKey(nameof(Lecturer)), Column(Order = 0)]
        public int? LecturerId { get; set; }

        //[ForeignKey(nameof(Tutor)), Column(Order = 1)]
        public int? TutorId { get; set; }

        // [Required]
        public Guid AppUserId { get; set; }

        /*Reference Table*/

        //[ForeignKey("LanguageClassId")]
        public LanguageClassViewModel LanguageClass { get; set; }

        public LecturerViewModel Lecturer { get; set; }

        public LecturerViewModel Tutor { get; set; }

        //[ForeignKey("AppUserId")]
        public AppUserViewModel AppUser { get; set; }

        /*List of References */
        public ICollection<AttendanceSheetDetailViewModel> AttendanceSheetDetails { set; get; }
    }
}
