using LanguageCenterPLC.Infrastructure.SharedKernel;
using System.ComponentModel.DataAnnotations.Schema;

namespace LanguageCenterPLC.Data.Entities
{

    [Table("AccountForLearners")]
    public class AccountForLearner : DomainEntity<int>
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public string LearnerId { get; set; }

        [ForeignKey("LearnerId")]
        public virtual Learner Learner { get; set; }
    }
}
