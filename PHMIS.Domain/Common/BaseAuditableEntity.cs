
namespace PHMIS.Domain.Common
{
    public abstract class BaseAuditableEntity// : BaseEntity
    {
        public int? CreatedBy { get; set; }
        public DateTime? DateCreated { get; set; }
        public int? LastModifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }

    }
}
