using LanguageCenterPLC.Application.ViewModels.Studies;
using LanguageCenterPLC.Application.ViewModels.Timekeepings;
using LanguageCenterPLC.Infrastructure.Enums;
using System;
using System.Collections.Generic;

namespace LanguageCenterPLC.Application.ViewModels.Categories
{
    public class PersonnelViewModel
    {
        public string Id { get; set; }

        public string CardId { get; set; }

        public string FirstName { get; set; }
        
        public string LastName { get; set; }

        public bool Sex { get; set; }

        public DateTime Birthday { get; set; }

        public string Address { get; set; }

        public string Nationality { get; set; }

        public int MarritalStatus { get; set; }

        public string ExperienceRecord { get; set; }

        public string Email { get; set; }

        public string Facebook { get; set; }

        public string Phone { get; set; }

        public string Position { get; set; }

        public string Certificate { get; set; }

        public string Image { get; set; }

        public decimal BasicSalary { get; set; }

        public decimal SalaryOfDay { get; set; }

        public decimal Allowance { get; set; }

        public decimal Bonus { get; set; }

        public decimal InsurancePremium { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime? DateModified { get; set; }

        public Status Status { get; set; }

        public string Note { get; set; }

        public DateTime QuitWorkDay { get; set; }

        public ICollection<ReceiptViewModel> Receipts { set; get; }

        public ICollection<TimesheetViewModel> Timesheets { set; get; }

        public ICollection<PaySlipViewModel> PersonnelPaySlip { set; get; }

        public ICollection<PaySlipViewModel> SendPersonnelPaySlip { set; get; }
    }
}
