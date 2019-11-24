using LanguageCenterPLC.Infrastructure.Interfaces;
using LanguageCenterPLC.Infrastructure.SharedKernel;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace LanguageCenterPLC.Data.Entities
{
    [Table("LogSystems")]
    public class LogSystem : DomainEntity<long>, IDateTracking
    {
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }

        public string Content { get; set; }

        public Guid UserId { get; set; }

        public string LearnerId { get; set; }

        public int? StudyProcessId { get; set; }

        public string ClassId { get; set; }

        public int? PeriodicPointId { get; set; }

        public int? EndingCoursePointId { get; set; }
       

        public int? LecturerId { get; set; }

        public int? TimesheetId { get; set; }
        public int? SalaryPayId { get; set; }
        public int? AttendanceId { get; set; }
     
        public bool? IsTimeSheetLog { get; set; }
        public bool? IsSalaryPayLog { get; set; }
        public bool? IsStudyProcessLog { get; set; }
        public bool? IsManagerPointLog { get; set; }


    }
}
