using LanguageCenterPLC.Infrastructure.Enums;
using LanguageCenterPLC.Infrastructure.Interfaces;
using LanguageCenterPLC.Infrastructure.SharedKernel;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LanguageCenterPLC.Data.Entities
{
    [Table("EndingCoursePointDetails")]
    public class EndingCoursePointDetail : DomainEntity<int>, ISwitchable, IDateTracking, ISortable
    {
        public decimal ListeningPoint { get; set; }

        public decimal SayingPoint { get; set; }

        public decimal WritingPoint { get; set; }

        public decimal ReadingPoint { get; set; }

        public decimal TotalPoint { get; set; }

        public decimal AveragePoint { get; set; }

        [Required]
        public Status Status { get; set; }

        [Required]
        public DateTime DateCreated { get; set; }

        public DateTime? DateModified { get; set; }

        public string Note { get; set; }
        public int SortOrder { get; set; }

        /* Foreign Key */
        [Required]
        public string LearnerId { get; set; }

        [Required]
        public int EndingCoursePointId { get; set; }

        /*Reference Table*/
        [ForeignKey("LearnerId")]
        public virtual Learner Learner { get; set; }


        [ForeignKey("EndingCoursePointId")]
        public virtual EndingCoursePoint EndingCoursePoint { get; set; }
    }
}
