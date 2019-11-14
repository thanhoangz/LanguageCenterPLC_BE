using LanguageCenterPLC.Infrastructure.Enums;
using LanguageCenterPLC.Infrastructure.Interfaces;
using LanguageCenterPLC.Infrastructure.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;

namespace LanguageCenterPLC.Data.Entities
{
    /// <summary>
    /// Nhân viên
    /// </summary>
    [Table("Personnels")]
    public class Personnel : DomainEntity<string>, ISwitchable, IDateTracking
    {

        [StringLength(100)]
        public string CardId { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required]
        public bool Sex { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Birthday { get; set; }

        [Required]
        [StringLength(500)]
        public string Address { get; set; }

        [Required]

        [StringLength(100)]
        public string Nationality { get; set; }

        [Required]
        public int MarritalStatus { get; set; }

        public string ExperienceRecord { get; set; }


        [StringLength(200)]
        public string Email { get; set; }


        [StringLength(200)]
        public string Facebook { get; set; }

        [Column(TypeName = "VARCHAR(16)")]
        public string Phone { get; set; }

        [Required]
        public string Position { get; set; }


        public string Certificate { get; set; }


        public string Image { get; set; }

        public decimal BasicSalary { get; set; }


        public decimal SalaryOfDay { get; set; }

        public decimal Allowance { get; set; }


        public decimal Bonus { get; set; }

        [Required]
        public decimal InsurancePremium { get; set; }

        [Required]
        public DateTime DateCreated { get; set; }

        public DateTime? DateModified { get; set; }

        [Required]
        public Status Status { get; set; }

        public string Note { get; set; }

        public DateTime? QuitWorkDay { get; set; }


        /* Foreign Key */

        /*Reference Table*/

        /*List of References */

        public virtual ICollection<Receipt> Receipts { set; get; }

     

        public virtual ICollection<PaySlip> PaySlips { set; get; }


        [InverseProperty(nameof(PaySlip.Personnel))]
        public ICollection<PaySlip> PersonnelPay { get; set; }

        [InverseProperty(nameof(PaySlip.ReceivePersonnel))]
        public ICollection<PaySlip> ReceivePersonnelPay { get; set; }
    }
}
