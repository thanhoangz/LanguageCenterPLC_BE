using LanguageCenterPLC.Infrastructure.Enums;
using LanguageCenterPLC.Infrastructure.Interfaces;
using LanguageCenterPLC.Infrastructure.SharedKernel;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LanguageCenterPLC.Data.Entities
{

    public class PeriodicPointDetail : DomainEntity<int>, ISwitchable, IDateTracking
    {
        [Required]
        public decimal Point { get; set; }

        [Required]
        public decimal AveragePoint { get; set; }


        [Required]
        public decimal SortedByAveragePoint { get; set; }

        [Required]
        public decimal SortedByPoint { get; set; }

        [Required]
        public Status Status { get; set; }

        [Required]
        public DateTime DateCreated { get; set; }

        public DateTime? DateModified { get; set; }
        public string Note { get; set; }

        [Required]
        public string LearnerId { get; set; }

        [Required]
        public int PeriodicPointId { get; set; }

        [ForeignKey("LearnerId")]
        public virtual Learner Learner { get; set; }


        [ForeignKey("PeriodicPointId")]
        public virtual PeriodicPoint PeriodicPoint { get; set; }
    }
}
