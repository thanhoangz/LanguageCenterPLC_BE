using LanguageCenterPLC.Infrastructure.Enums;
using LanguageCenterPLC.Infrastructure.Interfaces;
using LanguageCenterPLC.Infrastructure.SharedKernel;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LanguageCenterPLC.Data.Entities
{
    [Table("Permissions")]
    public class Permission : DomainEntity<int>, ISwitchable
    {
        public Permission() { }
        public Permission(Guid appUserId, string functionId, bool canCreate,
            bool canRead, bool canUpdate, bool canDelete)
        {
            AppUserId = appUserId;
            FunctionId = functionId;
            CanCreate = canCreate;
            CanRead = canRead;
            CanUpdate = canUpdate;
            CanDelete = canDelete;
        }
        [Required]
        public Guid AppUserId { get; set; }

        [StringLength(450)]
        [Required]
        public string FunctionId { get; set; }

        public bool CanCreate { set; get; }
        public bool CanRead { set; get; }

        public bool CanUpdate { set; get; }
        public bool CanDelete { set; get; }


        [ForeignKey("AppUserId")]
        public virtual AppUser AppUser { get; set; }

        [ForeignKey("FunctionId")]
        public virtual Function Function { get; set; }

        [Required]
        public Status Status { get; set; }
    }
}
