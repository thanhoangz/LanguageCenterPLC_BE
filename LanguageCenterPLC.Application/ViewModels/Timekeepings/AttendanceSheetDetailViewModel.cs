using LanguageCenterPLC.Application.ViewModels.Categories;
using LanguageCenterPLC.Application.ViewModels.Studies;
using System;
using System.Collections.Generic;
using System.Text;

namespace LanguageCenterPLC.Application.ViewModels.Timekeepings
{
    public class AttendanceSheetDetailViewModel
    {
      
        public int Id { get; set; }
        public StatusViewModel Status { get; set; }

        
        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }

        public string Note { get; set; }

        /* Foreign Key */
       // [Required]
        public string LearnerId { get; set; }

        public string LanguageClassId { get; set; }

       // [Required]
        public int AttendanceSheetId { get; set; }

        /*Reference Table*/
       // [ForeignKey("LearnerId")]
        public  LearnerViewModel Learner { get; set; }

        //[ForeignKey("LanguageClassId")]
        public  LanguageClassViewModel LanguageClass { get; set; }

        //[ForeignKey("AttendanceSheetId")]
        public virtual AttendanceSheetViewModel AttendanceSheet { get; set; }
        /*List of References */
    }
}
