using LanguageCenterPLC.Infrastructure.Enums;
using LanguageCenterPLC.Infrastructure.Interfaces;
using LanguageCenterPLC.Infrastructure.SharedKernel;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LanguageCenterPLC.Data.Entities
{
    /// <summary>
    /// Bảng chấm công cho nhân viên
    /// </summary>
    [Table("Timesheets")]
    public class Timesheet : DomainEntity<int>, ISwitchable, IDateTracking
    {
        [Required]
        public int Month { get; set; }

        [Required]
        public int Year { get; set; }

        #region Ngày chấm công
        public float Day_1 { get; set; }

        public float Day_2 { get; set; }

        public float Day_3 { get; set; }

        public float Day_4 { get; set; }

        public float Day_5 { get; set; }

        public float Day_6 { get; set; }

        public float Day_7 { get; set; }

        public float Day_8 { get; set; }

        public float Day_9 { get; set; }

        public float Day_10 { get; set; }

        public float Day_11 { get; set; }

        public float Day_12 { get; set; }

        public float Day_13 { get; set; }

        public float Day_14 { get; set; }

        public float Day_15 { get; set; }

        public float Day_16 { get; set; }

        public float Day_17 { get; set; }

        public float Day_18 { get; set; }

        public float Day_19 { get; set; }

        public float Day_20 { get; set; }

        public float Day_21 { get; set; }

        public float Day_22 { get; set; }

        public float Day_23 { get; set; }

        public float Day_24 { get; set; }

        public float Day_25 { get; set; }

        public float Day_26 { get; set; }

        public float Day_27 { get; set; }

        public float Day_28 { get; set; }

        public float Day_29 { get; set; }

        public float Day_30 { get; set; }

        public float Day_31 { get; set; }
        #endregion

        public float TotalWorkday { get; set; }

        public decimal Salary { get; set; }

        /// <summary>
        /// Phụ cấp
        /// </summary>
        public decimal Allowance { get; set; }

        public decimal Bonus { get; set; }

        public decimal TotalSalary { get; set; }
        
        /// <summary>
        /// Phí bảo hiểm
        /// </summary>
        public decimal InsurancePremiums { get; set; }

        /// <summary>
        /// Phí tạm ứng
        /// </summary>
        public decimal AdvancePayment { get; set; }

        /// <summary>
        /// Tổng tiền lương thực lĩnh
        /// </summary>
        public decimal TotalActualSalary { get; set; }


        [Required]
        public decimal SalaryOfDay { get; set; }

        [Required]
        public Status Status { get; set; }

        [Required]
        public DateTime DateCreated { get; set; }

        public DateTime? DateModified { get; set; }

        public string Note { get; set; }

        public bool isLocked { get; set; }

        [Required]
        public Guid AppUserId { get; set; }

        [Required]
        public string PersonnelId { get; set; }

      

        [ForeignKey("PersonnelId")]
        public virtual Personnel Personnel { get; set; }

        [ForeignKey("AppUserId")]
        public virtual AppUser AppUser { get; set; }

    }
}