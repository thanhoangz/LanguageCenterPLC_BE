using LanguageCenterPLC.Infrastructure.Enums;
using LanguageCenterPLC.Infrastructure.Interfaces;
using LanguageCenterPLC.Infrastructure.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LanguageCenterPLC.Data.Entities
{
    [Table("PaySlipTypes")]
    public class PaySlipType : DomainEntity<int>, ISwitchable, IDateTracking
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public Status Status { get; set; }

        [Required]
        public DateTime DateCreated { get; set; }


        public DateTime? DateModified { get; set; }


        public string Note { get; set; }

        /*List of References */

        public virtual ICollection<PaySlip> PaySlips { set; get; }
    }
}
