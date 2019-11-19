using LanguageCenterPLC.Application.ViewModels.Categories;
using LanguageCenterPLC.Application.ViewModels.Finances;
using LanguageCenterPLC.Infrastructure.Enums;
using System;
using System.Collections.Generic;

namespace LanguageCenterPLC.Application.ViewModels.Studies
{
    public class ReceiptViewModel
    {
        public string Id { get; set; }
        public string NameOfPaymentApplicant { get; set; }

        public string ForReason { get; set; }

        public DateTime CollectionDate { get; set; }

        public decimal TotalAmount { get; set; }

        public Status Status { get; set; }

        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }

        public string Note { get; set; }

        /* Foreign Key */

        public int ReceiptTypeId { get; set; }

        public string ReceiptTypeName { get; set; }

        public string PersonnelId { get; set; }

        public string PersonnelName { get; set; }

        public string LearnerId { get; set; }

        public string LearnerName { get; set; }

        public string LearnerCardId { get; set; }

        public string LearnerAdress { get; set; }
        public string LearnerPhone { get; set; }


        public Guid AppUserId { get; set; }

        public LearnerViewModel Learner { get; set; }

        public PersonnelViewModel Personnel { get; set; }

        public AppUserViewModel AppUser { get; set; }

        public ReceiptTypeViewModel ReceiptType { get; set; }

   //     public List<ReceiptDetailViewModel> ReceiptDetails { get; set; }

    }
}
