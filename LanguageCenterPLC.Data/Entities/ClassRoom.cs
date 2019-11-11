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
    /// Phòng học
    /// </summary>
    [Table("Classrooms")]
    public class Classroom : DomainEntity<int>, ISwitchable, IDateTracking
    {
        [Required]
        [StringLength(500)]
        public string Name { get; set; }

        [Required]
        public Status Status { get; set; }

        [Required]
        public DateTime DateCreated { get; set; }

        public DateTime? DateModified { get; set; }

        public string Note { get; set; }

        /* Foreign Key */
        /*Reference Table*/
        /*List of References */
        public virtual ICollection<TeachingSchedule> TeachingSchedules { set; get; }

    }
}
