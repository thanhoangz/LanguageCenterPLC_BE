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

        /// <summary>
        /// Nhân viên tạo phiếu, chi trả
        /// </summary>
        [Required]
        [ForeignKey(nameof(Personnel)), Column(Order = 0)]
        public string PersonnelId { get; set; }

        /// <summary>
        /// Nhân viên nhận chi trả (nếu có)
        /// </summary>
        /// 
        [ForeignKey(nameof(SendPersonnel)), Column(Order = 1)]
        public string SendPersonnelId { get; set; }

        [Required]
        public Guid AppUserId { get; set; }

        /*Reference Table*/


        public virtual Personnel Personnel { get; set; }


        public virtual Personnel SendPersonnel { get; set; }


        [ForeignKey("PaySlipTypeId")]
        public PaySlipType PaySlipType { get; set; }

        [ForeignKey("AppUserId")]
        public AppUser AppUser { get; set; }

        /*List of References */


    }
}
