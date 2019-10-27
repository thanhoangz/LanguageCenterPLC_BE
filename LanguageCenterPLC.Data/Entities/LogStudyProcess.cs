using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LanguageCenterPLC.Data.Entities
{
    [Table("LogStudyProcesses")]
    public class LogStudyProcess
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string LearnerId { get; set; }

        public DateTime Date { get; set; }

        public string ClassId { get; set; }

        public string ClassName { get; set; }

        public string CourseId { get; set; }

        public string CourseName { get; set; }

        public string Manipulation { get; set; }

        public string Note { set; get; }
    }
}
