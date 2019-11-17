using LanguageCenterPLC.Infrastructure.Enums;
using LanguageCenterPLC.Infrastructure.Interfaces;
using LanguageCenterPLC.Infrastructure.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LanguageCenterPLC.Data.Entities
{
    [Table("TeachingSchedules")]
    public class TeachingSchedule : DomainEntity<int>, ISwitchable, IDateTracking
    {
        [Required]
        [DataType(DataType.Date)]
        public DateTime FromDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime ToDate { get; set; }

        [StringLength(500)]
        public string TimeShift { get; set; }

        public int DaysOfWeek { get; set; }

        [Required]
        public Status Status { get; set; }

        [Required]
        public DateTime DateCreated { get; set; }

        public DateTime? DateModified { get; set; }

        public string Note { get; set; }

        /* Foreign Key */
        [Required]
        public int LecturerId { get; set; }

        public string Content { get; set; }

        [Required]
        public int ClassroomId { get; set; }

        [Required]
        public string LanguageClassId { get; set; }

        /*Reference Table*/

        [ForeignKey("LecturerId")]
        public virtual Lecturer Lecturer { get; set; }

        [ForeignKey("ClassroomId")]
        public virtual Classroom Classroom { get; set; }

        [ForeignKey("LanguageClassId")]
        public virtual LanguageClass LanguageClass { get; set; }

    }
}
