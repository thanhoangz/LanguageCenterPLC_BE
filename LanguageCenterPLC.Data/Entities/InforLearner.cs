using LanguageCenterPLC.Infrastructure.Enums;
using LanguageCenterPLC.Infrastructure.SharedKernel;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace LanguageCenterPLC.Data.Entities
{
    [Table("InforLearners")]
    public class InforLearner : DomainEntity<int>
    {
        // Bảng lưu tạm học viên đăng kí online.
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public bool Sex { get; set; }

        public DateTime Birthday { get; set; }

        public string Address { get; set; }

        public string Email { get; set; }

        public string Facebook { get; set; }

        [Column(TypeName = "VARCHAR(16)")]
        public string Phone { get; set; }

        public string ParentFullName { get; set; }

        [Column(TypeName = "VARCHAR(16)")]
        public string ParentPhone { get; set; }

        public string Image { get; set; }

        public Status Status { get; set; }

        public string Note { get; set; }
    }
}
