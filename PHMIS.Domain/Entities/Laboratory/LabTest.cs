using PHMIS.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace PHMIS.Domain.Entities.Laboratory
{
    public class LabTest : BaseEntity
    {
        public string? Description { get; set; }
        public int? Price { get; set; }
        public bool IsActive { get; set; } = true;
        public int LabTestGroupId { get; set; }

        // New fields
        public string? UnitOfMeasurment { get; set; }
        public string? NormalRange { get; set; }
        public string? Abbreviation { get; set; }

        [ForeignKey(nameof(LabTestGroupId))]
        public LabTestGroup LabTestGroup { get; set; }

    }
}
