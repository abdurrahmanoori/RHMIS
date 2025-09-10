using PHMIS.Domain.Common;

namespace PHMIS.Domain.Entities.Laboratory
{
    public class LabTest : BaseEntity
    {
        public string? Description { get; set; }
        public int? Price { get; set; }
        public bool IsActive { get; set; } = true;
        public int LabTestGroupId { get; set; }
        public LabTestGroup LabTestGroup { get; set; }

        // New fields
        public string? UnitOfMeasurment { get; set; }
        public string? NormalRange { get; set; }
        public string? Abbreviation { get; set; }
    }
}
