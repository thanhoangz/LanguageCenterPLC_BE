using LanguageCenterPLC.Application.ViewModels.Categories;
using LanguageCenterPLC.Infrastructure.Enums;
using System;

namespace LanguageCenterPLC.Application.ViewModels.Timekeepings
{
    public class TimesheetViewModel
    {
        public int Id { get; set; }

        public int Month { get; set; }

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

        public decimal SalaryOfDay { get; set; }

        public Status Status { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }

        public string Note { get; set; }

        public Guid AppUserId { get; set; }

        public string PersonnelName { get; set; }
        public string PersonnelLastName { get; set; }


        public string PersonnelId { get; set; }

        public bool isLocked { get; set; }


        public PersonnelViewModel Personnel { get; set; }

        public AppUserViewModel AppUser { get; set; }
    }
}
