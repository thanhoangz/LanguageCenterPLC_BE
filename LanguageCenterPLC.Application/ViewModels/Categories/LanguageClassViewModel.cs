using LanguageCenterPLC.Application.ViewModels.Finances;
using LanguageCenterPLC.Application.ViewModels.Studies;
using LanguageCenterPLC.Infrastructure.Enums;
using System;
using System.Collections.Generic;


namespace LanguageCenterPLC.Application.ViewModels.Categories
{
    public class LanguageClassViewModel
    {
      
        public string Name { get; set; }

       
        public decimal CourseFee { get; set; }

   
        public decimal MonthlyFee { get; set; }

        public decimal LessonFee { get; set; }

      
        public DateTime StartDay { get; set; }

 
        public DateTime EndDay { get; set; }


        public Status Status { get; set; }


        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }

        public string Note { get; set; }


        public ICollection<StudyProcessViewModel> StudyProcesses { set; get; }
        public ICollection<EndingCoursePointViewModel> EndingCoursePoints { set; get; }
        public ICollection<PeriodicPointViewModel> PeriodicPoints { set; get; }
        public ICollection<TeachingScheduleViewModel> TeachingSchedules { set; get; }
        public ICollection<ReceiptDetailViewModel> ReceiptDetails { set; get; }
        public ICollection<AttendanceSheetViewModel> AttendanceSheets { set; get; }
        public ICollection<AttendanceSheetDetailViewModel> AttendanceSheetDetails { set; get; }
    }
}
