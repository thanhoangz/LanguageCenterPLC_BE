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
    /// Điểm kết thúc khóa học
    /// </summary>
    [Table("EndingCoursePoints")]
    public class EndingCoursePoint : DomainEntity<int>, ISwitchable, IDateTracking
    {
        /// <summary>
        /// Ngày vào điểm
        /// </summary>
        [Required]
        public DateTime DateOnPoint { get; set; }

        /// <summary>
        /// Ngày tổ chức thi, kiểm tra
        /// </summary>
        [Required]
        public DateTime ExaminationDate { get; set; }


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
        public int LecturerId { get; set; }

        [Required]
        public int UserId { get; set; }

        /*Reference Table*/
        [ForeignKey("LanguageClassId")]
        public virtual LanguageClass LanguageClass { get; set; }

        [ForeignKey("LecturerId")]
        public virtual Lecturer Lecturer { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        /*List of References */
        public virtual ICollection<EndingCoursePointDetail> EndingCoursePointDetails { set; get; }

    }
}
