using LanguageCenterPLC.Infrastructure.Enums;
using System;
using System.Collections.Generic;

namespace LanguageCenterPLC.Application.ViewModels.Categories
{
    public class AppUserViewModel
    {
        public AppUserViewModel()
        {
            Roles = new List<string>();
        }
        public Guid Id { get; set; }

        public string FullName { get; set; }

        public string UserName { get; set; }

        public string Password { set; get; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Address { get; set; }

        public DateTime? BirthDay { set; get; }


        public string Gender { get; set; }

        public decimal Balance { get; set; }

        public string Avatar { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime? DateModified { get; set; }

        public Status Status { get; set; }

        public List<string> Roles { get; set; }


    }
}
