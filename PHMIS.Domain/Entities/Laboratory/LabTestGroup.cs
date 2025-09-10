using PHMIS.Domain.Common;

namespace PHMIS.Domain.Entities.Laboratory
{
    public class LabTestGroup : BaseEntity
    {
        public string? Description { get; set; }
        public short? SortOrder { get; set; }
        public ICollection<LabTest> LabTests { get; set; }
    }
}
