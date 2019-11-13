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
    /// Lớp ngôn ngữ (mặc định tiếng anh)
    /// </summary>
    [Table("LanguageClasses")]
    public class LanguageClass : DomainEntity<string>, ISwitchable, IDateTracking
    {
        [Required]

        [StringLength(200)]
        public string Name { get; set; }

        [Required]
        public decimal CourseFee { get; set; }

        [Required]
        public decimal MonthlyFee { get; set; }

        [Required]
        public decimal LessonFee { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime StartDay { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime EndDay { get; set; }

        public int MaxNumber { get; set; }

        [Required]
        public Status Status { get; set; }

        [Required]
        public DateTime DateCreated { get; set; }

        public DateTime? DateModified { get; set; }

        public string Note { get; set; }

        public decimal? WageOfLecturer { get; set; }

        public decimal? WageOfTutor { get; set; }


        [ForeignKey("CourseId")]
        public int CourseId { get; set; }
        public virtual Course Course { get; set; }



        public virtual ICollection<StudyProcess> StudyProcesses { set; get; }
        public virtual ICollection<EndingCoursePoint> EndingCoursePoints { set; get; }
        public virtual ICollection<PeriodicPoint> PeriodicPoints { set; get; }
        public virtual ICollection<TeachingSchedule> TeachingSchedules { set; get; }
        public virtual ICollection<ReceiptDetail> ReceiptDetails { set; get; }
        public virtual ICollection<AttendanceSheet> AttendanceSheets { set; get; }
        public virtual ICollection<AttendanceSheetDetail> AttendanceSheetDetails { set; get; }
    }
}
