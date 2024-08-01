using System.ComponentModel.DataAnnotations;

namespace ManagerVM.Data.Entities
{
    public class BaseEntity
    {
        [Key]
        public long Id { get; set; }
        public DateTime? CreatedAt { get; set; }

        public long? CreatedBy { get; set; }

        public DateTime? LastModifiedAt { get; set; }

        public long? LastModifiedBy { get; set; }

        public bool IsDeleted { get; set; }
    }
}
