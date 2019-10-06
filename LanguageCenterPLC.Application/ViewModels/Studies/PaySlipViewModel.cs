using LanguageCenterPLC.Application.ViewModels.Categories;
using LanguageCenterPLC.Infrastructure.Enums;
using System;

namespace LanguageCenterPLC.Application.ViewModels.Studies
{
    public class PaySlipViewModel
    {
        public string Id { get; set; }

        public string Content { get; set; }

        public decimal Total { get; set; }

        public DateTime Date { get; set; }

        public string Receiver { get; set; }

        public Status Status { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }

        public string Note { get; set; }

        public int PaySlipTypeId { get; set; }
       
        public string PersonnelId { get; set; }
        
        public string SendPersonnelId { get; set; }

        public Guid AppUserId { get; set; }

        public  PersonnelViewModel Personnel { get; set; }

        public  PersonnelViewModel SendPersonnel { get; set; }

        public PaySlipTypeViewModel PaySlipType { get; set; }

        public AppUserViewModel AppUser { get; set; }
    }
}
