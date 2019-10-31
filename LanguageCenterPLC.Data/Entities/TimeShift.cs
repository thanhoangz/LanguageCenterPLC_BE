using LanguageCenterPLC.Infrastructure.SharedKernel;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace LanguageCenterPLC.Data.Entities
{
    [Table("TimeShifts")]
    public class TimeShift: DomainEntity<int>
    {
        public string Name { get; set; }

        public TimeSpan FromTime { get; set; }

        public TimeSpan ToTime { get; set; }
    }
}
