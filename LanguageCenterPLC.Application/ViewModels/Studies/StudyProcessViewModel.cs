using LanguageCenterPLC.Infrastructure.Enums;
using System;

namespace LanguageCenterPLC.Application.ViewModels.Studies
{
    public class StudyProcessViewModel
    {
   
        public int Id { get; set; }
        public DateTime? OutDate { get; set; }

        public DateTime InDate { get; set; }

        public Status Status { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime? DateModified { get; set; }

        public string Note { get; set; }

        public string LanguageClassId { get; set; }

        public string LanguageClassName { get; set; }

        public decimal MonthlyFee { get; set; }

        public decimal CourseFee { get; set; }

        public string LearnerId { get; set; }

        public string LearnerName { get; set; }
        public string LearnerPhone { get; set; }

        public DateTime LearnerBriday { get; set; }
        public bool LearnerSex { get; set; }

        public string LearnerAdress { get; set; }

        public string LearnerNameOrderBy { get; set; }


        public LearnerViewModel Learner { get; set; }
    }
}
