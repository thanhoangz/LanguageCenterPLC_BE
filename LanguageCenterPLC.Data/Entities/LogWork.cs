using LanguageCenterPLC.Infrastructure.SharedKernel;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LanguageCenterPLC.Data.Entities
{
    [Table("LogWorks")]
    public class LogWork : DomainEntity<long>
    {

        [Required]
        [DataType(DataType.Text)]
        public string Content { get; set; }
        public DateTime DateCreated { set; get; }
    }
}
