using LanguageCenterPLC.Infrastructure.Enums;
using System;

namespace LanguageCenterPLC.Application.ViewModels.Studies
{
    public class ClassroomViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Status Status { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime? DateModified { get; set; }

        public string Note { get; set; }
    }
}
