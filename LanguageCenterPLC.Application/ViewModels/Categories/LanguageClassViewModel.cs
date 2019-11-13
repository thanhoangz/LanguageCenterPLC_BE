using LanguageCenterPLC.Infrastructure.Enums;
using System;


namespace LanguageCenterPLC.Application.ViewModels.Categories
{
    public class LanguageClassViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public decimal CourseFee { get; set; }

        public decimal MonthlyFee { get; set; }

        public decimal LessonFee { get; set; }

        public DateTime StartDay { get; set; }

        public DateTime EndDay { get; set; }

        public Status Status { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime? DateModified { get; set; }

        public string Note { get; set; }

        public string CourseName { get; set; }

        public string LectureName { get; set; }

        public int CourseId { get; set; }

        public int MaxNumber { get; set; }
        public decimal? WageOfLecturer { get; set; }

        public decimal? WageOfTutor { get; set; }

    }
}
