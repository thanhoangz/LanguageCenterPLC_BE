using LanguageCenterPLC.Infrastructure.Enums;
using System;

namespace LanguageCenterPLC.Application.ViewModels.Studies
{
    public class FeedbackViewModel
    {
        public int Id { get; set; }

        public string Name { set; get; }

        public string Email { set; get; }

        public string Message { set; get; }

        public Status Status { set; get; }

        public DateTime DateCreated { set; get; }

        public DateTime? DateModified { get; set; }
    }
}
