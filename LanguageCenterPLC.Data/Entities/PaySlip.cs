using LanguageCenterPLC.Infrastructure.Enums;
using LanguageCenterPLC.Infrastructure.Interfaces;
using LanguageCenterPLC.Infrastructure.SharedKernel;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LanguageCenterPLC.Data.Entities
{
    /// <summary>
    /// Phiếu chi (Phiếu xuất)
    /// </summary>
    [Table("PaySlips")]
    public class PaySlip : DomainEntity<string>, ISwitchable, IDateTracking
    {
        /// <summary>
        /// Nội dung chi
        /// </summary>
        [Required]
        public string Content { get; set; }

        public decimal Total { get; set; }

        public DateTime Date { get; set; }


        [StringLength(200)]
        public string Receiver { get; set; }


        [Required]
        public Status Status { get; set; }

        [Required]
        public DateTime DateCreated { get; set; }

        public DateTime? DateModified { get; set; }

        public string Note { get; set; }


        /* Foreign Key */

        [Required]
        public int PaySlipTypeId { get; set; }

        [Required]
        public string PersonnelId { get; set; }

        public string ReceivePersonnelId { get; set; }

        public string ReceiveLecturerId { get; set; }

        [Required]
        public Guid AppUserId { get; set; }

        /*Reference Table*/

        [ForeignKey("PersonnelId")]
        public virtual Personnel Personnel { get; set; }

        [ForeignKey("PaySlipTypeId")]
        public virtual PaySlipType PaySlipType { get; set; }

        [ForeignKey("AppUserId")]
        public virtual AppUser AppUser { get; set; }


    }
}
