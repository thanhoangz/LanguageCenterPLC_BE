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
    /// Đối tượng 
    /// </summary>
    [Table("GuestTypes")]
    public class GuestType : DomainEntity<int>, ISwitchable, IDateTracking
    {
        [Required]
        [StringLength(250)]
        public string Name { get; set; }

        [Required]
        public DateTime DateCreated { get; set; }

        public DateTime? DateModified { get; set; }

        [Required]
        public Status Status { get; set; }

        public string Note { get; set; }

        /* Foreign Key */
        /*Reference Table*/
        /*List of References */
        public virtual ICollection<Learner> Learners { set; get; }
    }
}
