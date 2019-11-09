using LanguageCenterPLC.Infrastructure.Enums;
using LanguageCenterPLC.Infrastructure.Interfaces;
using LanguageCenterPLC.Infrastructure.SharedKernel;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LanguageCenterPLC.Data.Entities
{

    [Table("PaySlips")]
    public class PaySlip : DomainEntity<string>, ISwitchable, IDateTracking
    {

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




        [Required]
        public int PaySlipTypeId { get; set; }

        [Required]
        [ForeignKey(nameof(Personnel)), Column(Order = 0)]
        public string PersonnelId { get; set; }

        [ForeignKey(nameof(ReceivePersonnel)), Column(Order = 1)]
        public string ReceivePersonnelId { get; set; }

        public int? ReceiveLecturerId { get; set; }

        [Required]
        public Guid AppUserId { get; set; }

        /*Reference Table*/

        public Personnel Personnel { get; set; }
        public Personnel ReceivePersonnel { get; set; }

        [ForeignKey("ReceiveLecturerId")]
        public virtual Lecturer ReceiveLecturer { get; set; }


        [ForeignKey("PaySlipTypeId")]
        public virtual PaySlipType PaySlipType { get; set; }

        [ForeignKey("AppUserId")]
        public virtual AppUser AppUser { get; set; }


    }
}
