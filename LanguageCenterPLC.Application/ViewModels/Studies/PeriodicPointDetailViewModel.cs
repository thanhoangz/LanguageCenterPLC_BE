using LanguageCenterPLC.Infrastructure.Enums;
using System;

namespace LanguageCenterPLC.Application.ViewModels.Studies
{
    public class PeriodicPointDetailViewModel
    {
        public int Id { get; set; }
        public decimal Point { get; set; }

        public decimal AveragePoint { get; set; }

        public decimal SortedByAveragePoint { get; set; }

        public decimal SortedByPoint { get; set; }

        public Status Status { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }

        public string Note { get; set; }

        public string LearnerId { get; set; }

        public int PeriodicPointId { get; set; }

        public  LearnerViewModel Learner { get; set; }

        public  PeriodicPointViewModel PeriodicPoint { get; set; }
    }
}
