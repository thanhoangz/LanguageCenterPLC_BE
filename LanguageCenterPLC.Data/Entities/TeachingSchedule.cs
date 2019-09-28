using LanguageCenterPLC.Infrastructure.Enums;
using LanguageCenterPLC.Infrastructure.Interfaces;
using LanguageCenterPLC.Infrastructure.SharedKernel;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LanguageCenterPLC.Data.Entities
{
    [Table("TeachingSchedules")]
    public class TeachingSchedule : DomainEntity<int>, ISwitchable, IDateTracking
    {
        [Required]
        public DateTime FromDate { get; set; }

        [Required]
        public DateTime ToDate { get; set; }


        /// <summary>
        /// Ca học
        /// </summary>
        [StringLength(500)]
        public string TimeShift { get; set; }

        /// <summary>
        /// Các ngày học trong tuần
        /// </summary>
        [Required]

        [StringLength(500)]
        public string DaysOfWeek { get; set; }

        [Required]
        public Status Status { get; set; }

        [Required]
        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }

        public string Note { get; set; }

        /* Foreign Key */
        [Required]
        public int LecturerId { get; set; }

        [Required]
        public int ClassRoomId { get; set; }

        [Required]
        public string LanguageClassId { get; set; }

        /*Reference Table*/

        [ForeignKey("LecturerId")]
        public virtual Lecturer Lecturer { get; set; }

        [ForeignKey("ClassRoomId")]
        public virtual ClassRoom ClassRoom { get; set; }

        [ForeignKey("LanguageClassId")]
        public virtual LanguageClass LanguageClass { get; set; }
    }
}
