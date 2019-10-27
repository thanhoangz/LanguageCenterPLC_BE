using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LanguageCenterPLC.Data.Entities
{
    [Table("LogTuitionRecords")]
    public class LogTuitionRecord
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string LearnerId { get; set; }

        public DateTime Date { get; set; }

        public int AmountOfMoney { get; set; }

        public string Content { get; set; }

        public string Manipulation { get; set; }

        public string Note { set; get; }
    }
}
