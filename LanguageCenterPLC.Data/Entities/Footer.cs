using LanguageCenterPLC.Infrastructure.SharedKernel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LanguageCenterPLC.Data.Entities
{
    /// <summary>
    /// Footer
    /// </summary>
    [Table("Footers")]
    public class Footer : DomainEntity<string>
    {
        [Required]
        public string Content { set; get; }
    }
}
