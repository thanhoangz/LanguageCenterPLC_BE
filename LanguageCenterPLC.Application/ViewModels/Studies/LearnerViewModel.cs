using LanguageCenterPLC.Application.ViewModels.Categories;
using LanguageCenterPLC.Infrastructure.Enums;
using System;

namespace LanguageCenterPLC.Application.ViewModels.Studies
{
    public class LearnerViewModel
    {
        public string Id { get; set; }

        public string CardId { get; set; }
    
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public bool Sex { get; set; }

        public DateTime Birthday { get; set; }

        public string Address { get; set; }

        public string Email { get; set; }

        public string Facebook { get; set; }

        public string Phone { get; set; }

        public string ParentFullName { get; set; }

        public string ParentPhone { get; set; }

        public string Image { get; set; }

        public Status Status { get; set; }

        public string Note { get; set; }

        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }

        public int GuestTypeId { get; set; }
        public string GuestTypeName { get; set; }

        public virtual GuestTypeViewModel GuestTypeViewModel { get; set; }

     
    }
}
