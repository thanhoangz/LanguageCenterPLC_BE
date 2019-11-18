using LanguageCenterPLC.Infrastructure.Enums;
using LanguageCenterPLC.Infrastructure.Interfaces;
using LanguageCenterPLC.Infrastructure.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LanguageCenterPLC.Data.Entities
{
    /// <summary>
    /// Giảng viên
    /// </summary>
    [Table("Lecturers")]
    public class Lecturer : DomainEntity<int>, ISwitchable, IDateTracking
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

        // quốc gia
        [Required]
        [StringLength(100)]
        public string Nationality { get; set; }

        // tình trạng hôn nhân
        [Required]
        public int MarritalStatus { get; set; }

        // kinh nghiệm
        public string ExperienceRecord { get; set; }

        [StringLength(200)]
        public string Email { get; set; }


        [StringLength(200)]
        public string Facebook { get; set; }

        [Column(TypeName = "VARCHAR(16)")]
        public string Phone { get; set; }

        [Required]
        public string Position { get; set; }

        [Required]
        public string Certificate { get; set; }

        [Required]
        public string Image { get; set; }

        // lương cơ bản
        [Required]
        public decimal BasicSalary { get; set; }

        // phụ cấp
        [Required]
        public decimal Allowance { get; set; }

        // tiền thưởng
        [Required]
        public decimal Bonus { get; set; }

        // tiền bảo hiểm
        [Required]
        public decimal InsurancePremium { get; set; }

        [Required]
        public DateTime DateCreated { get; set; }

        public DateTime? DateModified { get; set; }

        // tiền công giảng dạy với giáo viên
        public decimal WageOfLecturer { get; set; }

        // tiền công giảng dạy với trợ giảng
        public decimal WageOfTutor { get; set; }

        // giáo viên thỉnh giảng
        [Required]
        public bool IsVisitingLecturer { get; set; }

        // trợ giảng
        [Required]
        public bool IsTutor { get; set; }

        [Required]
        public Status Status { get; set; }

        public string Note { get; set; }

        //ngày nghỉ việc
        public DateTime? QuitWorkDay { get; set; }



        public virtual ICollection<PaySlip> PaySlips { set; get; }

        public virtual ICollection<EndingCoursePoint> EndingCoursePoints { set; get; }

        public virtual ICollection<PeriodicPoint> PeriodicPoints { set; get; }

        [InverseProperty(nameof(AttendanceSheet.Lecturer))]
        public ICollection<AttendanceSheet> LecturerAttendanceSheets { set; get; }

        [InverseProperty(nameof(AttendanceSheet.Tutor))]
        public ICollection<AttendanceSheet> TutorAttendanceSheets { set; get; }

    }
}
