using LanguageCenterPLC.Infrastructure.Enums;
using LanguageCenterPLC.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace LanguageCenterPLC.Data.Entities
{
    [Table("AppUsers")]
    public class AppUser : IdentityUser<Guid>, IDateTracking, ISwitchable
    {
        public AppUser() : base() { }
       
        public string FullName { get; set; }

        public DateTime? BirthDay { set; get; }

        public decimal Balance { get; set; }

        public string Avatar { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime? DateModified { get; set; }

        public Status Status { get; set; }

        public virtual ICollection<EndingCoursePoint> EndingCoursePoints { set; get; }
        public virtual ICollection<Receipt> Receipts { set; get; }
        public virtual ICollection<Timesheet> Timesheets { set; get; }
        public virtual ICollection<AttendanceSheet> AttendanceSheets { set; get; }

      
    }
}
