using LanguageCenterPLC.Application.ViewModels.Categories;
using System;
using System.Collections.Generic;
using System.Text;

namespace LanguageCenterPLC.Application.ViewModels.Studies
{
    public class EndingCoursePointDetailViewModel
    {
        public int Id { get; set; }
        public decimal ListeningPoint { get; set; }

        public decimal SayingPoint { get; set; }

        public decimal WritingPoint { get; set; }

        public decimal ReadingPoint { get; set; }

        public decimal TotalPoint { get; set; }

        public decimal AveragePoint { get; set; }

        public StatusViewModel Status { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }

        public string Note { get; set; }
        public int SortOrder { get; set; }

        /* Foreign Key */
        //[Required]
        public string LearnerId { get; set; }

       // [Required]
        public int EndingCoursePointId { get; set; }

        /*Reference Table*/
       // [ForeignKey("LearnerId")]
        public  LearnerViewModel Learner { get; set; }


        //[ForeignKey("EndingCoursePointId")]
        public EndingCoursePointViewModel EndingCoursePoint { get; set; }
    }
}
