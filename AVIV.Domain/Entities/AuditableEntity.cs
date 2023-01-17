using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AVIV.Domain.Entities
{
    public abstract class AuditableEntity
    {
        [MaxLength(36)]
        [Key]
        public virtual string Id { get; protected set; }

        [Column(TypeName = "datetimeoffset(7)")]
        public DateTimeOffset Created { get; set; }

        [ForeignKey("Creator")]
        public string? CreatedBy { get; set; }
        //public UserDetail Creator { get; set; }


        [Column(TypeName = "datetimeoffset(7)")]
        public DateTimeOffset? LastModified { get; set; }

        public string? LastModifiedBy { get; set; }

    }
}
