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
        public DateTime StartDay { get; set; }

        [Required]
        public DateTime EndDay { get; set; }

        [Required]
        public Status Status { get; set; }

        [Required]
        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }

        public string Note { get; set; }


        /* Foreign Key */
        /*Reference Table*/

        /*List of References */
        public ICollection<StudyProcess> StudyProcesses { set; get; }
        public ICollection<EndingCoursePoint> EndingCoursePoints { set; get; }
        public ICollection<PeriodicPoint> PeriodicPoints { set; get; }
        public ICollection<TeachingSchedule> TeachingSchedules { set; get; }
        public ICollection<ReceiptDetail> ReceiptDetails { set; get; }
        public ICollection<AttendanceSheet> AttendanceSheets { set; get; }
        public ICollection<AttendanceSheetDetail> AttendanceSheetDetails { set; get; }
    }
}
