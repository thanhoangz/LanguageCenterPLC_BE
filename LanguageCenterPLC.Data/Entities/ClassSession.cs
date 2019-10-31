using LanguageCenterPLC.Infrastructure.SharedKernel;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace LanguageCenterPLC.Data.Entities
{
    [Table("ClassSessions")]
    public class ClassSession : DomainEntity<int>
    {
        public DateTime Date { get; set; }

        public TimeSpan FromTime { get; set; }

        public TimeSpan ToTime { get; set; }

        public int TeachingScheduleId { get; set; }


        [ForeignKey("TeachingScheduleId")]
        public virtual TeachingSchedule TeachingSchedule { get; set; }
    }
}
