using LanguageCenterPLC.Application.ViewModels.Studies;
using LanguageCenterPLC.Application.ViewModels.Timekeepings;
using LanguageCenterPLC.Infrastructure.Enums;
using System;
using System.Collections.Generic;

namespace LanguageCenterPLC.Application.ViewModels.Categories
{
    public class AppUserViewModel
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string UserName { get; }
        public string Email { get; }
        public string PhoneNumber { get; }
        public DateTime? BirthDay { set; get; }

        public decimal Balance { get; set; }

        public string Avatar { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public Status Status { get; set; }

        public ICollection<EndingCoursePointViewModel> EndingCoursePoints { set; get; }
        public ICollection<ReceiptViewModel> Receipts { set; get; }
        public ICollection<TimesheetViewModel> Timesheets { set; get; }
        public ICollection<AttendanceSheetViewModel> AttendanceSheets { set; get; }

    }
}
