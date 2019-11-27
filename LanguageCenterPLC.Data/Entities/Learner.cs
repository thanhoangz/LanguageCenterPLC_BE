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
    /// Người học
    /// </summary>
    [Table("Learners")]
    public class Learner : DomainEntity<string>, ISwitchable, IDateTracking
    {
        public Learner()
        {

        }

        [StringLength(100)]
        public string CardId { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required]
        public bool Sex { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Birthday { get; set; }

        [Required]
        [StringLength(500)]
        public string Address { get; set; }

        [StringLength(200)]
        public string Email { get; set; }

        [StringLength(200)]
        public string Facebook { get; set; }

        [Column(TypeName = "VARCHAR(16)")]
        public string Phone { get; set; }


        [StringLength(100)]
        public string ParentFullName { get; set; }


        [Column(TypeName = "VARCHAR(16)")]
        public string ParentPhone { get; set; }

        public string Image { get; set; }

        [Required]
        public Status Status { get; set; }

        public string Note { get; set; }

        [Required]
        public DateTime DateCreated { get; set; }

        public DateTime? DateModified { get; set; }

        /* Foreign Key */
        [Required]
        public int GuestTypeId { get; set; }

        /*Reference Table*/
        [ForeignKey("GuestTypeId")]
        public virtual GuestType GuestType { get; set; }

        /*List of References */
        public virtual ICollection<StudyProcess> StudyProcesses { set; get; }
        public virtual ICollection<EndingCoursePointDetail> EndingCoursePointDetails { set; get; }
        public virtual ICollection<PeriodicPointDetail> PeriodicPointDetails { set; get; }
        public virtual ICollection<Receipt> Receipts { set; get; }
        public virtual ICollection<AttendanceSheetDetail> AttendanceSheetDetails { set; get; }

  
    }
}
