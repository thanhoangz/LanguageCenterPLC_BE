using LanguageCenterPLC.Application.ViewModels.Studies;
using LanguageCenterPLC.Infrastructure.Enums;
using System;
using System.Collections.Generic;

namespace LanguageCenterPLC.Application.ViewModels.Categories
{
    public class GuestTypeViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime? DateModified { get; set; }

        public Status Status { get; set; }

        public string Note { get; set; }

        public ICollection<LearnerViewModel> Learners { set; get; }
    }
}
