
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LanguageCenterPLC.Infrastructure.Enums;
using LanguageCenterPLC.Infrastructure.Interfaces;
using LanguageCenterPLC.Infrastructure.SharedKernel;
namespace LanguageCenterPLC.Data.Entities
{
    [Table("Users")]
    public class User : DomainEntity<int>, ISwitchable, IDateTracking
    {
        [Required]
        public string FullName { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public Status Status { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }


        /* Foreign Key */
        /*Reference Table*/
        /*List of References */
        public ICollection<EndingCoursePoint> EndingCoursePoints { set; get; }
        public ICollection<Receipt> Receipts { set; get; }
        public ICollection<Timesheet> Timesheets { set; get; }
        public ICollection<AttendanceSheet> AttendanceSheets { set; get; }
    }
}
