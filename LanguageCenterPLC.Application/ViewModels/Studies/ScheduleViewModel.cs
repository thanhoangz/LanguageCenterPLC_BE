using System;
using System.Collections.Generic;

namespace LanguageCenterPLC.Application.ViewModels.Studies
{
    public class ScheduleViewModel
    {
        public int Id { get; set; }
        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public int LecturerId { get; set; }

        public int ClassroomId { get; set; }

        public string LanguageClassId { get; set; }
        public string LecturerName { get; set; }
        public string ClassroomName { get; set; }
        public string LanguageClassName { get; set; }
        public List<ClassSessionViewModel> ClassSessions { get; set; }

    }
}
