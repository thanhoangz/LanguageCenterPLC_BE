using LanguageCenterPLC.Infrastructure.Enums;
using LanguageCenterPLC.Infrastructure.Interfaces;
using LanguageCenterPLC.Infrastructure.SharedKernel;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LanguageCenterPLC.Data.Entities
{
    [Table("ReceiptDetails")]
    public class ReceiptDetail : DomainEntity<int>, ISwitchable, IDateTracking
    {

        [Required]
        public int Month { get; set; }

        [Required]
        public int Year { get; set; }

        [Required]
        public decimal Tuition { get; set; }


        public decimal FundMoney { get; set; }


        public decimal InfrastructureMoney { get; set; }


        public decimal OtherMoney { get; set; }

        [Required]
        public decimal TotalMoney { get; set; }

        [Required]
        public Status Status { get; set; }

        [Required]
        public DateTime DateCreated { get; set; }

       
        public DateTime? DateModified { get; set; }


        public string Note { get; set; }

        /* Foreign Key */
        [Required]
        public string LanguageClassId { get; set; }

        [Required]
        public string ReceiptId { get; set; }

        /*Reference Table*/

        [ForeignKey("LanguageClassId")]
        public virtual LanguageClass LanguageClass { get; set; }

        [ForeignKey("ReceiptId")]
        public virtual Receipt Receipt { get; set; }
    }
}
