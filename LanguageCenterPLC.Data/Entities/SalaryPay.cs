using LanguageCenterPLC.Infrastructure.SharedKernel;
using System.ComponentModel.DataAnnotations.Schema;

namespace LanguageCenterPLC.Data.Entities
{
    [Table("SalaryPaies")]
    public class SalaryPay : DomainEntity<int>
    {
        public string? PersonnelId { get; set; }

        public int? LecturerId { get; set; }

        public int? TutorId { get; set; }

        public decimal? TotalBasicSalary { get; set; }


        public decimal? TotalSalaryOfDay { get; set; }

        public decimal? TotalAllowance { get; set; }


        public decimal? TotalBonus { get; set; }

        public decimal? TotalInsurancePremium { get; set; }

        public float TotalWorkdays { get; set; }

        public int TotalTeachingByLecturer { get; set; }
        public int TotalTeachingByTutor { get; set; }

        public decimal? TotalTheoreticalAmount { get; set; }

        public decimal? TotalAdvancePayment { get; set; }

        public decimal? TotalRealityAmount { get; set; }

        public int Month { get; set; }
        public int Year { get; set; }
 
    }
}
