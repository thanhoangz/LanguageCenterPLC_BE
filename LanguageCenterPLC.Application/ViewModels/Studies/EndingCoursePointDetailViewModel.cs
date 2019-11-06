using LanguageCenterPLC.Infrastructure.Enums;
using System;

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

        public Status Status { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime? DateModified { get; set; }

        public string Note { get; set; }
        public int SortOrder { get; set; }

        /* Foreign Key */
        public string LearnerId { get; set; }
        public string LearnerName { get; set; }

        public string LearnerCardId { get; set; }

        public DateTime LearnerBriday { get; set; }

        public bool LearnerSex { get; set; }
        public int EndingCoursePointId { get; set; }

        public  LearnerViewModel Learner { get; set; }

        public EndingCoursePointViewModel EndingCoursePoint { get; set; }
    }
}
