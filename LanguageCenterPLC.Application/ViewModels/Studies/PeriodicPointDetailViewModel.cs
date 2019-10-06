using LanguageCenterPLC.Infrastructure.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace LanguageCenterPLC.Application.ViewModels.Studies
{
    public class PeriodicPointDetailViewModel
    {
        public int Id { get; set; }
        public decimal Point { get; set; }

        public decimal AveragePoint { get; set; }

        /// <summary>
        /// Sắp xếp theo điểm trung bình cộng các bài trước đến thời điểm hiện tại
        /// </summary>
        public decimal SortedByAveragePoint { get; set; }

        /// <summary>
        /// Sắp xêp theo điểm bài thi hiện tại
        /// </summary>
        public decimal SortedByPoint { get; set; }

        public Status Status { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }
        public string Note { get; set; }


        /* Foreign Key */
        public string LearnerId { get; set; }

        public int PeriodicPointId { get; set; }

        /*Reference Table*/
        public  LearnerViewModel Learner { get; set; }


        public  PeriodicPointViewModel PeriodicPoint { get; set; }
    }
}
