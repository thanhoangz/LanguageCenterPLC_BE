using System;

namespace LanguageCenterPLC.Application.ViewModels.Studies
{
    public class ClassSessionViewModel
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }

        public TimeSpan FromTime { get; set; }

        public TimeSpan ToTime { get; set; }

        public int TeachingScheduleId { get; set; }

    }
}
