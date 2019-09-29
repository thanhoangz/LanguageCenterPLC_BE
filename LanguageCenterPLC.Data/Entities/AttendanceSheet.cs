using LanguageCenterPLC.Infrastructure.Enums;
using LanguageCenterPLC.Infrastructure.Interfaces;
using LanguageCenterPLC.Infrastructure.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LanguageCenterPLC.Data.Entities
{
    /// <summary>
    /// Tờ điểm danh
    /// </summary>
    [Table("AttendanceSheets")]
    public class AttendanceSheet : DomainEntity<int>, ISwitchable, IDateTracking
    {

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

        [Required]
        public Status Status { get; set; }

        [Required]
        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }

        public string Note { get; set; }

        /* Foreign Key */

        [Required]
        public string LanguageClassId { get; set; }

        [Required]
        [ForeignKey(nameof(Lecturer)), Column(Order = 0)]
        public int? LecturerId { get; set; }

        [ForeignKey(nameof(Tutor)), Column(Order = 1)]
        public int? TutorId { get; set; }

        [Required]
        public int UserId { get; set; }

        /*Reference Table*/

        [ForeignKey("LanguageClassId")]
        public virtual LanguageClass LanguageClass { get; set; }

        public virtual Lecturer Lecturer { get; set; }


        public virtual Lecturer Tutor { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        /*List of References */
        public ICollection<AttendanceSheetDetail> AttendanceSheetDetails { set; get; }
    }
}
