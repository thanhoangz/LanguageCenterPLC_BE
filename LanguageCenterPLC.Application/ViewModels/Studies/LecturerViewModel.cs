using LanguageCenterPLC.Application.ViewModels.Timekeepings;
using LanguageCenterPLC.Infrastructure.Enums;
using System;
using System.Collections.Generic;

namespace LanguageCenterPLC.Application.ViewModels.Studies
{
    public class LecturerViewModel
    {
        public int Id { get; set; }

        public string CardId { get; set; }
        
        public string FirstName { get; set; }
       
        public string LastName { get; set; }

        public bool Sex { get; set; }

        public DateTime Birthday { get; set; }

        public string Address { get; set; }
      
        public string Nationality { get; set; }

        public int MarritalStatus { get; set; }

        public string ExperienceRecord { get; set; }

        public string Email { get; set; }

        public string Facebook { get; set; }

        public string Phone { get; set; }

        public string Position { get; set; }

        public string Certificate { get; set; }

        public string Image { get; set; }

        public decimal BasicSalary { get; set; }

        // phụ cấp
        public decimal Allowance { get; set; }

        // tiền thưởng
        public decimal Bonus { get; set; }

        // tiền bảo hiểm
        public decimal InsurancePremium { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime? DateModified { get; set; }

        // tiền công giảng dạy với giáo viên
        public decimal WageOfLecturer { get; set; }

        // tiền công giảng dạy với trợ giảng
        public decimal WageOfTutor { get; set; }

        // giáo viên thỉnh giảng
        public bool IsVisitingLecturer { get; set; }

        // trợ giảng
        public bool IsTutor { get; set; }

        public Status Status { get; set; }

        public string Note { get; set; }

        //ngày nghỉ việc
        public DateTime QuitWorkDay { get; set; }

        //public ICollection<EndingCoursePointViewModel> EndingCoursePoints { set; get; }
        //public ICollection<PeriodicPointViewModel> PeriodicPoints { set; get; }

        //public ICollection<AttendanceSheetViewModel> LecturerAttendanceSheets { set; get; }

        //public ICollection<AttendanceSheetViewModel> TutorAttendanceSheets { set; get; }
    }
}
