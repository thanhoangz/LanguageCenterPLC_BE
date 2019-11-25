using LanguageCenterPLC.Infrastructure.Enums;
using LanguageCenterPLC.Infrastructure.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LanguageCenterPLC.Data.Entities
{
    /// <summary>
    /// Chi tiết điểm danh
    /// </summary>
    [Table("AttendanceSheetDetails")]
    public class AttendanceSheetDetail : ISwitchable, IDateTracking
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }


        [Required]
        public Status Status { get; set; }

        [Required]
        public DateTime DateCreated { get; set; }

        public DateTime? DateModified { get; set; }

        public string Note { get; set; }

        /* Foreign Key */
        [Required]
        public string LearnerId { get; set; }

        public string LanguageClassId { get; set; }

        [Required]
        public int AttendanceSheetId { get; set; }

        /*Reference Table*/
        [ForeignKey("LearnerId")]
        public virtual Learner Learner { get; set; }

        [ForeignKey("LanguageClassId")]
        public virtual LanguageClass LanguageClass { get; set; }

        [ForeignKey("AttendanceSheetId")]
        public virtual AttendanceSheet AttendanceSheet { get; set; }
        /*List of References */
    }
}
