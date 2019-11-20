using LanguageCenterPLC.Application.ViewModels.Categories;
using LanguageCenterPLC.Application.ViewModels.Studies;
using LanguageCenterPLC.Infrastructure.Enums;
using System;
using System.Collections.Generic;

namespace LanguageCenterPLC.Application.ViewModels.Finances
{
    public class ReceiptDetailViewModel
    {
        public int Id { get; set; }

        public int Month { get; set; }

        public int Year { get; set; }

        public decimal Tuition { get; set; }

        public decimal FundMoney { get; set; }

        public decimal InfrastructureMoney { get; set; }

        public decimal OtherMoney { get; set; }

        public decimal TotalMoney { get; set; }

        public Status Status { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime? DateModified { get; set; }

        public string Note { get; set; }
        public string ReceiptId { get; set; }
        public string LanguageClassId { get; set; }
        public string LanguageClassName { get; set; }

        public string LearnerName { get; set; }
        public string LearnerId { get; set; }

        public DateTime LearnerBirthday { get; set; }
        public DateTime CollectionDate { get; set; }

    }
}
