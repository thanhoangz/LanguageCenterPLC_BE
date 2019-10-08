using LanguageCenterPLC.Infrastructure.Enums;
using System;
using System.Collections.Generic;

namespace LanguageCenterPLC.Application.ViewModels.Studies
{
    public class ClassroomViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Status Status { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }

        public string Note { get; set; }

        public ICollection<TeachingScheduleViewModel> TeachingSchedules { set; get; }
    }
}
